using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ConfirmParagraphHighlight : MonoBehaviour
{
    public static SC_ConfirmParagraphHighlight instance;

    public Image img;
    private bool highlighted;
    private bool allSnapped;

    public SC_PaperSnapGrid[] snapList;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        img = GetComponent<Image>();
        snapList = FindObjectsOfType<SC_PaperSnapGrid>();
    }

    public void ChangeColor( bool high)
    {
        highlighted = high;
        if (highlighted)
        {
            img.color = Color.red;
        }
        else
        {
            img.color = Color.white;
        }

    }

    public void HighlightColor()
    {
        allSnapped = true;
        Debug.Log("allspannper 1 = " + allSnapped);
        for (int i = 0; i < snapList.Length; i++)
        {
            if (!snapList[i].hasSnappedObject)
                allSnapped = false;
        }

        Debug.Log("allspannper 2 = " + allSnapped);
        if(allSnapped == true)
        {
            img.color = Color.green;
        }

    }
}
