using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_Mot : MonoBehaviour
{
    private bool click = true;

    public void OnClick()
    {
        if (click)
        {
            click = false;
            //GM_1er_Mail.GM_1_Mail.GetMot(this.GetComponentInChildren<Text>().text);
        }
    }
}
