using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMush : Enemy
{
     
    Vector3 startingPosition;

	bool jump = false;
    public float chargeSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //test death animations
        //StartCoroutine(DealDamage());

        //StartCoroutine(MoveAtInterval());
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
        GameObject player = GameObject.Find("Player");
        if(player!=null)
        {
            float distance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));
            playerDirection = player.transform.position.x < transform.position.x ? -1: 1;
            if(distance<=agroDistance)
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
        //flip the player
        if(playerNear && shouldMove)
        {
            if(playerDirection != direction) changeDirection();
            transform.position+=new Vector3(playerDirection * movementSpeed*chargeSpeed * Time.fixedDeltaTime,0,0);
        }
        else if(shouldMove) transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime,0,0);
    }
}
