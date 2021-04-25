using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // Variables

    // Image
    public Image image;

    // Textures
    public Sprite[] healthTextures;

    // Start is called before the first frame update
    void Start()
    {

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
            image.sprite = healthTextures[newHealth];
        }
    }
}
