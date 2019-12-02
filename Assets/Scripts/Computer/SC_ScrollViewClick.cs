using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SC_ScrollViewClick : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent clickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        clickEvent.Invoke();
    }
}
