using UnityEngine;
using UnityEngine.UI;

public class AmmoCollision : MonoBehaviour
{
	public GameObject ammo;
	public Text scoreText;
	// The AmmoMovement script
	private AmmoMovement _ammoMovement;
	private GameObject _floor;

	void Start()
	{
    		_ammoMovement = ammo.GetComponent<AmmoMovement>();

		_floor = GameObject.FindWithTag("Floor");
	}

    	void Update()
    	{
		// Reset the ball if it goes below the plane after being fired
        	if (transform.position.y < _floor.transform.position.y && _ammoMovement.fired)
			_ammoMovement.ResetBall();
   	}

	void OnCollisionEnter(Collision other)
	{
		// Ignore collision if not fired yet.
		if (_ammoMovement.fired == false)
			return;
		// Add 10 points if ball hits a target
		if (other.gameObject.CompareTag("Target"))
			scoreText.text = (int.Parse(scoreText.text) + 10).ToString();
		_ammoMovement.ResetBall();
	}
}
