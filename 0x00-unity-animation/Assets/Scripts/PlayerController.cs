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

    // Run every fram for physics calculations.
    void FixedUpdate()
    {
		Forward = pivot.transform.forward;
		Right = pivot.transform.right;
		Forward.y = 0f;
		Right.y = 0f;
		Forward.Normalize();
		Right.Normalize();
		step = 720 * Time.deltaTime;

		// Multiplies the force of gravity on the player.
		rb.AddForce(Physics.gravity * 2f, ForceMode.Acceleration);

		// Moves the player with WASD while touching the ground.
		if (onGround)
		{
			if (Input.GetKey("w"))
			{
				// Moves the player horizontally over time in the given direction
				rb.AddForce(Forward * speed * Time.deltaTime, ForceMode.Impulse);
				// Sets the target angle to rotate towards to match the movement direction
				targetRotation = Quaternion.LookRotation(Forward, Vector3.up);
				// Rotates the player towards the targetRotation over time
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
				tyController.SetBool("IsRunning", true);
			}
			else if (Input.GetKey("s"))
			{
				rb.AddForce(Forward * -speed * Time.deltaTime, ForceMode.Impulse);
				targetRotation = Quaternion.LookRotation(-Forward, Vector3.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
				tyController.SetBool("IsRunning", true);
			}
			else if (Input.GetKey("d"))
			{
				rb.AddForce(Right * speed * Time.deltaTime, ForceMode.Impulse);
				targetRotation = Quaternion.LookRotation(Right, Vector3.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
				tyController.SetBool("IsRunning", true);
			}
			else if (Input.GetKey("a"))
			{
				rb.AddForce(Right * -speed * Time.deltaTime, ForceMode.Impulse);
				targetRotation = Quaternion.LookRotation(-Right, Vector3.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
				tyController.SetBool("IsRunning", true);
			}
			else
				tyController.SetBool("IsRunning", false);
		}

		// Makes the player jump with spacebar while on ground.
		if (Input.GetKey(KeyCode.Space) && onGround)
		{
			rb.AddForce(0, jumpForce, 0f);
			onGround = false;
			tyController.SetBool("IsJumping", true);
		}

		// Respawns the player at the start if they fall.
		if (transform.position.y < -60)
		{
			rb.velocity = new Vector3(0f, 0f, 0f);
			transform.position = new Vector3(0f, 25f, 0f);
			transform.rotation = new Quaternion(0, 0, 0, 0);
		}
    }

	// Keeps onGround boolean updated.
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Platform")
		{
			onGround = true;
			tyController.SetBool("IsJumping", false);
		}
	}
}
