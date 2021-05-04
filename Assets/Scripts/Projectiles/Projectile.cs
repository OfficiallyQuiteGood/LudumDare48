using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectileDeath;
    bool delayReleased = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected GameObject TryFindPlayer()
    {
        if(delayReleased)
        {
            lockSearch();
            GameObject player = GameObject.Find("Player");
            if(player != null) return player;
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    IEnumerator lockSearch()
    {
        delayReleased = false;
        yield return new WaitForSeconds(1f);
        delayReleased = true;
    }

    public void ProjectileDeath()
    {
        GameObject.Find("World Settings").GetComponent<WorldSettings>().PlayNoise(7,0);
        Instantiate(projectileDeath, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
