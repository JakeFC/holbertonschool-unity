using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float speedH = 2f;
	public float speedV = 2f;

	private float yaw = 0f;
	private float pitch = 0f;

    // Rotates the Pivot object and its Main Camera child based on mouse movement.
    void Update()
    {
		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}
