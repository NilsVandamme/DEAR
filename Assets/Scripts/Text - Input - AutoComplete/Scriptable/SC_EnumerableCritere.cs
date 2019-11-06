using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Enum
{
    public string key;
    public string[] value;
}

[CreateAssetMenu(fileName = "Critere.asset", menuName = "Custom/GenerateCritere", order = 1)]
public class SC_EnumerableCritere : ScriptableObject
{
    public TextAsset fichierCritere;
    public Enum myEnum;
    public static Enum myStaticEnum;
}