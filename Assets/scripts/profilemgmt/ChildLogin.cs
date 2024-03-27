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

public class ChildLogin : MonoBehaviour
{
    public Image[] pinCircles; // Reference to the 4 circles where the PIN is displayed.
    public Button[] pinPad; // Reference to the icons for setting the PIN.
    public TextMeshProUGUI pinError;


    private string[] currentPin; // Stores the current PIN as icons.
    private DatabaseReference databaseReference;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        // Initialize the PIN variables.
        currentPin = new string[4];
        // Reset the PIN PlayerPrefs values to null.
        for (int i = 0; i < currentPin.Length; i++)
        {
            PlayerPrefs.SetString("pin_" + i, null);
        }
        PlayerPrefs.SetString("PIN", null);


    }

    public void OnPinPadClicked(int iconIndex)
    {

        // Load the PIN from PlayerPrefs.
        for (int i = 0; i < currentPin.Length; i++)
        {
            currentPin[i] = PlayerPrefs.GetString("pin_" + i, "");
            Debug.Log(currentPin[i]);
            if (currentPin[i] != "")
            {
                pinCircles[i].sprite = pinPad[int.Parse(currentPin[i])].image.sprite;
            }
        }
        // Icon from the pin pad is clicked.
        for (int i = 0; i < currentPin.Length; i++)
        {
            if (currentPin[i] == "")
            {
                currentPin[i] = iconIndex.ToString(); // Store the icon's index as part of the PIN.
                Debug.Log("in pin clickd: " + currentPin[i] + "and i: " + i);
                pinCircles[i].sprite = pinPad[iconIndex].image.sprite; // Display the selected icon in the circle.
                PlayerPrefs.SetString("pin_" + i, currentPin[i]);
                pinError.text = "";
                string imageName = pinCircles[i].sprite.name;
                PlayerPrefs.SetString("PIN", PlayerPrefs.GetString("PIN", "") + imageName + " ");
                break;
            }
        }

    }
    public void OnClearButtonClicked()
    {
        // Check the pin circles starting from the last one.
        for (int i = currentPin.Length - 1; i >= 0; i--)
        {
            if (pinCircles[i].sprite != null)
            {
                // If the current pin circle is filled, clear it and update PlayerPrefs.
                string imageName = pinCircles[i].sprite.name;
                string pinString = PlayerPrefs.GetString("PIN", "");

                // Find the last occurrence of the image name in the PIN string.
                int indexToRemove = pinString.LastIndexOf(imageName + " ");
                if (indexToRemove != -1)
                {
                    // Remove the image name from the PIN string.
                    PlayerPrefs.SetString("PIN", pinString.Remove(indexToRemove, imageName.Length + 1));
                }

                pinCircles[i].sprite = null;
                PlayerPrefs.SetString("pin_" + i, null);
                currentPin[i] = ""; // Remove the icon from the PIN.
                break; // Exit the loop after clearing one pin circle.
            }
        }
    }



   

    public async void OnNextButtonClicked01()
    {
        Debug.Log("pin: " + PlayerPrefs.GetString("PIN"));

        // Fetch the child's PIN from the database.
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string Child01ID = PlayerPrefs.GetString("ChildProfile_" + 0); //for child 1 login
        DataSnapshot dataSnapshot = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(Child01ID).GetValueAsync();

        if (dataSnapshot.Exists)
        {
           // foreach (var childSnapshot in dataSnapshot.Children)
            {
                string childPIN = (string)dataSnapshot.Child("PIN").Value;
                Debug.Log("child pin: " + childPIN);
                string enteredPIN = PlayerPrefs.GetString("PIN");
                Debug.Log("entered pin: " +  enteredPIN);

                if (enteredPIN == childPIN && pinCircles[0].sprite.name != "pinCircle" && pinCircles[1].sprite.name != "pinCircle" && pinCircles[2].sprite.name != "pinCircle" && pinCircles[3].sprite.name != "pinCircle"
  && pinCircles[0].sprite != null && pinCircles[1].sprite != null && pinCircles[2].sprite != null && pinCircles[3].sprite != null)
                {
                    // PINs match, load the next scene.
                    try
                    {
                        Debug.Log("success");
                        PlayerPrefs.SetString("LoggedInChild", Child01ID);
                        SceneManager.LoadScene("childHome");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error loading the next scene: " + e.Message);
                        pinError.text = "Error loading the next scene!";
                    }
                }
                else
                {
                    // PINs do not match, display an error message.
                    pinError.text = "Incorrect PIN!";
                }

            }
        }
          
    } //function ending 


    public async void OnNextButtonClicked02()
    {
        Debug.Log("pin: " + PlayerPrefs.GetString("PIN"));

        // Fetch the child's PIN from the database.
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string Child02ID = PlayerPrefs.GetString("ChildProfile_" + 1); //for child 1 login
        DataSnapshot dataSnapshot = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(Child02ID).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            // foreach (var childSnapshot in dataSnapshot.Children)
            {
                string childPIN = (string)dataSnapshot.Child("PIN").Value;
                Debug.Log("child pin: " + childPIN);
                string enteredPIN = PlayerPrefs.GetString("PIN");
                Debug.Log("entered pin: " + enteredPIN);

                if (enteredPIN == childPIN && pinCircles[0].sprite.name != "pinCircle" && pinCircles[1].sprite.name != "pinCircle" && pinCircles[2].sprite.name != "pinCircle" && pinCircles[3].sprite.name != "pinCircle"
  && pinCircles[0].sprite != null && pinCircles[1].sprite != null && pinCircles[2].sprite != null && pinCircles[3].sprite != null)
                {
                    // PINs match, load the next scene.
                    try
                    {
                        Debug.Log("success");
                        PlayerPrefs.SetString("LoggedInChild", Child02ID);
                        SceneManager.LoadScene("childHome");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error loading the next scene: " + e.Message);
                        pinError.text = "Error loading the next scene!";
                    }
                }
                else
                {
                    // PINs do not match, display an error message.
                    pinError.text = "Incorrect PIN!";
                }

            }
        }

    } //function ending 


    public async void OnNextButtonClicked03()
    {
        Debug.Log("pin: " + PlayerPrefs.GetString("PIN"));

        // Fetch the child's PIN from the database.
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string Child03ID = PlayerPrefs.GetString("ChildProfile_" + 2); //for child 1 login
        DataSnapshot dataSnapshot = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(Child03ID).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            // foreach (var childSnapshot in dataSnapshot.Children)
            {
                string childPIN = (string)dataSnapshot.Child("PIN").Value;
                Debug.Log("child pin: " + childPIN);
                string enteredPIN = PlayerPrefs.GetString("PIN");
                Debug.Log("entered pin: " + enteredPIN);

                if (enteredPIN == childPIN && pinCircles[0].sprite.name != "pinCircle" && pinCircles[1].sprite.name != "pinCircle" && pinCircles[2].sprite.name != "pinCircle" && pinCircles[3].sprite.name != "pinCircle"
  && pinCircles[0].sprite != null && pinCircles[1].sprite != null && pinCircles[2].sprite != null && pinCircles[3].sprite != null)
                {
                    // PINs match, load the next scene.
                    try
                    {
                        Debug.Log("success");
                        PlayerPrefs.SetString("LoggedInChild", Child03ID);
                        SceneManager.LoadScene("childHome");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error loading the next scene: " + e.Message);
                        pinError.text = "Error loading the next scene!";
                    }
                }
                else
                {
                    // PINs do not match, display an error message.
                    pinError.text = "Incorrect PIN!";
                }

            }
        }

    } //function ending 


}
