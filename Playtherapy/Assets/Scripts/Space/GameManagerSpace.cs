﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerSpace : MonoBehaviour {

    public static GameManagerSpace gms;

    // Scenes
    public GameObject mainPanel;
    public GameObject gameOverPanel;
    public GameObject parametrersPanel;



    
    public int currentScene;                            // 
    public int level;                                   //

    private Text scoreText;

    // Used for states of the game
    private bool playing;                               // Is the player playing
    private bool gameOver;                              // if the game is over

    // Timers
    private float totalTime;                            
    private float currentTime;

    private int score;                                 // Current score in the game 

    // Use this for initialization
    void Start () {
        if (gms == null)
        {
            gms = this.gameObject.GetComponent<GameManagerSpace>();
        }
        currentTime = totalTime;

        scoreText = mainPanel.transform.FindChildByRecursive("Score Text").GetComponent<Text>();
        //mainPanel.transform.FindChildByRecursive("Text").

        playing = false;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (playing)
        {
            if (!gameOver)
            {

            }
        }
		
	}

    public void StartGame()
    {
        playing = true;


        parametrersPanel.SetActive(false);
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }


    // Getter and Setters
    public bool IsPlaying()
    {
        return playing;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}