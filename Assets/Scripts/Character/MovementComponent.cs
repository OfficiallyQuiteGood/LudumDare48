using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    /* Movement related variables */
        
    // Bool for is mouse down
    public bool isMouseDown = false;

    // Added force
    public Vector2 force;
    public float swingForce = 20.0f;

    // Component references
    private MainCharacter mainCharacter;
    private CharacterController2D characterController;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GetComponent<MainCharacter>();
        characterController = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(mainCharacter + " " + characterController + " " + rb);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
    }

    // Fixed update
    void FixedUpdate()
    {		
        if (isMouseDown)
        {
            force = new Vector2(Input.GetAxisRaw("Horizontal") * swingForce, - 10.0f);
        }
    }

    // On mouse down function
    void OnMouseDown()
    {
        // Get mouse pos
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        mousePos.z = 0;

        // Send it to the event manager
        // TODO: need to add events to main character/character controller in order to disable all physics related stuff
        EventManager.Instance.OnRopeAction(true, mousePos);
        isMouseDown = true;
        mainCharacter.enabled = false;
        characterController.enabled = false;
        rb.simulated = false;
    }

    // On mouse up
    void OnMouseUp()
    {
        EventManager.Instance.OnRopeAction(false, Vector3.zero);
        isMouseDown = false;
        mainCharacter.enabled = true;
        characterController.enabled = true;
        rb.simulated = true;
    }
}
