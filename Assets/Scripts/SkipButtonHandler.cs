using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonHandler : MonoBehaviour
{
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method called.");
    }

    public void OnSkipButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }


 
}
