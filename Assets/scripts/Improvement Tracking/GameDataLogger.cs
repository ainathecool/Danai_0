using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameDataLogger : MonoBehaviour
{

    public string gameTry; 
    private const string HINTS_USED_KEY = "Phase2Hints";
    private const string INCORRECT_PLAYS_KEY = "Phase2IncorrectCount";


    private void Start()
    {

        // Retrieve values from PlayerPrefs
      
      
        int hintsUsed = PlayerPrefs.GetInt(HINTS_USED_KEY);
        int incorrectPlays = PlayerPrefs.GetInt(INCORRECT_PLAYS_KEY);
      

        // Store values in ChildProfile's GameTry
        
        string childProfile = "Child1"; // Adjust as needed - idhar wo guardian ka laga dena
        StoreGameData(childProfile, gameTry, hintsUsed, incorrectPlays);

        // Reset PlayerPrefs
        ResetPlayerPrefs();
    }

 

    private void StoreGameData(string childProfile, string gameTry, float hintsUsed, int incorrectPlays)
    {
        // Calculate accuracy
        float accuracy = CalculateAccuracy(hintsUsed, incorrectPlays);

        // Store values in ChildProfile's GameTry
        PlayerPrefs.SetFloat(childProfile + "_Game" + gameTry + "_" + HINTS_USED_KEY, hintsUsed);
        PlayerPrefs.SetInt(childProfile + "_Game" + gameTry + "_" + INCORRECT_PLAYS_KEY, incorrectPlays);
        PlayerPrefs.SetFloat(childProfile + "_Game" + gameTry + "_Accuracy", accuracy);

        Debug.Log("Hints: " + PlayerPrefs.GetInt(HINTS_USED_KEY));
        Debug.Log("Incorrect: "+ PlayerPrefs.GetInt(INCORRECT_PLAYS_KEY));
        Debug.Log("Accuracy: " + PlayerPrefs.GetFloat(childProfile + "_Game" + gameTry + "_Accuracy"));


    }

    private float CalculateAccuracy(float hintsUsed, int incorrectPlays)
    {
        // Perform your accuracy calculation here, based on the provided values
        // This is just a placeholder
      
       
       float  accuracy = ((hintsUsed + 1) / (incorrectPlays + 1)) * 100;
       
        return accuracy;
    }

    private void ResetPlayerPrefs()
    {
       
        PlayerPrefs.DeleteKey(HINTS_USED_KEY);
        PlayerPrefs.DeleteKey(INCORRECT_PLAYS_KEY);
    }
}

