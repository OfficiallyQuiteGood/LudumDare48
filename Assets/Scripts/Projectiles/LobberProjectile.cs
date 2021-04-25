using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ExtensionMethods;

public class LobberProjectile : Projectile
{
    Vector3 startPos;
    Vector3 target;

    [Tooltip("Horizontal speed, in units/sec")]
	public float speed = 10;
	
	[Tooltip("How high the arc should be, in units")]
	public float arcHeight = 1;
    //TileMap tilemap;
    public int direction = 1;
    public float movementSpeed = 0.05f;
    public float timeInterval = 4f;

    private bool m_FacingRight = true;
    public float m_Thrust = 2f;
    Rigidbody2D m_Rigidbody;

    public GameObject projectileDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform.position;
        float distx = Mathf.Abs(target.x-startPos.x);
        if(distx < 2) target += new Vector3(3*direction, 0, 0);
    }

    protected IEnumerator destroyAfterInterval()
    {
        yield return new WaitForSeconds(timeInterval);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Compute the next position, with arc added in
		float x0 = startPos.x;
		float x1 = target.x;
		float dist = x1 - x0;
		float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
		float baseY = Mathf.Lerp(startPos.y, target.y, (nextX - x0) / dist);
		float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
		Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);
		
		// Rotate to face the next position, and then move there
		transform.rotation = LookAt2D(nextPos - transform.position);
		transform.position = nextPos;
		
		// Do something when we reach the target
		if (nextPos == target) Arrived();
    }

    void Arrived() {
		ProjectileDeath();
	}
	
	/// 
	/// This is a 2D version of Quaternion.LookAt; it returns a quaternion
	/// that makes the local +X axis point in the given forward direction.
	/// 
	/// forward direction
	/// Quaternion that rotates +X to align with forward
	static Quaternion LookAt2D(Vector2 forward) {
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
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
