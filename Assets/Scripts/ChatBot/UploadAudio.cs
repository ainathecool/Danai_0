using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class UploadAudio : MonoBehaviour
{
    void Start()
    {
        // Replace "path/to/your/audio.mp3" with the actual path to the audio file you wish to send
        StartCoroutine(UploadAud("\"C:\\Users\\DEEBYTE COMPUTERS\\Documents\\Sound Recordings\\Recording.m4a\""));
    }
    IEnumerator UploadAud(string audioFilePath)
    {
        // Load the audio file from the path
        byte[] audioBytes = System.IO.File.ReadAllBytes(audioFilePath);

        // Create a Web Request and set the method to POST
        UnityWebRequest www = new UnityWebRequest("http://127.0.0.1:5000/process_audio", UnityWebRequest.kHttpVerbPOST);

        // Create a form section and add the audio file bytes
        WWWForm form = new WWWForm();
        form.AddBinaryData("audio", audioBytes, "filename.mp3", "audio/mpeg");

        // Set the form as the request's upload handler
        www.uploadHandler = new UploadHandlerRaw(form.data);
        www.uploadHandler.contentType = "audio/mpeg";

        // The download handler will store the server's response
        www.downloadHandler = new DownloadHandlerBuffer();

        // Set the request headers
        www.SetRequestHeader("Content-Type", "audio/mpeg");

        // Wait for the request to complete
        yield return www.SendWebRequest();

        // Check for errors
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Audio processed successfully.");

            // Here you can handle the response audio data
            // For example, to play the received audio:
             AudioClip receivedClip = DownloadHandlerAudioClip.GetContent(www);
             AudioSource audioSource = GetComponent<AudioSource>();
             audioSource.clip = receivedClip;
             audioSource.Play();
        }
    }
}