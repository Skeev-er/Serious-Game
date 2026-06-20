using UnityEngine;

// This class is attached to the object to grab (the handle)
// It reminds the handle that it should target the new mouse position at every frame

public class S_Handle : MonoBehaviour
{
    private TargetJoint2D joint;

    void Update()
    {
        if (gameObject.GetComponent<TargetJoint2D>() != null)
        {
            joint.target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        }
    }

    void UpdateMouseLocation()
    {
        joint = gameObject.GetComponent<TargetJoint2D>();
    }
}
