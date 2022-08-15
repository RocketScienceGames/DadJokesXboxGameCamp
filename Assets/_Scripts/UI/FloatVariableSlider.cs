using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Slider))]
public class FloatVariableSlider : MonoBehaviour
{

    public FloatVariable value;

    public FloatVariable minValue, maxValue = 1;

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSlider();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
    }

    private void OnValidate()
    {
        UpdateSlider();
    }

    void UpdateSlider()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = value;
    }
}
