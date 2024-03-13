using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;

public class DeleteUserButton : MonoBehaviour
{

    public GameObject panelToUnhide;
    public Button anotherButtonToHide;
    public TextMeshProUGUI textMeshProObjectToHide;

    public void DeleteUserOnClick()
    {
        // Get the default instance of FirebaseAuth
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        // Get the current user
        FirebaseUser user = auth.CurrentUser;
        // Show the pop-up pannel
        // Unhide the panel
        panelToUnhide.SetActive(true);

        // Hide the clicked button
        gameObject.SetActive(false);

        // Hide the other button
        anotherButtonToHide.gameObject.SetActive(false);

        // Hide the TextMeshPro object
        textMeshProObjectToHide.gameObject.SetActive(false);
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

                
            });
        }
        else
        {
            Debug.LogWarning("No user is currently signed in.");
        }
    }

}
