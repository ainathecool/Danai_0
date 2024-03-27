using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement; // Add this using statement for SceneManager.
using Firebase.Database; // Add this using statement for Firebase Realtime Database.

public class GuardianLogin : MonoBehaviour
{
    public TextMeshProUGUI EmailInput;
    public TextMeshProUGUI PasswordInput;
    public TextMeshProUGUI LoginError;

    private FirebaseAuth auth;

    private async void Awake()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.GetAuth(app);
        }
        else
        {
            // Handle the error or show a message if Firebase dependencies are not available.
            Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
    }

    public async void OnCPLoginButtonClicked()
    {
        string email = EmailInput.text;
        string password = PasswordInput.text;
        Debug.Log("in login fucntion");

        try
        {
            Debug.Log("trying");
            await auth.SignInWithEmailAndPasswordAsync(email, password);
            Debug.Log("User logged in successfully!");
            SceneManager.LoadScene("ChangePassword");
        }
        catch (Exception e)
        {
            // Handle login failure and show an error message.
            Debug.LogError("Login failed: " + e.Message);
            LoginError.text = e.Message + " Try Again!";
        }
    }

    public async void OnLoginButtonClicked()
    {
        string email = EmailInput.text;
        string password = PasswordInput.text;
        Debug.Log("in login fucntion");

        try
        {
            Debug.Log("trying");
            await auth.SignInWithEmailAndPasswordAsync(email, password);
            Debug.Log("User logged in successfully!");
            SceneManager.LoadScene("ChildProfiles");
        }
        catch (Exception e)
        {
            // Handle login failure and show an error message.
            Debug.LogError("Login failed: " + e.Message);
            LoginError.text = e.Message + " Try Again!";
        }
    }





}
