using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    // Variables

    // Store the maximum whip distance
    public float whipDistance = 5.0f;

    // Store pivot point
    public Transform pivotPoint;

    // Store reference to line renderer
    public LineRenderer line;

    // private bools for if mouse is up or down
    private bool isMouseDown = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start hidden
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Store world position of mouse
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);
        worldMouse.z = 0;

        // Check for mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            OnMouseDown();
        }

        // Check for mouse button up
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            OnMouseUp();
        }

        // Check if mouse is down
        if (isMouseDown)
        {
            HandleMouseDown(worldMouse);
        }
    }

    // On mouse down
    void OnMouseDown()
    {
        // Enable line
        if (!line.enabled)
        {
            line.enabled = true;
        }
    }
    void OnMouseUp()
    {
        // Hide line
        if (line.enabled)
        {
            line.enabled = false;
        }
    }

    // Handle mouse down
    void HandleMouseDown(Vector3 worldMouse)
    {
        // Calculate relative vector
        Vector3 relVec = worldMouse - pivotPoint.position;
        relVec = Vector3.Normalize(relVec);
        relVec.z = 0;

        // Set the line endpoints equal to whip point position and distance * mouse
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + (whipDistance * relVec));
    }
}
