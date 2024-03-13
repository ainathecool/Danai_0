using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        // Subscribe to the onEndEdit event of the input field
        inputField.onEndEdit.AddListener(HandleEndEdit);
    }

    void HandleEndEdit(string inputText)
    {
        Debug.Log("Input Text: " + inputText);
    }
}
