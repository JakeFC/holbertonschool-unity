using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Text timer;
	private float time = 0f;

    // Keeps timer text updated with formatted current time.
    void Update()
    {
		time += Time.deltaTime;
		float minutes = Mathf.FloorToInt(time / 60);
		float seconds = Mathf.FloorToInt(time % 60);
		float centiseconds = (time % 1) * 100;

		timer.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, centiseconds);
	}
}
