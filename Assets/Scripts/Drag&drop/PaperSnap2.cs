using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script make drag&drop objects snap according to their line number

public class PaperSnap2 : MonoBehaviour
{
    public List<GameObject> snappedObjectsList; // List of objects currently snapped

    private DragDropControls otherDD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
