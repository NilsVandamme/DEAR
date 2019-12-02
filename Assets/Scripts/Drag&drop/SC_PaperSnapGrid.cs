using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PaperSnapGrid : MonoBehaviour
{
    public bool hasSnappedObject;
    public GameObject currentSnappedObject;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name + " has entered on " + gameObject.name);
        if (other.tag == "Paragraph")
        {
            //Debug.Log(other.name + " has snapped on " + gameObject.name);

            currentSnappedObject = other.gameObject;
            hasSnappedObject = true;
            SC_ConfirmParagraphHighlight.instance.HighlightColor();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.name + " has exited on " + gameObject.name);

        if (other.tag == "Paragraph" && other.gameObject == currentSnappedObject)
        {
            //Debug.Log(other.name + " has unsnapped on " + gameObject.name);
            
            currentSnappedObject = null;
            hasSnappedObject = false;
            SC_ConfirmParagraphHighlight.instance.HighlightColor();
        }
    }
}
