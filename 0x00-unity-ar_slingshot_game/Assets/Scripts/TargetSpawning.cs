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

		// Height offset added to y-position of the target so it appears on top of the plane.
		// The plane is set as a parent object. Position of the plane must be added
		// to convert from local to worldspace.
		Instantiate(target, _spawnPos + transform.position, new Quaternion(0, 0, 0, 1), transform);

		targetsMade++;
	}
}
