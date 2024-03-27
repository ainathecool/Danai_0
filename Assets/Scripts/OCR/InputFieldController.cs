using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    public TMP_Text tmpText; // Reference to your Text Mesh Pro field
    public TMP_InputField inputField; // Reference to your input field

    private void Start()
    {
        // Add listener to the input field to detect changes
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void OnInputFieldValueChanged(string newValue)
    {
        int fontSize;
        // Try parsing the input field's value into an integer
        if (int.TryParse(newValue, out fontSize))
        {
            // If parsing is successful, update the TMP font size
            tmpText.fontSize = fontSize;
        }
    }
}
