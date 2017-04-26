using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SustainedSlider : MonoBehaviour
{
    public Text label;
    public Slider slider;

    public void OnValueChanged()
    {
        label.text = "Movimiento Sostenido (segs): " + slider.value;
    }
}
