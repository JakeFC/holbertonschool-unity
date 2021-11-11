using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonPress : MonoBehaviour
{
    public GameObject[] objects;

    public void SpawnObjects()
    {
        objects[0].SetActive(true);
    }

    public void DestroyObjects()
    {
        foreach (GameObject obj in objects)
            obj.SetActive(false);
    }
    public void EmailClick()
    {
        Application.OpenURL("mailto:jacobchavera@yahoo.com");
    }

    public void GithubClick()
    {
        Application.OpenURL("https://github.com/JakeFC");
    }

    public void LinkedInClick()
    {
        Application.OpenURL("https://www.linkedin.com/in/jacob-chavera-929982205/");
    }

    public void MediumClick()
    {
        Application.OpenURL("https://medium.com/@2919");
    }
}
