using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointList checkpointList;
    bool isAdded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        MainCharacter player = hitInfo.GetComponent<MainCharacter>();
        if(player!=null && !isAdded)
        {
            checkpointList.AddCheckPoint(this);
            isAdded = true;
        }
    }
}
