using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script make the paragraphs snap on the paper when drag & dropped

public class PaperSnap : MonoBehaviour
{
    public bool HasSnap; // Is an object currently snapped to this object ?
    public GameObject SnappedObject;

    // Snap the object
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.name + " has collided with " + other.name);
        if (other.tag == "Paragraph" && SnappedObject == null)
        {
            //Debug.Log(gameObject.name + " has snapped with " + other.name);
            SnappedObject = other.gameObject;
            other.GetComponent<DragDropControls>().IsSnapping = true;
            other.GetComponent<DragDropControls>().SnapPosition = transform.position;
            HasSnap = true;
        }

    }

    // Unsnap the object
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(gameObject.name + " has ended collision with " + other.name);
        if (other.tag == "Paragraph" && other.name == SnappedObject.name)
        {
            //Debug.Log(gameObject.name + " has unsnapped with " + other.name);
            SnappedObject = null;
            other.GetComponent<DragDropControls>().IsSnapping = false;
            other.GetComponent<DragDropControls>().snapMovementActive = false;
            HasSnap = false;
        }

    }
}
