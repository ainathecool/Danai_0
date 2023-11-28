using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SButtonClickHandler : MonoBehaviour
{
    public AudioClip s2Sound;
    public GameObject progressBar;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClicked()
    {
        audioSource.PlayOneShot(s2Sound);
        Debug.Log("button clciked");

        // Spiral animation and grow
        StartCoroutine(SpiralAnimation());

        // Optional: You can trigger other actions here.
    }

    private IEnumerator SpiralAnimation()
    {

        float duration = 5.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Spiral animation logic
            float angle = t * 360f * 5f; // 5 rotations
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * t;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * t;

            // Optionally, you can add scaling logic here
            float scale = Mathf.Lerp(1f, 2f, t);
            transform.localScale = originalScale * scale;

            // Set position based on the spiral motion
            transform.position = new Vector3(x, y, 0f) + progressBar.transform.position;

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        transform.position = progressBar.transform.position;
        transform.localScale = originalScale * 2f; // Adjust the final scale as needed


        // Optional: You can trigger other actions here.
    }
}
