using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrapplingRope
{
    public class UtilityFunctions
    {
        /** UTILITY FUNCTIONS **/
        
        // Check whether the layer is in mask
        public static bool IsLayerInLayerMask(LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }

    }

    public class GrapplingHook : MonoBehaviour
    {
        /** Config Variables **/

        // End point of the rope
        public Vector2 endPoint;

        // Speed to send it at
        public float speed = 1f;

        // Keep a max throw distance
        public float maxThrowDistance = 2f;

        // Hold a layer mask for things to ignore
        public LayerMask ignoreLayers;

        // Hold a layer mask for this object's layer
        //public LayerMask collisionLayer;

        /** Object Variables **/

        // Hold a reference to the player
        public CharacterController2D player;

        // Hold a reference to the rope itself
        public GrapplingRope grapplingRope;

        // Hold a reference to the sprite representing the hook
        //public SpriteRenderer hookRenderer;

        /** Transient variables **/

        // Whether the hook has been thrown
        private bool hookHasBeenThrown = false;

        // Cache the camera.main
        private Camera main;

        // Start is called before the first frame update
        void Start()
        {
            // Store main camera in cache
            //main = Camera.main;
            //Debug.Log("main camera is " + main);
        }

        // Fixed update calls throw hook logic
        void FixedUpdate()
        {
            // Call Do throw hook in fixed update
            DoThrowHook();
        }

        // Throw hook logic simply throws the hook
        private void DoThrowHook()
        {
            // Check if hook has been thrown, then update
            if (hookHasBeenThrown)
            {
                // Move towards endpoint
                transform.position = Vector2.MoveTowards(transform.position, endPoint, speed * Time.fixedDeltaTime);
            }
        }

        // Handles on click
        public void OnMouseClick(bool isPressed)
        {
            if (isPressed)
            {
                // Destroy rope if it exists and make unable to throw rope
                DestroyRope();
                
                // Direction of click
                var aimDirection = CalculateAimDirection();

                // Calculate end point
                endPoint = aimDirection * maxThrowDistance;

                // Set initial location
                transform.position = player.transform.position;

                // Create rope
                CreateRope();
            }
            else
            {
                DestroyRope();
            }
        }

        // Create rope function sets the rope and hook to collide again and stuff
        private void CreateRope()
        {
            // Only create if hook hasn't been thrown
            if (!hookHasBeenThrown)
            {
                // Call create on the rope
                grapplingRope.ActivateRope();

                // Enable this
                //hookRenderer.enabled = true;

                // Set collisions to whatever collision layer we've chosen
                //gameObject.layer = collisionLayer;

                // Set active
                gameObject.SetActive(true);

                // Set hook has been thrown to true
                hookHasBeenThrown = true;
            }
        }

        // Destroy rope function simply hides the rope and grappling hook, and sets their collision to none
        private void DestroyRope()
        {
            // Only destroy if hook has been thrown
            if (hookHasBeenThrown)
            {
                // Call destroy rope on the grappling rope
                grapplingRope.DeactivateRope();

                // Hide this
                //hookRenderer.enabled = false;

                // Set collisions to none
                //gameObject.layer = 0;

                // Set inactive
                gameObject.SetActive(false);

                // Set hook has been thrown to false
                hookHasBeenThrown = false;
            }
        }

        /** USE FUNCTIONS **/

        // Calculate the aim angle
        private Vector3 CalculateAimDirection()
        {
            // Calculate the aim direction
            var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            var facingDirection = worldMousePosition - transform.position;
            var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
            var cosTheta = Vector3.Dot(facingDirection, new Vector3(0.0f, 1.0f, 0.0f));

            // Adjust on negative angle
            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }

            // Set aim direction
            var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

            // Return it
            return aimDirection.normalized;
        }

        // Check for collisions on this (if in correct layer, then hook)
        void OnCollisionEnter2D(Collision2D collider)
        {
            if (!UtilityFunctions.IsLayerInLayerMask(ignoreLayers, collider.gameObject.layer))
            {
                //...
            }
        }
    }
}