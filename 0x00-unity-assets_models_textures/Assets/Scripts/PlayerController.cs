using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;
	public float speed = 2000f;
	public float jumpForce = 2000f;
	private bool onGround = true;

    // Run every fram for physics calculations.
    void FixedUpdate()
    {
		if (onGround)
		{
			if (Input.GetKey("w"))
			{
				if (rb.velocity.z < 0)
					rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
				rb.AddForce(0, 0, speed * Time.deltaTime);
			}
			if (Input.GetKey("s"))
			{
				if (rb.velocity.z > 0)
					rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
				rb.AddForce(0, 0, -speed * Time.deltaTime);
			}
			if (Input.GetKey("d"))
			{
				if (rb.velocity.x < 0)
					rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
				rb.AddForce(speed * Time.deltaTime, 0, 0);
			}
			if (Input.GetKey("a"))
			{
				if (rb.velocity.x > 0)
					rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
				rb.AddForce(-speed * Time.deltaTime, 0, 0);
			}
		}
		if (Input.GetKey(KeyCode.Space) && onGround)
		{
			rb.AddForce(0, jumpForce, 0);
			onGround = false;
		}
    }

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Platform")
		{
			onGround = true;
		}
	}
}
