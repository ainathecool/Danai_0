using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMessage : MonoBehaviour
{
    public GameObject hintbody;
    public GameObject body;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip dropClip;

    private bool isAnimating = false;

    void Start()
    {
        StartCoroutine(StartAnimationWithDelay());
    }

    private IEnumerator StartAnimationWithDelay()
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay time as needed

        if (!isAnimating)
        {
            StartCoroutine(ScalingAnimation());
            source.PlayOneShot(dropClip);
        }
    }

    private IEnumerator ScalingAnimation()
    {
        isAnimating = true;

        float duration = 1.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = hintbody.transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float scale = Mathf.Lerp(3f, 6f, t);
            hintbody.transform.localScale = originalScale * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Go back to the original size
        hintbody.transform.localScale = originalScale;
        isAnimating = false;
    }
}
