using UnityEngine;
using TMPro;
using System.Collections;


public class TextMeshProController : MonoBehaviour
{
    public TextMeshProUGUI textMeshProField;  // Reference to your TextMeshPro component
    public float textChangeInterval = 2.0f;  // Time interval to change text (in seconds)

    private string[] textOptions;  // An array of text options
    private int currentIndex = 0;  // Index to keep track of which text to display

    void Start()
    {
        // Initialize your text options
        textOptions = new string[]
        {
            "Hi! Meet Pablo, the Penguin!",
            "Pablo loves to play with letters, write stories and sing poems",
            "Pablo is inviting you to have fun together!",
        };

        // Start a coroutine to change the text
        StartCoroutine(ChangeTextRoutine());
    }

    // Coroutine to change the text at regular intervals
    IEnumerator ChangeTextRoutine()
    {
        while (true)
        {
            // Change the text
            textMeshProField.text = textOptions[currentIndex];

            // Increment the index, or loop back to the start if at the end
            currentIndex = (currentIndex + 1) % textOptions.Length;

            yield return new WaitForSeconds(textChangeInterval);
        }
    }
}