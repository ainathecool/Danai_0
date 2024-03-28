using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Favorites : MonoBehaviour
{
    public string FavSceneName;
    public Button Fav;
    private DatabaseReference databaseReference;

    // Start is called before the first frame update
    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

   
    public void OnFavButtonClicked()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId; //parent id
        string childId = PlayerPrefs.GetString("LoggedInChild");

        if (!string.IsNullOrEmpty(FavSceneName))
        {
            // Assuming you have the guardian's ID
            if (!string.IsNullOrEmpty(userId))
            {
                // Construct the path to store feedback under the guardian's node
               // string favPath = "childProfiles/" + userId + "/favorites/";

               string favPath = $"childProfiles/{userId}/profiles/{childId}/favorites/";

                // await databaseReference.Child("guardians").Child(userId).Child("feedbacks").SetValueAsync(feedback);
                // Push the feedback to generate a unique key
                DatabaseReference feedbackRef = databaseReference.Child(favPath).Push();
                feedbackRef.SetValueAsync(FavSceneName).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("fav submitted successfully!");
                        Fav.image.color = Color.green;
                    }
                    else
                    {
                        Debug.LogError("Failed to submit feedback: " + task.Exception);
                        Fav.image.color = Color.red;
                    }
                });
            }
            else
            {
                Debug.LogError("Guardian ID is not set.");
            }
        }
        else
        {
            Debug.LogError("Feedback text is empty.");
        }
    }


}
