using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CheckPoint : MonoBehaviour
{
    MainCharacter mainCharacter;
    public List<Light2D> lights;
    public float[] lightIntensity = {0.28f, 0.15f, 0.15f};
    bool isLit;
    bool isTriggered = false;
    public Animator animator;
    Color colorStart = Color.black;
    Color colorEnd = Color.white;
    // WorldSettings required to play audio
    public WorldSettings worldSettings;

    // Start is called before the first frame update
    void Start()
    {
        isLit = false;
        mainCharacter = GameObject.Find("Player").GetComponent<MainCharacter>();
        foreach(var light in lights)
        {
            light.intensity = 0;
        }
        setAnimatorParameter("isLit", false);
        worldSettings = GameObject.Find("World Settings").GetComponent<WorldSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(!isLit)
        {
            MainCharacter player = hitInfo.GetComponent<MainCharacter>();
            if(player!=null)
            {
                LightFire(2f);
            }
        }
    }

    void LightFire(float intensity)
    {
        isLit = !isLit;

        //give sprite natural color
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        Debug.Log(gameObject.GetComponent<Renderer>().material.color);

        //fade in lights
        for(int i = 0; i<lights.Count; i++)
        {
            StartCoroutine(FadeInLight(lights[i], i, true, 2, i*0.3f));
        }

        //play the lighting up animation in a delay
        StartCoroutine(TriggerDelay(0.625f));
        playNoise(8);
        setAnimatorParameter("isLit", isLit);
        mainCharacter.setCheckPoint(this);
    }

    public void playNoise(int ind, float delay)
    {
        if(gameObject.GetComponent<Renderer>().isVisible) worldSettings.PlayNoise(ind, delay);
    }

    //play noise without IEnumerator
    public void playNoise(int ind)
    {
        worldSettings.PlayNoise(ind);
    }

    IEnumerator TriggerDelay(float delay)
    {
        isTriggered = true;
        setAnimatorParameter("isTriggered", isTriggered);
        yield return new WaitForSeconds(delay);
        isTriggered = false;
        setAnimatorParameter("isTriggered", isTriggered);

    }

    protected void setAnimatorParameter(string parameterName, bool val)
    {
        if(!HasParameter(parameterName, animator)) return;
        if(animator.GetBool(parameterName) != val)
        {
            animator.SetBool(parameterName, val);
        }
    }

    protected void setAnimatorParameter(string parameterName, float val)
    {
        if(!HasParameter(parameterName, animator)) return;
        if(animator.GetFloat(parameterName) != val)
        {
            animator.SetFloat(parameterName, val);
        }
    }

    protected void setAnimatorParameter(string parameterName, int val)
    {
        if(!HasParameter(parameterName, animator)) return;
        if(animator.GetInteger(parameterName) != val)
        {
            animator.SetInteger(parameterName, val);
        }
    }

    public static bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
        if (param.name == paramName)
            return true;
        }
        return false;
    }

    public IEnumerator FadeInLight(Light2D light, int indexOfLight, bool fadeIn = true, int fadeSpeed = 5,float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        


        var fadeAmount = light.intensity;
        if(fadeIn)
        {
            while(light.intensity < 1)
            {
                fadeAmount = light.intensity + (fadeSpeed * Time.deltaTime);

                light.intensity = fadeAmount;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while(light.intensity > 0)
            {
                fadeAmount = light.intensity - (fadeSpeed * Time.deltaTime);

                light.intensity = fadeAmount;
                yield return new WaitForSeconds(0.005f);
            }
        }
        

        
    }
        
    

    
}
