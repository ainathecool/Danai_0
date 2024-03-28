using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioManager audioManager;
    public Slider volumeSlider;

    void Start()
    {
        // Set initial volume value on the slider
        volumeSlider.value = audioManager.audioSource.volume;
    }

    // Called when the volume slider value changes
    public void SetVolume(float volume)
    {
        // Call the SetVolume method of the AudioManager
        audioManager.SetVolume(volume);
    }

    // Called when the mute/unmute button is clicked
    public void ToggleMute()
    {
        // Call the Mute method of the AudioManager and toggle the mute state
        audioManager.Mute(!audioManager.audioSource.mute);
    }
}
