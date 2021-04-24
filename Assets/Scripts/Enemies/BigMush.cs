using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMush : Enemy
{
    public float timeInterval = 1f;
    public Animator animator;

    Vector3 startingPosition;

	bool jump = false;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAtInterval());
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
            direction = Enemy.getRandomHorizontalDirection();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(shouldMove) transform.position+=new Vector3(direction * movementSpeed * Time.fixedDeltaTime,0,0);
    }

    virtual public void Move()
    {
        Vector3 roamingPos = getRoamingPosition();
        while(!transform.position.Equals(roamingPos))
        {
            Debug.Log("moving"+movementSpeed);
            transform.position+=new Vector3(movementSpeed * Time.fixedDeltaTime,0,0);
        }
        
    }

    protected IEnumerator allowMove()
    {
        shouldMove = true;
        yield return new WaitForSeconds(1);
        shouldMove = false;
    }
}
