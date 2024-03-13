using UnityEngine;
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

        inputField.textComponent.enableWordWrapping = true;
        
    }
}
