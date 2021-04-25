using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This is the main function for handling the win condition
    void OnWin()
    {
        // First, destroy all objects
        Object[] enemies = GameObject.FindObjectsOfType(typeof(Enemy));
        foreach (Object obj in enemies)
        {
            Destroy(obj);
        }

        // Play animation and stuff...
    }

    // On trigger entered
    void OnTriggerEntered2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            // Call on win
            OnWin();
        }
    }
}
