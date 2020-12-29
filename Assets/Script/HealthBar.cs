using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealth(int health, int current=100){
        slider.maxValue = health;
        slider.value = current;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetHealth(int health){
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
