using System.Collections.Generic;
using UnityEngine;

public class SC_GM_Master : MonoBehaviour
{
    // Ensemble des champs lexicaux
    public SC_ListChampLexicaux listChampsLexicaux;

    [HideInInspector]
    // Liste des mots choisi par le joueur (CL, Word, Use)
    public List<SC_WordInPull> choosenWords = new List<SC_WordInPull>();

    public static SC_GM_Master gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);
    }
}
