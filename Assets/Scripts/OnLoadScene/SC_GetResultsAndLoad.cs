using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script analyse the results and load the scene accordingly

public class SC_GetResultsAndLoad : MonoBehaviour
{
    public float NeededScore; // The score you need to get to the top ending

    public string DownScene;
    public string TopScene;

    private SC_LoadingScreen ls;

    private void Start()
    {
        ls = FindObjectOfType<SC_LoadingScreen>();
    }

    /*
    public void AnalyseResults()
    {
        
        if (ScoreBD.Total >= NeededScore)
        {
            ls.LoadedScene = TopScene;
            Debug.Log("loaded scene : " + TopScene);
            ls.LoadThisScene();
        }
        else
        {
            ls.LoadedScene = DownScene;
            Debug.Log("loaded scene : " + DownScene);
            ls.LoadThisScene();
        }

    }
    */

}
