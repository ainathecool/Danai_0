using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintE : MonoBehaviour
{
    public GameObject hintE;
    public GameObject EApple;
    public GameObject draggedApple;

    private bool isAnimating = false;


    public void OnHintsButtonClicked()
    {
        if (!isAnimating)
        {
            if (EApple.transform.position != draggedApple.transform.position)
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

        Vector3 originalScale = hintE.transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float scale = Mathf.Lerp(13f, 26f, t);
            hintE.transform.localScale = originalScale * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }



        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Go back to the original size
        hintE.transform.localScale = originalScale;
        isAnimating = false;
    }
}
