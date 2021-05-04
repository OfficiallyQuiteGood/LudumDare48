using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatImage : MonoBehaviour
{
    public Sprite[] images;
    Image defeatText;
    // Start is called before the first frame update
    void Start()
    {
        defeatText = gameObject.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayImage(float delay)
    {
        gameObject.GetComponent<Image>().sprite = images[Random.Range(0, images.Length)];
        StartCoroutine(FadeInImage(true, 5, delay));
    }

    public void HideImage(float delay)
    {
        gameObject.GetComponent<Image>().sprite = images[Random.Range(0, images.Length)];
        StartCoroutine(FadeInImage(false, 5, delay));
    }

    public IEnumerator FadeInImage(bool fadeIn = true, int fadeSpeed = 5,float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        Color objectColor = defeatText.color;
        float fadeAmount;

        if(fadeIn)
        {
            while(defeatText.color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                defeatText.color = objectColor;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while(defeatText.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                defeatText.color = objectColor;
                yield return new WaitForSeconds(0.005f);
            }
        }
    }
}
