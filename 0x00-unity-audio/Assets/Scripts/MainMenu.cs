using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
	public AudioMixer masterMixer;
	// Loads previously saved volume settings.
	void Start()
	{
		if (PlayerPrefs.HasKey("bgmVol"))
		{
			masterMixer.SetFloat("bgmVol", Mathf.Log10(PlayerPrefs.GetFloat("bgmVol")) * 80);
		}
		if (PlayerPrefs.HasKey("sfxVol"))
		{
			masterMixer.SetFloat("sfxVol", Mathf.Log10(PlayerPrefs.GetFloat("sfxVol")) * 80);
		}
	}
	// Loads scene with given number.
	public void LevelSelect(int level)
	{
		SceneManager.LoadScene(String.Format("Level0{0}", level));
	}

	// Saves the scene name to PlayerPrefs and loads OptionsMenu.
	public void Options()
	{
		PlayerPrefs.SetString("LastScene", "MainMenu");
		SceneManager.LoadScene("Options");
	}

	// Prints a message to console and exits the application.
	public void Exit()
	{
		Debug.Log("Exited");
		Application.Quit();
	}
}
