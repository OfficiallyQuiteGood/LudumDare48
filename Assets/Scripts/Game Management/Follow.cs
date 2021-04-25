using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Get reference to character
    public GameObject player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.position += offset;
        //transform.SetParent(player.transform);
    }

    public void PausePan(float delay)
    {
        StartCoroutine(PausePanDelay(delay));
    }

    IEnumerator PausePanDelay(float delay)
    {
        paused = true;
        yield return new WaitForSeconds(delay);
        paused = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null && !paused)
        {
            Vector3 desiredPosition = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y,transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else
        {
            player = GameObject.Find("Player");
        }
    }
}
