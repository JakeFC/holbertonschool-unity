using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSelection : MonoBehaviour
{
	private Renderer _renderer;

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
		_renderer.GetComponent<TargetSpawning>().enabled = true;
		Destroy(_renderer.GetComponent<PlaneSelection>());
    }
}
