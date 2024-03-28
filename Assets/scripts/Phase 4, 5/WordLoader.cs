using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Storage;

public class WordLoader : MonoBehaviour
{
    public GameObject wordPrefab; // Reference to the prefab containing word, image, and sound UI elements

    private DatabaseReference databaseReference;

    void Start()
    {
        // Firebase SDK is correctly installed and initialized
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Firebase initialized successfully!");

        // Fetch and display word data
        FetchAndDisplayWord();
    }

    private async void FetchAndDisplayWord()
    {
        if (databaseReference == null)
        {
            Debug.LogError("Firebase is not initialized.");
            return;
        }

        // Fetch all words from the database
        DatabaseReference wordsRef = databaseReference.Child("Words");
        DataSnapshot wordsSnapshot = await wordsRef.GetValueAsync();

        if (wordsSnapshot.Exists)
        {
            // Convert the dictionary of words to a list of word keys
            List<string> wordKeys = new List<string>();
            foreach (var childSnapshot in wordsSnapshot.Children)
            {
                wordKeys.Add(childSnapshot.Key);
            }

            // Select a random word key
            string randomWordKey = wordKeys[Random.Range(0, wordKeys.Count)];

            // Get the random word's data
            DataSnapshot randomWordSnapshot = await wordsRef.Child(randomWordKey).GetValueAsync();

            if (randomWordSnapshot.Exists)
            {
                // Extract sound URL
                var wordData = randomWordSnapshot.Value as Dictionary<string, object>;
               // var soundUrl = wordData["sound_url"].ToString();
               // Debug.Log("Sound URL: " + soundUrl);

                // Instantiate the prefab
                if (wordPrefab != null)
                {
                    try
                    {
                        GameObject wordInstance = Instantiate(wordPrefab, transform);
                        if (wordInstance != null)
                        {
                            Debug.Log("Prefab instantiated successfully.");

                            // Get references to text, image, and button components in the instantiated prefab
                            TextMeshProUGUI wordText = wordInstance.GetComponentInChildren<TextMeshProUGUI>();
                            Image wordImage = wordInstance.GetComponentInChildren<Image>();
                            Button soundButton = wordInstance.GetComponentInChildren<Button>();

                            // Set the word text
                            wordText.text = randomWordKey;

                            //set it in playerprefs to use it in the next scene 
                            PlayerPrefs.SetString("CorrectWord", randomWordKey);

                            // Load the image from local assets
                            string imagePath = "Images/" + randomWordKey;
                            Texture2D texture = Resources.Load<Texture2D>(imagePath);
                            if (texture != null)
                            {
                                wordImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                                Debug.Log("Loaded Texture: " + texture);

                            }
                            else
                            {
                                Debug.LogError("Failed to load image from path: " + imagePath);
                            }

                            // Load the sound from local assets
                            string soundPath = "Sounds/" + randomWordKey;
                            AudioClip audioClip = Resources.Load<AudioClip>(soundPath);
                            if (audioClip != null)
                            {
                                soundButton.onClick.AddListener(() => PlaySound(audioClip));
                            }
                            else
                            {
                                Debug.LogError("Failed to load sound from path: " + soundPath);
                            }
                        }
                        else
                        {
                            Debug.LogError("Failed to instantiate prefab.");
                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("Exception while instantiating prefab: " + e.Message);
                    }
                }
                else
                {
                    Debug.LogError("Word prefab reference is null.");
                }
            }
            else
            {
                Debug.LogError("Random word data not found in the database.");
            }
        }
        else
        {
            Debug.LogError("Word data not found in the database.");
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        Debug.Log("in sound");

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        Debug.Log("playing");
    }
}
