using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointList : MonoBehaviour
{
    // Start is called before the first frame update
    public Stack<Checkpoint> checkpoints;
    void Start()
    {
        checkpoints = new Stack<Checkpoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCheckPoint(Checkpoint checkpoint)
    {
        Debug.Log("addedCheckPoint");
        checkpoints.Push(checkpoint);
    }

    public Checkpoint getCheckpoint()
    {
        return checkpoints.Peek();
    }
}
