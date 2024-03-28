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
    public TextMeshProUGUI ChildNameError, ChildDOBError;

    public void OnNextButtonClicked()
    {
        // Get child information from UI elements.
        string childName = ChildNameInput.text;
        string childDOB = ChildDOBInput.text;
        string childGender = ChildGenderOptions.options[ChildGenderOptions.value].text;

        // Validate child name.
        if (!IsChildNameValid(childName))
        {
            ChildNameError.text = "Child name can only contain letters.";
            return;
        }
        ChildNameError.text = "";

        // Validate child date of birth.
        if (!IsChildDOBValid(childDOB))
        {
            ChildDOBError.text = "Invalid date format.";
            return;
        }
        ChildDOBError.text = "";

        // Store child information in PlayerPrefs.
        PlayerPrefs.SetString("ChildName", childName);
        PlayerPrefs.SetString("ChildBirthday", childDOB);
        PlayerPrefs.SetString("ChildGender", childGender);

        // Load the next scene (replace "SetChildPin" with your actual scene name).
        SceneManager.LoadScene("SetChildPin");
    }

    // Validate child name (allow only letters).
    private bool IsChildNameValid(string name)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(name, "^[a-zA-Z]+$");
    }

  
    // Validate child date of birth format and validity.
    private bool IsChildDOBValid(string dob)
    {
        // Check if the format is valid.
        if (!System.Text.RegularExpressions.Regex.IsMatch(dob, @"^\d{1,2}[-/]\d{1,2}[-/]\d{4}$"))
        {
            return false;
        }

        // Split the date components.
        string[] dateParts = dob.Split(new char[] { '/', '-' });

        // Check if there are three parts (day, month, year).
        if (dateParts.Length != 3)
        {
            return false;
        }

        // Parse day, month, and year.
        int day, month, year;
        if (!int.TryParse(dateParts[0], out day) || !int.TryParse(dateParts[1], out month) || !int.TryParse(dateParts[2], out year))
        {
            return false;
        }

        // Check if day, month, and year are within valid ranges.
        if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1900 || year > DateTime.Now.Year)
        {
            return false;
        }

        // Check for specific months with fewer than 31 days.
        if ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)
        {
            return false;
        }

        // Check for February and leap years.
        if (month == 2)
        {
            if (day > 29 || (day > 28 && !DateTime.IsLeapYear(year)))
            {
                return false;
            }
        }

        return true;
    }

}
