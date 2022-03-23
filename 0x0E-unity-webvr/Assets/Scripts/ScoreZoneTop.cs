using UnityEngine;

public class ScoreZoneTop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            other.GetComponent<Basketball>().canScore = true;
        }
    }
}
