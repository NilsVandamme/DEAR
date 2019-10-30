using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Word
{
    public int[] score;
    public string mot, champLexical;
    public string[] name;
}

[CreateAssetMenu(fileName = "BD.asset", menuName = "Custom/GenerateBD", order = 1)]
public class ListWords : ScriptableObject
{
    public TextAsset fichierWords;
    public List<Word> words;
}
