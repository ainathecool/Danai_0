using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject milkBox;
    public float maxX; // limit the left or right
    public Transform spawnPoint;
    public float spawnRate; // at what rate different blocks fall
    public GameObject tapText;
    public TextMeshProUGUI scoreText;
    int score = 0;
    bool gameStarted = false;

    private Transform milkBoxStack; // Parent object for stacking milk boxes

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartSpawning();
            tapText.SetActive(false);
            gameStarted = true;
        }
    }

    void StartSpawning()
    {
        milkBoxStack = new GameObject("MilkBoxStack").transform; // Create an empty game object to serve as the parent
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate); // This will call the function again and again
    }

    void SpawnBlock()
    {
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);

        GameObject newMilkBox = Instantiate(milkBox, spawnPos, Quaternion.identity);

        // Set the milk box as a child of the parent stack
        newMilkBox.transform.parent = milkBoxStack;

        score++;
        scoreText.text = score.ToString();
    }
}
