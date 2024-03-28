/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using Firebase.Database;
using Firebase.Auth;

public class Window_Graph : MonoBehaviour {

    public string phaseName;

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private DatabaseReference databaseReference; //will be used to get logged in guardian's stuff

    private void Awake() {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        // List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        //ShowGraph(valueList);

        string childToLoad = PlayerPrefs.GetString("ChildToLoad");

        if (childToLoad == "Child01ImpTracking")
        {
            string childId = PlayerPrefs.GetString("Child01ImpTracking");
            Phases(childId);

        }
        else if(childToLoad == "Child02ImpTracking")
        {
            string childId = PlayerPrefs.GetString("Child02ImpTracking");
            Phases(childId);
            
        }
        else if (childToLoad == "Child3ImpTracking")
        {
            string childId = PlayerPrefs.GetString("Child03ImpTracking");
            Phases(childId);
        }





    }

    private void Phases(string childId)
    {
        if (phaseName == "1")
        {
            GetDataPhase01(childId);
        }
        else if (phaseName == "2")
        {
            GetDataPhase02(childId);
        }
        else if (phaseName == "3")
        {
            GetDataPhase03(childId);
        }
        else if (phaseName == "4")
        {
            GetDataPhase04(childId);
        }
        else if (phaseName == "6")
        {
            GetDataPhase06(childId);
        }
    }

    private async void GetDataPhase01(string childId)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // Fetch data from the ImprovementTracking01 node
        DataSnapshot dataSnapshotPhase1 = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(childId).Child("ImprovementTracking01").GetValueAsync();

        // Initialize a list to store the values
        List<int> valueList = new List<int>();

        // Check if data exists in ImprovementTracking01
        if (dataSnapshotPhase1.Exists)
        {
            // Iterate through each child node under ImprovementTracking01
            foreach (var childSnapshot in dataSnapshotPhase1.Children)
            {
                // Parse the value as an integer and add it to the list
                int value = int.Parse(childSnapshot.Value.ToString());
                valueList.Add(value);
                Debug.Log(value);
            }
        }
        else
        {
            // If no data exists, add a default value of 0 to the list
            valueList.Add(0);
        }

        Debug.Log("valuelist: " + valueList.Count);
        // Now valueList contains all the values from ImprovementTracking01 or a single 0 if no data exists
        ShowGraph(valueList);
    }

    private async void GetDataPhase02(string childId)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // Fetch data from the ImprovementTracking01 node
        DataSnapshot dataSnapshotPhase1 = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(childId).Child("ImprovementTracking02").GetValueAsync();

        // Initialize a list to store the values
        List<int> valueList = new List<int>();

        // Check if data exists in ImprovementTracking01
        if (dataSnapshotPhase1.Exists)
        {
            // Iterate through each child node under ImprovementTracking01
            foreach (var childSnapshot in dataSnapshotPhase1.Children)
            {
                // Parse the value as an integer and add it to the list
                int value = int.Parse(childSnapshot.Value.ToString());
                valueList.Add(value);
                Debug.Log(value);
            }
        }
        else
        {
            // If no data exists, add a default value of 0 to the list
            valueList.Add(0);
        }

        Debug.Log("valuelist: " + valueList.Count);
        // Now valueList contains all the values from ImprovementTracking01 or a single 0 if no data exists
        ShowGraph(valueList);
    }

    private async void GetDataPhase03(string childId)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // Fetch data from the ImprovementTracking01 node
        DataSnapshot dataSnapshotPhase1 = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(childId).Child("ImprovementTracking03").GetValueAsync();

        // Initialize a list to store the values
        List<int> valueList = new List<int>();

        // Check if data exists in ImprovementTracking01
        if (dataSnapshotPhase1.Exists)
        {
            // Iterate through each child node under ImprovementTracking01
            foreach (var childSnapshot in dataSnapshotPhase1.Children)
            {
                // Parse the value as an integer and add it to the list
                int value = int.Parse(childSnapshot.Value.ToString());
                valueList.Add(value);
                Debug.Log(value);
            }
        }
        else
        {
            // If no data exists, add a default value of 0 to the list
            valueList.Add(0);
        }

        Debug.Log("valuelist: " + valueList.Count);
        // Now valueList contains all the values from ImprovementTracking01 or a single 0 if no data exists
        ShowGraph(valueList);
    }

    private async void GetDataPhase04(string childId)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // Fetch data from the ImprovementTracking01 node
        DataSnapshot dataSnapshotPhase1 = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(childId).Child("ImprovementTracking0506").GetValueAsync();

        // Initialize a list to store the values
        List<int> valueList = new List<int>();

        // Check if data exists in ImprovementTracking01
        if (dataSnapshotPhase1.Exists)
        {
            // Iterate through each child node under ImprovementTracking01
            foreach (var childSnapshot in dataSnapshotPhase1.Children)
            {
                // Parse the value as an integer and add it to the list
                int value = int.Parse(childSnapshot.Value.ToString());
                valueList.Add(value);
                Debug.Log(value);
            }
        }
        else
        {
            // If no data exists, add a default value of 0 to the list
            valueList.Add(0);
        }

        Debug.Log("valuelist: " + valueList.Count);
        // Now valueList contains all the values from ImprovementTracking01 or a single 0 if no data exists
        ShowGraph(valueList);
    }
    private async void GetDataPhase06(string childId)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // Fetch data from the ImprovementTracking01 node
        DataSnapshot dataSnapshotPhase1 = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(childId).Child("ImprovementTracking06").GetValueAsync();

        // Initialize a list to store the values
        List<int> valueList = new List<int>();

        // Check if data exists in ImprovementTracking01
        if (dataSnapshotPhase1.Exists)
        {
            // Iterate through each child node under ImprovementTracking01
            foreach (var childSnapshot in dataSnapshotPhase1.Children)
            {
                // Parse the value as an integer and add it to the list
                int value = int.Parse(childSnapshot.Value.ToString());
                valueList.Add(value);
                Debug.Log(value);
            }
        }
        else
        {
            // If no data exists, add a default value of 0 to the list
            valueList.Add(0);
        }

        Debug.Log("valuelist: " + valueList.Count);
        // Now valueList contains all the values from ImprovementTracking01 or a single 0 if no data exists
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList) {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float totalWidth = graphContainer.sizeDelta.x;

        float xSize = totalWidth / (valueList.Count + 1); // Adding 1 to count for space between points

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(159f / 255f, 63f / 255f, 54f / 255f, 0.5f); // Set color to RGB (159, 63, 54) with alpha 0.5
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

}
