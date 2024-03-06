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

public class MatchTheWordWithPictureGame : MonoBehaviour
{
    public GameObject gamePrefab; // Reference to the prefab containing the game elements
   // public Transform gameContainer; // Reference to the container where the game prefab will be instantiated

    void Start()
    {
        // Load the CorrectWord PlayerPrefs value
        string correctWord = PlayerPrefs.GetString("CorrectWord");

        // Instantiate the game prefab
        GameObject gameInstance = Instantiate(gamePrefab, transform);

        // Get references to text and image components in the instantiated prefab
        TextMeshProUGUI wordText = gameInstance.GetComponentInChildren<TextMeshProUGUI>();
        Image[] imageSlots = gameInstance.GetComponentsInChildren<Image>();

        // Assign the correct word to the text component
        wordText.text = correctWord;

        // Load the correct image into one of the image components
        Sprite correctImage = Resources.Load<Sprite>("Images/" + correctWord);
        if (correctImage != null)
        {
            int randomSlot = Random.Range(0, imageSlots.Length);
            imageSlots[randomSlot].sprite = correctImage;
        }

        // Load random images into the remaining image components (excluding the correct image)
        LoadRandomImages(correctWord, imageSlots);
    }

    void LoadRandomImages(string correctWord, Image[] imageSlots)
    {
        // Load all images from the "Images" folder
        Sprite[] allImages = Resources.LoadAll<Sprite>("Images");

        // Filter out the correct image
        List<Sprite> imagesExcludingCorrect = new List<Sprite>(allImages);
        imagesExcludingCorrect.RemoveAll(image => image.name == correctWord);

        // Shuffle the remaining images
        ShuffleArray(imagesExcludingCorrect.ToArray());

        // Assign the shuffled images to the remaining image slots
        for (int i = 0; i < imageSlots.Length; i++)
        {
            if (imageSlots[i].sprite == null)
            {
                if (i < imagesExcludingCorrect.Count)
                {
                    imageSlots[i].sprite = imagesExcludingCorrect[i];
                }
            }
        }
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // Method called when the user taps on an image
    public void OnImageTap(Image tappedImage)
    {
        // Get the name of the tapped image
        string tappedImageName = tappedImage.sprite.name;

        // Get the correct word
        string correctWord = PlayerPrefs.GetString("CorrectWord");

        // Compare the names
        if (tappedImageName == correctWord)
        {
            Debug.Log("You win!");
            // Add your win logic here
        }
        else
        {
            Debug.Log("Try again!");
            // Add your try again logic here
        }
    }
}
