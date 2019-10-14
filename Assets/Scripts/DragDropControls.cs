﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script make the object attached to drag & drop-able

public class DragDropControls : MonoBehaviour
{
    [Header("System values")]

    public float HoveringHeight = -5.25f; // Height at which the object will hover while being dragged
    public float SpeedDivider = 2.5f; // Divide the drag speed
    public float SnapSpeed; // Speed of the snapping movement

    [Header("Debug")]

    public bool IsSnapping; // Is the object being snapped ?
    public Vector3 SnapPosition; // Position at which the object snaps
    public bool snapMovementActive; // Is the object moving to it's snap position ?

    private Vector3 mouseOffset; 
    private float mouseZCoord;
    private Rigidbody rig; // Object rigidbidy

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Snap the object to it's target
        if (snapMovementActive == true && Vector3.Distance(transform.position, SnapPosition) >= 0.03)
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
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseZCoord = mouseZCoord / SpeedDivider;

        mouseOffset = gameObject.transform.position - GetMouseWorldPos();

        rig.useGravity = false;
    }

    // When the mouse is being released
    private void OnMouseUp()
    {
        if(IsSnapping == true)
        {
            snapMovementActive = true;
        }
        else
        {
            rig.useGravity = true;
            snapMovementActive = false;
        }
    }


    // When the mouse is kept being pressed
    private void OnMouseDrag()
    {
        rig.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, Mathf.Lerp(transform.position.y, HoveringHeight, Time.deltaTime * 2), GetMouseWorldPos().z + mouseOffset.z );
    }


    // System
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}