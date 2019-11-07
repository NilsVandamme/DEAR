using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This script manage the function of the top bar of the cumputer windows

public class SC_WindowTopBar : MonoBehaviour, IDragHandler
{

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("clicked " + gameObject.name + " window");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("moving " + gameObject.name + " window");
        Debug.Log("mousepos to world = " + Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6f)));
        transform.parent.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6f)) ;
    }
}
