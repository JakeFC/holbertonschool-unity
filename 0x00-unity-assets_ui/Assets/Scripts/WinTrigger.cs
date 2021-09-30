using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
	public Text timer;
	public GameObject WinCanvas;
	public GameObject Player;

	// Stops the Timer script on the Player object and changes
	// the font color to green and size to 64 when entered.
    void OnTriggerEnter(Collider other)
	{
		other.GetComponent<Timer>().enabled = false;
		timer.enabled = false;
		WinCanvas.SetActive(true);
		Player.GetComponent<Timer>().Win();
	}
}
