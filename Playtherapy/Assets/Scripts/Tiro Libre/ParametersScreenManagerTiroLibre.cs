using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParametersScreenManagerTiroLibre : MonoBehaviour
{
    public GameObject parametersPanel;
    public Dropdown dropdownGameType;
    public Slider sliderGameType;
    public Text labelGameType;
    public Toggle toggleFront;
    public InputField inputFrontAngle1;
    public InputField inputFrontAngle2;
    public InputField inputFrontAngle3;
    public Toggle toggleBack;
    public InputField inputBackAngle1;
    public InputField inputBackAngle2;
    public InputField inputBackAngle3;
    public Toggle toggleShifts;
    public Slider sliderShiftsFrequency;
    public Text labelShiftsFrequency;
    public Slider sliderTimeBetweenTargets;
    public Text labelTimeBetweenTargets;
    public Toggle toggleSustained;
    public Slider sliderSustained;
    public Toggle toggleChangeMovement;

    public void StartGame()
    {
        bool withTime = false;
        float time = 0;
        int repetitions = 0;
        float timeBetweenTargets = sliderTimeBetweenTargets.value * 0.5f;
        bool frontPlane = toggleFront.isOn;
        bool backPlane = toggleBack.isOn;
        bool shifts = toggleShifts.isOn;
        float shiftsFrequency = sliderShiftsFrequency.value * 10;
        bool changeMovement = toggleChangeMovement.isOn;
        //bool sustained = toggleSustained.isOn;

        if (dropdownGameType.value == 1)
        {
            withTime = true;
            time = sliderGameType.value * 30;
        }
        else
        {
            repetitions = (int)sliderGameType.value;
        }

        if (GameManagerTiroLibre.gm)
        {
            GameManagerTiroLibre.gm.StartGame(withTime, time, repetitions, timeBetweenTargets, frontPlane, 
                float.Parse(inputFrontAngle1.text), float.Parse(inputFrontAngle2.text), float.Parse(inputFrontAngle3.text),
                backPlane, float.Parse(inputBackAngle1.text), float.Parse(inputBackAngle2.text), float.Parse(inputBackAngle3.text), 
                shifts, shiftsFrequency, false, 0, changeMovement);
        }

		parametersPanel.SetActive (false);        
    }

    public void OnGameTypeChanged()
    {
        if (dropdownGameType.value == 1)
        {
            sliderGameType.minValue = ParametersTiroLibre.minTime;
            sliderGameType.maxValue = ParametersTiroLibre.maxTime;
        }
        else
        {
            sliderGameType.minValue = ParametersTiroLibre.minRepetitions;
            sliderGameType.maxValue = ParametersTiroLibre.maxRepetitions;
        }

        sliderGameType.value = sliderGameType.minValue;
    }

    public void OnGameTypeSliderValueChanged()
    {
        if (dropdownGameType.value == 1)
        {
            float time = sliderGameType.value * 30f;
            labelGameType.text = ((int)time / 60).ToString("00") + ":" + ((int)time % 60).ToString("00") + " mins";
        }
        else
        {
            labelGameType.text = sliderGameType.value.ToString();
        }
    }

    public void OnShiftsFrequencySliderValueChanged()
    {
        labelShiftsFrequency.text = (sliderShiftsFrequency.value * 10) + "%";
    }

    public void OnTimeBetweenTargetsSliderValueChanged()
    {
        labelTimeBetweenTargets.text = (sliderTimeBetweenTargets.value * 0.5f) + " segs";
    }
}
