using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script make the object attached to drag&drop-able

public class DragDropControls : MonoBehaviour
{
    public float HoveringHeight = -5.25f;
    public float SpeedDivider = 2.5f;

    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseZCoord = mouseZCoord / SpeedDivider;

        mouseOffset = gameObject.transform.position - GetMouseWorldPos();

        rig.useGravity = false;
    }

    private void OnMouseUp()
    {
        rig.useGravity = true;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        rig.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, Mathf.Lerp(transform.position.y, HoveringHeight, Time.deltaTime * 2), GetMouseWorldPos().z + mouseOffset.z );
    }
}
