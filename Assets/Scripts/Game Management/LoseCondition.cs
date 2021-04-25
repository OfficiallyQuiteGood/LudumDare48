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

    protected IEnumerator ReloadGameCoroutine()
    {
        Debug.Log("Before loading scene");

        // Wait x amount of seconds
        yield return new WaitForSeconds(1.5f);

        Debug.Log("after loading scene");

        // Load game again
        SceneManager.LoadScene("TreeScene");
    }

    public void ReloadGame()
    {
        StartCoroutine(ReloadGameCoroutine());
    }
}
