using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This scirpt manages the functions used by the different tab buttons on the computer

public class ComputerWindowsSwitch : MonoBehaviour
{
    [Header("Tabs")]
    public GameObject EmailTab; // The email tab
    public GameObject InfosTab; // The infos tab

    [Header("Buttons")]
    public Button EmailButton;
    public Button InfosButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Open the mail tab on the computer, attached to the corresponding button
    public void OpenMail()
    {
        EmailTab.SetActive(true);
        InfosTab.SetActive(false);

        EmailButton.interactable = false;
        InfosButton.interactable = true;
    }

    // Open the character infos tab on the computer, attached to the corresponding button
    public void OpenInfos()
    {
        EmailTab.SetActive(false);
        InfosTab.SetActive(true);

        EmailButton.interactable = true;
        InfosButton.interactable = false;
    }
}
