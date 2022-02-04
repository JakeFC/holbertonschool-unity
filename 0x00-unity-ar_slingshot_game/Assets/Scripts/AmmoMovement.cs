using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMovement : MonoBehaviour
{
	public Camera rayCamera;
	public GameObject ammoBall;
	public GameObject center;
	private float _speed;
	private bool _fired = false;
	private bool _mouseDown = false;
	private Vector3 _ballOGPosition;
	private Quaternion _ballOGRotation;


	void Start()
	{
		_ballOGPosition = ammoBall.transform.position;
		_ballOGRotation = ammoBall.transform.rotation;
	}
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
			_mouseDown = true;
		if (Input.GetMouseButtonUp(0) && _mouseDown && !_fired)
		{
			_mouseDown = false;
			Fire();
			_fired = true;
		}
		if (_mouseDown && !_fired)
			MoveBall();
    }

	// Moves the ball in the direction of the mouse.
	void MoveBall()
	{
		// Saves a ray object pointing from the camera to the mouse position.
		Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);

		// Rotates the parent object in the same direction as the saved ray, thereby
		// moving the child ball object in the same direction, with magnitude based
		// on distance from parent to child.
		this.transform.rotation = Quaternion.LookRotation(ray.direction, Vector3.up);

		// Rotates the ball object to face the center of the simulated slingshot, so
		// it will fire in the correct direction.
		ammoBall.transform.LookAt(center.transform);
	}

	void Fire()
	{
		// Fire speed scales with distance moved from starting position.
		_speed = 30 * (Math.Abs(ammoBall.transform.position.x) + Math.Abs(ammoBall.transform.position.y - 3.2f));
		// Turns on physics on shooting.
		ammoBall.GetComponent<Rigidbody>().isKinematic = false;
		// Removes parent relationship to avoid physics interactions.
		ammoBall.transform.parent = null;
		// Adds forward force on ball.
		ammoBall.GetComponent<Rigidbody>().AddForce(ammoBall.transform.forward * _speed, ForceMode.Impulse);
	}

	public void Reset()
	{
		transform.rotation = new Quaternion (0, 0, 0, 1);
		ammoBall.transform.parent = transform;
		ammoBall.GetComponent<Rigidbody>().isKinematic = true;
		ammoBall.transform.position = _ballOGPosition;
		ammoBall.transform.rotation = _ballOGRotation;
		_fired = false;
	}
}
