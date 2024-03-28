using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyValues : MonoBehaviour
{

    private DatabaseReference databaseReference; //will be used to get logged in guardian's stuff

    // Start is called before the first frame update
    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
