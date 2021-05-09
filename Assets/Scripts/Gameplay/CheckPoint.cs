﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CheckPoint : MonoBehaviour
{
    MainCharacter mainCharacter;
    public Light2D bonfire;
    bool isLit;
    public Animator animator;
    Color colorStart = Color.black;
    Color colorEnd = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        isLit = false;
        mainCharacter = GameObject.Find("Player").GetComponent<MainCharacter>();
        bonfire.intensity = 0;
        setAnimatorParameter("isLit", false);
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
        if(bonfire.intensity <= 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log(gameObject.GetComponent<Renderer>().material.color);
            bonfire.intensity = intensity;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.black;
            bonfire.intensity = 0;
        }
        setAnimatorParameter("isLit", isLit);
        mainCharacter.setCheckPoint(this);
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
        
    

    
}