using UnityEngine;

// This class is attached to each target on the bars
// It detects if the bar has entered or left the target, as well as randomly assigning the targets a position

public class S_Target : MonoBehaviour
{
    private float furthestLeft = 3.012f;
    private float furthestRight = 7.357f;

    private TargetJoint2D targetJoint;

    private bool barWithinTarget = false;


    void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();

        // Upon starting the game, randomise the targets positions along the bars
        RandomPos();
    }

    // Called by S_ScoreManager when at least one target has been scored
    public void RandomPos()
    {
        targetJoint.target = new Vector2 (Random.Range(furthestLeft, furthestRight), transform.position.y);
    }

    // Check for if the end of the fill bars are within the target or not
    private void OnTriggerEnter2D(Collider2D collision)
    {
        barWithinTarget = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        barWithinTarget = false;
    }

    // Called by S_Bar
    public bool GetBarWithinTarget()
    {
        return barWithinTarget;
    }
}
