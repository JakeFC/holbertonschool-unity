using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TargetSpawning : MonoBehaviour
{
	public int targetNumber = 5;
	public int targetsMade = 0;
	public int numVertices = 0;
	public GameObject target;
	public Vector3[] verticeList;
	private System.Random _rd = new System.Random();
	private int _randNum;
	private float _lastSpawnTime = -1;
	private Vector3 _pos;

    void Start()
    {
		// Saves the list of 121 vertices for the plane.
        verticeList = gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;

		//// Manually updates the vertice list by the plane's position in world space,
		// since the list is otherwise based on local position.
		//for (int i = 0; i < 120; i++)
		//{
		//	verticeList[i] = new Vector3(verticeList[i].x + transform.position.x,
		//								verticeList[i].y + transform.position.y,
		//								verticeList[i].z + transform.position.z);
		//}

		// Adds a NavMesh component if the plane doesn't have one.
		if (gameObject.GetComponent<NavMeshSurface>() == null)
		{
			gameObject.AddComponent<NavMeshSurface>();
		}

		// Sets NavMeshSurface script to use mesh collider instead of renderer for shape info, since
		// the mesh renderer will be disabled.
		gameObject.GetComponent<NavMeshSurface>().useGeometry = NavMeshCollectGeometry.PhysicsColliders;

		// Builds the actual NavMesh.
		gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void Update()
    {
		// If the goal is not met and it's been at least a second since last spawn, spawn again.
        if (targetsMade < targetNumber && Time.time - _lastSpawnTime > 1)
		{
			_lastSpawnTime = Time.time;
			SpawnTarget();
		}
    }

	// Attemps to spawn a target on one of the plane's inner vertices.
	void SpawnTarget()
	{
		_pos = transform.position;

		// Finds the number of vertices in verticeList.
		foreach(Vector3 vertex in verticeList)
			numVertices++;

		// Chooses a random vertex at which to spawn
		_randNum = _rd.Next(0, numVertices);

		// Top and bottom rows are excluded here.
		//_randNum = _rd.Next(12, 108);

		// Left and right columns are excluded here.
		//while(_randNum % 11 == 0 || _randNum % 11 == 10)
		//	_randNum = _rd.Next(12, 108);

		// 0.073 added to height of the target so it appears on top of the plane.
		// The plane is set as a parent object. Position of the plane must be added
		// to convert from local to worldspace.
		Instantiate(target, new Vector3(verticeList[_randNum].x + _pos.x,
					verticeList[_randNum].y + 0.073f + _pos.y,
					verticeList[_randNum].z + _pos.z), new Quaternion(0, 0, 0, 1), transform);

		GameObject.FindWithTag("Debug2").GetComponent<Text>().text =
		new Vector3(verticeList[_randNum].x + _pos.x,
					verticeList[_randNum].y + 0.073f + _pos.y,
					verticeList[_randNum].z + _pos.z).ToString();

		// Spawns a target slightly above the center of the plane with plane as parent.
		//Instantiate(target, new Vector3(transform.position.x, transform.position.y + 0.073f,
		//			transform.position.z), new Quaternion(0, 0, 0, 1), transform);

		targetsMade++;
	}
}
