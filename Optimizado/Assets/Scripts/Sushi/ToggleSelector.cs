using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleSelector : MonoBehaviour {

    public Toggle toggleRep;
    public Toggle toggleTime;


    public Slider sliderRep;
    public Slider sliderTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onRepValueChanged()
    {
        if (toggleRep.isOn)
        {
            toggleTime.isOn = false;
            sliderTime.interactable = false;
            Debug.Log("Se activo ToggleRep");
        } else {
            toggleTime.isOn = true;
            sliderTime.interactable = true;
            Debug.Log("Se activo ToggleRep");
        }
    }
    public void onTimeValueChanged()
    {
        if (toggleTime.isOn)
        {
            toggleRep.isOn = false;
            sliderRep.interactable = false;
            Debug.Log("Se activo ToggleTime");
        }
        else
        {
            toggleRep.isOn = true;
            sliderRep.interactable = true;
            Debug.Log("Se activo ToggleTime");
        }
    }
}
