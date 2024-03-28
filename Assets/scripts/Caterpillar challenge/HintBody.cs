using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBody : MonoBehaviour
{
    public GameObject hintbody;
    public GameObject body;
    public GameObject draggedBody;

    private bool isAnimating = false;


    public void OnHintsButtonClicked()
    {
        Debug.Log("burtn clicked");
        if (!isAnimating)
        {
            if (body.transform.position != draggedBody.transform.position)
            {
                StartCoroutine(ScalingAnimation());
            }
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

            float scale = Mathf.Lerp(10f, 20f, t);
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
