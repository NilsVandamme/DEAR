using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ConfirmParagraphHighlight : MonoBehaviour
{
    public static SC_ConfirmParagraphHighlight instance;

    public Image img;
    public bool highlighted;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        img = GetComponent<Image>();
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
}
