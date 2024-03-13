using Firebase;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeofaDys : MonoBehaviour
{
    private DatabaseReference databaseReference;

    public TextMeshProUGUI headingTextBox;
    public TextMeshProUGUI informationTextBox;
    public GameObject buttonsContainer;
    public string infoName;
    public GameObject otherButton;
    public GameObject exitbtn;

    private void Start()
    {
        // Hide the information text box initially
        informationTextBox.gameObject.SetActive(false);

        // Hide the buttons container initially
        buttonsContainer.SetActive(true);
        otherButton.SetActive(false);
    }

    public async void FetchDataFromFirebaseAndLoadScene()
    {
        // Disable the fetch button to prevent multiple requests
        //buttonsContainer.SetActive(false);

        // Initialize Firebase
        await FirebaseApp.CheckAndFixDependenciesAsync();

        // Get database reference
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Assuming you have a path to your data in the database
        string path = "wordtoSpread/"+infoName;

        // Retrieve data asynchronously
        DataSnapshot snapshot = await databaseReference.Child(path).GetValueAsync();

        // Check if data exists at the specified path
        if (snapshot.Exists)
        {
            // Access the data from the snapshot
            string heading = snapshot.Child("Heading").Value.ToString();
            string information = snapshot.Child("Information").Value.ToString();
            Debug.Log("Fetched Heading: " + heading);
            Debug.Log("Fetched Information: " + information);

            // Display fetched data
            headingTextBox.text = heading;
            informationTextBox.text = information;

        
            // Show the information text box
            informationTextBox.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No data found at the specified path.");
        }
// Enable the fetch button
        buttonsContainer.SetActive(false);
        // Hide the clicked button
        exitbtn.SetActive(false);

        //Unhide the previous btn
        otherButton.SetActive(true);
    }
}





/*public class LifeofaDys : MonoBehaviour
{
    private DatabaseReference databaseReference;

    public async void FetchDataFromFirebaseAndLoadScene()
    {
        // Initialize Firebase
        await FirebaseApp.CheckAndFixDependenciesAsync();

        // Get database reference
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Assuming you have a path to your data in the database
        string path = "wordtoSpread/Life of a Dyslexic";

        // Retrieve data asynchronously
        DataSnapshot snapshot = await databaseReference.Child(path).GetValueAsync();

        // Check if data exists at the specified path
        if (snapshot.Exists)
        {
            // Access the data from the snapshot
            string heading = snapshot.Child("Heading").Value.ToString();
            string information = snapshot.Child("Information").Value.ToString();
            Debug.Log("Fetched Heading: " + heading);
            Debug.Log("Fetched Information: " + information);

        }
        else
        {
            Debug.LogWarning("No data found at the specified path.");
        }
    }
}*/
