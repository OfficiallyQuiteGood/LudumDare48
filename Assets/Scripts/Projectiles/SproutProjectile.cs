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

    public GameObject projectileDeath;
    Vector3 prevPos;
    public float difficulty = 0.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        prevPos = startingPosition;
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
        
         
        Vector3 moveDirection = gameObject.transform.position - prevPos; 
        if (moveDirection != Vector3.zero) 
        {
           float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
           transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        prevPos = gameObject.transform.position;
    }

    void TrailPlayer()
    {
        
        //Debug.Log(tb);
        GameObject player = GameObject.Find("Player");
        if(player!=null)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            //set dir.x to get homing functionality
            transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime, difficulty*dir.y * movementSpeed * Time.fixedDeltaTime,0);
            //transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime,0,0);

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

        ProjectileDeath();
        
    }

    public void changeDirection(int hammerDirection)
    {
        if(hammerDirection!=direction)
        {
            Flip();
            direction = hammerDirection;
        }
    }

    //if projectile hits player
    public void ProjectileHit()
    {

        //ProjectileDeath();
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

    void ProjectileDeath()
    {
        Instantiate(projectileDeath, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
