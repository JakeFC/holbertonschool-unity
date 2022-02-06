using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TargetSpawning : MonoBehaviour
{
	public int targetNumber = 5;
	public int targetsMade = 0;
	public GameObject target;
	//public Vector3[] verticeList;
	private System.Random _rd = new System.Random();
	private int _randNum;
	private float _lastSpawnTime = -1;

    void Start()
    {
		//// Saves the list of 121 vertices for the plane.
        //verticeList = gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;

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
		//// Top and bottom rows are excluded here.
		//_randNum = _rd.Next(12, 108);

		//// Left and right columns are excluded here.
		//while(_randNum % 11 == 0 || _randNum % 11 == 10)
		//	_randNum = _rd.Next(12, 108);

		//// 0.733 added to height of the target so it appears on top of the plane.
		//// The plane is set as a parent object.
		//Instantiate(target, new Vector3(verticeList[_randNum].x, verticeList[_randNum].y + 0.733f,
		//			verticeList[_randNum].z), new Quaternion(0, 0, 0, 1), transform);

		// Spawns a target slightly above the center of the plane with plane as parent.
		Instantiate(target, new Vector3(transform.position.x, transform.position.y + 0.053f,
					transform.position.z), new Quaternion(0, 0, 0, 1), transform);

		//Instantiate(target, transform, false);

		targetsMade++;
	}
}
