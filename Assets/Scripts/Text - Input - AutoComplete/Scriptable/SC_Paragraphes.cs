using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TextPart
{
    public string partText;
}

[CreateAssetMenu(fileName = "Paragraphe.asset", menuName = "Custom/GenerateParagraphe", order = 1)]
public class SC_Paragraphes : ScriptableObject
{
    public TextAsset fichierTexte;
    public List<TextPart> texte;
}
