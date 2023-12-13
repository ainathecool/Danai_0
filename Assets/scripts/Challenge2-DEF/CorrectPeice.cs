using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorrectPeice : MonoBehaviour
{

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pickUpClip, dropClip,initial;
    [SerializeField] private string nextScene;
    private bool dragging, placed;
    private Vector2 offset, originalPosition;
    private PuzzleSlot _slot;

    public GameObject EApple;
    public GameObject draggedApple;
    public GameObject hintE;




    void Awake()
    {
        originalPosition = transform.position;
        source = GetComponent<AudioSource>();

        // Add a delay of 2 seconds (you can adjust the delay time as needed)
        Invoke("PlayInitialSound", 2f);
    }

    void PlayInitialSound()
    {
        source.PlayOneShot(initial);
    }
    void Update()
    {
        if (placed) return;
        if (!dragging) return;

        var mousePosition = GetMousePos();

        transform.position = mousePosition - offset;
    }
    void OnMouseDown()
    {
        dragging = true;
        source.PlayOneShot(pickUpClip);
        offset = GetMousePos() - (Vector2)transform.position;
    }

    void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, hintE.transform.position) < 1)
        {
            transform.position = hintE.transform.position;
            source.PlayOneShot(dropClip);
            placed = true;
            SceneManager.LoadScene(nextScene);

        }
        else
        {
            transform.position = originalPosition;  //here the grabbed alphabet goes back to its original position 
            dragging = false;
        }



    }



    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
