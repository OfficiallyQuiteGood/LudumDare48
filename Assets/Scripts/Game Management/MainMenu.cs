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
            isDone = false;
            Debug.Log("Game Start");
            SceneManager.LoadSceneAsync("TreeScene");
        }
    }

    // IEnumerator LoadYourAsyncScene()
    // {
    //     // The Application loads the Scene in the background as the current Scene runs.
    //     // This is particularly good for creating loading screens.
    //     // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
    //     // a sceneBuildIndex of 1 as shown in Build Settings.

    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TreeScene");

        
    //     // Wait until the asynchronous scene fully loads
    //     while (!asyncLoad.isDone)
    //     {
    //         yield return null;
    //     }
    // }


    // public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    // {
    //     float currentTime = 0;
    //     float start = audioSource.volume;

    //     while (currentTime < duration)
    //     {
    //         currentTime += Time.deltaTime;
    //         audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
    //         yield return null;
    //     }
    //     yield break;
    // }

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
