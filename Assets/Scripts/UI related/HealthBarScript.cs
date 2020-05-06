using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarScript : MonoBehaviour
{

    static Slider slider;
    PlayerScript player;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        
    }
    private void OnEnable()
    {
        player = PlayerScript.player;
        InitializeHealthBar();
    }
    private void OnDisable()
    {
        player = null;
    }

    void InitializeHealthBar()
    {
        slider.maxValue = player.MaxHealth;
        slider.value = player.MaxHealth;
    }

    private void Update()
    {
        if (slider.value != player.CurrentHealth)
        {
            slider.value = player.CurrentHealth;
        }
    }


}
