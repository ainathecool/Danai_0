using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class PictureController : MonoBehaviour
{
    public RawImage imageDisplay;
    public Button captureButton;
    public Button galleryButton;
    public GameObject imageTaken;

    private string imagePath;

    private void Start()
    {
        captureButton.onClick.AddListener(CaptureImage);
        galleryButton.onClick.AddListener(OpenGallery);
    }

    public void CaptureImage()
    {
        // Access the device's camera
        WebCamTexture webcamTexture = new WebCamTexture();
        imageDisplay.texture = webcamTexture;
        webcamTexture.Play();

        // Wait for a short delay before capturing the image frame
        StartCoroutine(CaptureImageAfterDelay(webcamTexture));
    }

    private IEnumerator CaptureImageAfterDelay(WebCamTexture webcamTexture)
    {
        // Wait for a short delay (adjust the delay duration as needed)
       
        yield return new WaitForSeconds(2f);

        // Capture an image frame
        Texture2D capturedImage = new Texture2D(webcamTexture.width, webcamTexture.height);
        capturedImage.SetPixels(webcamTexture.GetPixels());
        capturedImage.Apply();

        // Display the captured image
        imageDisplay.texture = capturedImage;
        imageTaken.SetActive(true);
        webcamTexture.Stop();
        
        Debug.Log("image capyured");
       

        // Save the captured image to a temporary file
        byte[] imageData = capturedImage.EncodeToPNG();
        imagePath = Application.persistentDataPath + "/capturedImage.png";
        File.WriteAllBytes(imagePath, imageData);
    }


    public void OpenGallery()
    {
        // Check if the application is running on an Android device
        if (Application.platform == RuntimePlatform.Android)
        {
            // Get the UnityPlayer class
            AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            // Check if the UnityPlayer class exists
            if (unityClass != null)
            {
                // Get the current activity from the UnityPlayer
                AndroidJavaObject unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");

                // Check if the current activity exists
                if (unityActivity != null)
                {
                    // Call the OpenGallery method on the current activity
                    unityActivity.Call("OpenGallery");
                }
                else
                {
                    Debug.LogError("Failed to get current activity: unityActivity is null");
                }
            }
            else
            {
                Debug.LogError("Failed to get UnityPlayer class: unityClass is null");
            }
        }
        else
        {
            Debug.LogError("OpenGallery is only supported on Android devices.");
        }
    }


    private IEnumerator SendImageForAnalysis(string imageUrl)
    {
        // Create a POST request with the image data
        WWWForm form = new WWWForm();
        byte[] imageData = File.ReadAllBytes(imageUrl);
        form.AddBinaryData("image", imageData, "image.png", "image/png");

        // Send the request to the analysis server
        using (UnityWebRequest request = UnityWebRequest.Post("http://analysis-server.com/analyze", form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Image analysis successful!");
                Debug.Log(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Failed to send image for analysis: " + request.error);
            }
        }
    }
}

