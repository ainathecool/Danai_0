using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VowelScript : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private float movementAmount = 0.1f; // Adjust this value to control the movement amount

    private bool moving;

    void Update()
    {
        if (moving)
        {
            // Perform the movement here
            transform.position += Vector3.up * movementAmount * Time.deltaTime;
        }
    }

    void OnMouseDown()
    {
        moving = true;
        source.PlayOneShot(clickClip);
    }

    void OnMouseUp()
    {
        moving = false;
    }
}
