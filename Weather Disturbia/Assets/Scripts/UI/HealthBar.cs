using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        // Set the color of healthbar at 1 on the gradient
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int _health)
    {
        slider.value = _health;
        fill.color = gradient.Evaluate(slider.normalizedValue); // Get the value between 0 and 1 because this is the limits of the gradient values
    }
}
