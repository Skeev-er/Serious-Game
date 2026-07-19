using UnityEngine;

// This class is part of the main scene
// It checks for if the player clicks on any objects on the "Moveable" layer
// If they do, create a joint so that the object follows the mouse
// Also attach a line renderer to the mouse to show this force

public class S_GrabObject : MonoBehaviour
{
    private GameObject objectToSpin;
    private Rigidbody2D body;
    public bool dragging;
    public GameObject mouse;

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

                objectToSpin.SendMessage("CreateTargetJoint");

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
                Destroy(objectToSpin.GetComponent<TargetJoint2D>());
                Destroy(mouse.GetComponent<LineRenderer>());
            }
        }
    }

    public string GetSelectedObject()
    {
        if (objectToSpin != null)
        {
            return objectToSpin.name;
        }
        else
        {
            return null;
        }
    }
}
