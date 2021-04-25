using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(
            (16f + (player.transform.position.x * 0.2f)) * 0.7f,
            (-13f + (player.transform.position.y * 0.8f)) * 0.4f,
            this.transform.position.z
        );
    }
}
