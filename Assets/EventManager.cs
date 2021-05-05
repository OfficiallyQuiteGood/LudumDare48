using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Singleton
    private static EventManager _instance;
    public static EventManager Instance { get {return _instance; }}

    // Delegates
    public event Action<bool> OnMouseClickedDelegate;
    public event Action<bool, Vector3> OnRopeActionDelegate;

    // Create instance?
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Delegate functions
    public void OnMouseClicked(bool isMouseDown)
    {
        if (OnMouseClickedDelegate != null)
        {
            OnMouseClickedDelegate(isMouseDown);
        }
    }

    public void OnRopeAction(bool createRope, Vector3 mousePos)
    {
        if (OnRopeActionDelegate != null)
        {
            OnRopeActionDelegate(createRope, mousePos);
        }
    }
}
