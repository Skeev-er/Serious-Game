using UnityEngine;

public class S_CrankRotations : MonoBehaviour
{
    [SerializeField] private float totalRotation = 0f;
    private Vector3 lastPoint;

    public bool clockwise = true;
    [SerializeField] private float rotationInDirection = 0f; 

    void Start()
    {
        lastPoint = transform.TransformDirection(Vector3.right);
        lastPoint.z = 0;
    }

    void Update()
    {
        Vector3 facing = transform.TransformDirection(Vector3.right);
        facing.z = 0;

        float angle = Vector3.Angle(lastPoint, facing);
        if (Vector3.Cross(lastPoint, facing).z < 0 )
        {
            angle *= -1;
        }

        totalRotation += angle;
        lastPoint = facing;

        if (angle > 0 && !clockwise)
        {
            rotationInDirection += angle;
        }
        else if (angle < 0 && clockwise)
        {
            rotationInDirection -= angle;
        }
    }

    public float ReturnRotation()
    {
        return rotationInDirection;
    }
}
