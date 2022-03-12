using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
	public AudioSource backgroundMusic;

	void Start()
	{
		backgroundMusic.Stop();
	}

	// Loads the Main Menu.
    	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	// Loads the next scene in the build order, or Main Menu if on last level.
	public void Next()
	{
		if (SceneManager.GetActiveScene().name == "Level03")
			MainMenu();
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
