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
            (18f - (17 - player.transform.position.x)  * 0.8f) * 1f,
            (-35f + (player.transform.position.y * 0.8f)) * 0.2f,
            this.transform.position.z
        );
    }
}
