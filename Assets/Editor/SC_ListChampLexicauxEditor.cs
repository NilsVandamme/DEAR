using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ListChampLexicaux))]
public class SC_ListChampLexicauxEditor : Editor
{
    private SC_ListChampLexicaux listChampLexicaux;

    private void OnEnable()
    {
        listChampLexicaux = target as SC_ListChampLexicaux;
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load Champ Lexical"))
            loadName();

        base.OnInspectorGUI();

    }

    /*
     * Calcul la liste des champs lexicaux dispo
     */
    private void loadName ()
    {
        listChampLexicaux.nameChampsLexicals = new string[listChampLexicaux.listChampsLexicals.Length];
        for (int i = 0; i < listChampLexicaux.listChampsLexicals.Length; i++)
            listChampLexicaux.nameChampsLexicals[i] = listChampLexicaux.listChampsLexicals[i].fichierWords.name;
    }
}
