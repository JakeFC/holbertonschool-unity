using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float speedH = 2f;
	public float speedV = 2f;

	private float yaw = 0f;
	private float pitch = 0f;

	public bool isInverted = false;

	// Checks PlayerPrefs for Y-axis inversion settings on load.
	void Start()
	{
		if (PlayerPrefs.HasKey("Inverted"))
		{
			if (PlayerPrefs.GetInt("Inverted") == 0)
				isInverted = false;
			else
				isInverted = true;
		}
	}
    // Rotates the Pivot object and its Main Camera child based on mouse movement.
    void Update()
    {
		yaw += speedH * Input.GetAxis("Mouse X");

		if (isInverted)
			pitch += speedV * Input.GetAxis("Mouse Y");
		else
			pitch -= speedV * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0);

		// the following could also work if placed on camera GameObject
		//
		// if (Input.GetMouseButton(0))
		// {
		//	transform.RotateAround(target.transform.position, -Vector3.up, Input.GetAxis("Mouse X"));
		//	transform.RotateAround(target.transform.position, Vector3.right, Input.GetAxis("Mouse Y"));
		//	}
    }
}
