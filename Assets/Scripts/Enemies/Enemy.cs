using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 1;
    public float movementSpeed = 1f;
    public int jumpForce = 150;
    public GameObject deathEffect;
    public bool shouldMove = true;
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Take damage function
    public void TakeDamage (int damage)
    {

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

    // Die (virtual) function
    virtual protected void Die()
    {
        //Debug.Log("dead: " + gameObject);
        // Update the game controller that this person has died
        // if (controller != null)
        // {
        //     TopDownController ControllerScript = Controller.GetComponent<TopDownController>();
        //     if (ControllerScript != null && ControllerScript.numEnemiesLeft != 0)
        //     {
        //         ControllerScript.numEnemiesLeft--;
        //         //Debug.Log(ControllerScript.numOfEnemiesInStage - ControllerScript.numEnemiesLeft);
        //     }
        // }

        // Create death effect and destroy this object
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected void setAnimatorParameter(string parameterName, bool val)
    {
        if(animator.GetBool(parameterName) != val)
        {
            animator.SetBool(parameterName, val);
            Debug.Log("parameter changed: "+parameterName);
        }
    }

    protected void setAnimatorParameter(string parameterName, float val)
    {
        if(animator.GetFloat(parameterName) != val)
        {
            animator.SetFloat(parameterName, val);
            Debug.Log("parameter changed: "+parameterName);
        }
    }

    protected void setAnimatorParameter(string parameterName, int val)
    {
        if(animator.GetInteger(parameterName) != val)
        {
            animator.SetInteger(parameterName, val);
        }
    }
}
