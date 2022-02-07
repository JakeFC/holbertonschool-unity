using UnityEngine;
using UnityEngine.UI;

public class TargetMovement : MonoBehaviour
{
	public float speed = 1;
	//private NavMeshAgent _target;
	private Vector3 _destination;
	private Vector3[] _verticeList;
	private System.Random _rd = new System.Random();
	private int _randNum = 0, _oppositeFromLast, _last = -1, _numVertices;
	private float _time = 0;
	//private Vector3 _pos;

    void Start()
    {
        //_target = GetComponent<NavMeshAgent>();

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

		transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);

		GameObject.FindWithTag("Debug").GetComponent<Text>().text =
		(transform.parent.position.ToString() + "\n" +
		transform.position.ToString());
    }

	// Sets the target's move destination to a random inner vertex.
	void SetRandomMove()
	{
		// A random vertex is chosen from the list.
		_randNum = _rd.Next(0, _numVertices - 1);

		// Duplicates from last move are removed.
		while (_randNum == _last)
			_randNum = _rd.Next(0, _numVertices - 1);

		// Top and bottom rows are excluded here.
		//_randNum = _rd.Next(12, 108);

		// Left and right columns are excluded here, as well as duplicates from last move.
		//while(_randNum % 11 == 0 || _randNum % 11 == 10 || _randNum == _last)
		//	_randNum = _rd.Next(12, 108);
		
		// Add parent position and spawn height difference to convert from local to worldspace.
		_destination = _verticeList[_randNum] + transform.parent.position + new Vector3(0, 0.1f,0);

		int i = 0;
		foreach(Vector3 vertex in _verticeList)
			i++;

		GameObject.FindWithTag("Debug3").GetComponent<Text>().text =
		(_verticeList[_randNum] + transform.parent.position).ToString() + i.ToString();

		//_pos = transform.position;
		//_randNum = _rd.Next(0, 7);

		//// Randomize until the number doesn't match the last one.
		//while(_randNum == _last)
		//	_randNum = _rd.Next(0, 7);
		
		//// Each number corresponds to one of eight directions from origin.
		//switch(_randNum)
		//{
		//	case 0:
		//		_destination.destination = new Vector3(_pos.x, _pos.y, _pos.z + .04f);
		//		break;
		//	case 1:
		//		_destination.destination = new Vector3(_pos.x + .02f, _pos.y, _pos.z + .02f);
		//		break;
		//	case 2:
		//		_destination.destination = new Vector3(_pos.x + .04f, _pos.y, _pos.z);
		//		break;
		//	case 3:
		//		_destination.destination = new Vector3(_pos.x + .02f, _pos.y, _pos.z - .02f);
		//		break;
		//	case 4:
		//		_destination.destination = new Vector3(_pos.x, _pos.y, _pos.z - .04f);
		//		break;
		//	case 5:
		//		_destination.destination = new Vector3(_pos.x - .02f, _pos.y, _pos.z - .02f);
		//		break;
		//	case 6:
		//		_destination.destination = new Vector3(_pos.x - .04f, _pos.y, _pos.z);
		//		break;
		//	case 7:
		//		_destination.destination = new Vector3(_pos.x - .02f, _pos.y, _pos.z + .02f);
		//		break;
		//}
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
		_last = _oppositeFromLast;

		//if (_last == -1)
		//	_last = _randNum;
		
		//// Each direction corresponds to the opposite of those in SetRandomMove()
		//switch(_last)
		//{
		//	case 4:
		//		_destination.destination = new Vector3(_pos.x, _pos.y, _pos.z + .04f);
		//		break;
		//	case 5:
		//		_destination.destination = new Vector3(_pos.x + .02f, _pos.y, _pos.z + .02f);
		//		break;
		//	case 6:
		//		_destination.destination = new Vector3(_pos.x + .04f, _pos.y, _pos.z);
		//		break;
		//	case 7:
		//		_destination.destination = new Vector3(_pos.x + .02f, _pos.y, _pos.z - .02f);
		//		break;
		//	case 0:
		//		_destination.destination = new Vector3(_pos.x, _pos.y, _pos.z - .04f);
		//		break;
		//	case 1:
		//		_destination.destination = new Vector3(_pos.x - .02f, _pos.y, _pos.z - .02f);
		//		break;
		//	case 2:
		//		_destination.destination = new Vector3(_pos.x - .04f, _pos.y, _pos.z);
		//		break;
		//	case 3:
		//		_destination.destination = new Vector3(_pos.x - .02f, _pos.y, _pos.z + .02f);
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
