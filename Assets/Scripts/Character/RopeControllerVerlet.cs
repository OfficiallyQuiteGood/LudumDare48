using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeControllerVerlet : MonoBehaviour
{
    // Variables

    // Rope related
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public int subdivideAmount = 5;
    public float ropeMaxCastDistance = 2f;
    public LayerMask ropeLayerMask;
    private bool ropeAttached;
    private float segLength;

    // Line related vars
    public float lineWidth = 0.02f;
    private LineRenderer lineRenderer;

    // General simulation variables
    public int constraintIterations = 50;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle input
        HandleInput(CalculateAimDirection());
        
        // Draw rope
        DrawRope();
    }

    // Fixed update
    private void FixedUpdate()
    {
        Simulate();
    }

    // Simulate functions
    private void Simulate()
    {
        // Abort if no rope
        if (ropeSegments.Count > 0)
        {
            // Forced gravity
            Vector2 forceGravity = new Vector2(0f, -1f);

            // Simulate each rope segment
            for (int i = 0; i < subdivideAmount; i++)
            {
                RopeSegment segment = ropeSegments[i];
                Vector2 velocity = segment.posNow - segment.posOld;
                segment.posOld = segment.posNow;
                segment.posNow += velocity;
                segment.posNow += forceGravity * Time.deltaTime;
                ropeSegments[i] = segment;
            }

            // Constrain the points
            for (int i = 0; i < constraintIterations; i++)
            {
                ApplyConstraints();
            }
        }
    }

    private void ApplyConstraints()
    {
        // Get rope segment 1
        for (int i = 0; i < subdivideAmount - 1; i++)
        {
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - segLength);
            Vector2 changeDir = Vector2.zero;

            if (dist > segLength)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < segLength)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * error;
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    // Calculate aim angle
    private Vector3 CalculateAimDirection()
    {
                // 3
        var worldMousePosition =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        // 4
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        // Return
        return aimDirection;
    }

    // Handle input
    private void HandleInput(Vector2 aimDirection)
    {
        // If primary button has been pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Set line renderer enabled
            lineRenderer.enabled = true;

            // Create a raycast
            var hit = Physics2D.Raycast(transform.position, aimDirection, ropeMaxCastDistance, ropeLayerMask);
        
            // If hit was detected
            if (hit.collider != null)
            {
                // Set rope attached
                ropeAttached = true;

                // Create segments
                CreateRopeSegments(transform.position, hit.point);

                // Attach rope on both ends
                AttachRope(gameObject, hit.collider.gameObject);
            }
        }
    }

    // Create rope method
    private void CreateRopeSegments(Vector3 startPoint, Vector3 endPoint)
    {
        // Calculate magnitude between end point and start point
        Vector3 dir = endPoint - startPoint;
        segLength = dir.magnitude / subdivideAmount;
        Vector3 temp = endPoint;
        for (int i = 0; i < subdivideAmount; i++)
        {
            ropeSegments.Add(new RopeSegment(temp));
            temp += Vector3.Normalize(dir) * segLength;
        }
    }

    private void AttachRope(GameObject start, GameObject end)
    {
        // Attach endpoint
        RopeSegment firstSeg = ropeSegments[0];
        firstSeg.posNow = end.transform.position;

        // Attach start point
        RopeSegment lastSeg = ropeSegments[ropeSegments.Count - 1];
        lastSeg.posNow = start.transform.position;
    }

    // Draw rope function
    private void DrawRope()
    {
        // Draw rope if it exists
        if (ropeSegments.Count > 0)
        {
            // Set width
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            // Create positions of points
            Vector3[] ropePositions = new Vector3[subdivideAmount];
            for (int i = 0; i < subdivideAmount; i++)
            {
                ropePositions[i] = ropeSegments[i].posNow;
            }

            // Update line renderer
            lineRenderer.positionCount = ropePositions.Length;
            lineRenderer.SetPositions(ropePositions);
        }
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
