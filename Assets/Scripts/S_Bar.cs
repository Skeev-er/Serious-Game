using UnityEngine;
using UnityEngine.UI;

// This class is attached to the bar in UI
// The fill increases according to the player turning the crank in the right direction
// The fill decreases by a set amount on update

public class S_Bar : MonoBehaviour
{
    private Slider slider;
    public Image fill;

    public float decreaseRate = 0.01f; // Have a max of around 0.07, but only if the toughness is low enough
    private float increaseBy = 0f;

    private float amount = 0f;

    public S_CrankRotations rotations;

    private float lastIncreaseBy = 0f;
    private float temp = 0f;

    public float toughness = 40; // Have a max of around 80, but only is the decrease rate stays near 0.01

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    private void Update()
    {
        IncreaseAmount();
        DecreaseAmount();

        SetBar(amount);
    }

    public void SetBar(float value)
    {
        slider.value = value;
    }

    private void IncreaseAmount()
    {
        // If the amount is not above the max (100)
        if (amount <= slider.maxValue)
        {
            // Get the rotation in the correct direction from the crank, then divide it by toughness
            // Keep in mind 1 full rotation in the correct direction returns 360 
            increaseBy = rotations.ReturnRotation() / toughness;

            temp = increaseBy;

            // Take away last update's rotation to find the difference
            increaseBy -= lastIncreaseBy;

            lastIncreaseBy = temp;

            // Add the difference to the final amount
            amount += increaseBy;

            // Cap the amount to always be less than or equal to the slider's max value
            if (amount > slider.maxValue)
            {
                amount = slider.maxValue;
            }
        }
    }

    // Decrease the slider at a constant rate
    private void DecreaseAmount()
    {
        if (amount > slider.minValue)
        {
            amount -= decreaseRate;
        }
    }
}
