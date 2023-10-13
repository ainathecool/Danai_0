using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabeticArrangement : MonoBehaviour

{
    public Text[] letterTexts; // Drag and drop the letter Text components in the Inspector.
    private string[] correctOrder = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsCorrectOrder())
        {
            // Handle when the player has correctly arranged the alphabet.
            Debug.Log("Congratulations! You've organized the alphabet!");
        }
    }

    bool IsCorrectOrder()
    {
        for (int i = 0; i < letterTexts.Length; i++)
        {
            if (letterTexts[i].text != correctOrder[i])
            {
                return false;
            }
        }
        return true;
    }
}
