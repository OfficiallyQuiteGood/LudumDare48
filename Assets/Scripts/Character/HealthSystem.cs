using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    // Variables

    // Health related
    public int totalHealth = 3;
    private int currHealth;
    public float iFrameDuration = 2.0f;
    private bool bCanBeDamaged = true;

    // Reference to the health UI
    public HealthUI healthUI;

    // Prefab for death
    public GameObject deathPrefab;
    public RuntimeAnimatorController deathEffect;
    
    // Animator
    public Animator animator;

    // Lost condition object
    public LoseCondition loseCondition;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Take damage function
    public void TakeDamage(int damage = 1)
    {
        if (bCanBeDamaged)
        {
            // Decrease health and die if necessary
            currHealth -= damage;
            //healthUI.OnHealthChanged(currHealth);
            if (currHealth <= 0)
            {
                //healthUI.OnHealthChanged(0);
                Die();
            }
            
            // Play iFrames
            StartCoroutine(PlayIFrames());
        }
    }

    public int GetHealth()
    {
        return currHealth;
    }

    //reset player and lose 1 health
    // public void FallFar(Vector3 lastPos)
    // {
    //     // Switch animations?
        
    //     GameObject.Find("World Settings").GetComponent<WorldSettings>().InstantiatePlayer(lastPos, currHealth-1);
    //     Instantiate(deathPrefab, transform.position, Quaternion.identity);
    //     Destroy(gameObject);
    // }
    // Die function
    private void Die()
    {
        //fade to black
        GameObject.Find("Canvas").GetComponent<UIController>().FadeScreen();


        // Switch animations?
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        GameObject.Find("World Settings").GetComponent<WorldSettings>().GameOver();
        Destroy(gameObject);

        // Reload entire scene?
        loseCondition.ReloadGame();
    }

    protected IEnumerator PlayIFrames()
    {
        // Set able to be damaged to false
        bCanBeDamaged = false;
        //to get blinker duration: iframduration/frames/2
        StartCoroutine(CharacterBlinker(20, 0.05f));

        // Wait a certain amount of time
        yield return new WaitForSeconds(iFrameDuration);

        // Now set it back to true
        bCanBeDamaged = true;
    }

    //display IFrames as character blinking in and out of view
    protected IEnumerator CharacterBlinker(int numFrames, float frameDuration)
    {
        for(int i = 0; i<numFrames; i++)
        {
            Debug.Log("Blink");
            GameObject.Find("Player").GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(frameDuration);
            GameObject.Find("Player").GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(frameDuration);
        }
    }

    // On trigger will simply increase health on the player
    // on the other side (the health) it will play special animation
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Health")
        {
            currHealth += 1;
            if (currHealth > totalHealth)
            {
                currHealth = totalHealth;
            }
            healthUI.OnHealthChanged(currHealth);
        }
    }
}
