using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This class is attached to the bar in UI
// The fill increases according to the player turning the crank in the right direction
// The fill decreases by a set amount on update

public class S_Bar : MonoBehaviour
{
    private Slider slider;
    public Gradient gradient;
    public Image fill;

    private float decreaseRate = 0.04f;
    private float increaseBy = 0f;

    private float amount = 0f;

    public S_CrankRotations rotations;

    private float lastIncreaseBy = 0f;
    private float temp = 0f;

    public float toughness = 40; // How tough it is to increase the bar

    private bool draining = false;
    private float drainRate = 0.7f;

    private S_Target target;
    public TextMeshProUGUI scoreText;

    private S_BarManager barManager;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
        fill.color = gradient.Evaluate(slider.maxValue);
        target = GetComponentInChildren<S_Target>();
        barManager = GetComponentInParent<S_BarManager>();
    }

    private void Update()
    {
        // If not draining, increase and decrease the bar as normal
        if (!draining)
        {
            IncreaseAmount();
            DecreaseAmount();
        }
        // If draining, take control from the player and drain the bar
        else if (draining)
        {
            amount -= drainRate;

            // Stop draining when the bar is empty
            if (slider.value == 0)
            {
                draining = false;
            }
        }
        // The bar colour depends on the gradient and the amount filled 
        fill.color = gradient.Evaluate(slider.normalizedValue);

        // Set the bar
        slider.value = amount;
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

    // Called by S_SliderHandle when it reaches the bottom
    public void DrainBars()
    {
        draining = true;

        // If the target was hit, tell the bar manager to add 1 to the combo. Otherwise, it has missed
        int targetHit = 0;
        if (target.GetBarWithinTarget() == true)
        {
            targetHit = 1;
        }
        barManager.Combo(targetHit);
    }
}
