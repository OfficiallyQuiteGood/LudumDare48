using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        MainCharacter player = collider.GetComponent<MainCharacter>();
        if(player!=null)
        {
            Debug.Log("reset");
            player.resetPlayerPosition(0f);
        }
    }
}
