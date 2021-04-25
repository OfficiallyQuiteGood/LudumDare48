using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(
            camera.transform.position.x * 8f / 9f + 1f,
            camera.transform.position.y * 1f / 2f - 15f,
            this.transform.position.z
        );
    }
}
