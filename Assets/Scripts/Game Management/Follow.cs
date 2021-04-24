using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Get reference to character
    public GameObject player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;
        transform.SetParent(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
