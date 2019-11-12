﻿using UnityEngine;

[CreateAssetMenu(fileName = "OrdiParagraphe.asset", menuName = "Custom/GenerateOrdiParagraphe", order = 1)]
public class SC_ParagrapheOrdi : ScriptableObject
{
    public SC_ListChampLexicaux listChampLexicaux;
    public string nameChampLexical;
    public bool[] motAccepter;

}
