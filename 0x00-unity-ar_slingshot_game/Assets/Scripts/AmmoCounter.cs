using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
	public GameObject ammo;
	public int ammoCount = 7;
    private List<Image> _ammoCircles = new List<Image>();
	private Color _baseColor;

    void Start()
    {
        foreach (Transform child in transform)
			if (child.name != "AmmoText")
				_ammoCircles.Add(child.gameObject.GetComponent<Image>());
		_baseColor = _ammoCircles[0].color;
    }

	public void ShootOnce()
	{
		_ammoCircles[--ammoCount].color = Color.black;
	}

	public void ResetAmmo()
	{
		ammoCount = 7;
		foreach (Image circle in _ammoCircles)
			circle.color = _baseColor;
	}
}
