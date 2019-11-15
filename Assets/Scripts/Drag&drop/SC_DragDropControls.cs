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

    [Space]
    public GameObject SnapPositionObjectOverTop;
    public GameObject SnapPositionObjectTop; // Which object Top is it snapped to ?
    public GameObject SnapPositionObjectDown; // Which object Down is it snapped to ?
    public GameObject SnapPositionObjectUnderDown;

    [Space]
    public bool overTopSnapped;
    public bool topSnapped;
    public bool downSnapped;
    public bool underDownSnapped;

    [Space]
    public bool snapMovementActive; // Is the object moving to it's snap ?

    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Rigidbody rig; // Object rigidbidy

    [Space]
    public float timer;

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

            // Disable the snap movement if the object is blocked too long
            timer += Time.deltaTime;
            if (timer > 1.5f)
            {
                timer = 0;
                snapMovementActive = false;
                rig.useGravity = true;
            }
        }
        // Unsnap the object from it's target
        else if(snapMovementActive == true && Vector3.Distance(transform.position, SnapPosition) <= 0.03)
        {
            timer = 0;
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
        timer = 0;

        if (enabled)
        {
            if (IsSelected == true)
            {
                //Debug.Log("MOUSE UP");

                // If the object has a snap point
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
                        else
                            SnapPosition = OriginalPosition;
                    }
                    else if (ParagraphSize == 2)
                    {
                        // Give the position to snap to
                        if (SnapPositionObjectTop != null && SnapPositionObjectDown != null) // Snap between the two spots
                            SnapPosition = Vector3.Lerp(SnapPositionObjectTop.transform.position, SnapPositionObjectDown.transform.position, 0.5f);
                        else if (topSnapped == true && SnapPositionObjectDown == null && overTopSnapped == true && SnapPositionObjectTop.tag != "FirstSnapPosition") // Snap on the upper part of the paper
                            SnapPosition = SnapPositionObjectTop.transform.position + Vector3.forward * 0.4f;
                        else if (downSnapped == true && SnapPositionObjectTop == null && underDownSnapped == true && SnapPositionObjectDown.tag != "LastSnapPosition") // Snap on the lower part of the paper
                            SnapPosition = SnapPositionObjectDown.transform.position + Vector3.back * 0.4f;
                        else if (topSnapped == true && SnapPositionObjectDown == null && underDownSnapped == false && SnapPositionObjectTop.tag == "FirstSnapPosition") // Return to original position because there's not enough space
                            SnapPosition = OriginalPosition;
                        else if (downSnapped == true && SnapPositionObjectTop == null && overTopSnapped == false && SnapPositionObjectDown.tag == "LastSnapPosition") // Return to original position because there's not enough space
                            SnapPosition = OriginalPosition;
                        else
                            SnapPosition = OriginalPosition;
                    }
                }
                // If the object has no snap point
                else if(SnapPositionObjectTop == null && SnapPositionObjectDown == null) 
                {
                    snapMovementActive = true;
                    IsSelected = false;

                    if (ParagraphSize == 1)
                    {
                        SnapPosition = OriginalPosition;
                    }
                    else if(ParagraphSize == 2)
                    {
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
                        if (topHit.transform.gameObject != SnapPositionObjectTop && ParagraphSize > 1)
                        {
                            //Debug.Log("Top raycast has found a free snap area");
                            SnapPositionObjectTop = topHit.transform.gameObject;
                            topSnapped = true;
                        }
                        else if(topHit.transform.gameObject != SnapPositionObjectTop && ParagraphSize == 1)
                        {
                            //Debug.Log("Top raycast has found a free snap area");
                            SnapPositionObjectTop = topHit.transform.gameObject;
                            topSnapped = true;
                        }
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
                        if(downHit.transform.gameObject != SnapPositionObjectTop && ParagraphSize > 1)
                        {
                            //Debug.Log("Down raycast has found a free snap area");
                            SnapPositionObjectDown = downHit.transform.gameObject;
                            downSnapped = true;
                        }
                        else if (downHit.transform.gameObject != SnapPositionObjectTop && ParagraphSize == 1)
                        {
                            //Debug.Log("Down raycast has found a free snap area");
                            SnapPositionObjectDown = downHit.transform.gameObject;
                            downSnapped = true;
                        }
                    }
                }
                else
                {
                    downSnapped = false;
                    SnapPositionObjectDown = null;
                }


                // Add more raycasts if the paragraph is larger than 1
                if(ParagraphSize > 1)
                {
                    Physics.Raycast(transform.GetChild(3).transform.position, Vector3.down, out RaycastHit overTopHit, 1000f);
                    Physics.Raycast(transform.GetChild(4).transform.position, Vector3.down, out RaycastHit underDownHit, 1000f);

                    Debug.DrawRay(transform.GetChild(3).transform.position, Vector3.down);
                    Debug.DrawRay(transform.GetChild(4).transform.position, Vector3.down);


                    // The over top raycast hit a snap area ?
                    if (overTopHit.transform.gameObject.layer == 8)
                    {
                        if (overTopHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false)
                        {
                            //Debug.Log("overTop raycast has found a free snap area");
                            SnapPositionObjectOverTop = overTopHit.transform.gameObject;
                            overTopSnapped = true;
                        }
                    }
                    else
                    {
                        overTopSnapped = false;
                        SnapPositionObjectOverTop = null;
                    }

                    // The under down raycast hit a snap area ?
                    if (underDownHit.transform.gameObject.layer == 8)
                    {
                        if (underDownHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false)
                        {
                            //Debug.Log("underDown raycast has found a free snap area");
                            SnapPositionObjectUnderDown = underDownHit.transform.gameObject;
                            underDownSnapped = true;
                        }
                    }
                    else
                    {
                        underDownSnapped = false;
                        SnapPositionObjectUnderDown = null;
                    }
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
