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
            gameObject.GetComponent<MainCharacter>().playNoise(3,0);
            currHealth -= damage;
            healthUI.OnHealthChanged(currHealth);
            if (currHealth <= 0)
            {
                healthUI.OnHealthChanged(0);
                Die();
            }
            else
            {
                StartCoroutine(PlayIFrames(iFrameDuration, true));
            }
            
            // Play iFrames
            
        }
    }

    public void TakeDamageReset(int damage, float delay)
    {
        if (bCanBeDamaged)
        {
            // Decrease health and die if necessary
            gameObject.GetComponent<MainCharacter>().playNoise(3,0);
            currHealth -= damage;
            healthUI.OnHealthChanged(currHealth);
            if (currHealth <= 0)
            {
                healthUI.OnHealthChanged(0);
                Die();
            }
            else
            {
                StartCoroutine(PlayIFrames(iFrameDuration, false));
            }
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
        gameObject.GetComponent<MainCharacter>().playNoise(0);

        // Switch animations?
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        GameObject.Find("Follow Camera").GetComponent<Follow>().PausePan(5f);
        GameObject.Find("World Settings").GetComponent<WorldSettings>().GameOver();
        StartCoroutine(HidePlayerForSeconds(5f));
        //Destroy(gameObject);

        // Reload entire scene?
        loseCondition.ReloadToCheckpoint();
    }

    protected IEnumerator HidePlayerForSeconds(float seconds)
    {
        bCanBeDamaged = false;
        GameObject.Find("Player").GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(seconds);
        GameObject.Find("Player").GetComponent<Renderer>().enabled = true;
        bCanBeDamaged = true;
    }


    protected IEnumerator PlayIFrames(float duration, bool playBlinker)
    {
        // Set able to be damaged to false
        bCanBeDamaged = false;
        //to get blinker duration: iframduration/frames/2
        if(playBlinker) StartCoroutine(CharacterBlinker((int)(duration*10), 0.05f));

        // Wait a certain amount of time
        yield return new WaitForSeconds(duration);

        // Now set it back to true
        bCanBeDamaged = true;
    }

    //display IFrames as character blinking in and out of view
    protected IEnumerator CharacterBlinker(int numFrames, float frameDuration)
    {
        for(int i = 0; i<numFrames; i++)
        {
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
            gameObject.GetComponent<MainCharacter>().playNoise(7,0.3f);
            currHealth += 1;
            if (currHealth > totalHealth)
            {
                currHealth = totalHealth;
            }
            healthUI.OnHealthChanged(currHealth);
        }
    }

    public void HealHealth(int health)
    {
        if(health<0) Debug.Log("Negative Health Amount Given");
        currHealth = currHealth + health < 3 ? currHealth + health : 3;
        healthUI.OnHealthChanged(currHealth);
    }
}
