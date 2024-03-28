using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPrefab; // Reference to the pause menu prefab
    private GameObject pauseMenuInstance; // Instance of the pause menu prefab
    //public GameObject bg;
    public string playOrPause;
    public Camera mainCamera;

    private float pauseStartTime; // Time when pause menu is activated
    private float pauseDuration; // Duration of pause

    private void Update()
    {
        // Check if the user presses the "Escape" key to pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        // If the pause menu is not active, activate it
        if (pauseMenuInstance == null)
        {
            PauseGame();
        }
        // If the pause menu is active, deactivate it
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {

     
        
            Time.timeScale = 0f; // Pause the game by setting the time scale to 0

        // Calculate the position to instantiate the prefab
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane); // Center of the viewport
        Vector3 worldPosition = mainCamera.ViewportToWorldPoint(viewportCenter);

        // Instantiate the pause menu prefab at the calculated position
        pauseMenuInstance = Instantiate(pauseMenuPrefab, worldPosition, Quaternion.identity);
        pauseMenuInstance.SetActive(true);


        Debug.Log("prefab created");
            // Record the time when the pause menu is activated
            pauseStartTime = Time.realtimeSinceStartup;
        
        

        if(playOrPause == "play")
        {
            Time.timeScale = 1f; // Resume the game by setting the time scale to 1

            // Destroy the pause menu prefab
            Destroy(pauseMenuInstance);
          
            // Calculate the duration of the pause
            pauseDuration = Time.realtimeSinceStartup - pauseStartTime;

            // Store the pause duration in PlayerPrefs or any other desired storage method
            PlayerPrefs.SetFloat("PauseDuration", pauseDuration);
            Debug.Log("pause durati" + pauseDuration);

        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game by setting the time scale to 1

        // Destroy the pause menu prefab
        Destroy(pauseMenuInstance);
 

        // Calculate the duration of the pause
        pauseDuration = Time.realtimeSinceStartup - pauseStartTime;

        // Store the pause duration in PlayerPrefs or any other desired storage method
        PlayerPrefs.SetFloat("PauseDuration", pauseDuration);
        Debug.Log("pause durati" + pauseDuration);
    }


    public void Replay()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        // Quit the application
        Application.Quit();
    }
}

