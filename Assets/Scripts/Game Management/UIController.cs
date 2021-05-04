using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject blackOutSquare;
    // Start is called before the first frame update

    void Start()
    {
        AppearScreen();
        
    }
    void Update()
    {

    }

    public void FadeScreen(float delay = 1)
    {
        StartCoroutine(FadeBlackOutSquare(true,5,1));
        GameObject.Find("DefeatText").GetComponent<DefeatImage>().DisplayImage(2f);
    }

    public void AppearScreen()
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        blackOutSquare.GetComponent<Image>().color = objectColor;
        StartCoroutine(FadeBlackOutSquare(false,20f,0.01f));
        GameObject.Find("DefeatText").GetComponent<DefeatImage>().HideImage(1f);
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, float fadeSpeed = 5,float delay = 0.05f)
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
                yield return new WaitForSeconds(delay);
            }
        }
    }

    // Update is called once per frame
}
