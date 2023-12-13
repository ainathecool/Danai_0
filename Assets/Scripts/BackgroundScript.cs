using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScript : MonoBehaviour
{
    public string nextSceneName;

    private void Start()
    {
        // Retrieve the "complete" value from PlayerPrefs
        int completeValue = PlayerPrefs.GetInt("complete", 0);

        // Check if the hints are complete (value is 1)
        if (completeValue == 1)
        {
            // Switch to the next scene or perform other actions
            PlayerPrefs.SetInt("complete", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // If not complete, check again every 3 seconds
            InvokeRepeating("CheckCompleteValue", 0f, 3f);
        }
    }

    private void CheckCompleteValue()
    {
        // Retrieve the "complete" value from PlayerPrefs
        int completeValue = PlayerPrefs.GetInt("complete", 0);

        // Check if the hints are complete (value is 1)
        if (completeValue == 1)
        {
            // Cancel the repeating check
            CancelInvoke("CheckCompleteValue");
            PlayerPrefs.SetInt("complete", 0);
            PlayerPrefs.Save();

            // Switch to the next scene or perform other actions
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
