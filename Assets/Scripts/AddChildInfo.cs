using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement; // Add this using statement for SceneManager.
using Firebase.Database;

public class AddChildInfo : MonoBehaviour
{
    public TMP_InputField ChildNameInput;
    public TMP_InputField ChildDOBInput;
    public TMP_Dropdown ChildGenderOptions;

    public void OnNextButtonClicked()
    {
        // Get child information from UI elements.
        string childName = ChildNameInput.text;
        string childDOB = ChildDOBInput.text;
        string childGender = ChildGenderOptions.options[ChildGenderOptions.value].text;

        // Store child information in PlayerPrefs.
        PlayerPrefs.SetString("ChildName", childName);
        PlayerPrefs.SetString("ChildBirthday", childDOB);
        PlayerPrefs.SetString("ChildGender", childGender);

        // Load the next scene (replace "SetChildPin" with your actual scene name).
        SceneManager.LoadScene("SetChildPin");
    }
}
