using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AButtonHandler : MonoBehaviour
{
    public AudioClip a1Sound;
    public AudioClip a2Sound;
    public AudioClip hintASound;
    public GameObject hintA;
    public GameObject progressBar;
    public GameObject a1, a2;
    public Transform moveAreaA1, moveAreaA2; // Reference to the empty object

    private AudioSource audioSource;
    private bool isAnimating = false;
    private bool shouldMoveButtons = true;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // StartCoroutine(MoveButtons());
        // StartCoroutine(MoveA1andA2InsideArea());
        if (a1.transform.position != progressBar.transform.position)
        {
            // Start the JigglingA1 coroutine every 5 seconds, with a delay of 0 seconds.
           // InvokeRepeating("StartMoving", 0f, 10f);
        }
    }

    private void StartMoving()
    {
        StartCoroutine(MoveA1andA2InsideArea());
    }

    private IEnumerator MoveA1andA2InsideArea()
    {
        float moveDuration = 10.0f; // Adjusted for faster movement
        float elapsedTime = 0f;

        Vector3 originalPositionA1 = a1.transform.position;
        Vector3 targetPositionA1 = moveAreaA1.position;

        Vector3 originalPositionA2 = a2.transform.position;
        Vector3 targetPositionA2 = moveAreaA2.position;


        while (elapsedTime < moveDuration && shouldMoveButtons)
        {
            float t = elapsedTime / moveDuration;
            a1.transform.position = Vector3.Lerp(originalPositionA1, targetPositionA1, t);
            a2.transform.position = Vector3.Lerp(originalPositionA2, targetPositionA2, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Keep moving until there's a click
        while (!isAnimating)
        {
            yield return null;
        }
    }

    public void OnHintsButtonClicked()
    {
        if (!isAnimating)
        {
            StartCoroutine(ScalingAnimation());
        }
    }

    private IEnumerator ScalingAnimation()
    {
        isAnimating = true;
        audioSource.PlayOneShot(hintASound);

        float duration = 2.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = hintA.transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float scale = Mathf.Lerp(20f, 40f, t);
            hintA.transform.localScale = originalScale * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.PlayOneShot(hintASound);

        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Go back to the original size
        hintA.transform.localScale = originalScale;
        isAnimating = false;
    }

    public void OnAButtonClicked()
    {
       
        Debug.Log("button clicked");
        shouldMoveButtons = false;

        if (transform.position != progressBar.transform.position)
        {
             audioSource.PlayOneShot(a2Sound);
           
            StartCoroutine(SpiralAnimation());
        }
    }

    private IEnumerator SpiralAnimation()
    {
        float duration = 3.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = transform.localScale;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Float up to the progress bar
            transform.position = Vector3.Lerp(originalPosition, progressBar.transform.position, t);
            Debug.Log("floating");

            // Spiral animation logic
            float angle = t * 360f * 5f; // 5 rotations
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * t;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * t;

            // Optionally, you can add scaling logic here
            float scale = Mathf.Lerp(1f, 2f, t);
            transform.localScale = originalScale * scale;

            // Set position based on the spiral motion
            transform.position += new Vector3(x, y, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the object reaches the progress bar precisely
        transform.position = progressBar.transform.position;

        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Go back to the original size
        transform.localScale = originalScale;
    }

    private IEnumerator JigglingA1()
    {
        float antsDuration = 5.0f;
        float antsElapsedTime = 0f;

        Vector3 originalAntsPosition = a1.transform.position;

        while (antsElapsedTime < antsDuration)
        {
            float antT = antsElapsedTime / antsDuration;

            // Jiggle animation logic
            float antX = Mathf.Sin(antT * Mathf.PI * 2f) * 0.1f; // Adjust the jiggle amount
            float antY = Mathf.Cos(antT * Mathf.PI * 2f) * 0.1f; // Adjust the jiggle amount

            // Set position based on the jiggle motion
            a1.transform.position = originalAntsPosition + new Vector3(antX, antY, 0f);

            antsElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the snake reaches the original position precisely
        a1.transform.position = originalAntsPosition;
    }

}
