using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject blackOutSquare;
    // Start is called before the first frame update
    void Update()
    {

    }

    public void FadeScreen(float delay = 1)
    {
        StartCoroutine(FadeBlackOutSquare(true,5,1));
    }

    public void AppearScreen()
    {
        StartCoroutine(FadeBlackOutSquare(false,5,1));
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 5,float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if(fadeToBlack)
        {
            while(blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while(blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return new WaitForSeconds(0.005f);
            }
        }
    }

    // Update is called once per frame
}
