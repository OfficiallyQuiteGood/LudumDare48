using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCondition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator ReloadToCheckpointCoroutine(float delay)
    {
        Debug.Log("Before loading scene");

        // Wait x amount of seconds
        yield return new WaitForSeconds(delay);

        Debug.Log("after loading scene");

        // Load game again
        GameObject player = GameObject.Find("Player");
        GameObject camera = GameObject.Find("Follow Camera");
        
        GameObject.Find("Player").GetComponent<MainCharacter>().loadCheckPoint();
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
        GameObject.Find("Canvas").GetComponent<UIController>().AppearScreen();
        
        //SceneManager.LoadScene("TreeScene");
    }

    public void ReloadToCheckpoint()
    {
        StartCoroutine(ReloadToCheckpointCoroutine(6f));
    }
}
