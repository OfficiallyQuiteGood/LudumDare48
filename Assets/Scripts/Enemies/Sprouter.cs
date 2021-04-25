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


    // Update is called once per frame
    void Update()
    {
        CheckAgro(true);
        
    }

    void FixedUpdate()
    {
        if(isBouncing)
        {
            transform.position += new Vector3(0.01f*-playerDirection,0.01f,0);
        }
        else if(playerNear && shouldMove && !isShooting)
        {
            StartCoroutine(ShootSprouts());
        }
        else if(shouldMove) 
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
        setAnimatorParameter("ShouldMove", true);
        yield return new WaitForSeconds(2f);
        isShooting = false;
        setAnimatorParameter("IsShooting", isShooting);
        
        //PauseMovement();
    }

    
}

