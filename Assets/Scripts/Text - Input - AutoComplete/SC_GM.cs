using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_GM : MonoBehaviour
{
    // Nombre de CL récupérables
    [HideInInspector]
    public int numberOfCLRecover = 0;
    public int numberOfCLRecoverable;

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
