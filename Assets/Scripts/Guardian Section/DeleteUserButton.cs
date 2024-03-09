using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;

public class DeleteUserButton : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public Button backButton;

    public void DeleteUserOnClick()
    {
        // Get the default instance of FirebaseAuth
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        // Get the current user
        FirebaseUser user = auth.CurrentUser;

        if (user != null)
        {
            // Delete the user
            user.DeleteAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("DeleteUserAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("DeleteUserAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User deleted successfully.");

                // Show the pop-up message
                ShowPopup("Account deleted successfully!");
            });
        }
        else
        {
            Debug.LogWarning("No user is currently signed in.");
        }
    }

    void ShowPopup(string message)
    {
        popupText.text = message;
        backButton.gameObject.SetActive(true); // Activate the back button
        // You can also activate a GameObject containing a pop-up UI element here.
    }

    public void BackButtonOnClick()
    {
        // Here you can implement the functionality to go back to the home screen.
        // For example, you can load a new scene or disable the pop-up UI element.
        Debug.Log("Back button clicked!");
    }
}
