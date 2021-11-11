using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonPress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void EmailClick()
    {
        Application.OpenURL("mailto:jacobchavera@yahoo.com");
    }

    public void GithubClick()
    {
        Application.OpenURL("Application.OpenURL");
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
