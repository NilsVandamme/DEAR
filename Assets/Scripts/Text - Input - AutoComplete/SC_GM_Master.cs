using System.Collections.Generic;
using UnityEngine;

public class SC_GM_Master : MonoBehaviour
{
    public SC_ListChampLexicaux listChampsLexicaux;

    [HideInInspector]
    public string[] nameChampsLexicals;

    public static SC_GM_Master gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);
    }
}
