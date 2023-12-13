using System.Collections;
using UnityEngine;

public class SButtonController : MonoBehaviour
{
    public AudioClip s1Sound;
    public GameObject s1Button, s2Button, s3Button; // Assign the s1 button GameObject in the Unity Editor
    public GameObject progressBar;

    private AudioSource audioSource;
    private Vector3 originalPosition;
    private bool isAnimating = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Assuming you have a method connected to the hintsButton's onClick event.
    public void OnHintsButtonClick()
    {
        if (!isAnimating)
        {
            if(s1Button.transform.position != progressBar.transform.position) 
            {
                originalPosition = s1Button.transform.position;
                StartCoroutine(HoverAnimationS1());
            }
            else if(s2Button.transform.position != progressBar.transform.position)
            {
                originalPosition = s2Button.transform.position;
                StartCoroutine(HoverAnimationS2());
            }
            else if(s3Button.transform.position != progressBar.transform.position)
            {
                originalPosition= s3Button.transform.position;
                StartCoroutine(HoverAnimationS3());
            }
            else
            {
                //stroing this here bcs it means all 3 buttons are on prpgress bar now, game is finished, move ahead
                PlayerPrefs.SetInt("complete", 1);
                PlayerPrefs.Save();
            }
            
        }
    }

    private IEnumerator HoverAnimationS1()
    {
        isAnimating = true;
        audioSource.PlayOneShot(s1Sound);

        // Jump up
       s1Button.transform.Translate(Vector3.up * 0.4f, Space.World);

        yield return new WaitForSeconds(1.5f);

        // Return to original position
        s1Button.transform.position = originalPosition;

        yield return new WaitForSeconds(1.5f);

        isAnimating = false;
    }

    private IEnumerator HoverAnimationS2()
    {
        isAnimating = true;
        audioSource.PlayOneShot(s1Sound);

        // Jump up
        s2Button.transform.Translate(Vector3.up * 0.4f, Space.World);

        yield return new WaitForSeconds(1.5f);

        // Return to original position
        s2Button.transform.position = originalPosition;

        yield return new WaitForSeconds(1.5f);

        isAnimating = false;
    }

    private IEnumerator HoverAnimationS3()
    {
        isAnimating = true;
        audioSource.PlayOneShot(s1Sound);

        // Jump up
        s3Button.transform.Translate(Vector3.up * 0.4f, Space.World);

        yield return new WaitForSeconds(1.5f);

        // Return to original position
        s3Button.transform.position = originalPosition;

        yield return new WaitForSeconds(1.5f);

        isAnimating = false;
    }
}
