using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SC_CameraDayDisplay : MonoBehaviour
{

    public string Day1;
    public TMP_Text DayText;

    void Start()
    {
        
    }

    public void DisplayDayText()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "L_A1")
        {
            DayText.text = "Day 1\nFirst day at work";

        }

        if (scene.name == "L_B1")
        {
            DayText.text = "Day 2\nThe Speech";
        }

        if (scene.name == "L_B2")
        {
            DayText.text = "Day 2\nThe Speech";
        }
    }
  
}
