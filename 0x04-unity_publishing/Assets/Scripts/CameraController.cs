using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Player's GameObject and the offset vector are both
	// assigned in the inspector.
	public GameObject player;
	public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
		// Camera's position will always be the player's position,
		// plus offset
        transform.position = player.GetComponent<Transform>().position
		+ offset;
    }
}
