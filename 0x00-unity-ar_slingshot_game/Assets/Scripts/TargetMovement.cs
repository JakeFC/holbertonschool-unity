using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetMovement : MonoBehaviour
{
	private NavMeshAgent _target;
	private Vector3[] _verticeList;
	private System.Random _rd = new System.Random();
	private int _randNum, _oppositeFromLast, _last = -1;
	private float _time = 0;

    void Start()
    {
        _target = GetComponent<NavMeshAgent>();

		// List of points on the plane are taken from the parent plane.
		_verticeList = transform.parent.GetComponent<TargetSpawning>().verticeList;
    }

    void Update()
    {
		_time += Time.deltaTime;

		// Direction updates every 1.3 seconds and 3.2 seconds.
		if (_time % 4 < 0.1 || _time % 10 < 0.1)
			RandomMove();
    }

	// Moves the target to one of 121 points on the plane.
	void RandomMove()
	{
		// Top and bottom rows are excluded here.
		_randNum = _rd.Next(12, 108);

		// Left and right columns are excluded here.
		while(_randNum % 11 == 0 || _randNum % 11 == 10 || _randNum == _last)
			_randNum = _rd.Next(12, 108);
		
		_target.destination = _verticeList[_randNum];
		_last = _randNum;
	}

	// Moves the target in the opposite of last direction.
	void MoveAway()
	{
		// Finds the opposite vertex from previous move.
		if (_last != -1)
			_oppositeFromLast = 120 - _last;
		else
			_oppositeFromLast = 120 - _randNum;
		_target.destination = _verticeList[_oppositeFromLast];
		_last = _oppositeFromLast;
	}

	void OnCollisionEnter(Collision other)
	{
	
		if (other.gameObject.CompareTag("Target"))
			MoveAway();
	}
}
