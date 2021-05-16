using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    public GameObject timerText;
    public Toggle toggleUI;
    public bool timerIsEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        toggleUI.onValueChanged.AddListener(delegate {
            timerIsEnabled = !timerIsEnabled;
            if(!timerIsEnabled) timerText.GetComponent<CanvasGroup>().alpha = 0;
            else
            {
                timerText.GetComponent<CanvasGroup>().alpha = 1;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
