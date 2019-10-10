using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    private int screenWidth; // Width of the screen
    private int screenHeight; // Height of the screen

    [Header("System values")]

    public float deadzone; // Size of the zone on screen edges which trigger movement
    public float speed; // Movement speed

    [Header("Angle values")]

    public float topAngle; // Angle at which the camera move up
    public float downAngle; // Angle at which the camera move down

    void Start()
    {
        // Get the size of the screen
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        // Move the camera up
        if(Input.mousePosition.y > screenHeight - deadzone)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(topAngle, transform.rotation.y, transform.rotation.z), Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(topAngle, transform.rotation.y, transform.rotation.z), Time.deltaTime * (Input.mousePosition.y - screenHeight +deadzone)/100);
        }

        // Move the camera down
        if (Input.mousePosition.y < 0 + deadzone)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(downAngle, transform.rotation.y, transform.rotation.z), Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(downAngle, transform.rotation.y, transform.rotation.z), Time.deltaTime * (deadzone - Input.mousePosition.y)/100);
        }
    }
}
