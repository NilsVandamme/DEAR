using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_Mot : MonoBehaviour
{
    public void OnClick()
    {
        GM_1er_Mail.GM_1_Mail.GetMot(this.GetComponentInChildren<Text>().text);
    }
}
