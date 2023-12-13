using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // This is needed to work with scene management.

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene you want to load.

    public float waitTime;
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitTime); // Wait for 5 seconds.

        // Load the specified scene.
        SceneManager.LoadScene(sceneToLoad);
    }
}
