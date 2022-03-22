using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;
	public GameObject pivot;
	public float speed;
	public float jumpForce;
	public Animator tyController;
	public AudioSource runningGrass;
	public AudioSource runningRock;
	public AudioSource landingGrass;
	public AudioSource landingRock;
	// Check for player on ground is an int to acknowledge more than 1 surface at once.
	private int onGround = 0;
	private Vector3 Forward;
	private Vector3 Right;
	// Player rotation speed scaler.
	private float step;
	// Target rotation for player model.
	private Quaternion targetRotation;
	private bool resetting = false;
	// Used to keep track of whether grass or rock sounds need playing
	private string groundType;
	private bool runningSound = false;
	private float time;

	void Start()
	{
		// Rotation change rate scales with frame time to keep speed uniform.
		step = 7200 * Time.deltaTime;
		targetRotation.Set(0, 0, 0, 1);
	}
	
	// Scripted loop to make sound clip match footsteps.
	void Update()
	{
		if (runningRock.isPlaying)
		{
			time += Time.deltaTime;
		}
		else if (time > 0 && time < 0.388f && runningSound)
		{
			time += Time.deltaTime;
		}
		else if (time >= 0.388f)
		{
			runningRock.Play();
			time = 0;
		}
	}

    	// Run every fram for physics calculations.
    	void FixedUpdate()
    	{
		Forward = pivot.transform.forward;
		Right = pivot.transform.right;
		// Movement directions should have no vertical value.
		Forward.y = 0f;
		Right.y = 0f;
		Forward.Normalize();
		Right.Normalize();

		// Doubles the force of gravity on the player.
		rb.AddForce(Physics.gravity * 2f, ForceMode.Acceleration);

		// Moves the player with WASD while touching the ground.
		if (onGround > 0)
		{
			if (Input.GetKey("w") && !resetting)
			{
				// Moves the player horizontally over time in the given direction
				rb.AddForce(Forward * speed * Time.deltaTime, ForceMode.Impulse);
				// Sets the target rotation to angle over time to match the movement direction
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
			
			// If player is basically still, player is no longer running.
			if (rb.velocity.x < 0.1 && rb.velocity.x > -0.1 &&
				rb.velocity.z < 0.1 && rb.velocity.z > -0.1)
			{
				tyController.SetBool("IsRunning", false);
				if (runningSound)
					StopRunning();
			}
			else
			{
				tyController.SetBool("IsRunning", true);
				if (!runningSound)
					PlayRunning();
			}
		}

		// Makes the player jump with spacebar while on ground.
		if (Input.GetKey(KeyCode.Space) && onGround > 0 && !resetting)
		{
			rb.AddForce(0, jumpForce, 0f);
			tyController.SetBool("IsJumping", true);
		}

		// If player falls far enough, change animation to falling.
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

	// Changes a boolean for 10 seconds that movement physics rely upon to stop player movement
	IEnumerator Reset()
	{
		resetting = true;
		yield return new WaitForSeconds(10);
		resetting = false;
	}

	// Keeps onGround, IsJumping, and IsFalling booleans updated.
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Grass")
		{
			// Add each platform, so player is not considered off ground while still touching one
			onGround++;
			tyController.SetBool("IsJumping", false);
			if (tyController.GetBool("IsFalling"))
			{
				tyController.SetBool("IsFalling", false);
				if (other.gameObject.tag == "Grass")
					landingGrass.Play();
				else
					landingRock.Play();
			}
			groundType = other.gameObject.tag;
		}
	}
	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Grass")
		{
			onGround--;
			if (runningSound)
				StopRunning();
		}
	}

	void PlayRunning()
	{
		if (groundType == "Grass")
			runningGrass.Play();
		else
			runningRock.Play();
		runningSound = true;
	}

	void StopRunning()
	{
		if (groundType == "Grass")
			runningGrass.Stop();
		else
			runningRock.Stop();
		runningSound = false;
		time = 0;
	}
}
