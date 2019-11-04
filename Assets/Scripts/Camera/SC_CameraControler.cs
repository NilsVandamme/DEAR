using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the movements of the camera with free movement

public class SC_CameraControler : MonoBehaviour
{
    private int screenWidth; // Width of the screen
    private int screenHeight; // Height of the screen

    [Header("System values")]

    public float deadzone; // Size of the zone on screen edges which trigger movement
    public float speed; // Movement speed

    [Header("Angle values")]

    public float topAngle; // Angle at which the camera move up
    public float downAngle; // Angle at which the camera move down
    public float rightAngle; // Angle at which the camera move right
    public float leftAngle; // Angle at which the camera move left

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
            //Debug.Log("Camera up");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(topAngle, transform.eulerAngles.y, transform.rotation.z), Time.deltaTime * (Input.mousePosition.y - screenHeight + deadzone)/100);
        }

        // Move the camera down
        if (Input.mousePosition.y < 0 + deadzone)
        {
            //Debug.Log("Camera down");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(downAngle, transform.eulerAngles.y, transform.rotation.z), Time.deltaTime * (deadzone - Input.mousePosition.y)/100);
        }

        // Move the camera right
        if(Input.mousePosition.x < 0 + deadzone)
        {
            //Debug.Log("Camera right");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, rightAngle, transform.rotation.z), Time.deltaTime * (deadzone - Input.mousePosition.x)/100);
        }

        // Move the camera left
        if (Input.mousePosition.x > screenWidth - deadzone)
        {
            //Debug.Log("Camera left");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, leftAngle, transform.rotation.z), Time.deltaTime * (Input.mousePosition.x - screenWidth + deadzone) / 100);
        }
    }
}
