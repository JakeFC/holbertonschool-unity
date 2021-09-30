using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
