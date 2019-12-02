using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_PaperGM : MonoBehaviour
{
    // Score personne courante
    [HideInInspector]
    public int peopleScore = 0;

    [HideInInspector]
    // Score de la scene
    public int score = 0;

    // Asset des mots
    public bool paragraphsConfirmed;
    public SC_PaperSnapGrid[] snapPositions;
    public SC_DragDropControls[] ddcontrols;
    //[HideInInspector]
    public List<SC_AutoComplete> acompletes;

    public bool DebugMode;

    private void Start()
    {
        snapPositions = FindObjectsOfType<SC_PaperSnapGrid>();
        ddcontrols = FindObjectsOfType<SC_DragDropControls>();
    }

    public void OnClickSubmitButton()
    {
        if (paragraphsConfirmed == true)
        {
            if (!DebugMode)
            {
                foreach (string elem in SC_GM.gm.choosenWordInLetter)
                    foreach (SC_ListWords listWord in SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals)
                        foreach (Word word in listWord.words)
                            foreach (string mot in word.critere)
                                if (elem == mot)
                                    score += word.score[peopleScore];
            }

            Debug.Log("Score = " + score);
            if (score > SC_GM.gm.firstPivotScene)
            {
                Debug.Log("Loaded first scene");
                SceneManager.LoadScene(SC_GM.gm.firstScene);
            }
            else if (SC_GM.gm.numberOfScene == 2)
            {
                Debug.Log("Loaded second scene");
                SceneManager.LoadScene(SC_GM.gm.secondScene);
            }

            else if (score > SC_GM.gm.secondPivotScene && SC_GM.gm.numberOfScene == 3)
            {
                Debug.Log("Loaded second scene");
                SceneManager.LoadScene(SC_GM.gm.secondScene);
            }
            else
            {
                Debug.Log("Loaded third scene");
                SceneManager.LoadScene(SC_GM.gm.thirdScene);
            }

        }
    }

    public void OnClickConfirmButton()
    {
        //Debug.Log("Blocked paragraphs placement");

        if (paragraphsConfirmed == false)
        {
            for (int i = 0; i < snapPositions.Length; i++)
                if (snapPositions[i].currentSnappedObject != null && !acompletes.Contains(snapPositions[i].currentSnappedObject.GetComponentInChildren<SC_AutoComplete>()))
                {
                    acompletes.Add(snapPositions[i].currentSnappedObject.GetComponentInChildren<SC_AutoComplete>());

                    for (int m = 0; m < acompletes.Count; m++)
                    {
                        acompletes[m].enabled = true;
                    }

                }

            for (int k = 0; k < ddcontrols.Length; k++)
                ddcontrols[k].enabled = false;

            if (ddcontrols.Length != 0)
                //Debug.Log("no paragraphs were placed");
                paragraphsConfirmed = true;
        }
        else
        {
            //Debug.Log("Unblocked paragraphs placement");

            for (int j = 0; j < acompletes.Count; j++)
                acompletes[j].enabled = false;

            for (int l = 0; l < ddcontrols.Length; l++)
                ddcontrols[l].enabled = true;

            acompletes.Clear();
            paragraphsConfirmed = false;
        }
    }

    public void DebugSubmit(int testScore)
    {
        DebugMode = true;
        paragraphsConfirmed = true;
        score = testScore;
        OnClickSubmitButton();
    }
}
