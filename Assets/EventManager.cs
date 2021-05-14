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
    public event Action<bool, Vector3> OnRopeActionDelegate;
    public event Action<Vector2> OnReleasedRopeDelegate;

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

    public void OnRopeAction(bool createRope, Vector3 mousePos)
    {
        if (OnRopeActionDelegate != null)
        {
            OnRopeActionDelegate(createRope, mousePos);
        }
    }

    public void OnReleasedRope(Vector2 releaseAcc)
    {
        if (OnReleasedRopeDelegate != null)
        {
            OnReleasedRopeDelegate(releaseAcc);
        }
    }
}
