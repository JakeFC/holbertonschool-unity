using System;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
	public float speed = 1;
	//private NavMeshAgent _target;
	private Vector3 _destination;
	private Vector3 _baseScale;
	private Vector3[] _verticeList;
	private System.Random _rd = new System.Random();
	private int _randNum = 0, _oppositeFromLast, _last = -1, _numVertices;
	private float _time = 0;
	//private Vector3 _pos;
	private float _distance;
	private float _heightOffset = 0.1f;
	private float _lastCollision = -1;

    void Start()
    {
		_baseScale = transform.localScale;
		_heightOffset = transform.parent.GetComponent<TargetSpawning>().heightOffset;

		// List of points on the plane are taken from the parent plane.
		_verticeList = transform.parent.GetComponent<TargetSpawning>().verticeList;

		// Number of vertices is also taken from parent plane.
		_numVertices = transform.parent.GetComponent<TargetSpawning>().numVertices;
    }

    void Update()
    {
		_time += Time.deltaTime;

		// Direction updates every 1.3 seconds and 4.2 seconds.
		if (_time % 2.6f < 0.1f || _time % 8.4f < 0.1f)
			SetRandomMove();

		// Move target toward destination at constant speed.
		transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);

		// Update scale based on distance from camera.
		UpdateScale();
    }

	// Sets the target's move destination to a random inner vertex.
	void SetRandomMove()
	{
		// A random vertex is chosen from the list.
		_randNum = _rd.Next(0, _numVertices - 1);

		// Duplicates from last move are removed.
		while (_randNum == _last)
			_randNum = _rd.Next(0, _numVertices - 1);

		// Add parent position to convert from local to worldspace.
		_destination = _verticeList[_randNum] + transform.parent.position;

		// Distance is measured from the camera to the destination.
		_distance = Math.Abs(Vector3.Distance(GameObject.FindWithTag("MainCamera").transform.position,
							_destination));

		// Add a y-offset to the destination that scales with expected size change by checking distance,
		// without going too far into exponential growth.
		if (_distance > 0.5f)
			_destination.y += _heightOffset / (2 * _distance);
		else
			_destination.y += _heightOffset;

		_last = _randNum;
	}

	// Sets the target's move destination to the opposite of last destination.
	void MoveAway()
	{
		// Finds the opposite vertex from previous move.
		if (_last != -1)
			_oppositeFromLast = _numVertices - _last;
		else
			_oppositeFromLast = _numVertices - _randNum;

		// Add parent position to convert from local to worldspace.
		_destination = _verticeList[_oppositeFromLast] + transform.parent.position;

		// Distance is measured from the camera to the destination.
		_distance = Math.Abs(Vector3.Distance(GameObject.FindWithTag("MainCamera").transform.position,
							_destination));

		// Add a y-offset to the destination that scales with expected size change by checking distance,
		// without going too far into exponential growth.
		if (_distance > 0.5f)
			_destination.y += _heightOffset / (2 * _distance);
		else
			_destination.y += _heightOffset;

		_last = _oppositeFromLast;

		_lastCollision = Time.time;
	}

	// Reverses target direction when hitting another target, at most every half-second.
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Target") && Time.time - _lastCollision > 0)
			MoveAway();
	}

	// Updates scale and height of the target.
	void UpdateScale()
	{
		// Distance is measured from the camera to the target.
		_distance = Math.Abs(Vector3.Distance(GameObject.FindWithTag("MainCamera").transform.position,
							transform.position));

		// Stop scaling target size by distance only when dividing by 0.
		if (_distance > 0)
			transform.localScale = _baseScale / (2 * _distance);
		// Stop scaling target height offset by distance when it begins growing exponentially
		if (_distance > 0.5f)
			transform.localPosition = new Vector3(transform.localPosition.x,
			_heightOffset / (2 * _distance), transform.localPosition.z);
		// Scale target size to cut in half for every meter away from camera and change height
		// offset to match.
		else
			transform.localPosition = new Vector3(transform.localPosition.x,
			_heightOffset, transform.localPosition.z);
	}
}
