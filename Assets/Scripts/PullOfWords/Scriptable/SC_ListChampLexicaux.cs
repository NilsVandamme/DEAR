using UnityEngine;

[CreateAssetMenu(fileName = "ListChampLexicaux.asset", menuName = "Custom/GenerateListChampLexicaux", order = 1)]
public class SC_ListChampLexicaux : ScriptableObject
{
    public SC_ListWords[] listChampsLexicals;

    [HideInInspector]
    public string[] nameChampsLexicals;

}
