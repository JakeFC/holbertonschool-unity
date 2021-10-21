using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;
	public GameObject pivot;
	public float speed = 40f;
	public float jumpForce = 600f;
	public Animator tyController;
	private bool onGround = true;
	private Vector3 Forward;
	private Vector3 Right;
	private float step;
	private Quaternion targetRotation;
	private bool resetting = false;

	void Start()
	{
		step = 7200 * Time.deltaTime;
		targetRotation.Set(0, 0, 0, 1);
	}
    // Run every fram for physics calculations.
    void FixedUpdate()
    {
		Forward = pivot.transform.forward;
		Right = pivot.transform.right;
		Forward.y = 0f;
		Right.y = 0f;
		Forward.Normalize();
		Right.Normalize();
		// Multiplies the force of gravity on the player.
		rb.AddForce(Physics.gravity * 2f, ForceMode.Acceleration);

		// Moves the player with WASD while touching the ground.
		if (onGround)
		{
			if (Input.GetKey("w") && !resetting)
			{
				// Moves the player horizontally over time in the given direction
				rb.AddForce(Forward * speed * Time.deltaTime, ForceMode.Impulse);
				// Sets the target angle to rotate towards to match the movement direction
				targetRotation = Quaternion.Slerp(targetRotation, Quaternion.LookRotation(Forward, Vector3.up), 0.5f);
				// Rotates the player towards the targetRotation over time
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
			}
			if (Input.GetKey("s") && !resetting)
			{
				rb.AddForce(Forward * -speed * Time.deltaTime, ForceMode.Impulse);
				targetRotation = Quaternion.Slerp(targetRotation, Quaternion.LookRotation(-Forward, Vector3.up), 0.5f);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
			}
			if (Input.GetKey("d") && !resetting)
			{
				rb.AddForce(Right * speed * Time.deltaTime, ForceMode.Impulse);
				targetRotation = Quaternion.Slerp(targetRotation, Quaternion.LookRotation(Right, Vector3.up), 0.5f);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
			}
			if (Input.GetKey("a") && !resetting)
			{
				rb.AddForce(Right * -speed * Time.deltaTime, ForceMode.Impulse);
				targetRotation = Quaternion.Slerp(targetRotation, Quaternion.LookRotation(-Right, Vector3.up), 0.5f);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
			}
			if (rb.velocity.x < 0.1 && rb.velocity.x > -0.1 &&
				rb.velocity.z < 0.1 && rb.velocity.z > -0.1)
				tyController.SetBool("IsRunning", false);
			else
				tyController.SetBool("IsRunning", true);
		}

		// Makes the player jump with spacebar while on ground.
		if (Input.GetKey(KeyCode.Space) && onGround && !resetting)
		{
			rb.AddForce(0, jumpForce, 0f);
			onGround = false;
			tyController.SetBool("IsJumping", true);
		}

		if (transform.position.y < -10)
		{
			tyController.SetBool("IsFalling", true);
		}

		// Respawns the player at the start if they fall.
		if (transform.position.y < -60)
		{
			rb.velocity = new Vector3(0f, 0f, 0f);
			transform.position = new Vector3(0f, 25f, 0f);
			transform.rotation = new Quaternion(0, 0, 0, 0);
			StartCoroutine(Reset());
		}
    }

	// Changes a boolean for 10 seconds that movement physics rely upon
	IEnumerator Reset()
	{
		resetting = true;
		yield return new WaitForSeconds(10);
		resetting = false;
	}
	// Keeps onGround, IsJumping, and IsFalling booleans updated.
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Platform")
		{
			onGround = true;
			tyController.SetBool("IsJumping", false);
			tyController.SetBool("IsFalling", false);
		}
	}
}
