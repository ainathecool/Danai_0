using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MatchTheSoundWithWordGame : MonoBehaviour
{
    public GameObject gamePrefab; // Reference to the prefab containing the game elements

    private int count;// for imp tracking
    private int incorrectCount; //for incorrect stuff in imp tracking

    void Start()
    {
        // Load the CorrectWord PlayerPrefs value
        string correctWord = PlayerPrefs.GetString("CorrectWord");

        // Instantiate the game prefab
        GameObject gameInstance = Instantiate(gamePrefab, transform);

        // Get references to text and sound components in the instantiated prefab
        TextMeshProUGUI[] wordSlots = gameInstance.GetComponentsInChildren<TextMeshProUGUI>();
        Button soundButton = gameInstance.GetComponentInChildren<Button>();

        // Assign the correct word to one of the text components
        int randomIndex = Random.Range(0, wordSlots.Length);
        wordSlots[randomIndex].text = correctWord;

        // Load the correct sound into the sound button
        AudioClip correctSound = Resources.Load<AudioClip>("Sounds/" + correctWord);
        if (correctSound != null)
        {
            soundButton.onClick.AddListener(() => PlaySound(correctSound));
        }

        // Load random words into the remaining text components (excluding the correct word)
        LoadRandomWords(correctWord, wordSlots, randomIndex);
    }


    void LoadRandomWords(string correctWord, TextMeshProUGUI[] wordSlots, int correctIndex)
    {
        // Load all words from the "Words" folder
        TextAsset[] allWords = Resources.LoadAll<TextAsset>("Words");

        // Filter out the correct word
        List<TextAsset> wordsExcludingCorrect = new List<TextAsset>(allWords);
        wordsExcludingCorrect.RemoveAll(word => word.name == correctWord);

        // Shuffle the remaining words
        ShuffleList(wordsExcludingCorrect);

        // Assign the shuffled words to the remaining text slots
        int slotIndex = 0;
        for (int i = 0; i < wordSlots.Length; i++)
        {
            if (i != correctIndex) // Skip the slot with the correct word
            {
                if (slotIndex < wordsExcludingCorrect.Count)
                {
                    wordSlots[i].text = wordsExcludingCorrect[slotIndex].text;
                    slotIndex++;
                }
            }
        }
    }


    void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    // Method called when the user taps on a word
    public void OnWordTap(TextMeshProUGUI tappedWord)
    {
        // Get the text of the tapped word
        string tappedWordText = tappedWord.text;

        // Get the correct word
        string correctWord = PlayerPrefs.GetString("CorrectWord");

        // Compare the texts
        if (tappedWordText == correctWord)
        {
            Debug.Log("You win!");
            SceneManager.LoadScene("DLS_game2_complete");
            // Add your win logic here
        }
        else
        {
            incorrectCount = PlayerPrefs.GetInt("Phase4and5IncorrectPlays");
            Debug.Log("Try again!");
            PlayerPrefs.SetInt("Phase4and5IncorrectPlays", incorrectCount + 1);
            // Add your try again logic here
        }
    }
}
