using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;
	public GameObject pivot;
	public float speed = 1200f;
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
		rb.AddForce(Physics.gravity * 2f, ForceMode.Acceleration);
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
		if (Input.GetKey(KeyCode.Space) && onGround)
		{
			rb.AddForce(0, jumpForce, 0f);
			onGround = false;
		}
		if (transform.position.y < -80)
		{
			rb.velocity = new Vector3(0f, 0f, 0f);
			transform.position = new Vector3(0f, 30f, 0f);
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
