using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    // Variables describing the various world settings
    public float gravity = 9.81f;

    public GameObject playerPrefab;
    public CheckpointList CheckpointList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiatePlayer()
    {
        Debug.Log("Instantiating Player");
        //yield return new WaitForSeconds(2);
        Checkpoint checkpoint = CheckpointList.getCheckpoint();
        Debug.Log(checkpoint.transform);
        GameObject newPlayer = Instantiate(playerPrefab, checkpoint.transform.position, checkpoint.transform.rotation);
        Debug.Log("Instantiating Player - should be in game");
        //set new follow
        Follow followCam = GameObject.Find("Follow Camera").GetComponent<Follow>();
        followCam.player = newPlayer;
    }
}
