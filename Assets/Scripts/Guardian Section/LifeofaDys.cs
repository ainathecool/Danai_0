using Firebase;
using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeofaDys : MonoBehaviour
{
    private DatabaseReference databaseReference;

   // public InputField headingTextBox;
    //public InputField informationTextBox;

    void Start()
    {
        Debug.Log("starting");
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError($"Failed to initialize Firebase: {task.Exception}");
                return;
            }

            // Firebase is ready, get the database reference
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void FetchDataFromFirebaseAndLoadScene()
    {
        Debug.Log("btn clicked");

        // Assuming you have a path to your data in the database
        string path = "wordToSpread/Life of a Dyslexic";
        Debug.Log(path);
        ;
        databaseReference.Child(path).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("in faulted");

                Debug.LogError($"Failed to fetch data from Firebase: {task.Exception}");
                return;
            }
            DataSnapshot snapshot = task.Result;

            if (snapshot.Exists) {
            string heading = snapshot.Child("Heading").Value.ToString();
            string information = snapshot.Child("Information").Value.ToString();
            Debug.Log("Fetched Heading: " + heading);
            Debug.Log("Fetched Information: " + information);

                // Display fetched data in the UI
                // headingTextBox.text = heading;
                //informationTextBox.text = information;
            }
            else
            {
                Debug.Log("Data not found");
            }
        });
    }
}
