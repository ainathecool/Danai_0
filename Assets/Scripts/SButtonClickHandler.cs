using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SButtonClickHandler : MonoBehaviour
{
    public AudioClip s2Sound;
    public GameObject progressBar;
    public GameObject snake;
    public GameObject s1Button, s2Button; //for checking if the other two buttons are on progress bar or where, so we can move to the next scene
    public string nextSceneName;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClicked()
    {
        audioSource.PlayOneShot(s2Sound);
        Debug.Log("button clicked");

        if(transform.position != progressBar.transform.position)
        { 
            // Spiral animation and grow
            StartCoroutine(SpiralAnimation());
            
        }
        StartCoroutine(SnakeAnimation());


        // Optional: You can trigger other actions here.
    }

    private IEnumerator SpiralAnimation()
    {
        float duration = 5.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = transform.localScale;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Float up to the progress bar
            transform.position = Vector3.Lerp(originalPosition, progressBar.transform.position, t);

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

        // Optional: You can trigger other actions here.

        // Start the snake animation
        StartCoroutine(SnakeAnimation());
    }

    private IEnumerator SnakeAnimation()
    {
        float snakeDuration = 5.0f;
        float snakeElapsedTime = 0f;

        Vector3 originalSnakePosition = snake.transform.position;

        while (snakeElapsedTime < snakeDuration)
        {
            float snakeT = snakeElapsedTime / snakeDuration;

            // Jiggle animation logic
            float snakeX = Mathf.Sin(snakeT * Mathf.PI * 2f) * 0.1f; // Adjust the jiggle amount
            float snakeY = Mathf.Cos(snakeT * Mathf.PI * 2f) * 0.1f; // Adjust the jiggle amount

            // Set position based on the jiggle motion
            snake.transform.position = originalSnakePosition + new Vector3(snakeX, snakeY, 0f);

            snakeElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the snake reaches the original position precisely
        snake.transform.position = originalSnakePosition;
        if (s1Button.transform.position == progressBar.transform.position && s2Button.transform.position == progressBar.transform.position)
        {
            StartCoroutine(LoadSceneAfterDelay());
        }
    }
    private IEnumerator LoadSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(3f);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
