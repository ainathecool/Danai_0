
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hint : MonoBehaviour
{
    // Tag of the button you want to show
    public string buttonTag = "hintC";
    public void hintAppear()
    {
        Debug.Log("Hint Button Clicked");

        // Find all game objects with the specified tag
        GameObject[] buttons = GameObject.FindGameObjectsWithTag(buttonTag);
        Debug.Log(buttons);

        // Log the number of buttons found
        Debug.Log("Number of Buttons Found: " + buttons.Length);

        // Loop through each button and activate it
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
            Debug.Log("Button Activated: " + button.name);
        }
    }
}