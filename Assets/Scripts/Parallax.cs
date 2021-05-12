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
            camera.transform.position.x * 13f / 14f + 1f,
            camera.transform.position.y * 2f / 3f - 15f,
            this.transform.position.z
        );

        //9 - > -90
        float colorvar = Mathf.Clamp((camera.transform.position.y + 50f) / 50f, 0.3f, 1.0f);
        
        this.GetComponent<SpriteRenderer>().color = 
            new Color(colorvar, colorvar, colorvar, 1);
    }
}
