using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMotion : MonoBehaviour
{
    // Variables

    // Take the character controller as an object
    public GameObject player;

    // Keep a variable for the distance between player and mouse
    private float mouseDist;

    // Store reference to sprite
    private SpriteRenderer spriteRenderer;
 
    // Start is called before the first frame update
    void Start()
    {
        // Get sprite
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Calculate the mouse dist as the magnitude of the vector from player to mouse
        mouseDist = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // Store world position of mouse
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);
        worldMouse.z = 0;

        // Basic update
        CalculateNewPositionInLocal(worldMouse);
    }

    // Calculate new position
    void CalculateNewPositionInLocal(Vector3 worldMouse)
    {
        // Create temp vec
        Vector3 origPos = transform.localPosition;
        Vector3 relMouse = player.transform.InverseTransformPoint(worldMouse);
        relMouse.z = 0;

        //Debug.Log("Original position = " + origPos + ", relative mouse = " + relMouse);

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

        Debug.Log("norm mouse = " + normMouse + ", newPos = " + newPos);

        transform.localPosition += newPos;

        //Debug.Log("Final position " + transform.position);
    }
}
