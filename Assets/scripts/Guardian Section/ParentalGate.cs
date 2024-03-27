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
using Unity.VisualScripting;

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

                PlayerPrefs.SetString("correctans", answer);
            }
        }
    }

    private void DisplayQuestion(string question, string answer)
    {
        questionText.text = question;
        answerInput.placeholder.GetComponent<TextMeshProUGUI>().text = "";


    }



    public void ValidateAnswer( )
    {
        string parentResponse = answerInput.text;
        string correctAnswer = PlayerPrefs.GetString("correctans");
        Debug.Log("enetered: a" + parentResponse);
        Debug.Log("correct: " + correctAnswer);

       // if (parentResponse.Trim().Equals(correctAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
       if(parentResponse == correctAnswer)
        {
            Debug.Log("Access Granted");
            PlayerPrefs.DeleteKey("correctans");
            SceneManager.LoadScene("childProfiles");
        }
        else
        {
            Debug.Log("Access Denied");
            tryAgain.text = "Try Again"; // Display "Try Again"
            answerInput.text = ""; // Clear user input
                                   // FetchRandomQuestion(); // Fetch another question
            PlayerPrefs.DeleteKey("correctans");
            SceneManager.LoadScene("ParentalGate");
        }
    }
}