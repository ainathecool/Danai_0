using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement; 
using Firebase.Database;
using System.Collections;

public class GuardianChildList : MonoBehaviour
{
    public TextMeshProUGUI GuardianNameText;
    public TextMeshProUGUI MakeAProfileFirst;
    public TextMeshProUGUI ChildName;
    public Image Avatar;
    public Button ChildSec;
    public TextMeshProUGUI text;

    //public Transform ChildProfilesList; // Reference to the Vertical Layout Group.
    //public GameObject ChildProfileRowPrefab; // Prefab for child profile rows.

    private DatabaseReference databaseReference;
   
   
    private void Start()
    {
        // Initialize the Firebase Realtime Database reference.
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        ChildName.text = null;
        Avatar.sprite = null;


        // Fetch and display the guardian's name.
        FetchAndDisplayGuardianName();

        // Fetch and display child profiles.
        FetchAndDisplayChildProfiles();
    }

    private async void FetchAndDisplayGuardianName()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DataSnapshot dataSnapshot = await databaseReference.Child("guardians").Child(userId).Child("name").GetValueAsync();
        string guardianName = (string)dataSnapshot.Value;
        GuardianNameText.text = "Hello " + guardianName;
    }

    private async void FetchAndDisplayChildProfiles()
    {
        TextMeshProUGUI buttonText = ChildSec.GetComponentInChildren<TextMeshProUGUI>();

        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DataSnapshot dataSnapshot = await databaseReference.Child("childProfiles").Child(userId).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            // Child profiles exist; iterate through them and create UI elements.
            foreach (var childSnapshot in dataSnapshot.Children)
            {
                // Instantiate a child profile row from the prefab.
              //  GameObject childProfileRow = Instantiate(ChildProfileRowPrefab, ChildProfilesList);

                // Set the child's name on the row.
                string childName = (string)childSnapshot.Child("Name").Value;
                ChildName.text = childName;
               // childProfileRow.GetComponentInChildren<TextMeshProUGUI>().text = childName;

                // Fetch the avatar name from the database.
                string avatarName = (string)childSnapshot.Child("Avatar").Value;

                // Load the corresponding sprite from Resources/Avatars/.
                Sprite avatarSprite = Resources.Load<Sprite>("Avatars/" + avatarName);
                Avatar.sprite = avatarSprite;
                buttonText.text = "yes";
           
              
            }
        }
        else
        {
            // No child profiles exist; display a message.
            GuardianNameText.text = "No child profiles exist.";
            buttonText.text = "no";
        }

        
    }

     
    public void GoToChildSection()
    {
        if(text.text == "no")
        {
            MakeAProfileFirst.text = "Make a child profile first, so you can visit Child Section.";
        }
        else if (text.text == "yes") { SceneManager.LoadScene("ChildWhoAreYou"); }
    }
    public void GoToAddChildInfoScene()
    {
        SceneManager.LoadScene("AddChildProfiles"); // Replace with your actual scene name.
    }
}

