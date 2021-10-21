using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
	public GameObject PauseCanvas;
	public AudioSource backgroundMusic;
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	private bool Paused = false;

    // Used to monitor escape keypress.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Paused)
			{
				unpaused.TransitionTo(0.01f);
				Resume();
			}
			else
			{
				paused.TransitionTo(0.01f);
				Pause();
			}
		}
    }

	// Stops the timer and opens the pause menu overlay.
	public void Pause()
	{
		Paused = true;
		GetComponentsInChildren<CameraController>()[0].enabled = false;
		GetComponent<Timer>().enabled = false;
		PauseCanvas.SetActive(true);
	}

	// Starts the timer again and hides the pause menu overlay.
	public void Resume()
	{
		Paused = false;
		GetComponentsInChildren<CameraController>()[0].enabled = true;
		GetComponent<Timer>().enabled = true;
		PauseCanvas.SetActive(false);
	}

	// Restarts the currently loaded scene.
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// Loads the MainMenu scene.
	public void MainMenu()
	{
		backgroundMusic.Stop();
		SceneManager.LoadScene("MainMenu");
	}

	// Loads the Options scene after saving the current's name to PlayerPrefs.
	public void Options()
	{
		PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
		SceneManager.LoadScene("Options");
	}
}
