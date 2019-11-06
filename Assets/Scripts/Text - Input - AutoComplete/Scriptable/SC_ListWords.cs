using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Word
{
    public int[] score;
    public string titre;
    public string[] name, critere;
}

[CreateAssetMenu(fileName = "ChampLexical.asset", menuName = "Custom/GenerateChampLexical", order = 1)]
public class SC_ListWords : ScriptableObject
{
    public TextAsset fichierWords;
    public List<Word> words;
}
