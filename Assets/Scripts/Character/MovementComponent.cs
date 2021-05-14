using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GrapplingRope;

public class MovementComponent : MonoBehaviour
{
    /* Movement related variables */

    // Movement related variables
    float horizontalMove = 0.0f;
    public float runSpeed = 40.0f;
    public bool m_Grounded = false;
    public bool m_AirControl = false;
    public float moveForce = 10.0f;
    private Vector3 m_Velocity = Vector3.zero;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
    private bool bShouldJump = true;                                            // Should jump within the sim
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	const float k_GroundedRadius = .2f;                                         // Radius of the overlap circle to determine if grounded

    // Bool for is mouse down
    public bool isMouseDown = false;

    // Added force
    public Vector2 force = new Vector2(0.0f, 0.0f);
    public float swingForce = 20.0f;
    public Vector2 exitAcceleration;
    public float exitAccelerationScale = 2.0f;

    // Component references
    public Rigidbody2D rb;

    // Collisions
    int numCollisions = 0;
    [SerializeField] private Transform snapshotCollisionTransform;
    float collisionRadius = 0.36f;
    private Collider2D[] colliderBuffer;
    private CollisionInfo[] collisionInfos;
    private const int MAX_COLLISIONS = 32;
    private const int COLLIDER_BUFFER_SIZE = 8;

    // On Enable
    void OnEnable()
    {
        EventManager.Instance.OnReleasedRopeDelegate += AddReleaseForce;
    }

    // On disable
    void OnDisable()
    {
        EventManager.Instance.OnReleasedRopeDelegate -= AddReleaseForce;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //InitializeCollisions();
    }

    // Initializes the physics collisions during swinging
    void InitializeCollisions()
    {
        colliderBuffer = new Collider2D[COLLIDER_BUFFER_SIZE];
        collisionInfos = new CollisionInfo[MAX_COLLISIONS];
        for (int i = 0; i < collisionInfos.Length; i++)
        {
            collisionInfos[i] = new CollisionInfo(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handle the input
        HandleInput();
    }

    void HandleInput()
    {
        // Register mouse press
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Register jump
        if (Input.GetButtonDown("Jump"))
        {
            bShouldJump = true;
        }

        // Do mouse down/up logic
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
            force = new Vector2(Input.GetAxisRaw("Horizontal") * swingForce, -10.0f);
        }
        else
        {
            CheckForOnGround();
            Move(horizontalMove * Time.fixedDeltaTime, bShouldJump);
            bShouldJump = false;
        }
    }

    // Movement function for the player
    void Move(float move, bool jump)
    {
        // If the player should move in the air or not
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * moveForce, rb.velocity.y);

			// And then smoothing it out and applying it to the character
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

        // The player might need to jump
        if (m_Grounded && jump)
        {
            // Add vertical force to the player
            m_Grounded = false;
            rb.AddForce(new Vector2(0.0f, m_JumpForce));
        }
    }

    // Checks for whether or not the player is on the ground
    private void CheckForOnGround()
    {
        bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
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
        isMouseDown = true;
        rb.simulated = false;
        //rb.isKinematic = true;
        //rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
        EventManager.Instance.OnRopeAction(true, mousePos);
    }

    // On mouse up
    void OnMouseUp()
    {
        isMouseDown = false;
        rb.simulated = true;
        //rb.isKinematic = false;
        //rb.interpolation = RigidbodyInterpolation2D.None;
        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        rb.angularVelocity = 0.0f;
        EventManager.Instance.OnRopeAction(false, Vector3.zero);
    }
    
    // Add a rope release force
    void AddReleaseForce(Vector2 releaseVelocity)
    {
        if (releaseVelocity != null)
        {
            //Debug.Log("vel = " + releaseForce.ToString("F4"));
            rb.velocity = releaseVelocity * exitAccelerationScale;
        }
    }
}
