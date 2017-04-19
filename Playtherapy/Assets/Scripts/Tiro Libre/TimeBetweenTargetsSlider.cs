using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBetweenTargetsSlider : MonoBehaviour
{
    public Text label;
    public Slider slider;

    public void OnValueChanged()
    {
        label.text = "Tiempo entre objetivos (segs): " + (slider.value * 0.5);
    }
}
