using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Database;

public class SplashScreenController : MonoBehaviour
{
    public string loginSceneName;
    public string childProfilesSceneName;
    public float delayBeforeLoad;

    private void Start()
    {
        //FirebaseDatabase.GetInstance().SetPersistenceEnabled(true);
        //FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);
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
            Debug.Log("user logged in " + FirebaseAuth.DefaultInstance.CurrentUser.UserId);
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);
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
