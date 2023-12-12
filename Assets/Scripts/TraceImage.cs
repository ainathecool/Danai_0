using System.Collections.Generic;
using UnityEngine;

public class TraceImage : MonoBehaviour
{
    public Material lineMaterial;
    private bool isDrawing = false;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

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
        // Create a new LineRenderer for the current drawing
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.sortingLayerName = "Default"; // Adjust to your sorting layer
        lineRenderer.sortingOrder = 4; // Adjust the sorting order

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
            currentLineRenderer.positionCount = currentPositionCount + 1;
            currentLineRenderer.SetPosition(currentPositionCount, mousePosition);
            Debug.Log("Drawing line point");
        }
    }
}
