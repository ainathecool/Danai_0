using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
{

    
 
    public GameObject hintA;
    public GameObject aApple;
    public GameObject draggedApple;

    private bool isAnimating = false;
    

    public void OnHintsButtonClicked()
    {
        Debug.Log("burtn clicked");
        if (!isAnimating)
        {
            if(aApple.transform.position != draggedApple.transform.position) { 
            StartCoroutine(ScalingAnimation());
           }
        }
    }

    private IEnumerator ScalingAnimation()
    {
        isAnimating = true;
        

        float duration = 1.0f;
        float elapsedTime = 0f;

        Vector3 originalScale = hintA.transform.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float scale = Mathf.Lerp(13f, 26f, t);
            hintA.transform.localScale = originalScale * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        

        // Wait for a moment
        yield return new WaitForSeconds(1.0f);

        // Go back to the original size
        hintA.transform.localScale = originalScale;
        isAnimating = false;
    }
}
