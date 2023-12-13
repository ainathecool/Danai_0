using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TButtonHandler : MonoBehaviour
{

    public AudioClip t1Sound;
    public AudioClip t2Sound;
    public AudioClip hintTSound;
    public GameObject progressBar;
    public GameObject t1, t2, tennisRacket;


    private AudioSource audioSource;
    private bool isAnimating = false;
    private Vector3 originalPosition;
    private Vector3 touchStart;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       // StartCoroutine(FallDownAnimation(t1, 10f));
       // StartCoroutine(FallDownAnimation(t2, 10f));

 

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - touchStart;
            tennisRacket.transform.position += new Vector3(direction.x, direction.y, 0f);
            if (Vector2.Distance(tennisRacket.transform.position, t1.transform.position) < 0.5)
            {
                OnTButtonClicked(t1);
            }
            if (Vector2.Distance(tennisRacket.transform.position, t2.transform.position) < 0.5)
            {
                OnTButtonClicked(t2);
            }
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }





    private IEnumerator FallDownAnimation(GameObject target, float fallDuration)
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = target.transform.position;
        Vector3 targetPosition = originalPosition - new Vector3(0f, 10f, 0f); // Move down by 10 units

        while (elapsedTime < fallDuration)
        {
            float t = elapsedTime / fallDuration;
            target.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait for a moment
        yield return new WaitForSeconds(1.0f);
    }

    public void OnHintsButtonClicked()
    {
        if (!isAnimating)
        {
            if (t1.transform.position != progressBar.transform.position)
            {
                originalPosition = t1.transform.position;
                StartCoroutine(AnimationT1());
            }
            else if(t2.transform.position != progressBar.transform.position)
            {
                originalPosition = t2.transform.position;
                StartCoroutine(AnimationT2());
            }
            else
            {
                PlayerPrefs.SetInt("complete", 1);
                PlayerPrefs.Save();
            }

        }
    }

    private IEnumerator AnimationT1()
    {
        audioSource.PlayOneShot(hintTSound);
        float Duration = 3.0f;
        float ElapsedTime = 0f;

         originalPosition = t1.transform.position;

        while (ElapsedTime < Duration)
        {
            float T = ElapsedTime / Duration;
          

            // Jiggle animation logic
            float X = Mathf.Sin(T * Mathf.PI * 2f) * 0.1f; // Adjust the jiggle amount
            float Y = Mathf.Cos(T * Mathf.PI * 2f) * 0.1f; // Adjust the jiggle amount

            // Set position based on the jiggle motion
            t1.transform.position = originalPosition + new Vector3(X, Y, 0f);

            ElapsedTime += Time.deltaTime;
            yield return null;
        }
   
        t1.transform.position = originalPosition;
    }

    private IEnumerator AnimationT2()
    {
        audioSource.PlayOneShot(hintTSound);
        float Duration = 3.0f;
        float ElapsedTime = 0f;

        originalPosition = t2.transform.position;

        while (ElapsedTime < Duration)
        {
            float T = ElapsedTime / Duration;

            // Jiggle animation logic
            float X = Mathf.Sin(T * Mathf.PI * 2f) * 0.1f; // Adjust the jiggle amount
            float Y = Mathf.Cos(T * Mathf.PI * 2f) * 0.1f; // Adjust the jiggle amount

            // Set position based on the jiggle motion
            t2.transform.position = originalPosition + new Vector3(X, Y, 0f);

            ElapsedTime += Time.deltaTime;
            yield return null;
        }


        t2.transform.position = originalPosition;
    }

    public void OnTButtonClicked(GameObject target)
    {

        Debug.Log("button clicked");

        if (target.transform.position != progressBar.transform.position)
        {
            audioSource.PlayOneShot(t1Sound);
            StartCoroutine(SpiralAnimation(target));
        }
    }

    private IEnumerator SpiralAnimation(GameObject target)
    {
        float duration = 5.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = target.transform.localScale;
        Vector3 originalPosition = target.transform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Float up to the progress bar
            target.transform.position = Vector3.Lerp(originalPosition, progressBar.transform.position, t);
            Debug.Log("floating");

            // Spiral animation logic
            float angle = t * 360f * 5f; // 5 rotations
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * t;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * t;

            // Optionally, you can add scaling logic here
            float scale = Mathf.Lerp(1f, 2f, t);
            target.transform.localScale = originalScale * scale;

            // Set position based on the spiral motion
            target.transform.position += new Vector3(x, y, 0f);


            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the object reaches the progress bar precisely
        target.transform.position = progressBar.transform.position;

        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Go back to the original size
        target.transform.localScale = originalScale;
    }





}
