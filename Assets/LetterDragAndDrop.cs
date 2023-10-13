using UnityEngine;
using UnityEngine.EventSystems;

public class LetterDragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 initialPosition;
    private bool isDragging = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDragging)
        {
            initialPosition = transform.position;
            isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = initialPosition.z; // Maintain the same z-coordinate
            transform.position = newPosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        // Add logic to check if the letter is dropped in the correct order
    }
}
