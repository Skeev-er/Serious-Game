using UnityEngine;

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

    public AudioClip grinding;
    public bool goingTheRightWay = false;
    public bool changeAudio = false;
    public bool playAudio = false;

    public S_CrankSounds crankAudioSource;

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

        lastPoint = facing;

        if (grabObject.dragging)
        {
            if (clockwise) // Intended direction is clockwise
            {
                if (angle < 0) // Going clockwise
                {
                    rotationInDirection -= angle; // Taking away the angle keeps this positive, which is better for use in the progress bar
                    goingTheRightWay = true;
                }
                else if (angle > 0) // Going anticlockwise
                {
                    rotationAwayFromDirection += angle;
                    goingTheRightWay = false;
                }
            }

            else if (!clockwise) // Intended direction is anticlockwise
            {
                if (angle > 0) // Going anticlockwise
                {
                    rotationInDirection += angle;
                    goingTheRightWay = true;
                }
                else if (angle < 0) // Going clockwise
                {
                    rotationAwayFromDirection -= angle;
                    goingTheRightWay = false;
                }
            }
        }

        ManageAudio();
    }

    private void ManageAudio()
    {
        // If the player is dragging and going the right way, play audio
        if (grabObject.dragging && goingTheRightWay)
        {
            if (!crankAudioSource.IsPlaying())
            {
                changeAudio = true;
                playAudio = true;
            }
        }
        // Otherwise, if the player isn't dragging or going in the right way, pause audio
        else if ((!grabObject.dragging || !goingTheRightWay) && crankAudioSource.IsPlaying())
        {
            changeAudio = true;
            playAudio = false;
        }

        if (changeAudio)
        {
            changeAudio = false;

            if (playAudio)
            {
                crankAudioSource.PlaySound();
            }
            else
            {
                crankAudioSource.PauseSound(name);
                print(name);
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
    public void ReverseUnlock()
    {
        reverseUpgrade = true;
    }
}
