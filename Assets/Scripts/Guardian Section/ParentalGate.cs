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
using System.Collections.Generic;

public class ParentalGate : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI tryAgain;
    public Button submitButton;

    private DatabaseReference databaseReference;

    private void Start()
    {

        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Firebase initialized successfully");
        FetchRandomQuestion();

        submitButton.onClick.AddListener(ValidateAnswer);

    }

    private async void FetchRandomQuestion()
    {
        DatabaseReference parentalGateStuff = databaseReference.Child("parentalGate");
        DataSnapshot stuffSnapshot = await parentalGateStuff.GetValueAsync();

        if (stuffSnapshot.Exists)
        {
            List<string> questionKeys = new List<string>();
            foreach (var childSnapshot in stuffSnapshot.Children)
            {
                questionKeys.Add(childSnapshot.Key);
            }

            // Selecting random
            string randomKey = questionKeys[UnityEngine.Random.Range(0, questionKeys.Count)];
            Debug.Log("random selectedy");

            // Get random data
            DataSnapshot randomStuffSnapshot = await parentalGateStuff.Child(randomKey).GetValueAsync();

            if (randomStuffSnapshot.Exists)
            {
                string question = randomStuffSnapshot.Child("Question").GetValue(true).ToString();
                Debug.Log("q" + question);
                string answer = randomStuffSnapshot.Child("Answer").GetValue(true).ToString();
                Debug.Log("a" + answer);
                DisplayQuestion(question, answer);
            }
        }
    }

    private void DisplayQuestion(string question, string answer)
    {
        questionText.text = question;
        answerInput.placeholder.GetComponent<TextMeshProUGUI>().text = "";
    }

    /* public void ValidateAnswer()
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
     }*/

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
            tryAgain.text = "Try Again"; // Display "Try Again"
            answerInput.text = ""; // Clear user input
            FetchRandomQuestion(); // Fetch another question
        }
    }
}