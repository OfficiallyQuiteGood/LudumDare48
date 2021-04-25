using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Variable
    public Vector2 endPoint;
    public float speed = 1;
    public float distance = 2;
    public GameObject nodePrefab;
    private GameObject player;
    private GameObject lastNode;
    private List<GameObject> Nodes = new List<GameObject>();
    int vertexCount = 2;
    bool done = false;
    private LineRenderer lineRenderer;

    // On awake
    void Awake()
    {
        // Find player
        player = GameObject.FindGameObjectWithTag("Player");
        lastNode = transform.gameObject;
        Nodes.Add(transform.gameObject);
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(FixedMovement());
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update
    void Update()
    {
    }

    protected IEnumerator FixedMovement()
    {
        while (true)
        {
            DoRopeLogic();
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void DoRopeLogic()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPoint, speed);

        if ((Vector2)transform.position != endPoint)
        {
            // Check for collision
            float dist = Vector2.Distance(player.transform.position, lastNode.transform.position);
            if (dist > distance)
            {
                CreateNode();
            }
        }
        else if (done == false)
        {
            done = true;
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }

        RenderLine();
    }
    
    private void RenderLine()
    {
        lineRenderer.positionCount = vertexCount;
        int i;
        for (i = 0; i < Nodes.Count; i++)
        {
            lineRenderer.SetPosition(i, Nodes[i].transform.position);
        }
        lineRenderer.SetPosition(i, player.transform.position);
    }

    // Create rope function (TESTING)
    public void CreateRope()
    {
        // Just instantaneously create them
        Vector2 initPos = transform.position;
        float dist = Vector2.Distance(endPoint, initPos);
        int segments = Mathf.FloorToInt(dist / distance);
        Debug.Log("Dist = " + dist + ", segments " + segments);
        for (int i = 0; i < segments - 1; i++)
        {
            CreateNode(i, (endPoint - initPos).normalized);
        }

        // Attach last node
        lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
    }

    private void CreateNode()
    {
        Vector2 pos2Create = player.transform.position - lastNode.transform.position;
        pos2Create.Normalize();
        pos2Create *= distance;
        pos2Create += (Vector2)lastNode.transform.position;

        GameObject go = (GameObject) Instantiate(nodePrefab, pos2Create, Quaternion.identity);

        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
    
        lastNode = go;

        Nodes.Add(lastNode);
        
        vertexCount++;
    }

    private void CreateNode(int i, Vector2 dir)
    {
        Vector2 pos2Create = (i + 1) * dir - i * dir;
        pos2Create.Normalize();
        pos2Create *= distance;
        pos2Create += (Vector2)lastNode.transform.position;

        GameObject go = (GameObject) Instantiate(nodePrefab, pos2Create, Quaternion.identity);

        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
    
        lastNode = go;

        Nodes.Add(lastNode);
        
        vertexCount++;
    }

    public void ClearRope()
    {
        foreach(GameObject go in Nodes)
        {
            Destroy(go);
        }
        Nodes.Clear();
        lineRenderer.positionCount = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.collider.transform.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(1);
                player.GetComponent<ThrowRope>().DestroyRope();
            }
        }
    }
}
