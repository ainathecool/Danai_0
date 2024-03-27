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

public class SetChildPartner : MonoBehaviour
{
    public Button[] partnerButtons; // Reference to the buttons for selecting a child partner.
    public Button saveProfileButton;
    public TextMeshProUGUI statusText;

    private string selectedPartner; // Store the selected child partner.
    private DatabaseReference databaseReference;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Hide the save button initially.
        saveProfileButton.interactable = false;
    }

    public void OnPartnerButtonClicked(int partnerIndex)
    {
        // A button for selecting a child partner is clicked.
        selectedPartner = partnerButtons[partnerIndex].GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log("Selected partner: " + selectedPartner);
        PlayerPrefs.SetString("Partner", selectedPartner);

        // Enable the save button.
        saveProfileButton.interactable = true;
    }

    public async void OnSaveProfileButtonClicked()
    {
        // Retrieve child information saved in PlayerPrefs.
        string childName = PlayerPrefs.GetString("ChildName");
        string childBirthday = PlayerPrefs.GetString("ChildBirthday");
        string childGender = PlayerPrefs.GetString("ChildGender");
        string childPIN = PlayerPrefs.GetString("PIN");
        string childAvatar = PlayerPrefs.GetString("ChildAvatar");
        string partner = PlayerPrefs.GetString("Partner");

        // Save the child profile to the Firebase Realtime Database.
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string childId = Guid.NewGuid().ToString(); // Generate a unique ID for the child profile.

        childProfiles childProfile = new childProfiles
        {
            Name = childName,
            Birthday = childBirthday,
            Gender = childGender,
            PIN = childPIN,
            Avatar = childAvatar,
            Partner = partner// Set the selected child partner.
        };
        Debug.Log(partner);

        try
        {
            await databaseReference.Child("childProfiles").Child(userId).Child(childId).SetRawJsonValueAsync(JsonUtility.ToJson(childProfile));
            statusText.text = "Child profile saved successfully!";
            SceneManager.LoadScene("ChildProfiles");
        }
        catch (Exception e)
        {
            statusText.text = "Failed to save child profile: " + e.Message;
        }
    }
}
