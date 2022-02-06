using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonBehavior : MonoBehaviour
{
    public GameObject planePrefab;

	public void SpawnPlane()
	{
		Instantiate(planePrefab, new Vector3(0, -1, 9), new Quaternion(0, 0, 0, 1));
	}
}