using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TraceImage : MonoBehaviour
{
    public Material lineMaterial;
    private bool isDrawing = false;
    private List<GameObject> lineObjects = new List<GameObject>();
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private float gapThreshold = 0.1f; // Adjust the gap threshold as needed

    public float minArea, maxArea;
    public string nextSceneName;

    public PolygonCollider2D outlinedCollider;
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
        lineRenderer.startWidth = 0.55f;
        lineRenderer.endWidth = 0.55f;
        lineRenderer.sortingLayerName = "Default"; // Adjust to your sorting layer
        lineRenderer.sortingOrder = 3; // Adjust the sorting order
        lineRenderer.sortingOrder = 4; // Adjust the sorting order
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
                CalculateAreaOfDrawnShape();
            }
        }
    }

    public void VerifyShape()
    {
        // Calculate the area of the drawn shape
        float drawnArea = PlayerPrefs.GetFloat("DrawnArea");


        if (drawnArea > minArea && drawnArea <= maxArea)
        {
            // Proceed to the next scene if the shape is accurate and complete within the tolerance
            Debug.Log("Shape verified. Proceeding to next scene.");
            Debug.Log("drawn area: " + drawnArea );
            // Add code here to load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Inform the user that the shape is not accurate or complete
            Debug.Log("Shape is not accurate or complete. Please try again.");
            Debug.Log("drawn area: " + drawnArea);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private float CalculateAreaOfDrawnShape()
    {
        float area = 0f;
        foreach (var lineRenderer in lineRenderers)
        {
            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);

            // Convert Vector3 points to Vector2 points
            Vector2[] points2D = new Vector2[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                points2D[i] = new Vector2(positions[i].x, positions[i].y);
            }

            area += CalculatePolygonArea(points2D);
        }
        PlayerPrefs.SetFloat("DrawnArea", area);
        return area;
    }



    private float CalculatePolygonArea(Vector2[] polygonPoints)
    {
        float area = 0f;
        for (int i = 0; i < polygonPoints.Length; i++)
        {
            Vector2 currentPoint = polygonPoints[i];
            Vector2 nextPoint = polygonPoints[(i + 1) % polygonPoints.Length];
            area += (currentPoint.x * nextPoint.y - nextPoint.x * currentPoint.y);
        }
        return Mathf.Abs(area / 2);
    }
}