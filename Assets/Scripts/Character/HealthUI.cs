using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // Variables

    // Image
    public Image image;
    public SpriteRenderer renderer;

    // Textures
    public Sprite[] healthTextures;
    public Animator animator;
    public int prev_health = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("Health", 3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // On health changes
    public void OnHealthChanged(int newHealth)
    {
        if (newHealth >= 0 && newHealth < 4)
        {
            if(prev_health>newHealth) GameObject.Find("HealthUIDamageOverlay").GetComponent<HealthUIDamageOverlay>().TakeHit();
            animator.SetInteger("Health", newHealth);
            prev_health = newHealth;
            //image.sprite = healthTextures[newHealth];
        }
    }
}
