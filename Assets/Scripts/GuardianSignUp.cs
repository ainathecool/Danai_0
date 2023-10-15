using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using System.Threading.Tasks; // Add this using statement to work with async/await.
using TMPro;


public class GuardianSignUp : MonoBehaviour
{
    public TextMeshProUGUI NameInput;
    public TextMeshProUGUI EmailInput;
    public TextMeshProUGUI PasswordInput;
    public TextMeshProUGUI ConfirmPasswordInput;

    Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

    private async void Awake()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.GetAuth(app);
            Debug.Log("Firebase connected");
        }
        else
        {
            // Handle the error or show a message if Firebase dependencies are not available.
            Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
    }


    public async void OnSignUpButtonClicked() // Note the 'async' keyword here.
    {
        string name = NameInput.text;
        string email = EmailInput.text;
        string password = PasswordInput.text;
        string confirmPassword = ConfirmPasswordInput.text;

        // Check if password and confirm password match
        if (password != confirmPassword)
        {
            // Show an error message to the user indicating that passwords do not match.
            Debug.LogError("Passwords do not match.");
            return;
        }

        try
        {
            Debug.Log("Firebase connected");
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
            });


        }
        catch (Exception e)
        {
            // Handle the sign-up failure and show an error message.
            Debug.LogError("Sign-up failed: " + e.Message);
        }
    }


}
