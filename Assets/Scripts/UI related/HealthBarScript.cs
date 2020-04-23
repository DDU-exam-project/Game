using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class HealthBarScript : MonoBehaviour
{

    static Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public static void InitializeHealthBar(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public static void UpdateHealthBar(int currentHealth)
    {
        slider.value = currentHealth;
    }

    
}
