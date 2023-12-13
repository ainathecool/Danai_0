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

public class GuardianSignUp : MonoBehaviour
{
    public TextMeshProUGUI NameInput;
    public TextMeshProUGUI EmailInput;
    public TextMeshProUGUI PasswordInput;
    public TextMeshProUGUI ConfirmPasswordInput;

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
            Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
    }

    public async void OnSignUpButtonClicked()
    {
        string name = NameInput.text;
        string email = EmailInput.text;
        string password = PasswordInput.text;
        string confirmPassword = ConfirmPasswordInput.text;

        if (password != confirmPassword)
        {
            Debug.LogError("Passwords do not match.");
            return;
        }

        try
        {
            AuthResult authResult = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            FirebaseUser newUser = authResult.User;
            string userId = newUser.UserId;

            Debug.Log("Sign-up successful! User ID: " + userId);

            // Create the guardian profile in Firebase Realtime Database.
            await CreateGuardianProfile(userId, name);

            // Signup successful, now load the login scene.
            SceneManager.LoadScene("GuardianLogin"); // Replace with your actual login scene name.

            Debug.Log("Sign-up successful! User ID: " + userId);
        }
        catch (Exception e)
        {
            Debug.LogError("Profile creation failed failed: " + e.Message);
        }
    }

    private async Task CreateGuardianProfile(string userId, string guardianName)
    {
        Debug.Log("in profile creation");
        // Get a reference to the Firebase Realtime Database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        // Create a new child node under "guardians" with the user ID as the key and the guardian's name as the value.
        await reference.Child("guardians").Child(userId).Child("name").SetValueAsync(guardianName);
    }
}
