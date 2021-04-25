using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Enemy
{
    Vector3 startingPosition;
    public Transform FirePoint;
    public GameObject hammerProjectile;

	bool jump = false;
    bool isShooting = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //test death animations
        //StartCoroutine(DealDamage());

        //StartCoroutine(MoveAtInterval());
        shouldMove = true;
        setAnimatorParameter("ShouldMove", shouldMove);
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
        if(player!=null)
        {
            
            float distance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));

            //change enemy direction if player in range           
            playerDirection = player.transform.position.x < transform.position.x ? -1: 1;
            if(playerDirection != direction) changeDirection();

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
            
            StartCoroutine(HammerSwing());
            
        }
        else if(shouldMove && !isShooting) 
        {
            transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime,0,0);
        }
    }

    protected IEnumerator HammerSwing()
    {
        
        isShooting = true;
        setAnimatorParameter("IsShooting", isShooting);
        setAnimatorParameter("ShouldMove", false);
        yield return new WaitForSeconds(0.3f); 
        
        GameObject hammerBullet = Instantiate(hammerProjectile, FirePoint.position, FirePoint.rotation);
        if(hammerBullet.GetComponent<HammerProjectile>().direction != direction)
        {
            hammerBullet.GetComponent<HammerProjectile>().changeDirection(direction);
        }
        yield return new WaitForSeconds(2f);
        isShooting = false;
        setAnimatorParameter("IsShooting", isShooting);
        setAnimatorParameter("ShouldMove", true);
        //PauseMovement();
    }

    
}
