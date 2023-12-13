using Firebase;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            // Firebase has been successfully initialized here.
            if (app != null)
            {
                Debug.Log("Firebase has been successfully initialized.");
            }
            else
            {
                Debug.LogError("Firebase initialization failed!");
            }
        });
    }
}
