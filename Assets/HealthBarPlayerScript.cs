using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayerScript : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();       
    }

    public void MaximumHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ActualHealth(float actualHealth)
    {
        slider.value = actualHealth;
    }

    public void InitHealthBar(float actualHealth)
    {
        MaximumHealth(actualHealth);
        ActualHealth(actualHealth);
    }
    
}
