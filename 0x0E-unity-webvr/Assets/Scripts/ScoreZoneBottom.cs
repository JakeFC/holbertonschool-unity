using UnityEngine;
using UnityEngine.UI;

public class ScoreZoneBottom : MonoBehaviour
{
    public Text scoreText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (other.GetComponent<Basketball>().canScore)
            {
                scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
            }
        }
    }
}
