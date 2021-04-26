using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMush : MonoBehaviour
{
    // Variables

    // Animator
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(1.625f);

        if (!animator.GetBool("IsIdle"))
        {
            animator.SetBool("IsIdle", true);
        }
    }
}
