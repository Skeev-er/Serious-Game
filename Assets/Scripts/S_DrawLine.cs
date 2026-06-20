using UnityEngine;

// This class is attached to the mouse
// It adds a line renderer and updates its position every frame

public class S_DrawLine : MonoBehaviour
{
    private LineRenderer lr;
    public Material lineMaterial;
    private Rigidbody2D objectBody;

    void Update()
    {
        // Keep the gameObject centred on the mouse at all times
        transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        // If dragging
        if (gameObject.GetComponent<LineRenderer>() != null && objectBody != null)
        {
            // Set the start and end points of the line
            lr.SetPosition(0, objectBody.position);
            lr.SetPosition(1, transform.position);
        }
    }

    // This is called by S_GrabObject when dragging starts
    void CreateLineRenderer(Rigidbody2D objectToSpin)
    {
        objectBody = objectToSpin;

        lr = gameObject.AddComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, objectBody.position);

        lr.startWidth = 0.3f;
        lr.endWidth = 0.15f;

        lr.material = lineMaterial;
        lr.startColor = new Color(1f, 1f, 1f, 0.5f); // White with half opacity
        lr.endColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Gray with half opacity
    }
}
