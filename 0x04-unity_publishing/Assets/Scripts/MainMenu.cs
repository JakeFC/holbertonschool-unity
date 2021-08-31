using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Button playButton;
	public Button quitButton;
	public Material trapMat;
	public Material goalMat;
	public Toggle colorblindMode;

	void Start ()
	{
		// When each button is pressed, their respective scripts will run.
		playButton.GetComponent<Button>().onClick.AddListener(PlayMaze);
		quitButton.GetComponent<Button>().onClick.AddListener(QuitMaze);
	}

	// Starts the maze scene after setting colors dependent on colorblind setting.
    public void PlayMaze()
	{
		if (colorblindMode.GetComponent<Toggle>().isOn)
		{
			trapMat.color = new Color32(255, 112, 0, 1);
			goalMat.color = Color.blue;
		}
		else
		{
			trapMat.color = Color.red;
			goalMat.color = Color.green;
		}
		SceneManager.LoadScene("maze");
	}

	// Exits the game.
	public void QuitMaze()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}
}
