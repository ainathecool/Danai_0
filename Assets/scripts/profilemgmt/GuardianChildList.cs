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
  //  public TextMeshProUGUI MakeAProfileFirst;
    public TextMeshProUGUI ChildName1, ChildName2, ChildName3;
    public Image Avatar1, Avatar2, Avatar3;
    public Button ChildSec1, ChildSec2, ChildSec3;
  //  public TextMeshProUGUI text;
    public Button add;

    //public Transform ChildProfilesList; // Reference to the Vertical Layout Group.
    //public GameObject ChildProfileRowPrefab; // Prefab for child profile rows.

    private DatabaseReference databaseReference;
   
   
    private void Start()
    {
        // Initialize the Firebase Realtime Database reference.
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        ChildName1.text = null;
        Avatar1.sprite = null;
        ChildName2.text = null;
        Avatar2.sprite = null;
        ChildName3.text = null;
        Avatar3.sprite = null;
        ChildSec1.gameObject.SetActive(false);
        ChildSec2.gameObject.SetActive(false);
        ChildSec3.gameObject.SetActive(false);

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

    /*  private async void FetchAndDisplayChildProfiles()
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
        else if (text.text == "yes") 
        {
            
            SceneManager.LoadScene("ChildWhoAreYou");
        }
    }

      */

    private async void FetchAndDisplayChildProfiles()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DataSnapshot dataSnapshot = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").GetValueAsync();

        if (dataSnapshot.Exists)
        {
            int childCount = 0;

            // Iterate through child profiles and populate UI placeholders
            foreach (var childSnapshot in dataSnapshot.Children)
            {
                string childName = (string)childSnapshot.Child("Name").Value;
                string avatarName = (string)childSnapshot.Child("Avatar").Value;
                string childId = childSnapshot.Key;

                // Load avatar sprite
                Sprite avatarSprite = Resources.Load<Sprite>("Avatars/" + avatarName);

                // Update UI placeholders based on child count
                switch (childCount)
                {
                    case 0:
                        ChildName1.text = childName;
                        Avatar1.sprite = avatarSprite;
                        ChildSec1.gameObject.SetActive(true);
                        break;
                    case 1:
                        ChildName2.text = childName;
                        Avatar2.sprite = avatarSprite;
                        ChildSec2.gameObject.SetActive(true);
                        break;
                    case 2:
                        ChildName3.text = childName;
                        Avatar3.sprite = avatarSprite;
                        ChildSec3.gameObject.SetActive(true);
                        add.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }

                // Store child profile ID in PlayerPrefs
                PlayerPrefs.SetString("ChildProfile_" + childCount, childId);

                childCount++;

                // If child count exceeds 3, break loop
                if (childCount >= 3)
                    break;
            }


        }
       
    }

    public void OnImpTrackingButtonClicked(string name)
    {
        if(name == "imp1")
        {
            string Child01ID = PlayerPrefs.GetString("ChildProfile_" + 0);
            PlayerPrefs.SetString("Child01ImpTracking", Child01ID);
           // SceneManager.LoadScene(""); //add maaidah wala scene idhar
        }
        else if(name == "imp2")
        {
            string Child02ID = PlayerPrefs.GetString("ChildProfile_" + 1);
            PlayerPrefs.SetString("Child02ImpTracking", Child02ID);
            // SceneManager.LoadScene(""); //add scene idhar
        }
        else if (name == "imp3")
        {
            string Child03ID = PlayerPrefs.GetString("ChildProfile_" + 2);
            PlayerPrefs.SetString("Child03ImpTracking", Child03ID);
            // SceneManager.LoadScene(""); //add scene idhar
        }
    }

    public void OnChildSectionButtonClicked(string name)
    {
        if(name == "ChildSection1")
        {
            string Child01ID = PlayerPrefs.GetString("ChildProfile_" + 0);
            TextMeshProUGUI buttonText1 = ChildSec1.GetComponentInChildren<TextMeshProUGUI>();
            buttonText1.text = Child01ID;
            SceneManager.LoadScene("ChildWhoAreYou");
        }
        else if(name == "ChildSection2")
        {
            string Child02ID = PlayerPrefs.GetString("ChildProfile_" + 1);
            TextMeshProUGUI buttonText2 = ChildSec2.GetComponentInChildren<TextMeshProUGUI>();
            buttonText2.text = Child02ID;
            SceneManager.LoadScene("ChildWhoAreYou02");
        }
        else if(name == "ChildSection3")
        {
            string Child03ID = PlayerPrefs.GetString("ChildProfile_" + 2);
            TextMeshProUGUI buttonText3 = ChildSec3.GetComponentInChildren<TextMeshProUGUI>();
            buttonText3.text = Child03ID;
            SceneManager.LoadScene("ChildWhoAreYou03");
        }
    }

    public void GoToAddChildInfoScene()
    {
        SceneManager.LoadScene("AddChildProfiles"); // Replace with your actual scene name.
    }
}

