using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
	// Loads the MainMenu.
	 public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	// Loads the next scene in the build order. MainMenu is ordered after Level03.
	public void Next()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
