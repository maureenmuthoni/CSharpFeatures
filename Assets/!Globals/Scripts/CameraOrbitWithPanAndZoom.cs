﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitWithPanAndZoom : MonoBehaviour
{
    public Transform target; // Target object to orbit around
    public float panSpeed = 5f; // Speed of panning
    public float sensitivity = 1f; // Sensitivity of mouse

    //Minimum & Maximum zoom distance
    public float distanceMin = 5f;
    public float distanceMax = 15f;
    private float distance = 0f; // Current distance between target and camera

    //Stored X & Y euler rotation
    private float x = 0.0f;
    private float y = 0.0f;

    // Create an enum to use for mouse input (just for readability)
    public enum MouseButton
    {
        LEFTMOUSE = 0,
        RIGHTMOUSE = 1,
        MIDDLEMOUSE = 2,

    }

    // Use this for initialization
    void Start()
    {
        // CALL target transform's SetParent(null)
        target.SetParent(null);
        //... Detaches the target from parent

        // SET distance = vector3.Distance(target's position, transform's position)
        distance = Vector3.Distance(target.position, transform.position);
        // Calculates distance to target

        // LET angles = transform's eulerAngles
        Vector3 angles = transform.eulerAngles;

        // SET x = angles.x
        x = angles.x;
        // SET y = angles.y
        y = angles.y;
    }

    void Orbit()
    {
        // SET x = x + Input Axis "Mouse Y" x sensitivity
        x += Input.GetAxis("Mouse Y") * sensitivity;
        // SET y = y -  Input Axis "Mouse X" x sensitivity
        y -= Input.GetAxis("Mouse X") * sensitivity;
    }

    void Movement()
    {
        // IF target != null
        if (target != null)
        {
            //LET rotation = Quaternion Euler(x, y, 0)
            Quaternion rotation = Quaternion.Euler(x, y, 0);

            // LET desiredDist = distance - Input Axis "Mouse ScrollWheel"
            float desiredDist = distance - Input.GetAxis("Mouse ScrollWheel");

            // SET desiredDist = desiredDist x sensitivity
            desiredDist = desiredDist * sensitivity;
            // ...Amplifies desiredDist by sensitivity(Scroll Speed)


            // SET distance = mathf Clamp (desiredDist, distanceMin, distanceMax);
            distance = Mathf.Clamp(desiredDist, distanceMin, distanceMax);
            //... Clamps the results so that the distance doesn't go outside of constraints


            // LET invDistanceZ = new Vector3(0, 0, -distance)
            Vector3 invDistanceZ = new Vector3(0, 0, -distance);

            //SET invDistanceZ = rotation * invDistanceZ
            invDistanceZ = rotation * invDistanceZ;
            // ... Rotates the direction of vector to be local to camera

            // LET position = target.position + invDistanceZ
            Vector3 position = target.position + invDistanceZ;

            // SET  transform.rotation = rotation
            transform.rotation = rotation;
            // SET transform.position = position  
            transform.position = position;
        }

    }
    // Moves the target using X and Y mouse coordinates to create panning effect
    void Pan()
    {
        // LET inputX = -Input GetAxis "Mouse X"
        float inputX = -Input.GetAxis("Mouse X");
        // LET inputY = -Input GetAxis "Mouse Y"
        float inputY = -Input.GetAxis("Mouse Y");

        // LET inputDir = new Vector3(inputX, inputY)
        Vector3 inputDir = new Vector3(inputX, inputY);
        // LET movement = transform.TransformDirection(inputDir)
        Vector3 movement = transform.TransformDirection(inputDir);
        //SET target.transform.position += movement x panSpeed x deltaTime
        target.transform.position += movement * panSpeed * Time.deltaTime;
    }
    // Hides/Unhides the cursor
    void HideCursor(bool isHiding)
    {
        // IF isHiding
        if (isHiding)
        {
            // Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            // Hide the cursor
            Cursor.visible = false;
        }
        // ELSE
        else
        {
            // Unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            //Unhide the cursor
            Cursor.visible = true;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        // IF Input MouseButton Right 
        if (Input.GetMouseButton((int)MouseButton.RIGHTMOUSE))
        {
            // CALL HideCursor(true) ... Hides the cursor
            HideCursor(true);
            // CALL orbit() ... Update orbit of the camera
            Orbit();

        }
        // ELSE IF Input MouseButton Middle
        else if (Input.GetMouseButton((int)MouseButton.MIDDLEMOUSE))
        {
            // CALL HideCursor(true) ... Hides the cursor
            HideCursor(true);
        // CALL Pan() ... Pans the camera
        Pan();
        }
    
        // ELSE
        else
        {
            // CALL HideCursor(false) ... Unhides the cursor
            HideCursor(false);
        }
        // CALL Movement() ... Always update movement regardless of any input
        Movement();
    }
}

