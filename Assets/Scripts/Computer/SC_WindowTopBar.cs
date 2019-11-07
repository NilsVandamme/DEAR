using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This script manage the function of the top bar of the cumputer windows

public class SC_WindowTopBar : MonoBehaviour, IDragHandler
{
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("moving " + gameObject.name + " window");
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out Vector2 localpoint);
        CalculateCameraOffset();
        transform.localPosition = localpoint + offset;
        Debug.Log("mousepos to world = " + localpoint);
    }

    private void CalculateCameraOffset()
    {

    }
}
