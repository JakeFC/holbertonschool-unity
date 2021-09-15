using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
	public Text timer;

	// Stops the Timer script on the Player object and changes
	// the font color to green and size to 64.
    void OnTriggerEnter(Collider other)
	{
		other.GetComponent<Timer>().enabled = false;
		timer.color = Color.green;
		timer.fontSize = 64;
	}
}
