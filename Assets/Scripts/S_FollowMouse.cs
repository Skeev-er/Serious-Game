using UnityEngine;

public class S_FollowMouse : MonoBehaviour
{
    private Rigidbody2D body;
    private LineRenderer lineRenderer;
    public Material lineMaterial;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        if (gameObject.GetComponent<LineRenderer>())
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    void CreateLineRenderer(Vector3 objectToDragPos)
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, objectToDragPos);

        lineRenderer.startWidth = 0.4f;
        lineRenderer.endWidth = 0.2f;

        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
    }
}
