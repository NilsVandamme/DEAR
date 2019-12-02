using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct WordForEachCritere
{
    public int verb;
    public int noun;
    public int adjectif;
}

public class SC_GM : MonoBehaviour
{
    // Score personne courante
    [HideInInspector]
    public string[] persoCourant = new string[] { "Granny Donna", "Mr. S" };
    [HideInInspector]
    public int peopleScore;

    // Nombre de CL récupérables
    [HideInInspector]
    public int numberOfCLRecover = 0;
    public int numberOfCLRecoverable;

    // Choose X Word
    public int numberOfWordInWheel;
    public WordForEachCritere numberOfWordForEachCritere;
    public bool activeBonus;

    // Prochaines Scenes
    [HideInInspector]
    public int numberOfScene;
    [HideInInspector]
    public int firstPivotScene;
    [HideInInspector]
    public int secondPivotScene;
    [HideInInspector]
    public string firstScene;
    [HideInInspector]
    public string secondScene;
    [HideInInspector]
    public string thirdScene;

    [HideInInspector]
    // Liste des mots entre par le joueur
    public List<string> choosenWordInLetter;
    [HideInInspector]
    // Liste des mots choisis par le joueur
    public List<Word> wheelOfWords;

    public static SC_GM gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);

    }

}
