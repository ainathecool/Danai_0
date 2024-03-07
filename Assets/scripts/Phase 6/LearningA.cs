using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LearningA : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip learnA;
    [SerializeField] private string nextScene; 

    private Material originalMaterial; // Store the original material of the object
    private Material highlightMaterial; // Material for the highlight effect

    private bool isSoundPlaying = false; // Flag to track if the sound is playing

    void Start()
    {
        // Check if AudioSource and AudioClip are assigned
        if (source != null && learnA != null)
        {
            // Store the original material
            originalMaterial = GetComponent<Renderer>().material;

            // Create a new material for the highlight effect
            highlightMaterial = new Material(originalMaterial);
            highlightMaterial.color = Color.green; // Set highlight color to green

            // Enlarge the object
            StartCoroutine(EnlargeObject());

            // Play the sound after a delay
            StartCoroutine(PlaySoundWithDelay());

            // Change the highlight color and load next scene after a delay
            StartCoroutine(ChangeHighlightAndLoadScene());
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip not assigned!");
        }
    }

    IEnumerator EnlargeObject()
    {
        // Define the scale increase factor
        float scaleFactor = 1.5f;
        // Define the duration of the enlargement
        float duration = 2f; // 2 seconds

        // Initial scale
        Vector3 originalScale = transform.localScale;
        // Target scale
        Vector3 targetScale = originalScale * scaleFactor;

        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            // Increase scale gradually over time
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the object is set to its target scale
        transform.localScale = targetScale;
    }

    IEnumerator PlaySoundWithDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Play the sound
        source.PlayOneShot(learnA);
        isSoundPlaying = true;
    }

    IEnumerator ChangeHighlightAndLoadScene()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        // Apply the highlight material to the object
        GetComponent<Renderer>().material = highlightMaterial;

        // Load the next scene after a delay
        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        // Revert back to the original material
        GetComponent<Renderer>().material = originalMaterial;

        // Load the next scene
        // Replace "NextSceneName" with the name of your next scene
        SceneManager.LoadScene(nextScene);

        // For now, let's just log a message
        Debug.Log("Changing scene...");
    }
}
