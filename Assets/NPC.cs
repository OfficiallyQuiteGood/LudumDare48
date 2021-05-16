using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    bool isBouncing = false;
    public float bounceY = 0.1f;
    public float bounceX = 0.1f;
    float playerDirection;
    public WorldSettings worldSettings;
    public GameObject deathEffect;
    void Start()
    {
        worldSettings = GameObject.Find("World Settings").GetComponent<WorldSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            
            float distance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));

            //change enemy direction if player in range           
            playerDirection = player.transform.position.x < transform.position.x ? -1: 1;
        }

        if(isBouncing)
        {
            transform.position += new Vector3(bounceX*-playerDirection,bounceY,0);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("layer: "+other.collider.transform.gameObject.layer);
        if (other.collider.transform.gameObject.layer == 17 && !isBouncing)
        {
            playNoise(9, 0);
            isBouncing = true;
        }
        if (other.collider.transform.gameObject.layer == 18)
        {
            //playNoise(9, 0);
            Die();
        }
        
    }

    virtual public void Die()
    {
        
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void playNoise(int ind, float delay)
    {
        if(gameObject.GetComponent<Renderer>().isVisible || isBouncing) worldSettings.PlayNoise(ind, delay);
    }

    //play noise without IEnumerator
    public void playNoise(int ind)
    {
        if(gameObject.GetComponent<Renderer>().isVisible || isBouncing) worldSettings.PlayNoise(ind);
    }
}
