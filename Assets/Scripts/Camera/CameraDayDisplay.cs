using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CameraDayDisplay : MonoBehaviour
{

    public string Day1;
    public TMP_Text DayText;

    void Start()
    {
        
    }

    public void DisplayDayText()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "Scene_Intro_Prez1")
        {
            DayText.text = "Day 1\nFirst day at work";

        }

        if (scene.name == "Scene_Bad_Prez" || scene.name == "Scene_Good_Prez")
        {
            DayText.text = "Day 2\nThe Speech";
        }
        else
        {
            DayText.text = "Day 65\nShit is going down";
        }
    }
  
}
