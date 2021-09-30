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
    }
}
