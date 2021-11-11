using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonPress : MonoBehaviour
{
    public VirtualButtonBehaviour[] buttons;
    public Material EmailMtr;
    // Start is called before the first frame update
    void Start()
    {
        foreach (VirtualButtonBehaviour button in buttons)
        {
            button.RegisterOnButtonPressed(OnButtonPressed);
            button.RegisterOnButtonReleased(OnButtonReleased);
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("BTN pressed");
        EmailMtr.EnableKeyword("_EMISSION");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("BTN released");
        EmailMtr.DisableKeyword("_EMISSION");
    }

    public void Click()
    {
        Debug.Log("Click");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
