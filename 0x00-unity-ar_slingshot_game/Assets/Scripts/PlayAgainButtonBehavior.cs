using UnityEngine;
using UnityEngine.UI;

public class PlayAgainButtonBehavior : MonoBehaviour
{
	private GameObject _floor;

	public Text scoreText;

	// Deletes all targets, so they'll spawn again.
    public void ResetTargets()
    {
        _floor = GameObject.FindWithTag("Floor");
		foreach (Transform child in _floor.transform)
			Destroy(child.gameObject);
		_floor.GetComponent<TargetSpawning>().targetsMade = 0;
    }

    // Sets the score to 0.
    public void ResetScore()
    {
        scoreText.text = "0";
    }
}
