using UnityEngine;

// This class is attached to the slider handle
// It detects when the handle has reached the top or bottom of the rail, displayed via the lights next to the handle

public class S_SliderHandle : MonoBehaviour
{
    private bool atTop = true;

    public float maxHeight = -0.25f;
    public float minHeight = -3.25f;

    public SpriteRenderer topLED;
    public SpriteRenderer bottomLED;

    public S_Bar[] bars;


    private void Start()
    {
        topLED.color = Color.green;
        bottomLED.color = Color.red;
    }

    private void Update()
    {
        if (transform.position.y >= maxHeight && atTop == false) // At top of rail
        {
            atTop = true;
            topLED.color = Color.green;
            bottomLED.color = Color.red;
        }
        else if (transform.position.y <= minHeight && atTop == true) // At bottom of rail
        {
            atTop = false;

            bottomLED.color = Color.green;
            topLED.color = Color.red;

            for (int i = 0; i < bars.Length; i++)
            {
                bars[i].DrainBars();
            }
        }
    }
}
