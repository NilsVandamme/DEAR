using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_GM : MonoBehaviour
{
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

    // Score personne courante
    private int peopleScore = 0;

    // Asset des mots
    public bool paragraphsConfirmed;
    public List<SC_PaperSnap> snapPositions;
    public List<SC_DragDropControls> ddcontrols;
    [HideInInspector]
    public List<SC_AutoComplete> acompletes;

    [HideInInspector]
    // Liste des mots entre par le joueur
    public List<string> choosenWordInLetter;

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
        if(paragraphsConfirmed == true)
        {
            foreach (string elem in choosenWordInLetter)
                foreach (SC_ListWords listWord in SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals)
                    foreach (Word word in listWord.words)
                        foreach (string mot in word.critere)
                            if (elem == mot)
                                score += word.score[peopleScore];

            if (score >= firstPivotScene)
                SceneManager.LoadScene(firstScene);
            else if (numberOfScene == 1)
                SceneManager.LoadScene(secondScene);
            else if (score >= secondPivotScene)
                SceneManager.LoadScene(secondScene);
            else
                SceneManager.LoadScene(thirdScene);
           
        }
    }

    public void OnClickConfirmButton()
    {
        //Debug.Log("Blocked paragraphs placement");

        if(paragraphsConfirmed == false)
        {
            Debug.Log("snapPositions.Count = " + snapPositions.Count);
            for (int i = 0; i < snapPositions.Count; i++)
                if (snapPositions[i].SnappedObject != null && !acompletes.Contains(snapPositions[i].SnappedObject.GetComponentInChildren<SC_AutoComplete>()))
                {
                    acompletes.Add(snapPositions[i].SnappedObject.GetComponentInChildren<SC_AutoComplete>());
                    Debug.Log("value of i = " + i);

                    for(int m = 0; m < acompletes.Count; m++)
                    {
                        acompletes[m].enabled = true;
                    }

                }

            for(int k = 0; k < ddcontrols.Count; k++)
                ddcontrols[k].enabled = false;

            if (ddcontrols.Count != 0)
                Debug.Log("no paragraphs were placed");
                paragraphsConfirmed = true;
        }
        else
        {
            //Debug.Log("Unblocked paragraphs placement");

            for (int j = 0; j < acompletes.Count; j++)
                acompletes[j].enabled = false;

            for (int l = 0; l < ddcontrols.Count; l++)
                ddcontrols[l].enabled = true;

            acompletes.Clear();
            paragraphsConfirmed = false;
        }
    }
}
