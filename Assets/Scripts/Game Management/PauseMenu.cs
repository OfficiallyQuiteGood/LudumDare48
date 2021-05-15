using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    bool isPaused = false;
    void Start()
    {
        setActiveChildren(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            if(isPaused) ResumeGame();
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame ()
    {
        Time.timeScale = 0;
        setActiveChildren(true);
        isPaused = true;
    }

    void ResumeGame ()
    {
        Time.timeScale = 1;
        setActiveChildren(false);
        isPaused = false;
    }

    void setActiveChildren(bool isActive)
    {
        for(int i=0; i<gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }
}
