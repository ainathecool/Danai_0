/*using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class KidsChatBot : MonoBehaviour
{
    public Button startSpeakingButton;
    private float maxRecordingDuration = 10f;
    private bool isRecording = false;
    private bool isRecordingStopped = false;
    private AudioClip recordedAudioClip;
    private AudioSource audioSource;
    private Color originalButtonColor;

    void Start()
    {
        originalButtonColor = startSpeakingButton.image.color;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartSpeakingClick()
    {
        if (!isRecording)
        {
            recordedAudioClip = Microphone.Start(null, false, Mathf.CeilToInt(maxRecordingDuration), 44100);
            isRecording = true;
            isRecordingStopped = false;
            StartCoroutine(UpdateButtonColorDuringRecording());
            StartCoroutine(StopRecordingAfterDuration(maxRecordingDuration));
        }
    }

    public void OnStopSpeakingClick()
    {
        if (isRecording && !isRecordingStopped)
        {
            Microphone.End(null);
            isRecording = false;
            isRecordingStopped = true;
            startSpeakingButton.image.color = originalButtonColor;
            StartCoroutine(WaitForRecordingToCompleteAndUpload());
        }
    }

    IEnumerator StopRecordingAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        OnStopSpeakingClick();
    }

    IEnumerator WaitForRecordingToCompleteAndUpload()
    {
        while (Microphone.IsRecording(null))
        {
            yield return null;
        }

        byte[] audioBytes;
        string filepath;
        int length, samples;

        // Convert AudioClip to WAV bytes
        audioBytes = WavUtility.FromAudioClip(recordedAudioClip, out filepath, out length, out samples);
        StartCoroutine(UploadAudio(audioBytes));
    }


    IEnumerator UploadAudio(byte[] audioBytes)
    {
        // Create form and add the WAV file byte array to it
        WWWForm form = new WWWForm();
        form.AddBinaryData("audio", audioBytes, "recording.wav", "audio/wav");

        // Create a UnityWebRequest to post the form data to the server
        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:5000/process_audio", form);
        www.downloadHandler = new DownloadHandlerAudioClip(www.url, AudioType.MPEG);

        // Send the request and wait for the response
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {

            AudioClip receivedClip = DownloadHandlerAudioClip.GetContent(www);
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = receivedClip;
            audioSource.Play();

        }
    }



    IEnumerator UpdateButtonColorDuringRecording()
    {
        while (isRecording && !isRecordingStopped)
        {
            float lerpValue = Mathf.PingPong(Time.time, 1f);
            startSpeakingButton.image.color = Color.Lerp(originalButtonColor, Color.white, lerpValue);
            yield return null;
        }

        // Reset the button color once recording is stopped
        if (!isRecording)
        {
            startSpeakingButton.image.color = originalButtonColor;
        }
    }
}
*/









































using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class KidsChatBot : MonoBehaviour
{
    public Button startSpeakingButton;
    private float maxRecordingDuration = 10f;
    private bool isRecording = false;
    private bool isRecordingStopped = false;
    private AudioClip recordedAudioClip;
    private AudioSource audioSource;
    private Color originalButtonColor;

    void Start()
    {
        originalButtonColor = startSpeakingButton.image.color;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartSpeakingClick()
    {
        if (!isRecording)
        {
            recordedAudioClip = Microphone.Start(null, false, Mathf.CeilToInt(maxRecordingDuration), 44100);
            isRecording = true;
            isRecordingStopped = false;
            StartCoroutine(UpdateButtonColorDuringRecording());
            StartCoroutine(StopRecordingAfterDuration(maxRecordingDuration));
        }
    }

    public void OnStopSpeakingClick()
    {
        if (isRecording && !isRecordingStopped)
        {
            Microphone.End(null);
            isRecording = false;
            isRecordingStopped = true;
            startSpeakingButton.image.color = originalButtonColor;
            StartCoroutine(WaitForRecordingToCompleteAndUpload());
        }
    }

    IEnumerator StopRecordingAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        OnStopSpeakingClick();
    }

    IEnumerator WaitForRecordingToCompleteAndUpload()
    {
        while (Microphone.IsRecording(null))
        {
            yield return null;
        }

        byte[] audioBytes;
        string filepath;
        int length, samples;

        // Convert AudioClip to WAV bytes
        audioBytes = WavUtility.FromAudioClip(recordedAudioClip, out filepath, out length, out samples);

        // Check if audioBytes is null or empty before attempting to upload
        if (audioBytes == null || audioBytes.Length == 0)
        {
            Debug.LogError("Audio clip is empty or null.");
            yield break; // Exit the coroutine
        }

        StartCoroutine(UploadAudio(audioBytes));
    }

    IEnumerator UploadAudio(byte[] audioBytes)
    {
        // Create form and add the WAV file byte array to it
        WWWForm form = new WWWForm();
        form.AddBinaryData("audio", audioBytes, "recording.wav", "audio/wav");

        // Create a UnityWebRequest to post the form data to the server
        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:5000/process_audio", form);
        www.downloadHandler = new DownloadHandlerAudioClip(www.url, AudioType.MPEG);

        // Send the request and wait for the response
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            AudioClip receivedClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = receivedClip;
            audioSource.Play();
        }
    }

    IEnumerator UpdateButtonColorDuringRecording()
    {
        while (isRecording && !isRecordingStopped)
        {
            float lerpValue = Mathf.PingPong(Time.time, 1f);
            startSpeakingButton.image.color = Color.Lerp(originalButtonColor, Color.white, lerpValue);
            yield return null;
        }

        // Reset the button color once recording is stopped
        if (!isRecording)
        {
            startSpeakingButton.image.color = originalButtonColor;
        }
    }
}
