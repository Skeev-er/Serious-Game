using UnityEngine;

// This class is attached to the slider handle
// It detects when the handle has reached the top or bottom of the rail, displayed via the lights next to the handle

public class S_SliderHandle : MonoBehaviour
{
    private bool atTop = true;

    public float maxHeight = -0.64f; // These depend on the handle's actualy Y position, which is not accurate when a child of the rail
    public float minHeight = -3.3f;

    public SpriteRenderer topLED;
    public SpriteRenderer bottomLED;

    public S_Bar[] bars;

    private Color screenGreen = new Color32(117, 231, 97, 255);
    private Color screenRed = new Color32(229, 68, 80, 255);

    private void Start()
    {
        topLED.color = screenGreen;
        bottomLED.color = screenRed;
    }

    private void Update()
    {
        if (transform.position.y >= maxHeight && atTop == false) // At top of rail
        {
            atTop = true;
            topLED.color = screenGreen;
            bottomLED.color = screenRed;
        }
        else if (transform.position.y <= minHeight && atTop == true) // At bottom of rail
        {
            atTop = false;

            bottomLED.color = screenGreen;
            topLED.color = screenRed;

            for (int i = 0; i < bars.Length; i++)
            {
                bars[i].DrainBars();
            }
        }
    }
}
