using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollision : MonoBehaviour
{
	public GameObject floor;
	public GameObject ammo;
	private AmmoMovement _ammoMovement;

	void Start()
	{
    	_ammoMovement = ammo.GetComponent<AmmoMovement>();
	}
    void Update()
    {
        if (transform.position.y < floor.transform.position.y)
			_ammoMovement.Reset();
    }

	void OnCollisionEnter()
	{
		_ammoMovement.Reset();
	}
}
