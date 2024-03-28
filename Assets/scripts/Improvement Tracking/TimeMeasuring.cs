using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMeasuring : MonoBehaviour
{
    private const string END_TIME = "EndTime";
    private const string TIME_TAKEN_KEY = "TimeTaken";


    // Start is called before the first frame update
    void Start()
    {
        float timeTaken = PlayerPrefs.GetFloat(TIME_TAKEN_KEY);
        // Start timer to measure scene display time
        StartCoroutine(MeasureSceneDisplayTime());

    }


    private System.Collections.IEnumerator MeasureSceneDisplayTime()
    {
        string endTime = PlayerPrefs.GetString(END_TIME);
        float startTime = Time.time;
        while (endTime != "1")
        {
            yield return null; // Wait for next frame
        }
        float endingTime = Time.time;
        float sceneDisplayTime = endingTime - startTime;

        // Store scene display time in PlayerPrefs
        PlayerPrefs.SetFloat(TIME_TAKEN_KEY, sceneDisplayTime);
    }
}
