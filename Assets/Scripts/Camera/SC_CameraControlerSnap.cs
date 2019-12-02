using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the movements of the camera with snapped positions

public class SC_CameraControlerSnap : MonoBehaviour
{
    public bool isLookingUp; // Is the camera looking at the PC screen ?
    public bool isAligning; // Is the camera currently aligning ?

    private int screenWidth; // Width of the screen
    private int screenHeight; // Height of the screen

    [Header("System values")]

    public float deadzone; // Size of the zone on screen edges which trigger movement
    public float movementSpeed; // Movement speed
    public float alignSpeed; // Aligment speed

    //[Header("Current angle values")]

    public float topAngle; // Angle at which the camera move up
    public float downAngle; // Angle at which the camera move down
    public float rightAngle; // Angle at which the camera move right
    public float leftAngle; // Angle at which the camera move left

    [Header("Set angle values")]

    public float screenTopAngle; // Angle at which the camera move up in the computer area
    public float screenDownAngle; // Angle at which the camera move down in the computer area
    public float letterTopAngle; // Angle at which the camera move up in the letter area
    public float letterDownAngle; // Angle at which the camera move down in the letter area

    void Start()
    {
        // Get the size of the screen
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        if(isLookingUp == true)
        {
            topAngle = screenTopAngle;
        }
        else
        {
            downAngle = letterDownAngle;
        }
    }

    void Update()
    {
        // MOVE THE CAMERA UP AND DOWN

        // Move the camera up
        if (Input.mousePosition.y > screenHeight - deadzone && isAligning == false)
        {
            //Debug.Log("Camera up");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(topAngle, transform.eulerAngles.y, transform.rotation.z), Time.deltaTime * (Input.mousePosition.y - screenHeight + deadzone) / 100);
        }
        if (isLookingUp == true && isAligning == true && transform.rotation.x >= Quaternion.Euler(topAngle, 0, 0).x) // Automatically align the camera to the upper position
        {
            //Debug.Log("camera is aligning to the upper position");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(topAngle, transform.eulerAngles.y, transform.rotation.z), Time.deltaTime * alignSpeed);
        }


        // Move the camera down
        if (Input.mousePosition.y < 0 + deadzone && isAligning == false)
        {
            //Debug.Log("Camera down");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(downAngle, transform.eulerAngles.y, transform.rotation.z), Time.deltaTime * (deadzone - Input.mousePosition.y) / 100);
        }
        if (isLookingUp == false && isAligning == true && transform.rotation.x <= Quaternion.Euler(downAngle, 0, 0).x) // Automatically align the camera to the lower position
        {
            //Debug.Log("camera is aligning to the lower position");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(downAngle, transform.eulerAngles.y, transform.rotation.z), Time.deltaTime * alignSpeed);
        }



        // MOVE THE CAMERA LEFT AND RIGHT

        // Move the camera right
        if (Input.mousePosition.x < 0 + deadzone)
        {
            //Debug.Log("Camera right");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, rightAngle, transform.rotation.z), Time.deltaTime * (deadzone - Input.mousePosition.x) / 100);
        }

        // Move the camera left
        if (Input.mousePosition.x > screenWidth - deadzone)
        {
            //Debug.Log("Camera left");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, leftAngle, transform.rotation.z), Time.deltaTime * (Input.mousePosition.x - screenWidth + deadzone) / 100);
        }



        // GET IF THE CAMERA NEED TO BE SWITCHED TO UPPER OR LOWER POSITION

        // Move the camera to the upper position
        if(Input.mousePosition.y > screenHeight - 30 && isLookingUp == false)
        {
            //Debug.Log("camera moved the the upper position");
            downAngle = screenDownAngle;
            topAngle = screenTopAngle;
            isAligning = true;
            isLookingUp = true;
        }

        // Move the camera to the lower position
        if (Input.mousePosition.y < 0 + 30 && isLookingUp == true)
        {
            //Debug.Log("camera moved the the lower position");
            downAngle = letterDownAngle;
            topAngle = letterTopAngle;
            isAligning = true;
            isLookingUp = false;
        }


        // Disable aligning
        if(transform.rotation.x < Quaternion.Euler(22, 0, 0).x || transform.rotation.x > Quaternion.Euler(62, 0, 0).x)
        {
            //Debug.Log("Camera finished aligning");
            isAligning = false;
        }
    }
}
