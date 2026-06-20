using UnityEngine;

// This class is attached to the mouse
// It adds a line renderer and updates its position every frame

public class S_DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Material lineMaterial;
    private Rigidbody2D objectBody;

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        if (gameObject.GetComponent<LineRenderer>() != null && objectBody != null)
        {
            lineRenderer.SetPosition(0, objectBody.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    void CreateLineRenderer(Rigidbody2D objectToSpin)
    {
        objectBody = objectToSpin;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, objectBody.position);

        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.15f;

        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = new Color(1f, 1f, 1f, 0.5f); // White with half opacity
        lineRenderer.endColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Gray with half opacity
    }
}
