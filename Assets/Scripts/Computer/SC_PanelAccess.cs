using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PanelAccess : MonoBehaviour
{
    public GameObject WindowClients;
    public SC_WindowTopBar ClientsWtb;
    public GameObject WindowEmails;
    public SC_WindowTopBar EmailsWtb;
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

            if (ClientsWtb.IsOpen == false)
            {
                ClientsWtb.MaximizeWindow();
            }
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

            if (EmailsWtb.IsOpen == false)
            {
                ClientsWtb.MaximizeWindow();
            }
        }
    }

    public void OpenWindowTreeview()
    {
        if (WindowTreeview.activeSelf == false)
        {
            WindowTreeview.SetActive(true);
        }
    }

    public void CloseWindowTreeView()
    {
        if (WindowTreeview.activeSelf == true)
        {
            WindowTreeview.SetActive(false);
        }
    }
}
