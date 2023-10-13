using UnityEngine;

public class LetterController : MonoBehaviour
{
    public GameObject LetterPrefab;
    public Transform LetterContainer; // Assign the LetterContainer in the Inspector

    void Start()
    {
        InstantiateLetters();
    }

    void InstantiateLetters()
    {
        // Set the initial positions for each letter
        Vector3 initialPosition = LetterContainer.position;

        for (int i = 0; i < 26; i++)
        {
            GameObject letter = Instantiate(LetterPrefab, LetterContainer);
            letter.transform.position = initialPosition;

            // Adjust the horizontal position to separate the letters
            float spacing = 2.0f; // Adjust this value based on your layout
            initialPosition.x += spacing;

            // Rename the instantiated letter for clarity (e.g., "LetterA," "LetterB," etc.)
            letter.name = "Letter" + (char)('A' + i);

            // Optionally, you can set the text of each letter here if it's not set in the prefab.
            // Example: letter.GetComponent<Text>().text = ((char)('A' + i)).ToString();
        }
    }

    // Add more methods and logic as needed
}

