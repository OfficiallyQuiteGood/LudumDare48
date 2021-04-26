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

    protected bool isBouncing = false;
    public float bounceY = 0.1f;
    public float bounceX = 0.1f;
    bool hasIframes = false;

    private bool m_FacingRight = true;  // For determining which way the Enemy is currently facing.
    protected bool isAtEdge = false;

    // WorldSettings required to play audio
    public WorldSettings worldSettings;
    // Vertical Velocity
    protected Vector3 prev;
    protected float maxVerticalVelocity;
    public void Start()
    {
        prev = transform.position;
        worldSettings = GameObject.Find("World Settings").GetComponent<WorldSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Fall Damage Logic
    //on every update calculate velocity
    public void CalculateVelocity()
    {

        float newVelocity = Mathf.Abs(transform.position.y - prev.y)*50;
        maxVerticalVelocity = Mathf.Max(maxVerticalVelocity, newVelocity);
        prev = transform.position;

        if(maxVerticalVelocity >= 12 && newVelocity < maxVerticalVelocity) 
        {
            Debug.Log("kill from falling");
            Die();
        }
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
        
        
        if(!hasIframes) health -= damage;
        StartCoroutine(BounceBack());
        

        Debug.Log(health);
        if(health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("take damage");
            playNoise(3);
        }
    }

    public IEnumerator BounceBack()
    {
        StartCoroutine(giveIframes(0.5f));
        StartCoroutine(CharacterBlinker(5,0.1f));
        isBouncing = true;
        yield return new WaitForSeconds(1);
        isBouncing = false;
    }

    public IEnumerator giveIframes(float delay)
    {
        hasIframes = true;
        yield return new WaitForSeconds(delay);
        hasIframes = false;
    }

    protected IEnumerator CharacterBlinker(int numFrames, float frameDuration)
    {
        for(int i = 0; i<numFrames; i++)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(frameDuration);
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(frameDuration);
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

    // Kill by calling die
    public void Kill()
    {
        Die();
    }

    // Die (virtual) function
    virtual protected void Die()
    {
        playNoise(0, 0);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected void setAnimatorParameter(string parameterName, bool val)
    {
        if(!HasParameter(parameterName, animator)) return;
        if(animator.GetBool(parameterName) != val)
        {
            animator.SetBool(parameterName, val);
        }
    }

    protected void setAnimatorParameter(string parameterName, float val)
    {
        if(!HasParameter(parameterName, animator)) return;
        if(animator.GetFloat(parameterName) != val)
        {
            animator.SetFloat(parameterName, val);
        }
    }

    protected void setAnimatorParameter(string parameterName, int val)
    {
        if(!HasParameter(parameterName, animator)) return;
        if(animator.GetInteger(parameterName) != val)
        {
            animator.SetInteger(parameterName, val);
        }
    }

    public static bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
        if (param.name == paramName)
            return true;
        }
        return false;
    }


    //Change Enemy Direction when they reach an edge
    void OnTriggerExit2D(Collider2D other)
    {
        if (other is TilemapCollider2D)
        {
            changeDirection();
            StartCoroutine(PauseMovement());
            isAtEdge = true;
            setAnimatorParameter("IsAtEdge", true);
        }
        
    }

    //hard check to see if enemy at edge
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other is Terrain || other is TilemapCollider2D)
        {
            isAtEdge = false;
            setAnimatorParameter("IsAtEdge", false);
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
