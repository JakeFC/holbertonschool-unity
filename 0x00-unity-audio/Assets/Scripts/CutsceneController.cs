using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
	public GameObject player;
	public GameObject MainCamera;
	public GameObject TimerCanvas;
    
	// Enables player movement and timer start when called.
	void EndCutscene()
	{
		player.GetComponent<PlayerController>().enabled = true;
		MainCamera.SetActive(true);
		TimerCanvas.SetActive(true);
		transform.gameObject.SetActive(false);
	}
}
