using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectileDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProjectileDeath()
    {
        GameObject.Find("World Settings").GetComponent<WorldSettings>().PlayNoise(7,0);
        Instantiate(projectileDeath, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
