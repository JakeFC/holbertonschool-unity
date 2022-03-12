using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
	public GameObject invertYToggle;
	public AudioMixer masterMixer;
	public Slider bgmSlider;
	public Slider sfxSlider;

	// Sets the state of Invert Y-Axis to the saved setting on load, as well as volume
	// variables' starting values for use in other functions.
	void Start()
	{
		if (PlayerPrefs.HasKey("Inverted"))
		{
			if (PlayerPrefs.GetInt("Inverted") == 0)
				invertYToggle.GetComponent<Toggle>().isOn = false;
			else
				invertYToggle.GetComponent<Toggle>().isOn = true;
		}
		if (PlayerPrefs.HasKey("bgmVol"))
		{
			bgmSlider.value = PlayerPrefs.GetFloat("bgmVol");
		}
		if (PlayerPrefs.HasKey("sfxVol"))
		{
			sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");
		}
	}

	// Loads the previous scene, saved to PlayerPrefs whenever OptionsMenu
	// is loaded.
	public void Back()
	{
		SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
	}

	// Saves the inverted and volume preferences and loads the previous scene.
	public void Apply()
	{
		if (invertYToggle.GetComponent<Toggle>().isOn)
			PlayerPrefs.SetInt("Inverted", 1);
		else
			PlayerPrefs.SetInt("Inverted", 0);
		SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
		PlayerPrefs.SetFloat("bgmVol", bgmSlider.value);
		PlayerPrefs.SetFloat("sfxVol", sfxSlider.value);
	}

	// Sets the background music volume in the master mixer.
	public void SetBgmVolume(float bgmLvl)
	{
		masterMixer.SetFloat("bgmVol", Mathf.Log10(bgmLvl) * 80);
	}

	// Sets the sound effects volume in the master mixer.
	public void SetSfxVolume(float sfxLvl)
	{
		masterMixer.SetFloat("sfxVol", Mathf.Log10(sfxLvl) * 80);
	}
}
