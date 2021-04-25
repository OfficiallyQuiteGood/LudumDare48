using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallCollision : MonoBehaviour
{
    Enemy enemy;
    bool isDetecting = true;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D colliderInfo)
    {
        Debug.Log("wall collide");
        if(colliderInfo is TilemapCollider2D && isDetecting)
        {
            enemy.changeDirection();
        }
    }

    protected IEnumerator pauseDetection()
    {
        isDetecting = false;
        enemy.transform.position = new Vector3(enemy.transform.position.x + enemy.direction*-1f,enemy.transform.position.y,0);
        yield return new WaitForSeconds(0.1f);
        isDetecting = true;
    }
}
