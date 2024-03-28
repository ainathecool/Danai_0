using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1DataLogger : MonoBehaviour
{
    private const string HINTS_USED_KEY = "Phase1Hints";
    private DatabaseReference databaseReference;

    // Start is called before the first frame update
    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        int hintsUsed = PlayerPrefs.GetInt(HINTS_USED_KEY);//retreive values;

        AddAccuracyToDB(hintsUsed);

        // Reset PlayerPrefs
        ResetPlayerPrefs();
    }

    public void AddAccuracyToDB(int hintsUsed)
    {
        float accuracy = CalculateAccuracy(hintsUsed);
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string childId = PlayerPrefs.GetString("LoggedInChild");

        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(childId))
        {
                // Construct the path to update the avatar value in the database
                string path = $"childProfiles/{userId}/profiles/{childId}/ImprovementTracking01/";

                 DatabaseReference impRef = databaseReference.Child(path).Push();
           
            // Update the avatar value in the Firebase Realtime Database
            impRef.SetValueAsync(accuracy).ContinueWith(task =>
            {
                    if (task.IsCompleted)
                    {
                        Debug.Log("accuracy updated successfully!");
                    }
                    else if (task.IsFaulted)
                    {
                        Debug.LogError("Failed to update accuracy: " + task.Exception);
                    }
                });
            }
            else
            {
                Debug.LogWarning("accuracy name is empty!");
            }

    }

    private float CalculateAccuracy(float hintsUsed)
    {
        // Perform your accuracy calculation here, based on the provided values
        // This is just a placeholder
        float accuracy;
        Debug.Log("hints: "+ hintsUsed);

        if (hintsUsed == 0)
        {
            accuracy = 100;
            PlayerPrefs.SetFloat("Accuracy", accuracy);
        }

         accuracy = (100 - ((hintsUsed + 1) * 10));
        PlayerPrefs.SetFloat("Accuracy", accuracy);

        return accuracy;
    }

    private void ResetPlayerPrefs()
    {

        PlayerPrefs.DeleteKey(HINTS_USED_KEY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
