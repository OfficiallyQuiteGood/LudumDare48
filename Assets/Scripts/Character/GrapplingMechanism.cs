using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrapplingRope
{
    public class GrapplingMechanism : MonoBehaviour
    {
        /** Transient Variables **/

        // Whether the hook can be thrown
        private bool canThrowRope = false;
        
        // Initial throw delay
        public float initialThrowDelay = 1.0f;

        // Frequency of being able to throw
        public float throwFreq = 1.0f;

        /** OBJECT REFS **/

        // Reference to hook object
        public GrapplingHook grapplingHook;

        // Start is called before the first frame update
        void Start()
        {
            // Delay a little before being able to throw
            StartCoroutine(WaitToThrowRope(initialThrowDelay));
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        // Handle input function checks for mouse click and unclick
        private void HandleInput()
        {
            // Check for mouse button down and ability to throw rope
            if (Input.GetMouseButtonDown(0) && canThrowRope)
            {
                // Set mouse clicked for hook
                grapplingHook.OnMouseClick(true);

                // Set can throw rope to false
                canThrowRope = false;

                // Start coroutine
                StartCoroutine(WaitToThrowRope(throwFreq));
            }

            // If mouse unclicked, delete rope
            if (Input.GetMouseButtonUp(0))
            {
                // Set mouse unclicked for hook
                grapplingHook.OnMouseClick(false);
            }
        }

        // Coroutine for being able to throw rope
        protected IEnumerator WaitToThrowRope(float freq)
        {
            // Wait x amount of time before being able to throw rope again
            yield return new WaitForSeconds(freq);

            // Now, set able to throw rope again
            canThrowRope = true;
        }
    }
}