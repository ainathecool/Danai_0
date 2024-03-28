using UnityEngine;

public class IncorrectBody : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pickUpClip, dropClip;
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePosition = GetMousePos();
            transform.position = mousePosition + offset;
        }
    }

    void OnMouseDown()
    {
        dragging = true;
        source.PlayOneShot(pickUpClip);
        offset = transform.position - GetMousePos();
    }

    void OnMouseUp()
    {
        dragging = false;
        transform.position = originalPosition;
        source.PlayOneShot(dropClip);
    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
