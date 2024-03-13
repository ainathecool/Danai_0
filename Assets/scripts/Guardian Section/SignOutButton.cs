using UnityEngine;
using Firebase.Auth;

public class SignOutButton : MonoBehaviour
{
    public void SignOutOnClick()
    {
        // Get the default instance of FirebaseAuth
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        // Sign out the current user
        auth.SignOut();

        Debug.Log("User signed out successfully!");
    }
}
