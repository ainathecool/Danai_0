using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;

public class SplashScreenController : MonoBehaviour
{
    public string loginSceneName;
    public string childProfilesSceneName;
    public float delayBeforeLoad;

    private void Start()
    {
        // Start the coroutine to wait for a few seconds before loading the next scene.
        StartCoroutine(LoadNextSceneWithDelay());
    }

    private System.Collections.IEnumerator LoadNextSceneWithDelay()
    {
        // Wait for the specified delay.
        yield return new WaitForSeconds(delayBeforeLoad);

        // Check if a user is currently logged in.
        if (IsUserLoggedIn())
        {
            // User is logged in, load the Child Profiles scene.
            SceneManager.LoadScene(childProfilesSceneName);
        }
        else
        {
            // No user is logged in, load the Login scene.
            SceneManager.LoadScene(loginSceneName);
        }
    }

    private bool IsUserLoggedIn()
    {
        // Check if there is a Firebase user currently authenticated.
        return FirebaseAuth.DefaultInstance.CurrentUser != null;
    }
}
