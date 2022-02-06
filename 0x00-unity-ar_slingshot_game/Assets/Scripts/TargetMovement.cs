using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TargetMovement : MonoBehaviour
{
	private NavMeshAgent _target;
	private Vector3[] _verticeList;
	private System.Random _rd = new System.Random();
	private int _randNum = 4, _oppositeFromLast, _last = -1;
	private float _time = 0;
	private Vector3 _pos;

    void Start()
    {
        _target = GetComponent<NavMeshAgent>();

		// List of points on the plane are taken from the parent plane.
		_verticeList = transform.parent.GetComponent<TargetSpawning>().verticeList;
    }

    void Update()
    {
		_time += Time.deltaTime;

		// Direction updates every 1.3 seconds and 4.2 seconds.
		if (_time % 1.3f < 0.1f || _time % 4.2f < 0.1f)
			RandomMove();

		GameObject.FindWithTag("Debug").GetComponent<Text>().text =
		(transform.parent.position.ToString() + "\n" +
		transform.position.ToString());
    }

	// Moves the target toward a random inner vertex.
	void RandomMove()
	{
		// Top and bottom rows are excluded here.
		_randNum = _rd.Next(12, 108);

		// Left and right columns are excluded here, as well as duplicates from last move.
		while(_randNum % 11 == 0 || _randNum % 11 == 10 || _randNum == _last)
			_randNum = _rd.Next(12, 108);
		
		_target.destination = _verticeList[_randNum];

		//_pos = transform.position;
		//_randNum = _rd.Next(0, 7);

		//// Randomize until the number doesn't match the last one.
		//while(_randNum == _last)
		//	_randNum = _rd.Next(0, 7);
		
		//// Each number corresponds to one of eight directions from origin.
		//switch(_randNum)
		//{
		//	case 0:
		//		_target.destination = new Vector3(_pos.x, _pos.y, _pos.z + .04f);
		//		break;
		//	case 1:
		//		_target.destination = new Vector3(_pos.x + .02f, _pos.y, _pos.z + .02f);
		//		break;
		//	case 2:
		//		_target.destination = new Vector3(_pos.x + .04f, _pos.y, _pos.z);
		//		break;
		//	case 3:
		//		_target.destination = new Vector3(_pos.x + .02f, _pos.y, _pos.z - .02f);
		//		break;
		//	case 4:
		//		_target.destination = new Vector3(_pos.x, _pos.y, _pos.z - .04f);
		//		break;
		//	case 5:
		//		_target.destination = new Vector3(_pos.x - .02f, _pos.y, _pos.z - .02f);
		//		break;
		//	case 6:
		//		_target.destination = new Vector3(_pos.x - .04f, _pos.y, _pos.z);
		//		break;
		//	case 7:
		//		_target.destination = new Vector3(_pos.x - .02f, _pos.y, _pos.z + .02f);
		//		break;
		//}
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

		//if (_last == -1)
		//	_last = _randNum;
		
		//// Each direction corresponds to the opposite of those in RandomMove()
		//switch(_last)
		//{
		//	case 4:
		//		_target.destination = new Vector3(_pos.x, _pos.y, _pos.z + .04f);
		//		break;
		//	case 5:
		//		_target.destination = new Vector3(_pos.x + .02f, _pos.y, _pos.z + .02f);
		//		break;
		//	case 6:
		//		_target.destination = new Vector3(_pos.x + .04f, _pos.y, _pos.z);
		//		break;
		//	case 7:
		//		_target.destination = new Vector3(_pos.x + .02f, _pos.y, _pos.z - .02f);
		//		break;
		//	case 0:
		//		_target.destination = new Vector3(_pos.x, _pos.y, _pos.z - .04f);
		//		break;
		//	case 1:
		//		_target.destination = new Vector3(_pos.x - .02f, _pos.y, _pos.z - .02f);
		//		break;
		//	case 2:
		//		_target.destination = new Vector3(_pos.x - .04f, _pos.y, _pos.z);
		//		break;
		//	case 3:
		//		_target.destination = new Vector3(_pos.x - .02f, _pos.y, _pos.z + .02f);
		//		break;
		//}

		//// _last variable is updated to the opposite _randNum value
		//if (_last > 3)
		//	_last -= 4;
		//else
		//	_last += 4;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Target"))
			MoveAway();
	}
}
