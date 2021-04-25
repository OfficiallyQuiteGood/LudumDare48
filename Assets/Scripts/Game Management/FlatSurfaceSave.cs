using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Created By: Fred
public class FlatSurfaceSave : MonoBehaviour
{
    // Start is called before the first frame update
    MainCharacter player;
    void Start()
    {
        player = gameObject.GetComponentInParent<MainCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        //if exiting tile
        if(hitInfo is TilemapCollider2D) player.SetLastLegalPosition();
    }
}
