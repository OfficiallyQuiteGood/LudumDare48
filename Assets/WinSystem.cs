using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSystem : MonoBehaviour
{
    // Variables

    // Minimush prefab
    public GameObject miniMushPrefab;
    public int maxNumMiniMush = 20;
    public float spawnFreq = 2.0f;
    public float spawnDelay = 1.5f;
    public float minDistX = 0.5f;
    public float maxDistX = 1.0f;
    public float minDistY = 0.5f;
    public float maxDistY = 1.0f;
    public float minDistZ = 0.5f;
    public float maxDistZ = 1.0f;
    public float minDistXNeg = -0.5f;
    public float maxDistXNeg = -1.0f;
    public float minDistYNeg = -0.5f;
    public float maxDistYNeg = -1.0f;
    public float minDistZNeg = -0.5f;
    public float maxDistZNeg = -1.0f;
    public float thresholdX = 30.0f;
    public float thresholdY = 30.0f;
    public float thresholdZ = 30.0f;
    public float incX = 0.5f;
    public float incY = 0.5f;
    public float incZ = 0.5f;

    // Sprite
    private Sprite sprite;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(StartSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator StartSpawn()
    {
        // Wait then spawn minimush
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnMiniMush());
    }

    // Enumerator for spawning them
    protected IEnumerator SpawnMiniMush()
    {
        // Curr num minimush
        int currNum = 0;
        while (currNum < maxNumMiniMush)
        {
            // Instantiate new mush
            Instantiate(miniMushPrefab, GetRandomLocationInRadius(transform.position, true, true, currNum), Quaternion.identity);

            // Wait for x amount of seconds before spawning anew
            yield return new WaitForSeconds(spawnFreq);

            // Add 1 to curr num
            currNum++;
        }
    }

    Vector3 GetRandomLocationInRadius(Vector3 center, bool clampY, bool clampZ, int currNum)
    {
        // Vector to return
        Vector3 temp = transform.position;

        // Calculate random range
        float distX = Random.Range(minDistX, maxDistX);
        float distY = Random.Range(minDistY, maxDistY);
        float distZ = Random.Range(minDistZ, maxDistY);
        float distXNeg = Random.Range(minDistXNeg, maxDistXNeg);
        float distYNeg = Random.Range(minDistYNeg, maxDistYNeg);
        float distZNeg = Random.Range(minDistZNeg, maxDistYNeg);
        
        // Pick random dir
        int dirX = Random.Range(0, 2) == 0 ? 1 : -1;
        int dirY = Random.Range(0, 2) == 0 ? 1 : -1;
        int dirZ = Random.Range(0, 2) == 0 ? 1 : -1;

        // Update accordingly
        if (dirX > 0)
        {
            minDistX += incX;
            maxDistX += incX;
        }
        else
        {
            minDistXNeg -= incX;
            maxDistXNeg -= incX;
        }
        if (dirY > 0)
        {
            minDistY += incY;
            maxDistY += incY;
        }
        else
        {
            minDistYNeg -= incY;
            maxDistYNeg -= incY;
        }
        if (dirZ > 0)
        {
            minDistZ += incZ;
            maxDistZ += incZ;
        }
        else
        {
            minDistZNeg -= incZ;
            maxDistZNeg -= incZ;
        }

        // Debug.Log
        Debug.Log("minDistX = " + minDistX + " maxDistX = " + maxDistX + " minDistXNeg = " + minDistXNeg + " maxDistXNeg = " + maxDistXNeg);
        Debug.Log("distX = " + distX + " distXNeg = " + distXNeg);

        // return vector at that dist
        return temp + new Vector3(dirX > 0 ? distX : distXNeg, clampY ? -0.3f : distY * dirY, clampZ ? 0 : distZ * dirZ);
    }
}
