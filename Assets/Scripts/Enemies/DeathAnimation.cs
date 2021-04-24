using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public float animationLength;
    void Start()
    {
        StartCoroutine(DeathInit());
    }

    protected IEnumerator DeathInit()
    {
        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
