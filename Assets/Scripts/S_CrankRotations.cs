using UnityEngine;
using UnityEngine.UIElements;

// This class is attached to the crank
// It keeps track of how much the crank has rotated in total and in whichever direction it is meant to go in

public class S_CrankRotations : MonoBehaviour
{
    private Vector3 lastPoint;

    public bool clockwise = true;
    private float rotationInDirection = 0f; 
    private float rotationAwayFromDirection = 0f;

    public bool reverseUpgrade = false;

    public S_GrabObject grabObject;

    private S_SoundManager soundManager;

    private enum DirectionEnum { Correct, Incorrect, None };
    [SerializeField] private DirectionEnum direction;

    public int soundThreshold = 3;
    private GameObject handle;

    public AudioClip correctCrankSound, incorrectCrankSound;

    private float angle;

    void Start()
    {
        lastPoint = transform.TransformDirection(Vector3.right);
        lastPoint.z = 0;

        soundManager = GameObject.Find("SoundManager").GetComponent<S_SoundManager>();
        handle = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        // Find where the crank is facing
        Vector3 facing = transform.TransformDirection(Vector3.right);
        facing.z = 0;

        // Get the angle between the current direction being faced and the one from last update
        angle = Vector3.Angle(lastPoint, facing);

        // Make sure the angle stays positive
        if (Vector3.Cross(lastPoint, facing).z < 0 )
        {
            angle *= -1;
        }

        lastPoint = facing;


        if (clockwise && grabObject.dragging) // Intended direction is clockwise
        {
            if (angle < 0) // Going clockwise (Correct)
            {
                rotationInDirection -= angle; // Taking away the angle keeps this positive, which is better for use in the progress bar

                // If the crank is rotating more than the threshold
                if (angle < -soundThreshold)
                {
                    direction = DirectionEnum.Correct;
                }
                // If it is rotating less than the threshold
                else
                {
                    direction = DirectionEnum.None;
                }
            }

            else if (angle > 0) // Going anticlockwise (Incorrect)
            {
                rotationAwayFromDirection += angle;

                // If the crank is rotating more than the threshold
                if (angle > soundThreshold)
                {
                    direction = DirectionEnum.Incorrect;
                }
                // If it is rotating less than the threshold
                else
                {
                    direction = DirectionEnum.None;
                }
            }
        }

        else if (!clockwise && grabObject.dragging) // Intended direction is anticlockwise
        {
            if (angle > 0) // Going anticlockwise (Correct)
            {
                rotationInDirection += angle;

                // If the crank is rotating more than the threshold
                if (angle > soundThreshold)
                {
                    direction = DirectionEnum.Correct;
                }
                // If it is rotating less than the threshold
                else
                {
                    direction = DirectionEnum.None;
                }
            }

            else if (angle < 0) // Going clockwise (Incorrect)
            {
                rotationAwayFromDirection -= angle;

                // If the crank is rotating more than the threshold
                if (angle < -soundThreshold)
                {
                    direction = DirectionEnum.Incorrect;
                }
                // If it is rotating less than the threshold
                else
                {
                    direction = DirectionEnum.None;
                }
            }
        }

        ManageSound();
    }

    // This gets called every frame
    private void ManageSound()
    {
        // Make sure that only the clicked crank can cause sounds
        if (grabObject.GetSelectedObject() == handle.name)
        {
            // If you are still holding and rotating the crank
            if (grabObject.dragging && direction != DirectionEnum.None)
            {
                // If you are holding the crank and are going the right way, play the correct sound
                if (direction == DirectionEnum.Correct)
                {
                    soundManager.PlayLoopingSource(correctCrankSound);
                }
                // If you are holding the crank and are going the wrong way, play the incorrect sound
                else if (direction == DirectionEnum.Incorrect)
                {
                    soundManager.PlayLoopingSource(incorrectCrankSound);
                }
            }
            // If you let go of the crank or aren't rotating more than the threshold, stop sound
            else
            {
                soundManager.PauseLoopingSource();
            }
        }
    }

    // This is used by S_Bar for use in the progress bar
    public float ReturnRotation()
    {
        if (!reverseUpgrade)
        {
            return rotationInDirection;
        }
        else
        {
            // Allow the crank rotating backwards to take away from final value (decreasing bar)
            return rotationInDirection - rotationAwayFromDirection;
        }
    }

    // Called by S_UpgradeManager
    // Allows the player to make the bar go backwards when spinning the wrong way
    public void ReverseUnlock()
    {
        reverseUpgrade = true;
        // Change the sound effects so that it no longer grinds when going backwards
        incorrectCrankSound = correctCrankSound;
    }
}
