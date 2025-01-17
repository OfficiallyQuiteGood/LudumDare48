﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobber : Enemy
{
    Vector3 startingPosition;
    public Transform FirePoint;
    public GameObject LobProjectile;

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
        startingPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        CheckAgro(true);
        
    }

    void FixedUpdate()
    {
        CalculateVelocity();
        if(isBouncing)
        {
            transform.position += new Vector3(bounceX*-playerDirection,bounceY,0);
        }
        else if(playerNear && !isShooting)
        {
            StartCoroutine(ShootLob());
        }
    }

    protected IEnumerator ShootLob()
    {
        
        isShooting = true;
        setAnimatorParameter("IsShooting", isShooting);
        yield return new WaitForSeconds(0.66f); 
        
        playNoise(4,0.3f);
        GameObject b1 = Instantiate(LobProjectile, FirePoint.position, FirePoint.rotation);
        b1.GetComponent<LobberProjectile>().changeDirection(direction);

        yield return new WaitForSeconds(1f);
        isShooting = false;
        setAnimatorParameter("IsShooting", isShooting);
        
        //PauseMovement();
    }

    
}


