using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SproutProjectile : MonoBehaviour
{
    Vector3 startingPosition;
    //TileMap tilemap;
    public int direction = 1;
    public float movementSpeed = 0.05f;
    public float timeInterval = 4f;

    private bool m_FacingRight = true;
    public float m_Thrust = 2f;
    Rigidbody2D m_Rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody2D>();
        //StartCoroutine(destroyAfterInterval());
        //tilemap = GameObject.Find("Collisions");
        //TrailPlayer();
    }

    protected IEnumerator destroyAfterInterval()
    {
        yield return new WaitForSeconds(timeInterval);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        TrailPlayer();
    }

    void TrailPlayer()
    {
        
        //Debug.Log(tb);
        GameObject player = GameObject.Find("Player");
        if(player!=null)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            //transform.position+=new Vector3(dir.x * movementSpeed * Time.fixedDeltaTime,dir.y * movementSpeed * Time.fixedDeltaTime,0);
            transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime,0,0);

            //m_Rigidbody.AddForce(player.transform.position * m_Thrust);
        }
        else
        {
            transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime,0,0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Edge");
        // if(other is TilemapCollider2D)
        // {
        //     Debug.Log(other);
        //     Destroy(gameObject);
        // }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        MainCharacter player = hitInfo.GetComponent<MainCharacter>();
        if(player!=null)
        {
            ProjectileHit();
        }
        
    }

    public void changeDirection(int hammerDirection)
    {
        if(hammerDirection!=direction)
        {
            Flip();
            direction = hammerDirection;
        }
    }

    public void ProjectileHit()
    {

        Destroy(gameObject);
    }

    public void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
