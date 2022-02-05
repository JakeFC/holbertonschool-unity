using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetSpawning : MonoBehaviour
{
	public int targetNumber;
	public int targetsMade = 0;
	public GameObject target;
	//public Vector3[] verticeList;
	private System.Random _rd = new System.Random();
	private int _randNum;

    void Start()
    {
		//// Saves the list of 121 vertices for the plane.
        //verticeList = gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;

		// Adds a NavMesh component if the plane doesn't have one.
		if (gameObject.GetComponent<NavMeshSurface>() == null)
		{
			gameObject.AddComponent<NavMeshSurface>();
		}
		// Builds the actual NavMesh.
		gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void Update()
    {
        if (targetsMade < targetNumber && Time.time % 1 < 0.003)
			SpawnTarget();
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

		// Spawns a target in the center of the plane with plane as parent.
		Instantiate(target, transform.position, new Quaternion(0, 0, 0, 1), transform);

		targetsMade++;
	}
}
