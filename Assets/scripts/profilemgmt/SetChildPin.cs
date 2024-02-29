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

public class SetChildPin : MonoBehaviour
{
    public Image[] pinCircles; // Reference to the 4 circles where the PIN is displayed.
    public Button[] pinPad; // Reference to the icons for setting the PIN.
    public Button buttonClicked;


    private int currentIndex; // Tracks the current position in the PIN.
    private string[] currentPin; // Stores the current PIN as icons.

    private int pinIndex = 0; // Store the index in PlayerPrefs for pinIndex.

    private void Start()
    {
        // Initialize the PIN variables.
        currentPin = new string[4];

        // Load pinIndex from PlayerPrefs or set it to 0 if it doesn't exist.
        pinIndex = PlayerPrefs.GetInt("pinIndex", 0);

        // Load the PIN from PlayerPrefs.
        for (int i = 0; i < currentPin.Length; i++)
        {
            currentPin[i] = PlayerPrefs.GetString("pin_" + i, "");
            if (currentPin[i] != "")
            {
                currentIndex = i + 1;
                pinCircles[i].sprite = pinPad[int.Parse(currentPin[i])].image.sprite;
            }
        }

        Debug.Log("current index: " + currentIndex);
        Debug.Log("current pin: " + currentPin);
        


    
    }

    public void OnPinPadClicked(int iconIndex)
    {
      

        
        // Icon from the pin pad is clicked.
        if (currentIndex < 4)
        {
            currentPin[currentIndex] = iconIndex.ToString(); // Store the icon's index as part of the PIN.
            pinCircles[currentIndex].sprite = pinPad[iconIndex].image.sprite; // Display the selected icon in the circle.
            currentIndex++;

        }
    }

    public void OnClearButtonClicked()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            currentPin[currentIndex] = null; // Remove the icon from the PIN.
            pinCircles[currentIndex].sprite = null; // Clear the corresponding circle.

          
        }
    }

    public void OnNextButtonClicked()
    {
        Debug.Log(currentIndex);
        // Check if the PIN is complete.
       
        {
            // Store the PIN in PlayerPrefs.
            for (int i = 0; i < currentPin.Length; i++)
            {
                PlayerPrefs.SetString("pin_" + i, currentPin[i]);
            }

            PlayerPrefs.SetInt("pinIndex", pinIndex);

            // Try loading the next scene.
            try
            {
                SceneManager.LoadScene("SetChildAvatar"); // Replace with your actual scene name.
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading the next scene: " + e.Message);
            }
        }
    }
}
