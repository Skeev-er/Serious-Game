using UnityEngine;

// This class is attached to the crank
// It keeps track of how much the crank has rotated in total and in whichever direction it is meant to go in

public class S_CrankRotations : MonoBehaviour
{
    private float totalRotation = 0f;
    private Vector3 lastPoint;

    public bool clockwise = true;
    private float rotationInDirection = 0f; 
    private float rotationAwayFromDirection = 0f;


    void Start()
    {
        lastPoint = transform.TransformDirection(Vector3.right);
        lastPoint.z = 0;
    }

    void Update()
    {
        // Find where the crank is facing
        Vector3 facing = transform.TransformDirection(Vector3.right);
        facing.z = 0;

        // Get the angle between the current direction being faced and the one from last update
        float angle = Vector3.Angle(lastPoint, facing);

        // Make sure the angle stays positive
        if (Vector3.Cross(lastPoint, facing).z < 0 )
        {
            angle *= -1;
        }

        totalRotation += angle;
        lastPoint = facing;

        if (clockwise) // Intended direction is clockwise
        {
            if (angle < 0) // Going clockwise
            {
                rotationInDirection -= angle; // Taking away the angle keeps this positive, which is better for use in the progress bar
            }
            else if (angle > 0) // Going anticlockwise
            {
                rotationAwayFromDirection += angle;
            }
        }

        else if (!clockwise) // Intended direction is anticlockwise
        {
            if (angle > 0) // Going anticlockwise
            {
                rotationInDirection += angle;
            }
            else if (angle < 0) // Going clockwise
            {
                rotationAwayFromDirection -= angle;
            }
        }
    }

    // This is used by S_Bar for use in the progress bar
    public float ReturnRotation()
    {
        return rotationInDirection - rotationAwayFromDirection;
    }
}
