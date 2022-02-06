using UnityEngine;
using UnityEngine.XR.ARFoundation;

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
		_renderer.material.color = Color.green;
		// Creates an array of ARPlane mesh renderers.
        Renderer[] renderers = (Renderer[]) Object.FindObjectsOfType(typeof(Renderer));

		foreach(Renderer rd in renderers)
		{
			if (rd.material.color != Color.green)
				Destroy(rd.gameObject);
		}
		// Ends search for new planes.
		GameObject.FindWithTag("Origin").GetComponent<ARPlaneManager>().enabled = false;

		// Start spawning targets.
		_renderer.GetComponent<TargetSpawning>().enabled = true;

		// Enable the start button through its active parent.
		GameObject.FindWithTag("Start Button").transform.GetChild(0).gameObject.SetActive(true);

		// Destroy this script.
		Destroy(_renderer.GetComponent<PlaneSelection>());
    }
}
