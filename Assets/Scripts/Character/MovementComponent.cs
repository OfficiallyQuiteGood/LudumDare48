using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    /* Movement related variables */

    // Physics related vars
    public Vector3 gravity = new Vector3(0, -20f, 0);
    public float mass = 1.0f;

    // final movement vector
    public Vector3 finalPos;

    // Bool for is mouse down
    public bool isMouseDown = false;

    // Rigid body
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        finalPos = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Do some more physics stuff
        finalPos += gravity * Time.deltaTime * Time.deltaTime;

        // Apply position
        transform.position = finalPos;

        // Final thing before ending is resetting the final vel
        finalPos = Vector3.zero;
        */
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
    }
}
