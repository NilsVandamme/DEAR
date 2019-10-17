using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    public void OnClick()
    {
        GM_SceneBD.GM_BD.Stop();
    }
}
