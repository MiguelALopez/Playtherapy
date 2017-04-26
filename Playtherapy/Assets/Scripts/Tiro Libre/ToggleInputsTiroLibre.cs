using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInputsTiroLibre : MonoBehaviour
{
    public Toggle toggle;
    public InputField[] inputs;

    public void Toggle()
    {
        if (toggle.isOn)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].interactable = false;
            }
        }
    }
}
