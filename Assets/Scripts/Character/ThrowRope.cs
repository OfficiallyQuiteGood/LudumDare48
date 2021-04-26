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
    public LayerMask ignoreLayers;
    private bool ropeWasCast = false;
    public CharacterController2D characterController;
    public SpriteRenderer crossHair;
    //public SpriteRenderer crossHairHit;
    //public SpriteRenderer crossHairAttack;

    // Textures....
    public Sprite crossHairNormal;
    public Sprite crossHairAttach;
    public Sprite crossHairAttack;

    public float checkFrequency = 0.25f;

    // Can grapple again bool
    private bool canThrowRope = false;
    public float throwFreq = 1.0f;
    public float initialThrowDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToThrowRope(initialThrowDelay));
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
            yield return new WaitForSeconds(checkFrequency);

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

                if (hit.collider != null && hit.collider.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    crossHair.sprite = crossHairAttack;
                }
                else if (hit.collider != null)
                {
                    crossHair.sprite = crossHairAttach;
                }
                else
                {
                    crossHair.sprite = crossHairNormal;
                }
            }
        }
    }

    private bool IsLayerInLayerMask(LayerMask layerMask, int layer)
    {
        return layerMask == (layerMask | (1 << layer));
    }

    // Handle input
    private void HandleInput()
    {
        // Need to check for mouse position about to hit something
        if (Input.GetMouseButtonDown(0) && canThrowRope)
        {
            // Set throw rope to false
            canThrowRope = false;

            // Calculate aim direction
            var worldMousePosition =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            var facingDirection = worldMousePosition - transform.position;
            var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
            var cosTheta = Vector3.Dot(facingDirection, new Vector3(0.0f, 1.0f, 0.0f));

            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }

            // Set it
            var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

            // Create ray cast
            var hit = Physics2D.Raycast(transform.position, aimDirection, ropeMaxCastDistance, ropeLayerMask);

            // if hit
            if (hit.collider != null && !IsLayerInLayerMask(ignoreLayers, hit.collider.gameObject.layer) && !ropeWasCast)
            {
                // Start animation for attack
                GameObject.Find("Player").GetComponent<MainCharacter>().Attack();

                // Endpoint
                crossHair.enabled = false;
                ropeWasCast = true;
                characterController.isSwinging = true;
                Vector2 endPoint = hit.point;
                currHook = (GameObject) Instantiate(hook, transform.position, Quaternion.identity);
                currHook.GetComponent<Rope>().endPoint = endPoint;
            }

            // Start coroutine
            StartCoroutine(WaitToThrowRope(throwFreq));
        }

        // Handle mouse up
        if (Input.GetMouseButtonUp(0))
        {
            DestroyRope();
        }
    }

    public void DestroyRope()
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

    protected IEnumerator WaitToThrowRope(float freq)
    {
        // Wait x amount of time before being able to throw rope again
        yield return new WaitForSeconds(freq);

        // Now, set able to throw rope again
        canThrowRope = true;
    }
}
