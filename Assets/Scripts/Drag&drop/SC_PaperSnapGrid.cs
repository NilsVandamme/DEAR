using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PaperSnapGrid : MonoBehaviour
{
    public bool HasSnap; // Is an object currently snapped to this object ?
    public GameObject SnappedObject;

    private SC_DragDropControls otherDD;

    private void Start()
    {
        //GetComponent<MeshRenderer>().enabled = false; // Disable the renderer on start
    }

    // Snap the object
    private void OnTriggerEnter(Collider other)
    {
        otherDD = other.GetComponent<SC_DragDropControls>();

        Debug.Log(other.name + " has entered collision with " + gameObject.name);


        if (other.tag == "Paragraph" && SnappedObject == null) // Snap the object if there's the emplacement is free
        {
            Debug.Log(gameObject.name + " has snapped with " + other.name);
            SnappedObject = other.gameObject;
            otherDD.IsSnapping = true;
            otherDD.SnapPositionObject = gameObject;
            otherDD.SnapPosition = transform.position;
            HasSnap = true;
        }
        else if (other.tag == "Paragraph" && other != SnappedObject)  // Prevent another object from snapping if there's already one
        {
            if (SnappedObject != null)
                Debug.Log(gameObject.name + " already has an object snapped : " + SnappedObject.name);
            else
                Debug.Log(gameObject.name + "has no snapped object");
        }

    }

    // Unsnap the object
    private void OnTriggerExit(Collider other)
    {
        otherDD = other.GetComponent<SC_DragDropControls>();
        Debug.Log(other.name + " has exited collision with " + gameObject.name);
        //Debug.Log(gameObject.name + " ; " + other.name + " is Snapped To " + (otherDD.SnapPositionObject));

        if (otherDD.SnapPositionObject == gameObject && other != SnappedObject) // Unsnap the object currently snapped
        {
            Debug.Log(gameObject.name + " has unsnapped with " + other.name);
            otherDD.IsSnapping = false;
            otherDD.SnapPositionObject = null;
            otherDD.snapMovementActive = false;
            SnappedObject = null;
            HasSnap = false;
        }
        else if (otherDD.SnapPositionObject != gameObject && otherDD.IsSnapping == true && other == SnappedObject) // Unsnap the object currently snapped and transfer snap to another point
        {
            Debug.Log(gameObject.name + " has unsnapped with " + other.name + " and transfered snap");
            otherDD.IsSnapping = true;
            SnappedObject = null;
            HasSnap = false;
        }
        else
        {
            Debug.Log(gameObject.name + " nothing happenned on collision exit");
        }

    }
}
