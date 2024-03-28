using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Phase4and5DataLogger : MonoBehaviour
{

    // public string gameTry;
    private const string HINTS_USED_KEY = "Phase4and5Hints";
    private const string INCORRECT_PLAYS_KEY = "Phase4and5IncorrectPlays";

    private DatabaseReference databaseReference;

    private void Start()
    {

        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        // Retrieve values from PlayerPrefs


        int hintsUsed = PlayerPrefs.GetInt(HINTS_USED_KEY);
        int incorrectPlays = PlayerPrefs.GetInt(INCORRECT_PLAYS_KEY);


        // Store values in ChildProfile's GameTry

        AddAccuracyToDB(hintsUsed, incorrectPlays);

        // Reset PlayerPrefs
        ResetPlayerPrefs();
   

    }

    public void AddAccuracyToDB(int hintsUsed, int incorrectPlays)
    {
        float accuracy = CalculateAccuracy(hintsUsed, incorrectPlays);
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string childId = PlayerPrefs.GetString("LoggedInChild");

        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(childId))
        {
            // Construct the path to update the avatar value in the database
            string path = $"childProfiles/{userId}/profiles/{childId}/ImprovementTracking0506/";

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


    private float CalculateAccuracy(float hintsUsed, int incorrectPlays)
    {
        // Perform your accuracy calculation here, based on the provided values
        // This is just a placeholder


        float accuracy = ((hintsUsed + 1) / (incorrectPlays + 1)) * 10;

        return accuracy;
    }

    private void ResetPlayerPrefs()
    {

        PlayerPrefs.DeleteKey(HINTS_USED_KEY);
        PlayerPrefs.DeleteKey(INCORRECT_PLAYS_KEY);
    }
}

