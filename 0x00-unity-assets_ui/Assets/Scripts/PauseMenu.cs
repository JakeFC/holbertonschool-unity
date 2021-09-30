using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public GameObject PauseCanvas;
	private bool Paused = false;

    // Used to monitor escape keypress.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Paused)
				Resume();
			else
				Pause();
		}
    }

	// Stops the timer and opens the pause menu overlay.
	public void Pause()
	{
		Paused = true;
		GetComponent<Timer>().enabled = false;
		PauseCanvas.SetActive(true);
	}

	public void Resume()
	{
		Paused = false;
		GetComponent<Timer>().enabled = true;
		PauseCanvas.SetActive(false);
	}
}
