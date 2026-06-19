using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class S_PlayerMovement : MonoBehaviour
{
    private GameObject objectToSpin;
    private Rigidbody2D body;
    private bool dragging;
    public GameObject mouse;
    private Rigidbody2D mouseBody;
    private SpringJoint2D joint;
    private LineRenderer visualJoint;

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

                joint = objectToSpin.AddComponent<SpringJoint2D>();
                joint.connectedBody = mouseBody;
                joint.anchor = objectToSpin.transform.position;
                joint.autoConfigureConnectedAnchor = true;

                mouse.SendMessage("CreateLineRenderer", hit.transform.position);
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
