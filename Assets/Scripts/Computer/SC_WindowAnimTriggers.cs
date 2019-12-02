using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WindowAnimTriggers : MonoBehaviour
{
    public GameObject windowName;
    public GameObject windowLogo;
    public GameObject windowContent;

    // Start is called before the first frame update
    void Start()
    {
        windowLogo.SetActive(false);
    }


    // Functions for animation triggers
    public void DisplayWindowText()
    {
        windowName.SetActive(true);
    }

    public void HideWindowText()
    {
        windowName.SetActive(false);
    }

    public void DisplayWindowLogo()
    {
        windowLogo.SetActive(true);
    }

    public void HideWindowLogo()
    {
        windowLogo.SetActive(false);
    }

    public void DisplayWindowContent()
    {
        windowContent.SetActive(true);
    }

    public void HideWindowContent()
    {
        windowContent.SetActive(false);
    }
}
