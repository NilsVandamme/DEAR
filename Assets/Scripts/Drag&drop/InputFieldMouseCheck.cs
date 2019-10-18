using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This script disable the drag&drop if the mouse is over an inputfield

public class InputFieldMouseCheck : MonoBehaviour
{
    private DragDropControls DDC;

    // Start is called before the first frame update
    void Start()
    {
        DDC = GetComponentInParent<DragDropControls>();
    }

    public void OnEnter()
    {
        DDC.IsOnInputField = true;

        //Debug.Log("Mouse is over an input field");
    }

    public void OnExit()
    {
        DDC.IsOnInputField = false;

        // Deselect the inputfield if the mouse isn't on it
        //EventSystem.current.SetSelectedGameObject(null);

        //Debug.Log("Mouse is not over an input field anymore");
    }

}
