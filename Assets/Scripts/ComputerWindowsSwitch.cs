using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This scirpt manages the functions used by the different tab buttons on the computer

public class ComputerWindowsSwitch : MonoBehaviour
{
    [Header("Tabs")]
    public GameObject EmailTab; // The email tab
    public GameObject InfosTab; // The infos tab

    [Header("Subtabs")]
    public GameObject[] ListeEmailsTabs;
    public GameObject[] ListeInfosTabs;

    [Header("Buttons")]
    public Button EmailButton;
    public Button InfosButton;

    [Header("Subtabs Buttons")]
    public Button[] ListeEmailsButtons;
    public Button[] ListeInfosButtons;

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

    public void OpenMailTabs()
    {
        for(int i = 0; i < ListeEmailsTabs.Length; i++)
        {
            ListeEmailsTabs[i].SetActive(false);
            ListeEmailsButtons[i].interactable = true;
            if(EventSystem.current.currentSelectedGameObject != null)
            {
                if (EventSystem.current.currentSelectedGameObject.name == ListeEmailsButtons[i].name)
                {
                    //Debug.Log("found the right button");
                    ListeEmailsTabs[i].SetActive(true);
                    ListeEmailsButtons[i].interactable = false;
                }
            }

        }

    }

    public void OpenInfosTabs()
    {
        for (int i = 0; i < ListeInfosTabs.Length; i++)
        {
            ListeInfosTabs[i].SetActive(false);
            ListeInfosButtons[i].interactable = true;
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (EventSystem.current.currentSelectedGameObject.name == ListeInfosButtons[i].name)
                {
                    //Debug.Log("found the right button");
                    ListeInfosTabs[i].SetActive(true);
                    ListeInfosButtons[i].interactable = false;
                }
            }

        }
    }
}
