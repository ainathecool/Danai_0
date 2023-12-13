using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public GameObject hintI;
    public GameObject iApple;
    public GameObject draggedApple;

    private bool isAnimating = false;


    public void OnHintsButtonClicked()
    {
        if (!isAnimating)
        {
            if (iApple.transform.position != draggedApple.transform.position)
            {
                Debug.Log("enter");
                StartCoroutine(ScalingAnimation());
            }
        }
    }

    private IEnumerator ScalingAnimation()
    {
        isAnimating = true;


        float duration = 1.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = hintI.transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float scale = Mathf.Lerp(13f, 26f, t);
            hintI.transform.localScale = originalScale * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }



        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Go back to the original size
        hintI.transform.localScale = originalScale;
        isAnimating = false;
    }
}
