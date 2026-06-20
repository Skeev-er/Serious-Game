using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_Bar : MonoBehaviour
{
    private Slider slider;
    public Image fill;

    public float decreaseRate = 0.01f;
    private float increaseBy = 0f;

    private float amount = 0f;

    public S_CrankRotations rotations;

    private float lastIncreaseBy = 0f;
    private float temp = 0f;

    public float toughness = 80;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    public void SetBar(float value)
    {
        slider.value = value;
    }

    private void IncreaseAmount()
    {
        if (amount <= slider.maxValue)
        {
            increaseBy = rotations.ReturnRotation() / toughness;
            temp = increaseBy;
            increaseBy -= lastIncreaseBy;

            lastIncreaseBy = temp;

            amount += increaseBy;

            if (amount > slider.maxValue)
            {
                amount = slider.maxValue;
            }
        }

        
    }

    private void DecreaseAmount()
    {
        if (amount > slider.minValue)
        {
            amount -= decreaseRate;
        }
    }

    private void Update()
    {
        IncreaseAmount();
        DecreaseAmount();

        SetBar(amount);
    }
}
