using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script make the object attached to drag & drop-able

public class SC_DragDropControls : MonoBehaviour
{
    [Header("System values")]

    public float HoveringHeight = -5.25f; // Height at which the object will hover while being dragged
    public float SpeedDivider = 2.5f; // Divide the drag speed
    public float SnapSpeed; // Speed of the snapping movement
    public int NumberOfLines; // The number of lines the paragraph contains

    [Header("Debug")]

    public bool IsSnapping; // Is the object being snapped ?
    public bool IsSelected; // Is the object being selected ?
    public bool IsOnInputField; // Is the cursor on the input field ?
    public Vector3 OriginalPosition;
    public Vector3 SnapPosition; // Position at which the object snaps
    public GameObject SnapPositionObject; // WHich ibject is it snapped to ?
    public bool snapMovementActive; // Is the object moving to it's snap ?

    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Rigidbody rig; // Object rigidbidy

    private void Start()
    {
        OriginalPosition = transform.position;
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Snap the object to it's target
        if (snapMovementActive == true && Vector3.Distance(transform.position, SnapPosition) > 0.03)
        {
            rig.position = Vector3.Lerp(rig.position, SnapPosition, SnapSpeed*Time.deltaTime);
        }
        // Unsnap the object from it's target
        else if(snapMovementActive == true && Vector3.Distance(transform.position, SnapPosition) <= 0.03)
        {
            snapMovementActive = false;
            rig.useGravity = true;
        }
    }

    // When the mouse is being pressed
    private void OnMouseDown()
    {
        if (enabled)
        {
            if (IsOnInputField == false) // Disable if the mouse is over an inputfield, check script "InputFieldMouseCheck" for further details
            {
                IsSelected = true;
                //Debug.Log("MOUSE DOWN");
                mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mouseZCoord /= SpeedDivider;

                mouseOffset = gameObject.transform.position - GetMouseWorldPos();
                if (rig != null)
                    rig.useGravity = false;
            }
        }
    }

    // When the mouse is being released
    private void OnMouseUp()
    {
        if (enabled)
        {
            if (IsSelected == true)
            {
                //Debug.Log("MOUSE UP");
                if (IsSnapping == true)
                {
                    snapMovementActive = true;
                    IsSelected = false;
                }
                else
                {
                    if (rig != null)
                        rig.useGravity = true;
                    snapMovementActive = false;
                    IsSelected = false;
                }
            }
        }
    }


    // When the mouse is kept being pressed
    private void OnMouseDrag()
    {
        if (enabled)
        {
            if (IsSelected == true)
            {
                //Debug.Log("MOUSE DRAG");
                if (rig != null)
                    rig.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, Mathf.Lerp(transform.position.y, HoveringHeight, Time.deltaTime * 2), GetMouseWorldPos().z + mouseOffset.z);
            }
        }
    }


    // System
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
