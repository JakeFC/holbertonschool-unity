using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonBehavior : MonoBehaviour
{
	// Reloads the scene.
    public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
