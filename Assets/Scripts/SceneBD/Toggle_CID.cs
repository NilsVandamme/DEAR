using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Toggle_CID : MonoBehaviour
{
    public void OnClick()
    {
        GM_SceneBD.GM_BD.CID(this.name);
    }
}
