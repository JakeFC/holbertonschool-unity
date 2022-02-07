using System;
using UnityEngine;

public class TargetSpawning : MonoBehaviour
{
	public int targetNumber = 5;
	public int targetsMade = 0;
	public int numVertices = 0;
	public float heightOffset = 0.1f;
	public GameObject target;
	public Vector3[] verticeList;
	private System.Random _rd = new System.Random();
	private int _randNum;
	private float _lastSpawnTime = -1;
	private float _distance;
	private Vector3 _spawnPos;

    void Start()
    {
		// Saves the list of vertices for the plane once its shape is locked in.
        verticeList = gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;

		// Finds the number of vertices in verticeList.
		foreach(Vector3 vertex in verticeList)
			numVertices++;

		//// Manually updates the vertice list by the plane's position in world space,
		// since the list is otherwise based on local position.
		//for (int i = 0; i < 120; i++)
		//{
		//	verticeList[i] = new Vector3(verticeList[i].x + transform.position.x,
		//								verticeList[i].y + transform.position.y,
		//								verticeList[i].z + transform.position.z);
		//}

		// Adds a NavMesh component if the plane doesn't have one.
		//if (gameObject.GetComponent<NavMeshSurface>() == null)
		//{
		//	gameObject.AddComponent<NavMeshSurface>();
		//}

		// Sets NavMeshSurface script to use mesh collider instead of renderer for shape info, since
		// the mesh renderer will be disabled.
		//gameObject.GetComponent<NavMeshSurface>().useGeometry = NavMeshCollectGeometry.PhysicsColliders;

		// Builds the actual NavMesh.
		//gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
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
		// Chooses a random vertex at which to spawn
		_randNum = _rd.Next(0, numVertices - 1);

		_spawnPos = verticeList[_randNum];

		// Distance is measured from the camera to the spawn location.
		_distance = Math.Abs(Vector3.Distance(GameObject.FindWithTag("MainCamera").transform.position,
							_spawnPos));

		// Add a y-offset to the destination that scales with size change by checking distance,
		// without going over base offset.
		if (_distance > 0.5f)
			_spawnPos.y += heightOffset / (2 * _distance);
		else
			_spawnPos.y += heightOffset;

		// Top and bottom rows are excluded here.
		//_randNum = _rd.Next(12, 108);

		// Left and right columns are excluded here.
		//while(_randNum % 11 == 0 || _randNum % 11 == 10)
		//	_randNum = _rd.Next(12, 108);

		// 0.1 added to height of the target so it appears on top of the plane.
		// The plane is set as a parent object. Position of the plane must be added
		// to convert from local to worldspace.
		Instantiate(target, _spawnPos + transform.position, new Quaternion(0, 0, 0, 1), transform);

		// Spawns a target slightly above the center of the plane with plane as parent.
		//Instantiate(target, new Vector3(transform.position.x, transform.position.y + 0.1f,
		//			transform.position.z), new Quaternion(0, 0, 0, 1), transform);

		targetsMade++;
	}
}
