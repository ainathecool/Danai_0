using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Database;

public class SetChildAvatar : MonoBehaviour
{
    public Image avatarDisplay; // Reference to the circle where the chosen avatar is displayed.
    public Button[] avatarButtons; // Reference to the avatar selection buttons.
    public Button nextButton;

    private int selectedAvatarIndex = -1; // Store the index of the selected avatar.
    private DatabaseReference databaseReference;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        // Disable the "Next" button at the start.
        nextButton.interactable = false;
    }

    public void OnAvatarButtonClicked(int avatarIndex)
    {
        // An avatar selection button is clicked.

        // Check if the same avatar button was clicked again (deselect).
        if (avatarIndex == selectedAvatarIndex)
        {
            // Deselect the current avatar.
            avatarDisplay.sprite = null;
            selectedAvatarIndex = -1;

            // Disable the "Next" button.
            nextButton.interactable = false;
        }
        else
        {
            // A different avatar is selected.
            selectedAvatarIndex = avatarIndex;

            // Display the selected avatar in the circle.
            avatarDisplay.sprite = avatarButtons[avatarIndex].image.sprite;

            // Enable the "Next" button.
            nextButton.interactable = true;
        }
    }

    public void OnNextButtonClicked()
    {
        // Ensure an avatar is selected.
       
        {
            // Store the selected avatar index and go to the next scene.
            PlayerPrefs.SetString("ChildAvatar", avatarDisplay.sprite.name);
            Debug.Log("acatar chosen: " + PlayerPrefs.GetString("ChildAvatar"));
            SceneManager.LoadScene("SetChildPartner"); // Replace with your actual scene name.
        }
    }

    public void UpdateChildAvatar()
    {
        PlayerPrefs.SetString("ChildAvatar", avatarDisplay.sprite.name);
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string childId = PlayerPrefs.GetString("LoggedInChild");

        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(childId))
        {
            string avatarName = PlayerPrefs.GetString("ChildAvatar");

            if (!string.IsNullOrEmpty(avatarName))
            {
                // Construct the path to update the avatar value in the database
                string path = $"childProfiles/{userId}/profiles/{childId}/Avatar";

                // Update the avatar value in the Firebase Realtime Database
                databaseReference.Child(path).SetValueAsync(avatarName).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("Avatar updated successfully!");
                    }
                    else if (task.IsFaulted)
                    {
                        Debug.LogError("Failed to update avatar: " + task.Exception);
                    }
                });
            }
            else
            {
                Debug.LogWarning("Avatar name is empty!");
            }
        }
        else
        {
            Debug.LogWarning("User ID or child ID is empty!");
        }
    }

}
