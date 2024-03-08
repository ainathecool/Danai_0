using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DynamicTextLoader : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public string textFromFirebase; // Text loaded from Firebase
    public TextMeshProUGUI heading;
    public string headingFromFirebase;

    void Start()
    {
        // Load text from Firebase (Replace this with your Firebase loading code)
        textFromFirebase = "Sample text loaded from Firebase" +
            "TextMeshPro Settings:\r\n\r\nReview the settings of the TextMeshPro component itself. Ensure that properties such as Text Overflow and Word Wrapping are configured appropriately for your text content.\r\nCheck if any custom settings in TextMeshPro are conflicting with the resizing behavior.\r\nScripting or Animation Interference:\r\n\r\nCheck if there are any scripts or animations affecting the size or position of the TextMeshPro object. There might be conflicting behaviors that prevent the dynamic resizing from working correctly.\r\nTesting in a Clean Scene:\r\n\r\nCreate a minimal test scene with just a TextMeshPro object, Scroll Rect, and Content Size Fitter to isolate any potential issues from other parts of your project. This can help identify if the problem lies within your setup or elsewhere in your project.\r\nBy carefully examining these aspects, you should be able to identify the cause of the issue with TextMeshPro not dynamically adjusting its size and rectify it accordingly.\r\n\r\n\r\n\r\nUser\r\ntell me how can i give a script to check if the text would load from script with preferred text box size\r\nChatGPT\r\nTo dynamically adjust the size of a TextMeshPro object based on the content loaded from a script, you can create a simple script that updates the text content and then adjusts the size of the TextMeshPro object accordingly. Here's a basic example of how you can achieve this:";
        headingFromFirebase = "Hello this is Maaidah from FAST";


            textMeshPro.text = textFromFirebase;
        //textMeshPro.overflowMode = TextOverflowModes.Truncate; // or TextOverflowModes.Ellipsis
        heading.text = headingFromFirebase;

        // Adjust the size of the TextMeshPro object
        //textMeshPro.autoSizeTextContainer = true; // Automatically adjusts the size of the text container
        //textMeshPro.enableAutoSizing = true;

    }

   /* void AdjustTextMeshProSize()
    {
        // Force TextMeshPro to update its layout
        LayoutRebuilder.ForceRebuildLayoutImmediate(textMeshPro.rectTransform);

        // Optionally, you can adjust the size of the parent RectTransform
        RectTransform parentRectTransform = textMeshPro.rectTransform.parent.GetComponent<RectTransform>();
        if (parentRectTransform != null)
        {
            parentRectTransform.sizeDelta = new Vector2(textMeshPro.preferredWidth, textMeshPro.preferredHeight);
        }
    }*/
}
