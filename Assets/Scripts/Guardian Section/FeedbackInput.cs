/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/

using UnityEngine;
using TMPro;

public class FeedbackInput : MonoBehaviour
{
    public TMP_Text textMeshProText;

    void Start()
    {
        // Set text to be editable
        textMeshProText.richText = true;
    }
    void Update()
    {
        // Check for keyboard input
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Set text to editable
            //textMeshProText.isInputAllowed = true;

            // Place focus on the text field
           // textMeshProText.Select();
        }
    }
    /*void Update()
    {
        // Check for input and update text accordingly
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Input Text: " + textMeshProText.text);
            // You can process or store the input text here

            // Clear input text
            textMeshProText.text = "";
        }
    }*/
}
