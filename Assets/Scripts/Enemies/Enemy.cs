using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 1;
    public float movementSpeed = 1f;
    public int jumpForce = 150;
    public GameObject deathEffect;
    public bool shouldMove = true;
    public Animator animator;
    public float timeInterval = 1f;
    public int direction = 1;
    protected int playerDirection = 1;
    public float agroDistance = 2f;
    protected bool playerNear = false;

    private bool m_FacingRight = true;  // For determining which way the Enemy is currently facing.

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeDirection()
    {
        direction = -direction;
        StartCoroutine(PauseMovement());
        Flip();
    }

    protected IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("take damage");
        TakeDamage(1);
    }

    protected IEnumerator PauseMovement()
    {

        shouldMove = false;
        setAnimatorParameter("ShouldMove", shouldMove);
        yield return new WaitForSeconds(timeInterval);
        //body.velocity = new Vector2(-speed, 0);
        //Move();
        shouldMove = true;
        setAnimatorParameter("ShouldMove", shouldMove);
    }

    // Take damage function
    public void TakeDamage (int damage)
    {
        Debug.Log("take damage");

        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    protected static Vector3 getRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1,1), UnityEngine.Random.Range(-1f,1f)).normalized;
    }

    protected static int getRandomHorizontalDirection()
    {
        if(UnityEngine.Random.Range(-1,1) <= 0) return -1;
        else return 1;
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

    // Die (virtual) function
    virtual protected void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected void setAnimatorParameter(string parameterName, bool val)
    {
        if(animator.GetBool(parameterName) != val)
        {
            animator.SetBool(parameterName, val);
        }
    }

    protected void setAnimatorParameter(string parameterName, float val)
    {
        if(animator.GetFloat(parameterName) != val)
        {
            animator.SetFloat(parameterName, val);
        }
    }

    protected void setAnimatorParameter(string parameterName, int val)
    {
        if(animator.GetInteger(parameterName) != val)
        {
            animator.SetInteger(parameterName, val);
        }
    }

    //Change Enemy Direction when they reach an edge
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Edge");
        if(other is TilemapCollider2D)
        {
            //Debug.Log("edge reached");
            changeDirection();
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(PauseMovement());
    }

    //see if player is nearby and change direction accordingly
    protected void CheckAgro(bool shouldFlip)
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            
            float distance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));

            //change enemy direction if player in range           
            playerDirection = player.transform.position.x < transform.position.x ? -1: 1;
            if (playerNear && playerDirection != direction && shouldFlip)
            {
                changeDirection();
            }

            if(distance <= agroDistance)
            {
                playerNear = true;
            }
            else
            {
                playerNear = false;
            }

            setAnimatorParameter("PlayerNear", playerNear);
        }
    }
}
