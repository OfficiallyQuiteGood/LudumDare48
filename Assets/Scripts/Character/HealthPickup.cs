using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Variables

    // Animator ref
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HealthGet") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            Debug.Log("Destroying this " + name);
            Destroy(gameObject);
        }
    }

    // Trigger collision
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (animator.GetBool("gotHealth") != true)
            {
                animator.SetBool("gotHealth", true);
                // animator.StartPlayback();
                Debug.Log("Got health!");
            }
        }
    }
}
