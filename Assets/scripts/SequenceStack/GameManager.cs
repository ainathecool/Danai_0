using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject milkBox;
    public float maxX; // limit the left or right
    public Transform spawnPoint;
    public float spawnRate; // at what rate different blocks fall
    public int maxMilkBoxes = 3; // maximum number of milk boxes allowed
    private int currentMilkBoxes = 0;

    bool gameStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartSpawning();
            gameStarted = true;
        }

    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate); // This will call the function again and again
    }

    void SpawnBlock()
    {
        if (currentMilkBoxes < maxMilkBoxes)
        {
            Vector3 spawnPos = spawnPoint.position;
            spawnPos.x = Random.Range(-maxX, maxX);

            Instantiate(milkBox, spawnPos, Quaternion.identity);

            // Increment the current milk boxes counter
            currentMilkBoxes++;
        }
        else
        {
            // If the maximum number of milk boxes is reached, stop spawning
            CancelInvoke("SpawnBlock");
        }
    }
}
