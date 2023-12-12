using System.Collections.Generic;
using UnityEngine;

public class TraceImage : MonoBehaviour
{
    public Material lineMaterial;
    private bool isDrawing = false;
    private List<GameObject> lineObjects = new List<GameObject>();
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private float gapThreshold = 0.1f; // Adjust the gap threshold as needed

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            StartDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }

        if (isDrawing)
        {
            DrawLine();
        }
    }

    private void StartDrawing()
    {
        // Create a new empty GameObject for the current drawing
        GameObject lineObject = new GameObject("LineObject");
        lineObject.transform.parent = transform; // Set the current object as the parent

        // Add a LineRenderer component to the new GameObject
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.sortingLayerName = "Default"; // Adjust to your sorting layer
        lineRenderer.sortingOrder = 3; // Adjust the sorting order

        lineObjects.Add(lineObject);
        lineRenderers.Add(lineRenderer);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, mousePosition);
            Debug.Log("Line starting point");
        }
    }

    private void DrawLine()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            LineRenderer currentLineRenderer = lineRenderers[lineRenderers.Count - 1];

            int currentPositionCount = currentLineRenderer.positionCount;


            // Check the distance between the last point of the previous line and the new point
            if (currentPositionCount == 0 || Vector2.Distance(currentLineRenderer.GetPosition(currentPositionCount - 1), mousePosition) > gapThreshold)
            {
                // Add the new point to the line
                currentLineRenderer.positionCount = currentPositionCount + 1;
                currentLineRenderer.SetPosition(currentPositionCount, mousePosition);
                Debug.Log("Drawing line point");
            }
        }
    }
}