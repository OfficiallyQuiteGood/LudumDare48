using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMotion : MonoBehaviour
{
    // Variables

    // Take the character controller as an object
    public GameObject player;

    // Keep a variable for the world mouse position
    private Vector3 worldMouse;

    // Keep a variable for the distance between player and mouse
    private float mouseDist;
 
    // Start is called before the first frame update
    void Start()
    {
        // Calculate the mouse dist as the magnitude of the vector from player to mouse
        mouseDist = transform.position.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // Store world position of mouse
        worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Calculate new position
    Vector2 CalculateNewPosition(Vector3 mouse)
    {
        // Create temp vec
        Transform final = transform;
        Vector3 origPos = transform.position;

        // First, translate object by -position
        final.position -= transform.position;

        // Then rotate by a calculated amount based on vector of original position and mouse pos
        float cosTheta = Vector3.Dot(origPos, mouse);
        float theta = Mathf.Acos(cosTheta);
        final.Rotate(new Vector3(0.0f, 0.0f, 1.0f), theta);

        // Rotate back to new location which is magnitude of original position (mouseDist) * unit vector in direction of mouse
        final.position += mouseDist * Vector3.Normalize(mouse);

        // Return temp
        return final.position;
    }
}
