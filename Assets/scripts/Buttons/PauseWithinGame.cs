using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWithinGame : MonoBehaviour
{
    public GameObject pause;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void OnPauseButtonClicked()
    {
        pause.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {
        pause.SetActive(false);
    }

    public void Replay()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
