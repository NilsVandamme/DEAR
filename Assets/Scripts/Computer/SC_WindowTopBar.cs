using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This script manage the function of the top bar of the cumputer windows

public class SC_WindowTopBar : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Vector2 offset;
    public GameObject windowContent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("being moving " + gameObject.name);

        // position of the mouse
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out Vector2 localpoint);

        offset = new Vector2(transform.parent.localPosition.x, transform.parent.localPosition.y) - localpoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("moving " + gameObject.name + " window");

        // position of the mouse
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out Vector2 localpoint);

        // move the window on the x axis
        if(localpoint.x < 363 && localpoint.x > -363)
        {
            // Move the window
            transform.parent.localPosition = new Vector3(localpoint.x + offset.x, transform.parent.localPosition.y, transform.parent.localPosition.z);
        }

        // move the window on the y axis
        if (localpoint.y < 207 && localpoint.y > -207)
        {
            transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, localpoint.y + offset.y, transform.parent.localPosition.z);
        }

    }

    // Top bar buttons functions

    // Close button
    public void CloseWindow()
    {
        transform.parent.gameObject.SetActive(false);
    }

    // Maximize button
    public void MaximizeWindow()
    {
        windowContent.SetActive(true);
    }

    // Minimize button
    public void MinimizeWindow()
    {
        windowContent.SetActive(false);
    }

}
