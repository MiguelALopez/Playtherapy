using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartTiroLibre : MonoBehaviour
{
    public GameObject parametersPanel;
    public Toggle toggleRepetitions;
    public Toggle toggleTime;
    public Slider sliderRepetitions;
    public Slider sliderTime;
    public Slider sliderTimeBetweenTargets;
    public Toggle toggleFront;
    public InputField inputFrontAngle1;
    public InputField inputFrontAngle2;
    public InputField inputFrontAngle3;
    public Toggle toggleBack;
    public InputField inputBackAngle1;
    public InputField inputBackAngle2;
    public InputField inputBackAngle3;
    public Toggle toggleShifts;
    public Toggle toggleSustained;
    public Slider sliderSustained;

    public void StartGame()
    {
        bool withTime = false;
        float time = 0;
        int repetitions = 0;
        float timeBetweenTargets = sliderTimeBetweenTargets.value * 0.5f;
        bool frontPlane = toggleFront.isOn;
        bool backPlane = toggleBack.isOn;
        bool shifts = toggleShifts.isOn;
        bool sustained = toggleSustained.isOn;

        if (toggleTime.isOn)
        {
            withTime = true;
            time = sliderTime.value * 30;
        }
        else
        {
            repetitions = (int)sliderRepetitions.value;
        }

        if (GameManagerTiroLibre.gm)
        {
            GameManagerTiroLibre.gm.StartGame(withTime, time, repetitions, timeBetweenTargets, frontPlane, 
                float.Parse(inputFrontAngle1.text), float.Parse(inputFrontAngle2.text), float.Parse(inputFrontAngle3.text),
                backPlane, float.Parse(inputBackAngle1.text), float.Parse(inputBackAngle2.text), float.Parse(inputBackAngle3.text), 
                shifts, sustained, sliderSustained.value);
        }

		parametersPanel.SetActive (false);        
    }
}
