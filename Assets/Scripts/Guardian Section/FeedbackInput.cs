/*using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeedbackInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public float minHeight = 30f;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = inputField.GetComponent<RectTransform>();
    }

    void Update()
    {
        TMP_Text textComponent = inputField.textComponent;
        float preferredHeight = LayoutUtility.GetPreferredHeight(textComponent.rectTransform) + 10f; // Adding some padding

        // Ensure minimum height
        preferredHeight = Mathf.Max(preferredHeight, minHeight);

        // Keep the width constant
        float currentWidth = rectTransform.rect.width;
        rectTransform.sizeDelta = new Vector2(currentWidth, preferredHeight);
    }
}*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeedbackInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public ScrollRect scrollView;
    public float minHeight = 30f;

    RectTransform rectTransform;
    RectTransform contentRectTransform;

    void Start()
    {
        rectTransform = inputField.GetComponent<RectTransform>();
        contentRectTransform = scrollView.content.GetComponent<RectTransform>();
    }

    void Update()
    {
        TMP_Text textComponent = inputField.textComponent;
        float preferredHeight = LayoutUtility.GetPreferredHeight(textComponent.rectTransform) + 10f; // Adding some padding

        // Ensure minimum height
        preferredHeight = Mathf.Max(preferredHeight, minHeight);

        // Keep the width constant
        float currentWidth = rectTransform.rect.width;
        rectTransform.sizeDelta = new Vector2(currentWidth, preferredHeight);

        // Check if the input field's height exceeds the available space in the Scroll View
        if (preferredHeight > contentRectTransform.rect.height)
        {
            // Enable vertical scrolling
            scrollView.vertical = true;

            // Enable masking
            scrollView.viewport.GetComponent<Mask>().enabled = true;
        }
        else
        {
            // Disable vertical scrolling
            scrollView.vertical = false;

            // Disable masking
            scrollView.viewport.GetComponent<Mask>().enabled = false;
        }
    }
}

