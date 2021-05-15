using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUIDamageOverlay : MonoBehaviour
{
    public Image image;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit()
    {
        StartCoroutine(delayHit(1f));
    }

    IEnumerator delayHit(float delay)
    {
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(delay);
        animator.SetBool("isHit", false);
        tempColor.a = 0f;
        image.color = tempColor;

    }
}
