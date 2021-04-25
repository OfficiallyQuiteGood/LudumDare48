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
    protected IEnumerator OnWin(GameObject go)
    {
        // Wait a frame (this makes sure you die properly if landed badly)
        yield return null;

        // Check if player is still alive
        HealthSystem hs = go.GetComponent<HealthSystem>();
        if (hs && hs.GetHealth() <= 0)
        {
            yield return;
        }

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
            GameObject go = (GameObject) collider.transform.gameObject;
            StartCoroutine(OnWin(go));
        }
    }
}
