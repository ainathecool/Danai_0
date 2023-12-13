using UnityEngine;
using TMPro;
using System.Collections;

<<<<<<< HEAD

=======
>>>>>>> phase2
public class TextMeshProController : MonoBehaviour
{
    public TextMeshProUGUI textMeshProField;  // Reference to your TextMeshPro component
    public float textChangeInterval = 2.0f;  // Time interval to change text (in seconds)
<<<<<<< HEAD
=======
    public float fadeDuration = 0.5f;  // Duration of fade animation (in seconds)
>>>>>>> phase2

    private string[] textOptions;  // An array of text options
    private int currentIndex = 0;  // Index to keep track of which text to display

    void Start()
    {
        // Initialize your text options
        textOptions = new string[]
        {
<<<<<<< HEAD
            "Sammy The Squirrel Needs Your Help",
            "Sammy is looking for the letter s",
            "Let's Help Sammy The Squirrel!",
=======
            "Welcome to",
            "ALPHABET ADVENTURE",
            "with Pablo"
>>>>>>> phase2
        };

        // Start a coroutine to change the text
        StartCoroutine(ChangeTextRoutine());
    }

<<<<<<< HEAD
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
=======
    // Coroutine to change the text with fade-in and fade-out animation
    IEnumerator ChangeTextRoutine()
    {
        float startFontSize = textMeshProField.fontSize;

        for (int i = 0; i < textOptions.Length; i++)
        {
            // Set the new text
            textMeshProField.text = textOptions[i];

            // Fade in the new text
            yield return StartCoroutine(FadeInText());

            // Wait for fade-in animation to complete
            yield return new WaitForSeconds(textChangeInterval);

            // If it's not the last text, start fading it out
            if (i < textOptions.Length - 1)
            {
                yield return StartCoroutine(FadeOutText(startFontSize));
            }
        }
    }

    // Coroutine for fade-out animation
    IEnumerator FadeOutText(float startFontSize)
    {
        float progress = 0f;
        Color textColor = textMeshProField.color;
        float targetAlpha = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime / fadeDuration;
            textColor.a = Mathf.Lerp(1f, targetAlpha, progress);
            textMeshProField.color = textColor;
            textMeshProField.fontSize = Mathf.Lerp(startFontSize, 0, progress);
            yield return null;
        }
    }

    // Coroutine for fade-in animation
    IEnumerator FadeInText()
    {
        float progress = 0f;
        Color textColor = textMeshProField.color;

        while (progress < 1f)
        {
            progress += Time.deltaTime / fadeDuration;
            textColor.a = Mathf.Lerp(0f, 1f, progress);
            textMeshProField.color = textColor;
            yield return null;
>>>>>>> phase2
        }
    }
}
