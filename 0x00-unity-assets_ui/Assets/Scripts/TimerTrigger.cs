using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
	// Starts the Timer script of the Player object when this object is exited.
    void OnTriggerExit(Collider other)
	{
		other.GetComponent<Timer>().enabled = true;
	}
}
