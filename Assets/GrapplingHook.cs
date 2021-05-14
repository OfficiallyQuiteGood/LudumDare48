using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrapplingRope
{
    public class GrapplingHook : MonoBehaviour
    {
        /* VARIABLES */
        public Vector2 gravity = new Vector2(0.0f, -20.0f);
        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Add force of gravity
            //rb.AddForce(gravity);
        }
    }
}