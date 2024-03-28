using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement; // Add this using statement for SceneManager.
using Firebase.Database;

public class WhoAreYou02 : MonoBehaviour
{

    public TextMeshProUGUI ChildName;
    public Image Avatar;
    //public Transform ChildProfilesList; // Reference to the Vertical Layout Group.
    //public GameObject ChildProfileRowPrefab; // Prefab for child profile rows.

    private DatabaseReference databaseReference;

    private void Start()
    {
        // Initialize the Firebase Realtime Database reference.
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        ChildName.text = null;
        Avatar.sprite = null;

        // Fetch and display child profiles.
        FetchAndDisplayChildProfiles();
    }



    private async void FetchAndDisplayChildProfiles()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string Child02ID = PlayerPrefs.GetString("ChildProfile_" + 1);
        DataSnapshot dataSnapshot = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(Child02ID).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            // Child profiles exist; iterate through them and create UI elements.

            string childName = (string)dataSnapshot.Child("Name").Value;
            ChildName.text = childName;

            string avatarName = (string)dataSnapshot.Child("Avatar").Value;
            Sprite avatarSprite = Resources.Load<Sprite>("Avatars/" + avatarName);
            Avatar.sprite = avatarSprite;

            /*  foreach (var childSnapshot in dataSnapshot.Children)
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

              }*/
        }

    }


    public void GoToChildLoginScene()
    {
        SceneManager.LoadScene("ChildLogin");
    }
}


