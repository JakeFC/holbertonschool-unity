using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		// Rotates the object on the x-axis over time.
        transform.Rotate(45 * Time.deltaTime, 0 , 0);
    }
}
