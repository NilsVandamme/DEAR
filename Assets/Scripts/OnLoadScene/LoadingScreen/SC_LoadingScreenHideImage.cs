using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_LoadingScreenHideImage : MonoBehaviour
{
    private Image loadImage;

    // Start is called before the first frame update
    void Start()
    {
        loadImage = GetComponent<Image>();
    }

    public void Hide()
    {
        loadImage.enabled = false;
    }
}
