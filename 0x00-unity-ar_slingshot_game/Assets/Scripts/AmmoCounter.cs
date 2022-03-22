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
		// Add each ammo circle image to the array
        	foreach (Transform child in transform)
			if (child.name != "AmmoText")
				_ammoCircles.Add(child.gameObject.GetComponent<Image>());
		_baseColor = _ammoCircles[0].color;
    	}

	// Ammo count decrements and one circle is turned to black when called.
	public void ShootOnce()
	{
		_ammoCircles[--ammoCount].color = Color.black;
	}

	// Sets ammoCount to 7 again and each image to the original color.
	public void ResetAmmo()
	{
		ammoCount = 7;
		foreach (Image circle in _ammoCircles)
			circle.color = _baseColor;
	}
}
