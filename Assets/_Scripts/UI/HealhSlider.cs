using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealhSlider : MonoBehaviour
{

    public Health health;
    public Slider slider;


    public IEnumerator Start()
    {
        if (health == null) health = GetComponent<Health>();
        if (slider == null) slider = GetComponent<Slider>();
        health.OnTakeDamage.AddListener(UpdateSlider);
        yield return null;
        UpdateSlider();
    }


    [Button]
    public void UpdateSlider()
    {
        slider.maxValue = health.maxHealth;
        slider.value = health.health;
    }



}
