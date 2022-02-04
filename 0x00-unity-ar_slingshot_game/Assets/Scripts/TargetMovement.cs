using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetMovement : MonoBehaviour
{
	private NavMeshAgent _target;
	private Vector3[] _movePosition = new Vector3[] {new Vector3(-5,0,5),
													new Vector3(0,0,5),
													new Vector3(5,0,5),
													new Vector3(-5,0,0),
													new Vector3(5,0,0),
													new Vector3(-5,0,-5),
													new Vector3(0,0,-5),
													new Vector3(5,0,-5),
													new Vector3(0,0,0)};
	private System.Random _rd = new System.Random();
	private int _randNum, _last = 8;

    void Start()
    {
        _target = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.time % 1.3 < 0.1 || Time.time % 3.2 < 0.1)
			RandomMove();
    }

	void RandomMove()
	{
		_randNum = _rd.Next(0, 7);
		if (_randNum != _last)
		{
			_target.destination = _movePosition[_randNum];
			_last = _randNum;
		}
		else
		{
			_target.destination = _movePosition[8];
			_last = 8;
		}
	}
}
