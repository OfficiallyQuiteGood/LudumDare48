using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallCollisionProjectile : MonoBehaviour
{
    HammerProjectile projectile;
    bool isDetecting = true;
    // Start is called before the first frame update
    void Start()
    {
        projectile = gameObject.GetComponentInParent<HammerProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D colliderInfo)
    {
        Debug.Log("PROJECTILE wall collide");
        // Debug.Log(colliderInfo);
        // if(colliderInfo is TilemapCollider2D)
        // {
        //     Debug.Log("PROJECTILE is wall");
            
        // }

        projectile.ProjectileDeath();
    }

    void OnTriggerExit2D(Collider2D colliderInfo)
    {
        Debug.Log("PROJECTILE wall exit");
    }
}
