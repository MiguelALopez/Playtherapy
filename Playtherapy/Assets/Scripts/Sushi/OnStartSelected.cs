using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnStartSelected : MonoBehaviour {

    public Toggle toggleRep;
    public Toggle toggleTime;

    public Slider sliderRep;
    public Slider sliderTime;
    public Slider sliderLevel;

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
        if (GameManagerSushi.gms)
        {
            GameManagerSushi.gms.StartGame(level, time, timeRepValue);
        }
		menu.SetActive (false);
        
    }
}
