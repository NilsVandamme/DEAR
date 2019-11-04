using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_GM : MonoBehaviour
{
    // Prochaines Scenes
    public int pivotScene;
    public string goodScene;
    public string badScene;


    // Score personne courante
    private int peopleScore = 0;

    // Asset des mots
    public SC_ListWords bd;

    [HideInInspector]
    // Liste des mots entre par le joueur
    public List<string> tabInputStrings;

    [HideInInspector]
    // Score de la scene
    public int score = 0;

    public static SC_GM gm = null;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);
    }

    public void OnClickSubmitButton()
    {
        Debug.Log("cc");
        foreach (string elem in tabInputStrings)
        {
            Debug.Log(elem);
            foreach (Word word in bd.words)
                if (elem == word.mot)
                {
                    score += word.score[peopleScore];
                    break;
                }
        }

        if (score >= pivotScene)
            SceneManager.LoadScene(goodScene);
        else
            SceneManager.LoadScene(badScene);
    }
}
