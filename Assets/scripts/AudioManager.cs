using Gpm.Common.ThirdParty.SharpCompress.Common;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    // Adjust the volume of the audio source
    public void SetVolume(float volume)
    {
        // Ensure volume is clamped between 0 and 1
        volume = Mathf.Clamp01(volume);

        // Set the volume of the audio source
        audioSource.volume = volume;
        Debug.Log("name of audio: " + audioSource.name + "volume: " + volume);
    }

    // Mute the audio source
    public void Mute(bool isMuted)
    {
        // Set the mute state of the audio source
        audioSource.mute = isMuted;
        Debug.Log("name of audio: " + audioSource.name + "muted: " + audioSource.mute);
    }
}
