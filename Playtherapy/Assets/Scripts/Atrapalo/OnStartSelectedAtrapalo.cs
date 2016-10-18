using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnStartSelectedAtrapalo : MonoBehaviour {

    public Toggle toggleRep;
    public Toggle toggleTime;

    public Slider sliderRep;
    public Slider sliderTime;
    public Slider sliderLevel;
	public Slider sliderTimeLaunch;

	private GameObject menu;

    // Use this for initialization
    void Start () {
		menu = GameObject.Find ("SelectionMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RunGame()
    {

        int level = (int)sliderLevel.value;
        bool time = false;
        int timeRepValue = 0;
		int timeLaunch = (int)sliderTimeLaunch.value;
        if (toggleTime.isOn)
        {
            time = true;
            timeRepValue = (int)sliderTime.value * 30;
        }
        else
        {
            time = false;
            timeRepValue = (int)sliderRep.value;
        }
        if (GameManagerAtrapalo.gms)
        {
			GameManagerAtrapalo.gms.StartGame(level, time, timeRepValue, timeLaunch);
        }
		menu.SetActive (false);
        
    }
}
