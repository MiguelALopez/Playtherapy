using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DefaultParametersValues
{
    public float minTime = 1f;
    public float maxTime = 20f;
    public int minRepetitions = 5;
    public int maxRepetitions = 80;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 10f;
    public float stepSpawnTime = 0.5f;
}

public class ParametersManagerSpace : MonoBehaviour {

    public Dropdown dropdownGameType;
    public Slider sliderGameType;
    public Text labelGameType;

    public Slider sliderSpawnTime;
    public Text labelSpawnTime;

    public void Start()
    {
        sliderGameType.minValue = parametersValues.minRepetitions;
        sliderGameType.maxValue = parametersValues.maxRepetitions;

        sliderSpawnTime.minValue = parametersValues.minSpawnTime;
        sliderSpawnTime.maxValue = parametersValues.maxSpawnTime;
    }


    public DefaultParametersValues parametersValues;

    public void StartGame()
    {
        GameManagerSpace.gms.StartGame();
    }

    public void OnGameTypeChanged()
    {
        if (dropdownGameType.value == 1)
        {
            sliderGameType.minValue = parametersValues.minTime;
            sliderGameType.maxValue = parametersValues.maxTime;
        } else
        {
            sliderGameType.minValue = parametersValues.minRepetitions;
            sliderGameType.maxValue = parametersValues.maxRepetitions;
        }

        sliderGameType.value = sliderGameType.minValue;
    }

    public void OnGameTypeSliderValueChanged()
    {
        if(dropdownGameType.value == 1)
        {
            float time = sliderGameType.value * 30f;
            labelGameType.text = ((int)time / 60).ToString("00") + ":" + ((int)time % 60).ToString("00") + " mins";
        }
        else
        {
            labelGameType.text = sliderGameType.value.ToString();
        }
    }

    public void OnSpawnTimeSliderValueChanged()
    {
        labelSpawnTime.text = (sliderSpawnTime.value * parametersValues.stepSpawnTime) + " segs";
    }
}
