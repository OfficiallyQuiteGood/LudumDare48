using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCutscene : MonoBehaviour
{
    // Variables

    // Ending title object
    public GameObject endingTitle;
    public GameObject followCamera;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On trigger function
    public void OnWin()
    {
        // Pause timer
        GameObject.Find("Timer").GetComponent<Timer>().isPaused = true;
        // Set title location
        endingTitle.transform.position = followCamera.transform.position + offset;
        endingTitle.transform.position = new Vector3(endingTitle.transform.position.x, endingTitle.transform.position.y, 0);
    }
}
