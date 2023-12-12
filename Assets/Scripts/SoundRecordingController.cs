using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundRecordingController : MonoBehaviour
{
    public Button startSpeakingButton;
    public string sceneInputField;

    // Reference audio clip for 'sss' sound
    public AudioClip referenceAudio;

    // The maximum recording duration in seconds
    private float maxRecordingDuration = 3f;

    // Flag to indicate whether recording is in progress
    private bool isRecording = false;

    // Flag to indicate whether the recording has been stopped
    private bool isRecordingStopped = false;

    // Variable to store the reference to the microphone recording
    private AudioClip recordedAudioClip;

    // Original color of the button
    private Color originalButtonColor;

    void Start()
    {
        // Save the original color of the button
        originalButtonColor = startSpeakingButton.image.color;

        Debug.Log("Start method called.");
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

        // Get the recorded audio and analyze
        bool isCorrectSound = AnalyzeSound(recordedAudioClip);

        if (isCorrectSound)
        {
            // Correct sound, move to the next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneInputField);
        }
        else
        {
            // Incorrect sound, provide feedback
            Debug.Log("Try again with the 'ssss' sound.");
        }
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

    bool AnalyzeSound(AudioClip recordedAudioClip)
    {
        float[] referenceSamples = new float[referenceAudio.samples];
        referenceAudio.GetData(referenceSamples, 0);

        float[] recordedSamples = new float[recordedAudioClip.samples];
        recordedAudioClip.GetData(recordedSamples, 0);

        float referenceZeroCrossings = GetZeroCrossingRate(referenceSamples);
        float recordedZeroCrossings = GetZeroCrossingRate(recordedSamples);

        // Set a threshold for acceptable variation
        float threshold = 0.1f;

        // Compare the Zero Crossing Rate
        if (Mathf.Abs(referenceZeroCrossings - recordedZeroCrossings) > threshold)
        {
            Debug.Log("Not a match. Zero Crossing Rate variation: " + Mathf.Abs(referenceZeroCrossings - recordedZeroCrossings));
            return false; // Not a match
        }

        Debug.Log("Match.");
        return true; // Match
    }

    float GetZeroCrossingRate(float[] samples)
    {
        int zeroCrossings = 0;

        for (int i = 1; i < samples.Length; i++)
        {
            if ((samples[i] >= 0 && samples[i - 1] < 0) || (samples[i] < 0 && samples[i - 1] >= 0))
            {
                zeroCrossings++;
            }
        }

        // Normalize by the length of the signal
        return (float)zeroCrossings / samples.Length;
    }
}
