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
    public int ParagraphSize; // The number of lines the paragraph contains

    [Header("Debug")]

    public bool IsSnapping; // Is the object being snapped ?
    public bool IsSelected; // Is the object being selected ?
    //public bool IsOnInputField; // Is the cursor on the input field ?
    public Vector3 OriginalPosition;
    public Vector3 SnapPosition; // Position at which the object snaps
    public GameObject SnapPositionObject; // Which object is it snapped to 

    public GameObject SnapPositionObjectTop; // Which object Top is it snapped to ?
    public GameObject SnapPositionObjectDown; // Which object Down is it snapped to ?

    public bool topSnapped;
    public bool downSnapped;

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
            //Debug.Log("aligning");
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
                IsSelected = true;
                //Debug.Log("MOUSE DOWN");
                mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mouseZCoord /= SpeedDivider;

                mouseOffset = gameObject.transform.position - GetMouseWorldPos();
                if (rig != null)
                    rig.useGravity = false;
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
                if ((topSnapped == true && downSnapped == true) || (topSnapped == true && SnapPositionObjectDown == null) || (downSnapped == true && SnapPositionObjectTop == null))
                {
                    snapMovementActive = true;
                    IsSelected = false;

                    // Snap the paragraph to the right position if it's on the top or bottom position depending on it's size
                    if (ParagraphSize == 1)
                    {
                        // Give the position to snap to
                        if (SnapPositionObjectTop != null)
                            SnapPosition = SnapPositionObjectTop.transform.position;
                        else if (SnapPositionObjectDown != null)
                            SnapPosition = SnapPositionObjectDown.transform.position;
                    }
                    else if (ParagraphSize == 2)
                    {
                        // Give the position to snap to
                        if (SnapPositionObjectTop != null && SnapPositionObjectDown != null) // Snap between the two spots
                            SnapPosition = Vector3.Lerp(SnapPositionObjectTop.transform.position, SnapPositionObjectDown.transform.position, 0.5f);
                        else if (topSnapped == true && SnapPositionObjectDown == null && SnapPositionObjectTop.GetComponent<SC_PaperSnapGrid>().tag != "FirstSnapPosition") // Snap on the upper part of the paper
                            SnapPosition = SnapPositionObjectTop.transform.position + Vector3.forward * 0.4f;
                        else if (downSnapped == true && SnapPositionObjectTop == null && SnapPositionObjectDown.GetComponent<SC_PaperSnapGrid>().tag != "LastSnapPosition") // Snap on the lower part of the paper
                            SnapPosition = SnapPositionObjectDown.transform.position + Vector3.back * 0.4f;
                        else if (topSnapped == true && SnapPositionObjectDown == null && SnapPositionObjectTop.GetComponent<SC_PaperSnapGrid>().tag == "FirstSnapPosition")
                            SnapPosition = OriginalPosition;
                        else if (downSnapped == true && SnapPositionObjectTop == null && SnapPositionObjectDown.GetComponent<SC_PaperSnapGrid>().tag == "LastSnapPosition")
                            SnapPosition = OriginalPosition;
                    }
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
                // Move the object to mouse position
                if (rig != null)
                    rig.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, Mathf.Lerp(transform.position.y, HoveringHeight, Time.deltaTime * 2), GetMouseWorldPos().z + mouseOffset.z);


                Physics.Raycast(transform.GetChild(1).transform.position, Vector3.down, out RaycastHit topHit, 1000f);
                Physics.Raycast(transform.GetChild(2).transform.position, Vector3.down,out RaycastHit downHit, 1000f);

                Debug.DrawRay(transform.GetChild(1).transform.position, Vector3.down);
                Debug.DrawRay(transform.GetChild(2).transform.position, Vector3.down);

                // The top raycast hit a snap area ?
                if(topHit.transform.gameObject.layer == 8)
                {
                    if (topHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false)
                    {
                        //Debug.Log("Top raycast has found a free snap area");
                        SnapPositionObjectTop = topHit.transform.gameObject;
                        topSnapped = true;
                    }
                }
                else
                {
                    topSnapped = false;
                    SnapPositionObjectTop = null;
                }

                // The down raycast hit a snap area ?
                if (downHit.transform.gameObject.layer == 8)
                {
                    if (downHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false)
                    {
                        //Debug.Log("Down raycast has found a free snap area");
                        SnapPositionObjectDown = downHit.transform.gameObject;
                        downSnapped = true;
                    }
                }
                else
                {
                    downSnapped = false;
                    SnapPositionObjectDown = null;
                }
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
