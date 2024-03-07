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

public class ParentalGate : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;

    private DatabaseReference databaseReference;
    private string currentQuestionKey;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference.Child("parentalGate");
                FetchRandomQuestion();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    private void FetchRandomQuestion()
    {
        databaseReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int randomIndex = UnityEngine.Random.Range(0, (int)snapshot.ChildrenCount);

                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    if (randomIndex == 0)
                    {
                        string question = childSnapshot.Child("Question").GetValue(true).ToString();
                        string answer = childSnapshot.Child("Answer").GetValue(true).ToString();

                        currentQuestionKey = childSnapshot.Key;
                        DisplayQuestion(question, answer);
                        break;
                    }
                    randomIndex--;
                }
            }
        });
    }

    private void DisplayQuestion(string question, string answer)
    {
        questionText.text = question;
        answerInput.placeholder.GetComponent<TextMeshProUGUI>().text = answer;
    }

    public void ValidateAnswer()
    {
        string parentResponse = answerInput.text;
        string correctAnswer = answerInput.placeholder.GetComponent<TextMeshProUGUI>().text;

        if (parentResponse.Trim().Equals(correctAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Access Granted");
            // Perform actions for accessing content (e.g., load next scene)
        }
        else
        {
            Debug.Log("Access Denied");
            FetchRandomQuestion();
        }
    }
}
