using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
	public GameObject invertYToggle;

	// Sets the state of Invert Y-Axis to the saved setting on load.
	void Start()
	{
		if (PlayerPrefs.HasKey("Inverted"))
		{
			if (PlayerPrefs.GetInt("Inverted") == 0)
				invertYToggle.GetComponent<Toggle>().isOn = false;
			else
				invertYToggle.GetComponent<Toggle>().isOn = true;
		}
	}

	// Loads the previous scene, saved to PlayerPrefs whenever OptionsMenu
	// is loaded.
    public void Back()
	{
		SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
	}

	// Saves the inverted preference and loads the previous scene.
	public void Apply()
	{
		if (invertYToggle.GetComponent<Toggle>().isOn)
			PlayerPrefs.SetInt("Inverted", 1);
		else
			PlayerPrefs.SetInt("Inverted", 0);
		SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
	}
}
