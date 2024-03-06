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

    private void Start()
    {
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
}
