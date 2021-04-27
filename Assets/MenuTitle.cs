using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        StartCoroutine(enableDelay(5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator enableDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}
