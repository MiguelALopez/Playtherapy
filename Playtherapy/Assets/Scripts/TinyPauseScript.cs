using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TinyPauseScript : MonoBehaviour
{
	GameObject panelPause;
	Button buttonPause;

	// Use this for initialization
	void Start()
    {
		panelPause = GameObject.Find("pause_data");
		buttonPause = GameObject.Find("bt_pause").GetComponent<Button>();
		HidePause();
	}

	public void HidePause()
	{
		Time.timeScale = 1;
		panelPause.SetActive(false);
		buttonPause.interactable = true;
	}

	public void ShowPause()
	{
		Time.timeScale = 0;
		panelPause.SetActive(true);
		buttonPause.interactable = false;
	}

    public void ResetCurrentScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (panelPause.activeSelf)
				HidePause();
            else
				ShowPause();
		}
	}
}
