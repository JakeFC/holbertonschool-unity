using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSelection : MonoBehaviour
{
	private static Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

	// Deletes all other planes when one is selected, as well as this script
	// component on the selected plane.
	void OnMouseDown()
    {
		// Creates an array of ARPlane mesh renderers.
        Renderer[] renderers = (Renderer[]) Object.FindObjectsOfType(typeof(Renderer));

		foreach(Renderer rd in renderers)
		{
			if (rd != _renderer)
				Destroy(rd.gameObject);
		}
		// Start spawning targets.
		_renderer.GetComponent<TargetSpawning>().enabled = true;

		// Enable the start button.
		GameObject.FindWithTag("Start Button").SetActive(true);

		// Disable this script.
		Destroy(_renderer.GetComponent<PlaneSelection>());
    }
}
