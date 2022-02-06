using System;
using UnityEngine;

public class AmmoMovement : MonoBehaviour
{
	public Camera rayCamera;
	public GameObject ammoBall;
	public GameObject center;
	public GameObject ammoUI;
	public GameObject playAgainButton;
	public GameObject line;
	public Transform ammoOrigin;
	public bool fired = false;
	private bool _mouseDown = false;
	private float _speed
	{
		// Fire speed scales with distance moved from starting position.
		get
		{
			return (100 * (Math.Abs(ammoBall.transform.position.x - ammoOrigin.position.x)
						+ Math.Abs(ammoBall.transform.position.y - ammoOrigin.position.y)
						+ Math.Abs(ammoBall.transform.position.z - ammoOrigin.position.z)));
		}
	}

    void Update()
    {
		if (Input.touchCount > 0 && !_mouseDown)
			_mouseDown = true;
		if (Input.touchCount == 0 && _mouseDown && !fired)
		{
			_mouseDown = false;
			Fire();
		}
		if (_mouseDown && !fired)
			MoveBall();
    }

	// Moves the ball in the direction of the mouse.
	void MoveBall()
	{
		if (!line.activeSelf)
			line.SetActive(true);
		// Saves a ray object pointing from the camera to the mouse position.
		Ray ray = rayCamera.ScreenPointToRay(Input.GetTouch(0).position);

		// Rotates the parent object in the same direction as the saved ray, thereby
		// moving the child ball object in the same direction, with magnitude based
		// on distance from parent to child.
		this.transform.rotation = Quaternion.LookRotation(ray.direction, Vector3.up);

		// Rotates the ball object to face the center of the simulated slingshot, so
		// it will fire in the correct direction.
		ammoBall.transform.LookAt(center.transform);

		line.GetComponent<TrajectoryPhysics>().UpdateTrajectory(ammoBall.transform.forward * _speed,
																ammoBall.GetComponent<Rigidbody>(),
																ammoBall.transform.position);
	}

	void Fire()
	{
		fired = true;
		// Hide the line renderer by disabling it.
		line.SetActive(false);
		// Turns on physics on shooting.
		ammoBall.GetComponent<Rigidbody>().isKinematic = false;
		// Removes parent relationship to avoid physics interactions.
		ammoBall.transform.parent = null;
		// Adds forward force on ball.
		ammoBall.GetComponent<Rigidbody>().AddForce(ammoBall.transform.forward * _speed, ForceMode.Impulse);

		ammoUI.GetComponent<AmmoCounter>().ShootOnce();
	}

	// Resets the object's rotation, as well as the entire ball object.
	// Disables the object afterward if ammoCount is zero.
	public void ResetBall()
	{
		transform.rotation = rayCamera.transform.rotation;
		ammoBall.transform.parent = transform;
		ammoBall.GetComponent<Rigidbody>().isKinematic = true;
		ammoBall.transform.position = ammoOrigin.position;
		ammoBall.transform.rotation = ammoOrigin.rotation;
		fired = false;

		// If out of ammo, bring up the play again button and disable the ammo object to hide it.
		if (ammoUI.GetComponent<AmmoCounter>().ammoCount < 1)
		{
			playAgainButton.SetActive(true);
			transform.gameObject.SetActive(false);
		}
	}
}
