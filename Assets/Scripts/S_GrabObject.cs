using UnityEngine;

public class S_GrabObject : MonoBehaviour
{
    private GameObject objectToSpin;
    private Rigidbody2D body;
    private bool dragging;
    public GameObject mouse;
    private Rigidbody2D mouseBody;
    private TargetJoint2D joint;

    private void Start()
    {
        mouseBody = mouse.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If the mouse is down
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse's position
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Moveable"));

            // If the mouse hit anything from the moveable layer
            if (hit)
            {
                // Record the hit item and get its rigidbody
                objectToSpin = hit.transform.gameObject;
                body = objectToSpin.GetComponent<Rigidbody2D>();

                // You are now dragging
                dragging = true;

                joint = objectToSpin.AddComponent<TargetJoint2D>();
                joint.target = mouseBody.position;
                objectToSpin.SendMessage("UpdateMouseLocation");

                mouse.SendMessage("CreateLineRenderer", body);
            }
        }

        // If the player lets go
        else if (Input.GetMouseButtonUp(0))
        {
            // If they were holding an item before
            if (dragging)
            {
                // You are no longer dragging
                dragging = false;
                Destroy(joint);
                Destroy(mouse.GetComponent<LineRenderer>());
            }
        }
    }

}
