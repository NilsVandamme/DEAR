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
    public List<SC_PaperSnapGrid> snapPositions;
    public SC_DragDropControls[] ddcontrols;
    [HideInInspector]
    public List<SC_AutoComplete> acompletes;

    private void Start()
    {
        ddcontrols = FindObjectsOfType<SC_DragDropControls>();
    }

    public void OnClickSubmitButton()
    {
        if (paragraphsConfirmed == true)
        {
            foreach (string elem in SC_GM.gm.choosenWordInLetter)
                foreach (SC_ListWords listWord in SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals)
                    foreach (Word word in listWord.words)
                        foreach (string mot in word.critere)
                            if (elem == mot)
                                score += word.score[peopleScore];

            if (score >= SC_GM.gm.firstPivotScene)
                SceneManager.LoadScene(SC_GM.gm.firstScene);
            else if (SC_GM.gm.numberOfScene == 1)
                SceneManager.LoadScene(SC_GM.gm.secondScene);
            else if (score >= SC_GM.gm.secondPivotScene)
                SceneManager.LoadScene(SC_GM.gm.secondScene);
            else
                SceneManager.LoadScene(SC_GM.gm.thirdScene);

        }
    }

    public void OnClickConfirmButton()
    {
        //Debug.Log("Blocked paragraphs placement");

        if (paragraphsConfirmed == false)
        {
            for (int i = 0; i < snapPositions.Count; i++)
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
}
