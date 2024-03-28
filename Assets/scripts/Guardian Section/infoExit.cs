using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class infoExit : MonoBehaviour
{
    public TextMeshProUGUI headingText;
    public TextMeshProUGUI informationTextBox;
    public GameObject buttonsContainer;
    public GameObject otherButton;
    public GameObject btnItself;

    public void OnButtonClick()
    {
        // Change heading text to "Word to Spread"
        headingText.text = "Word to Spread";

        // Hide information text box
        informationTextBox.gameObject.SetActive(false);

        // Hide the clicked button
        btnItself.SetActive(false);

        //Unhide the previous btn
        otherButton.SetActive(true);

        // Unhide the buttons container
        buttonsContainer.SetActive(true);
    }
}
