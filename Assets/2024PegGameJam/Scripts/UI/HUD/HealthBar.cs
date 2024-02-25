using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBarSlider;

    public void SetMaxHealth(int maxValue)
    {
        healthBarSlider.maxValue = maxValue;
        healthBarSlider.value = maxValue;
    }

    public void UpdateSlider(int value)
    {
        healthBarSlider.value = value;
    }
}
