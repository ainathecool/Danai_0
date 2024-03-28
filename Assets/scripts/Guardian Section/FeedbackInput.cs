using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Auth;

public class FeedbackInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button submitButton;
    public float minHeight = 30f;
    public TextMeshProUGUI status;

    RectTransform rectTransform;

    DatabaseReference databaseReference;

    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        rectTransform = inputField.GetComponent<RectTransform>();
    }

    void Update()
    {

        inputField.textComponent.enableWordWrapping = true;
        
    }

   public void SubmitFeedback()
    {
        string feedback = inputField.text;
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        if (!string.IsNullOrEmpty(feedback))
        {
            // Assuming you have the guardian's ID
            if (!string.IsNullOrEmpty(userId))
            {
                // Construct the path to store feedback under the guardian's node
                string feedbackPath = "guardians/" + userId + "/feedbacks/";

               // await databaseReference.Child("guardians").Child(userId).Child("feedbacks").SetValueAsync(feedback);
                // Push the feedback to generate a unique key
                DatabaseReference feedbackRef = databaseReference.Child(feedbackPath).Push();
                feedbackRef.SetValueAsync(feedback).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("Feedback submitted successfully!");
                        inputField.text = "";
                        status.text = "Successfull!";
                        status.color = Color.green;
                    }
                    else
                    {
                        Debug.LogError("Failed to submit feedback: " + task.Exception);
                        status.text = "Try Again!";
                        status.color = Color.red;
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
