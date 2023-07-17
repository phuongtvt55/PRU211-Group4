using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayerScript : MonoBehaviour
{
    private Slider slider;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
           
    }

    public void MaximumHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ActualHealth(float actualHealth)
    {
        animator.SetTrigger("Glope");
        slider.value = actualHealth;
    }

    public void InitHealthBar(float actualHealth)
    {
        gameObject.SetActive(true);
        slider = GetComponent<Slider>();
        animator = GetComponent<Animator>();        
        MaximumHealth(actualHealth);
        ActualHealth(actualHealth);
    }
    
}
