using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncorrectOptions : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pickUpClip, dropClip;
    private bool dragging;
    private Vector2 offset, originalPosition;




    void Awake()
    {
        originalPosition = transform.position;
    }
    void Update()
    {
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
        transform.position = originalPosition;  //here the grabbed alphabet goes back to its original position 
        dragging = false;
        source.PlayOneShot(dropClip);
    }

    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
