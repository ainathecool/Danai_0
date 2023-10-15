using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using System.Threading.Tasks; // Add this using statement to work with async/await.
using TMPro;


public class GuardianLogin : MonoBehaviour
{
 
    public TextMeshProUGUI EmailInput;
    public TextMeshProUGUI PasswordInput;


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

    public async void OnLoginButtonClicked()
    {
        string email = EmailInput.text;
        string password = PasswordInput.text;

        try
        {
            await auth.SignInWithEmailAndPasswordAsync(email, password);
            Debug.Log("User logged in successfully!");
        }
        catch (Exception e)
        {
            // Handle login failure and show an error message.
            Debug.LogError("Login failed: " + e.Message);
        }
    }





}
