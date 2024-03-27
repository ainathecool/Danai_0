using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    public TMP_Text tmpText; // Reference to your Text Mesh Pro field
    public TMP_InputField inputField; // Reference to your input field

    private void Start()
    {
        // Set the initial value of the input field to "25"
        inputField.text = "25";
        // Set the initial font size of the Text Mesh Pro field to the default font size of the input field
        tmpText.fontSize = int.Parse(inputField.text);

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

    public void IncreaseFontSize()
    {
        // Increase font size of TMP text
        tmpText.fontSize++;
        // Update input field text
        inputField.text = tmpText.fontSize.ToString();
    }

    public void DecreaseFontSize()
    {
        // Decrease font size of TMP text
        tmpText.fontSize--;
        // Update input field text
        inputField.text = tmpText.fontSize.ToString();
    }
}
