using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMush : Enemy
{
     
    Vector3 startingPosition;

	bool jump = false;
    public float chargeSpeed = 3f;
    bool charging = false;

    // Start is called before the first frame update
    void Start()
    {
        //test death animations
        //StartCoroutine(DealDamage());

        //StartCoroutine(MoveAtInterval());
        base.Start();
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
            //body.velocity = new Vector2(-speed, 0);
            //Move();
            shouldMove = !shouldMove;
            setAnimatorParameter("ShouldMove", shouldMove);
            direction = Enemy.getRandomHorizontalDirection();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckAgro(false);
        
    }

    void FixedUpdate()
    {
        //flip the player
        if(isBouncing)
        {
            transform.position += new Vector3(bounceX*-playerDirection,bounceY,0);
        }
        else if(playerNear && shouldMove && !charging)
        {
            StartCoroutine(Charge());
            
        }
        else if(charging && shouldMove)
        {
            transform.position+=new Vector3(direction * movementSpeed*chargeSpeed * Time.fixedDeltaTime,0,0);
            playNoise(2,0.2f);
        }
        else if(shouldMove)
        {
            playNoise(2,0.3f);
            transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime,0,0);
        }

    }

    protected IEnumerator Charge()
    {
        charging = true;
        if(playerDirection != direction) changeDirection();
        yield return new WaitForSeconds(2);
        charging = false;
        
        StartCoroutine(PauseMovement());
    }
}
