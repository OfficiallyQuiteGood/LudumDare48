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
    public SpriteRenderer crossHair;
    public SpriteRenderer crossHairHit;

    // Start is called before the first frame update
    void Start()
    {
        crossHairHit.enabled = false;
        StartCoroutine(CheckForCrossHairHit());
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    // Check for changed crosshair
    protected IEnumerator CheckForCrossHairHit()
    {
        while (gameObject)
        {
            yield return new WaitForSeconds(0.25f);

            // Calculate aim direction
            if (!ropeWasCast)
            {
                var worldMousePosition =
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
                var facingDirection = worldMousePosition - transform.position;
                var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
                var cosTheta = Vector3.Dot(facingDirection, new Vector3(0.0f, 1.0f, 0.0f));
                var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

                var hit = Physics2D.Raycast(transform.position, aimDirection, ropeMaxCastDistance, ropeLayerMask);

                if (cosTheta > 0 && hit.collider != null)
                {
                    crossHair.enabled = false;
                    crossHairHit.enabled = true;
                }
                else
                {
                    crossHair.enabled = true;
                    crossHairHit.enabled = false;
                }
            }
        }
    }

    // Handle input
    private void HandleInput()
    {
        // Need to check for mouse position about to hit something
        if (Input.GetMouseButtonDown(0))
        {
            // Calculate aim direction
            var worldMousePosition =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            var facingDirection = worldMousePosition - transform.position;
            var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
            var cosTheta = Vector3.Dot(facingDirection, new Vector3(0.0f, 1.0f, 0.0f));

            if (cosTheta < 0)
            {
                return;
            }
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
                crossHair.enabled = false;
                crossHairHit.enabled = false;
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
                crossHair.enabled = true;
                ropeWasCast = false;
                characterController.isSwinging = false;

                // Clear stuff
                currHook.GetComponent<Rope>().ClearRope();
                Destroy(currHook);
            }
        }
    }
}
