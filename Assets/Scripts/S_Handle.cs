using UnityEngine;

public class S_Handle : MonoBehaviour
{
    private TargetJoint2D joint;

    void FixedUpdate()
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
