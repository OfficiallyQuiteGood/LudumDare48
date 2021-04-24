using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRope : MonoBehaviour
{
    // Variables

    // Hold hook prefab
    public GameObject hook;
    private GameObject currHook;
    public float ropeMaxCastDistance = 2f;
    public LayerMask ropeLayerMask;
    private bool ropeWasCast = false;
    public CharacterController2D characterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    // Handle input
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Calculate aim direction
            var worldMousePosition =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            var facingDirection = worldMousePosition - transform.position;
            var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }

            // Set it
            var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

            // Create ray cast
            var hit = Physics2D.Raycast(transform.position, aimDirection, ropeMaxCastDistance, ropeLayerMask);
            
            // if hit
            if (hit.collider != null && !ropeWasCast)
            {
                // Endpoint
                ropeWasCast = true;
                characterController.isSwinging = true;
                Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currHook = (GameObject) Instantiate(hook, transform.position, Quaternion.identity);
                currHook.GetComponent<Rope>().endPoint = endPoint;
            }
        }

        // Handle mouse up
        if (Input.GetMouseButtonUp(0))
        {
            if (currHook != null)
            {
                // Rope was cast is false now
                ropeWasCast = false;
                characterController.isSwinging = false;

                // Clear stuff
                currHook.GetComponent<Rope>().ClearRope();
                Destroy(currHook);
            }
        }
    }
}
