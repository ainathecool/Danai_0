using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class ImageUpload : MonoBehaviour
{
    // The URL of the endpoint to which the image is sent
    private string uploadURL = "https://7zv3os0755.execute-api.us-east-1.amazonaws.com/default/DANAI";

    // The local path to the image you want to upload
    private string imagePath = @"C:\Users\DEEBYTE COMPUTERS\Pictures\Screenshots\Screenshot 2023-10-11 140021.png";

    void Start()
    {
        StartCoroutine(UploadImage(imagePath));
    }

    IEnumerator UploadImage(string filePath)
    {
        byte[] imageData = File.ReadAllBytes(filePath);

        // Create a UnityWebRequest POST object
        UnityWebRequest www = UnityWebRequest.Post(uploadURL, new WWWForm());

        // Upload the image data
        www.uploadHandler = new UploadHandlerRaw(imageData);

        // Set the content type header depending on the image format
        // For PNG, use "image/png"
        www.SetRequestHeader("Content-Type", "image/jpeg");

        // Send the request and wait for the response
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"Error uploading image: {www.error}");
        }
        else
        {
            Debug.Log("Image successfully uploaded.");
            // Display or process the response
            string responseText = www.downloadHandler.text;
            Debug.Log($"Server response: {responseText}");
        }

        // Dispose of the UnityWebRequest object
        www.Dispose();
    }
}
