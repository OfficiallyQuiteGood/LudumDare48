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
        if(hitInfo is TilemapCollider2D) 
        {
            if(GameObject.Find("Player").GetComponent<MainCharacter>().horizontalMove > 0)
            {
                Vector3 posBack = gameObject.transform.position + new Vector3(-1f,0,0);
                Debug.Log("new posback at: "+posBack);
                player.SetLastLegalPosition(posBack);
            }
            else if(GameObject.Find("Player").GetComponent<MainCharacter>().horizontalMove <0)
            {
                Vector3 posForward = gameObject.transform.position + new Vector3(1f,0,0);
                Debug.Log("new posForward at: "+posForward);
                player.SetLastLegalPosition(posForward);
            }
            
            // set legal position backwards or forwards if its valid
            // Vector3 posBack = gameObject.transform.position + new Vector3(-1,-0.5f,0);
            // Vector3 posForward = gameObject.transform.position + new Vector3(1,-0.5f,0);
            // posBack.z = hitInfo.transform.position.z;
            // posForward.z = hitInfo.transform.position.z;
            // if(hitInfo.bounds.Contains(posBack))
            // {
            //     posBack.y = gameObject.transform.position.y;
            //     posBack.z = gameObject.transform.position.z;
            //     Debug.Log("new posback at: "+posBack);
            //     player.SetLastLegalPosition(posBack);
            // } 
            // else if(hitInfo.bounds.Contains(posForward))
            // {
            //     posForward.y = gameObject.transform.position.y;
            //     posForward.z = gameObject.transform.position.z;
            //     Debug.Log("new posforward at: "+posForward);
            //     player.SetLastLegalPosition(posForward);
            // }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        //if exiting tile
        //if(hitInfo is TilemapCollider2D) player.SetLastLegalPosition(gameObject.transform.position);
        //if(hitInfo is TilemapCollider2D) player.SetLastLegalPosition(gameObject.transform.position);
    }
}
