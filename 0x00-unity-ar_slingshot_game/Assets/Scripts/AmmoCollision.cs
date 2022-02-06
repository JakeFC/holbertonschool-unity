using UnityEngine;
using UnityEngine.UI;

public class AmmoCollision : MonoBehaviour
{
	public GameObject ammo;
	public Text scoreText;
	private AmmoMovement _ammoMovement;
	private GameObject _floor;

	void Start()
	{
    	_ammoMovement = ammo.GetComponent<AmmoMovement>();

		_floor = GameObject.FindWithTag("Floor");
	}
    void Update()
    {
        if (transform.position.y < _floor.transform.position.y && _ammoMovement.fired)
			_ammoMovement.ResetBall();
    }

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Target"))
			scoreText.text = (int.Parse(scoreText.text) + 10).ToString();
		if (_ammoMovement.fired == false)
			return;
		_ammoMovement.ResetBall();
	}
}
