using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationChain : MonoBehaviour
{
    public GameObject nextObject;

    // Enables the next object in the sequence, beginning its animation
    void StartAnimation()
    {
        nextObject.SetActive(true);
        Debug.Log("Next");
    }
}
