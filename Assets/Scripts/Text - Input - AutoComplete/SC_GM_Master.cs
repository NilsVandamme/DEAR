using System.Collections.Generic;
using UnityEngine;

public class SC_GM_Master : MonoBehaviour
{
    public SC_ListWords[] listChampsLexicals;

    [HideInInspector]
    public List<string> nameChampsLexicals;

    public static SC_GM_Master gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (SC_ListWords elem in listChampsLexicals)
            nameChampsLexicals.Add(elem.fichierWords.name);
    }
}
