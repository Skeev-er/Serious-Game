using UnityEngine;

// This class is attached to the object to grab
// It reminds the handle that its targetJoint should target the new mouse position at every frame

public class S_Handle : MonoBehaviour
{
    private TargetJoint2D joint;
    public float maxForce = 0;

    void Update()
    {
        if (gameObject.GetComponent<TargetJoint2D>() != null)
        {
            // Continuously update the target position to be the mouse position
            joint.target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        }
    }

    void CreateTargetJoint()
    {
        joint = gameObject.AddComponent<TargetJoint2D>();
        // Apply max force, but only if it has been set publically. Otherwise, use default (10,000)
        if (maxForce != 0)
        {
            joint.maxForce = maxForce;
        }
    }
}
