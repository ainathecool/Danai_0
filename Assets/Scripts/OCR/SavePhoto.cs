using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using TMPro; // Import TextMeshPro namespace

public class SavePhoto : MonoBehaviour
{
    public UnityEvent Function_onPicked_Return; // Visual Scripting trigger [On Unity Event]
    public UnityEvent Function_onSaved_Return; // Visual Scripting trigger [On Unity Event]

    public NativeFilePicker.Permission permission; // Permission to access Camera Roll
    
    public TextMeshProUGUI textField; // Reference to TextMeshPro Text component
    public GameObject toDisplay; 
    public GameObject toUpload; 


    public void SavePhotoToCameraRoll(Texture2D MyTexture, string AlbumName, string filename)
    {
        NativeGallery.SaveImageToGallery(MyTexture, AlbumName, filename, (callback, path) =>
        {
            if (callback == false)
            {
                Debug.Log("Failed to save !");
            }
            else
            {
                Debug.Log("Photo is saved to Camera Roll on phone device.");
                Function_onSaved_Return.Invoke(); // Triggered [On Unity Event] in Visual Scripting
            }

        });
    }

    public void PickPhotoCameraRoll()
    {
        if (permission == NativeFilePicker.Permission.Granted)
        {
            Debug.Log("Permission Granted");
            NativeFilePicker.PickFile((path) =>
            {
                if (path == null)
                {
                    Debug.Log("Pick Photo : Canceled");
                }
                else
                {
                    Debug.Log("Pick Photo : Success");
                    Variables.ActiveScene.Set("Picked File", path);

                    StartCoroutine(UploadImage(path));

                    Function_onPicked_Return.Invoke(); // Triggered [On Unity Event] in Visual Scripting
                }
            }, "image/*");
        }
        else
        {
            Debug.Log("Permission Not Granted");
            AskPermission();
        }
    }

    public async void AskPermission()
    {
        NativeFilePicker.Permission permissionResult = await NativeFilePicker.RequestPermissionAsync(false);
        permission = permissionResult;

        if (permission == NativeFilePicker.Permission.Granted)
        {
            PickPhotoCameraRoll();
        }
    }

    // The URL of the endpoint to which the image is sent
    private string uploadURL = "https://7zv3os0755.execute-api.us-east-1.amazonaws.com/default/DANAI";

    void Start()
    {
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

            // Set the text of the TMP text field
            textField.text = responseText.Substring(19, responseText.Length - 21);
            toUpload.SetActive(false);
            // Unhide the empty GameObject
            toDisplay.SetActive(true);

            Function_onSaved_Return.Invoke(); // Triggered [On Unity Event] in Visual Scripting
        }

        // Dispose of the UnityWebRequest object
        www.Dispose();
    }
}
