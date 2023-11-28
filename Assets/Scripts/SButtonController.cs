using System.Collections;
using UnityEngine;

public class SButtonController : MonoBehaviour
{
    public AudioClip s1Sound;
    private AudioSource audioSource;
    private Vector3 originalPosition;
    private bool isAnimating = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalPosition = transform.position;

        // Start the HoverAnimation coroutine every 10 seconds, with a delay of 0 seconds.
        InvokeRepeating("StartHoverAnimation", 5f, 50f);
    }

    private void StartHoverAnimation()
    {
        if (!isAnimating)
        {
            StartCoroutine(HoverAnimation());
        }
    }

    private IEnumerator HoverAnimation()
    {
        isAnimating = true;
        audioSource.PlayOneShot(s1Sound);

        // Jump up
        transform.Translate(Vector3.up * 0.3f, Space.World);

        yield return new WaitForSeconds(1.5f);

        // Return to original position
        transform.position = originalPosition;

        yield return new WaitForSeconds(1.5f);

        isAnimating = false;
    }
}
