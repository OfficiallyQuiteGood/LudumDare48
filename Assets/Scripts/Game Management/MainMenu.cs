using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public Image IntroScreen;
    bool isDone;
    public AudioClip introMusic;
    public AudioSource source;
    void Start()
    {
        if(source!=null && introMusic!=null) source.PlayOneShot(introMusic);
        StartCoroutine(FadeInImage(true, 2, 1f));
        StartCoroutine(FadeInImage(false, 2, 3.7f));
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDone && Input.anyKey)
        {
            Debug.Log("Game Start");
            SceneManager.LoadScene("TreeScene");
        }
    }

    public IEnumerator FadeInImage(bool fadeIn = true, int fadeSpeed = 5,float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        Color objectColor = IntroScreen.color;
        float fadeAmount;

        if(fadeIn)
        {
            while(IntroScreen.color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                IntroScreen.color = objectColor;
                yield return new WaitForSeconds(0.003f);
            }
        }
        else
        {
            while(IntroScreen.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                IntroScreen.color = objectColor;
                yield return new WaitForSeconds(0.003f);
            }
            isDone = true;
        }
    }
}
