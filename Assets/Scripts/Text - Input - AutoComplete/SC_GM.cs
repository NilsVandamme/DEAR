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
    public SC_ListWords listMots;
    public bool paragraphsConfirmed;
    public List<SC_PaperSnap> snapPositions;
    public List<SC_DragDropControls> ddcontrols;
    [HideInInspector]
    public List<SC_AutoComplete> acompletes;

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
        if(paragraphsConfirmed == true)
        {
            foreach (string elem in tabInputStrings)
                foreach (Word word in listMots.words)
                    foreach (string mot in word.critere)
                        if (elem == mot)
                            score += word.score[peopleScore];

            if (score >= pivotScene)
                SceneManager.LoadScene(goodScene);
            else
                SceneManager.LoadScene(badScene);
        }
    }

    public void OnClickConfirmButton()
    {
        //Debug.Log("Blocked paragraphs placement");

        if(paragraphsConfirmed == false)
        {
            for (int i = 0; i < snapPositions.Count; i++)
                if (snapPositions[i].SnappedObject != null && !acompletes.Contains(snapPositions[i].SnappedObject.GetComponentInChildren<SC_AutoComplete>()))
                {
                    acompletes.Add(snapPositions[i].SnappedObject.GetComponentInChildren<SC_AutoComplete>());
                    acompletes[i].enabled = true;
                }

            for(int k = 0; k < ddcontrols.Count; k++)
                ddcontrols[k].enabled = false;

            if(ddcontrols.Count != 0)
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
