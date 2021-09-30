using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
	// Loads the previous scene, saved to PlayerPrefs whenever OptionsMenu
	// is loaded.
    public void Back()
	{
		SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
	}
}
