using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KidsChatBot : MonoBehaviour
{
    public Button startSpeakingButton;

    // The maximum recording duration in seconds
    private float maxRecordingDuration = 10f;

    // Flag to indicate whether recording is in progress
    private bool isRecording = false;

    // Flag to indicate whether the recording has been stopped
    private bool isRecordingStopped = false;

    // Variable to store the reference to the microphone recording
    private AudioClip recordedAudioClip;
    private AudioSource audioSource;

    // Original color of the button
    private Color originalButtonColor;

    void Start()
    {
        // Save the original color of the button
        originalButtonColor = startSpeakingButton.image.color;
        // Initialize audioSource
        audioSource = GetComponent<AudioSource>();

        Debug.Log("Start method called.");
        // audioSource.PlayOneShot(congrats);
    }

    public void OnStartSpeakingClick()
    {
        // Start recording
        recordedAudioClip = Microphone.Start(null, false, Mathf.CeilToInt(maxRecordingDuration), 44100);
        isRecording = true;
        isRecordingStopped = false;

        Debug.Log("Recording started.");

        // Update button color during recording
        StartCoroutine(UpdateButtonColorDuringRecording());

        // Use a coroutine to stop recording after a specified duration
        StartCoroutine(StopRecordingAfterDuration(maxRecordingDuration));
    }

    public void OnStopSpeakingClick()
    {
        if (isRecording && !isRecordingStopped)
        {
            // Stop recording
            Microphone.End(null);
            isRecording = false;
            isRecordingStopped = true;

            Debug.Log("Recording stopped.");

            // Update button color after recording stops
            startSpeakingButton.image.color = originalButtonColor;

            // Use a coroutine to wait for the recording to complete
            StartCoroutine(WaitForRecordingComplete());
        }
        else
        {
            Debug.Log("Recording is not in progress or has already been stopped.");
        }
    }

    IEnumerator StopRecordingAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Stop recording after the specified duration
        OnStopSpeakingClick();
    }

    IEnumerator WaitForRecordingComplete()
    {
        // Wait until the recording is complete
        while (Microphone.IsRecording(null))
        {
            yield return null;
        }

        //send request to chatbot
        Debug.Log("voiceRecorded");
    }

    IEnumerator UpdateButtonColorDuringRecording()
    {
        while (isRecording && !isRecordingStopped)
        {
            // Update the button color during recording (e.g., fade in)
            float lerpValue = Mathf.PingPong(Time.time, 1f); // PingPong between 0 and 1
            startSpeakingButton.image.color = Color.Lerp(originalButtonColor, Color.gray, lerpValue);

            yield return null;
        }
    }

}
