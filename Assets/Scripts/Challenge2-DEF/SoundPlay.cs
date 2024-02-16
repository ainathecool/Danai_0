
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clickClip;

    void OnMouseDown()
    {
        source.PlayOneShot(clickClip);
    }
}