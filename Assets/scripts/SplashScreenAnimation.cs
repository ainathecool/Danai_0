using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenAnimation : MonoBehaviour
{
    
    public GameObject logo;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScalingAnimation());
    }

    private IEnumerator ScalingAnimation()
    {
       

        float duration = 3.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = logo.transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float scale = Mathf.Lerp(5f, 10f, t);
            logo.transform.localScale = originalScale * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

      

        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Go back to the original size
        logo.transform.localScale = logo.transform.localScale;

    }



}
