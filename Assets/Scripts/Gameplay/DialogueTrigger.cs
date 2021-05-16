using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject interactButton;
    public GameObject textToDisplay;
    // Start is called before the first frame update
    void Start()
    {
        textToDisplay.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void displayText()
    {
        textToDisplay.GetComponent<SpriteRenderer>().enabled = true;
    }

    void hideText()
    {
        textToDisplay.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
	{
        Debug.Log("Enter");
        MainCharacter player = hitInfo.GetComponent<MainCharacter>();
        if(player!=null)
        {
            Debug.Log("display text");
            displayText();
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
	{
        Debug.Log("Exit");
        MainCharacter player = hitInfo.GetComponent<MainCharacter>();
        if(player!=null)
        {
            hideText();
        }
    }
}
