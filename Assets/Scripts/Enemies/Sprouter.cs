using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprouter : Enemy
{
    Vector3 startingPosition;
    public Transform FirePoint;
    public GameObject SproutProjectile;

	bool jump = false;
    bool isShooting = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //test death animations
        //StartCoroutine(DealDamage());

        //StartCoroutine(MoveAtInterval());
        shouldMove = true;
        startingPosition = transform.position;
    }

    private Vector3 getRoamingPosition()
    {
        return startingPosition + new Vector3(10*Enemy.getRandomHorizontalDirection(), 0, 0);
    }

    protected IEnumerator MoveAtInterval()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            shouldMove = !shouldMove;
            setAnimatorParameter("ShouldMove", shouldMove);
            direction = Enemy.getRandomHorizontalDirection();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            
            float distance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));

            //change enemy direction if player in range           
            playerDirection = player.transform.position.x < transform.position.x ? -1: 1;
            if (playerNear && playerDirection != direction)
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

    void FixedUpdate()
    {
        if(playerNear && shouldMove && !isShooting)
        {
            
            StartCoroutine(ShootSprouts());
            
        }
        else if(shouldMove && !isShooting) 
        {
            transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime,0,0);
        }
    }

    protected IEnumerator ShootSprouts()
    {
        
        isShooting = true;
        setAnimatorParameter("IsShooting", isShooting);
        setAnimatorParameter("ShouldMove", false);
        yield return new WaitForSeconds(0.3f); 
        
        GameObject b1 = Instantiate(SproutProjectile, FirePoint.position, FirePoint.rotation);
        b1.GetComponent<SproutProjectile>().changeDirection(direction);
        yield return new WaitForSeconds(0.3f);
        GameObject b2 = Instantiate(SproutProjectile, FirePoint.position, FirePoint.rotation);
        b2.GetComponent<SproutProjectile>().changeDirection(direction);
        yield return new WaitForSeconds(2f);
        isShooting = false;
        setAnimatorParameter("IsShooting", isShooting);
        setAnimatorParameter("ShouldMove", true);
        //PauseMovement();
    }

    
}

