using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;
	public GameObject pivot;
	public float speed = 40f;
	public float jumpForce = 600f;
	private bool onGround = true;
	private Vector3 Forward;
	private Vector3 Right;

    // Run every fram for physics calculations.
    void FixedUpdate()
    {
		Forward = pivot.transform.forward;
		Right = pivot.transform.right;
		Forward.y = 0f;
		Right.y = 0f;

		// Multiplies the force of gravity on the player.
		rb.AddForce(Physics.gravity * 2f, ForceMode.Acceleration);

		// Moves the player with WASD while touching the ground.
		if (onGround)
		{
			if (Input.GetKey("w"))
			{
				rb.AddForce(Forward * speed * Time.deltaTime, ForceMode.Impulse);
			}
			if (Input.GetKey("s"))
			{
				rb.AddForce(Forward * -speed * Time.deltaTime, ForceMode.Impulse);
			}
			if (Input.GetKey("d"))
			{
				rb.AddForce(Right * speed * Time.deltaTime, ForceMode.Impulse);
			}
			if (Input.GetKey("a"))
			{
				rb.AddForce(Right * -speed * Time.deltaTime, ForceMode.Impulse);
			}
		}

		// Makes the player jump with spacebar while on ground.
		if (Input.GetKey(KeyCode.Space) && onGround)
		{
			rb.AddForce(0, jumpForce, 0f);
			onGround = false;
		}

		// Respawns the player at the start if they fall.
		if (transform.position.y < -60)
		{
			rb.velocity = new Vector3(0f, 0f, 0f);
			transform.position = new Vector3(0f, 25f, 0f);
		}
    }

	// Keeps onGround boolean updated.
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Platform")
		{
			onGround = true;
		}
	}
}
