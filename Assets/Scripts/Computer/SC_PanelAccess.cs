using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PanelAccess : MonoBehaviour
{
    public GameObject WindowClients;
    public GameObject WindowEmails;
    public GameObject WindowTreeview;

    public void OpenWindowInfos()
    {
        if (WindowClients.activeSelf == false)
        {
            WindowClients.SetActive(true);
            WindowClients.transform.SetAsLastSibling();
        }
        else
        {
            WindowClients.transform.SetAsLastSibling();
        }
    }

    public void OpenWindowEmails()
    {
        if (WindowEmails.activeSelf == false)
        {
            WindowEmails.SetActive(true);
            WindowEmails.transform.SetAsLastSibling();
        }
        else
        {
            WindowEmails.transform.SetAsLastSibling();
        }
    }

    public void OpenWindowTreeview()
    {
        if (WindowTreeview.activeSelf == false)
        {
            WindowTreeview.SetActive(true);
        }
    }
}
