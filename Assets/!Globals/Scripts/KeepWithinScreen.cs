﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Renderer))] // Force renderer to be attatched
public class KeepWithinScreen : MonoBehaviour
{
    private Renderer rend; // Renderer attatched to the object
    private Camera cam; // camera container (variable)
    private Bounds camBounds; // camera bound structure
    private float camWidth, camHeight;

	// Use this for initialization
	void Start ()
    {
        // set camera variable to main camera
        cam = Camera.main;
        // GetComponent renderer component
        rend = GetComponent<Renderer>();

	}
	// Updates the camBounds variables with camera values
    void UpdateCamBounds()
    {
        // Calculate camera bounds
        camHeight = 2f * cam.orthographicSize; // height = 2 x orthographic size
        camWidth = camHeight * cam.aspect; // width = height x aspect
        camBounds = new Bounds(cam.transform.position, new Vector3(camWidth, camHeight));
 }
    Vector3 CheckBounds()
    {
        Vector3 pos = transform.position;
        Vector3 size = rend.bounds.size;
        float halfWidth = size.x * 0.5f;
        float halfHeight = size.y * 0.5f;
        float halfCamWidth = camWidth * 0.5f;
        float halfCamHeight = camHeight * 0.5f;
        // check left
        if (pos.x - halfWidth < camBounds.min.x)
        {
            pos.x = camBounds.min.x + halfWidth;
        }
        // check right
        if (pos.x + halfWidth > camBounds.max.x)
        {
            pos.x = camBounds.max.x - halfWidth;
        }
        // check down 
        if (pos.y - halfHeight < camBounds.min.y)
        {
            pos.y = camBounds.min.y + halfHeight;
        }
        
        // check up
        if (pos.y + halfHeight > camBounds.max.y)
        {
            pos.y = camBounds.max.y - halfHeight;
        }
        return pos; // Returns adjusted position
    }
    
	// Update is called once per frame
	void Update ()
    {
        // Update the camera bounds
        UpdateCamBounds();
        // Setr the position after checking the bounds
        transform.position = CheckBounds();
	}
}
