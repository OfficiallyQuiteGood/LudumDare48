using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    // Variables describing the various world settings
    public float gravity = 9.81f;

    public GameObject playerPrefab;
    public CheckpointList CheckpointList;

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(RandomClip());
        //Debug.Log("started");

    }

    AudioClip RandomClip()
    {
        return audioClipArray[Random.Range(0, audioClipArray.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void InstantiatePlayer(Vector3 lastpos, int health)
    // {
    //     Debug.Log("instantiae pos: "+lastpos);
    //     StartCoroutine(InstantiatePlayerAfterDelay(lastpos, new Quaternion()));
        

    // IEnumerator InstantiatePlayerAfterDelay(Vector3 lastpos1, Quaternion lastrot1)
    // {
    //     yield return new WaitForSeconds(2);
    //     Debug.Log("Instantiating Player");
    //     Debug.Log("instantiae pos: "+lastpos1);
    //     GameObject newPlayer = Instantiate(playerPrefab, lastpos1, lastrot1);
    //     Debug.Log("Instantiating Player - should be in game");
    //     //set new follow
    //     Follow followCam = GameObject.Find("Follow Camera").GetComponent<Follow>();
    //     followCam.player = newPlayer;
    // }
    // }

    public void GameOver()
    {

    }
}
