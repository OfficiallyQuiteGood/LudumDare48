using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMotion : MonoBehaviour
{
    // Variables

    // Hold a reference to the character controller
    public CharacterController2D characterController;

    // Take the character controller as an object
    public Transform pivotPoint;

    // Keep a variable for the distance between player and mouse
    private float mouseDist;

    // Forward vector
    private Vector3 forwardVector;
 
    // Start is called before the first frame update
    void Start()
    {
        // Calculate the mouse dist as the magnitude of the vector from player to mouse
        mouseDist = transform.localPosition.magnitude;

        // Initial vec
        forwardVector = new Vector3(1.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Store world position of mouse
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);
        worldMouse.z = 0;

        // Simply set position
        transform.position = worldMouse;

        // Basic update
        //CalculateNewPositionInLocal(worldMouse);

        // Check for mouse slip
        CheckForMouseFlip(worldMouse);
    }

    // Calculate new position
    void CalculateNewPositionInLocal(Vector3 worldMouse)
    {
        // Create temp vec
        Vector3 origPos = transform.localPosition;
        Vector3 relMouse = pivotPoint.InverseTransformPoint(worldMouse);
        relMouse.z = 0;

        // First, translate object by -position
        transform.localPosition -= origPos;

        // Then rotate by a calculated amount based on vector of original position and mouse pos
        /*
        Debug.Log("origPos = " + Vector3.Normalize(origPos) + ", relMouse = " + Vector3.Normalize(relMouse));
        float cosTheta = Vector3.Dot(Vector3.Normalize(origPos), Vector3.Normalize(relMouse));
        float theta = Mathf.Acos(cosTheta);
        Debug.Log("cosTheta = " + cosTheta + ", theta = " + theta);
        if (!float.IsNaN(theta))
        {
            transform.Rotate(0.0f, 0.0f, theta);
        }
        */

        // Rotate back to new location which is magnitude of original position (mouseDist) * unit vector in direction of mouse
        Vector3 newPos = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 normMouse = Vector3.Normalize(relMouse);
        newPos += mouseDist * normMouse;

        transform.localPosition += newPos;
    }

    // Check for a flip
    void CheckForMouseFlip(Vector3 worldMouse)
    {
        // Compare mouse with forward vector
        worldMouse = pivotPoint.InverseTransformPoint(worldMouse);
        worldMouse.z = 0;
        float cosTheta = Vector3.Dot(forwardVector, worldMouse);

        // If smaller, need to flip
        if (cosTheta < 0)
        {
            characterController.Flip();
        }
    }
}
