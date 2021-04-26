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
        base.Start();
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
        CheckAgro(true);
        
    }

    void FixedUpdate()
    {
        if(isBouncing)
        {
            transform.position += new Vector3(bounceX*-playerDirection,bounceY,0);
        }
        else if(playerNear && shouldMove && !isShooting && !isAtEdge)
        {
            StartCoroutine(HammerSwing());
        }
        else if(shouldMove && !isAtEdge) 
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
        StartCoroutine(PauseMovement());
        yield return new WaitForSeconds(0.7f);
        
        setAnimatorParameter("ShouldMove", true);
        yield return new WaitForSeconds(1f);
        isShooting = false;
        setAnimatorParameter("IsShooting", isShooting);
        
        //PauseMovement();
    }

    
}
