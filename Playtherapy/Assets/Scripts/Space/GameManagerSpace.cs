﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameManagerSpace : MonoBehaviour {

    public static GameManagerSpace gms;

    // Panels used in the scene
    public GameObject mainPanel;
    public GameObject parametrersPanel;
    public GameObject resultsPanel;

    //public int currentScene;                            // 
    public int level;                                   //

    // Used for states of the game
    private bool playing;                               // Is the player playing
    private bool gameOver;                              // If the game is over
    private bool withTime;                              // If the game is with time or repetitions

    // Timers
    private float totalTime;                            //
    private float timeMillis;
    private float currentTime;
    private float timeBetweenChange;
    public Slider sliderCurrentTime;
    public Text currentTimeText;
    public GameObject timerPanel;

    // Repetitions
    private int totalRepetitions;
    private int remainingRepetitions;
    public Text repetitionsText;
    public GameObject repetitionsPanel;

    private int score;                                  // Current score in the game
    public Text scoreText;                              // 

    public Text resultsScoreText;
    public Text resultsBestScoreText;
    public Sprite starOn;
    public Sprite starOff;
    public Image star1;
    public Image star2;
    public Image star3;

    public Animator shipAnimator;

    // Parameters
    private float spawnTime;

    private bool withGrab;
    private bool withFlexionExtension;
    private bool withPronation;
    private bool withBothHands;
    private bool isRightHand;
    private float flexion;
    private float extension;

    private int changes;


    public enum PlayState
    {
        NONE,
        ASTEROIDS,
        STARS,
        ENEMIES
    }

    private PlayState state;

    // Use this for initialization
    void Start () {
        if (gms == null)
        {
            gms = this.gameObject.GetComponent<GameManagerSpace>();
        }
        currentTime = totalTime;
        timeMillis = 1000f;

        //scoreText = mainPanel.transform.FindChildByRecursive("Score Text").GetComponent<Text>();
        //mainPanel.transform.FindChildByRecursive("Text").

        playing = false;
        score = 0;

        //state = PlayState.ASTEROIDS;
        //state = (PlayState)UnityEngine.Random.Range(0, (float)Enum.GetValues(typeof(PlayState)).Cast<PlayState>().Max());
        

    }
	
	// Update is called once per frame
	void Update () {
        if (playing)
        {
            if (!gameOver)
            {
                UpdatePlayState();
                if (withTime)
                {
                    currentTime -= Time.deltaTime;


                    if(currentTime >= 0)
                    {
                        timeMillis -= Time.deltaTime * 1000f;
                        if(timeMillis < 0)
                        {
                            timeMillis = 1000f;
                        }
                        currentTimeText.text = (((int)currentTime) / 60).ToString("00") + ":"
                            + (((int)currentTime) % 60).ToString("00") + ":"
                            + ((int)(timeMillis * 60 / 1000)).ToString("00");
                        sliderCurrentTime.value = currentTime * 100 / totalTime;
                    }else
                    {
                        playing = false;
                        gameOver = true;
                        currentTimeText.text = "00:00:00";
                        state = PlayState.NONE;
                    }
                }else
                {
                    totalTime += Time.deltaTime;
                }
            }
        }else if (IsGameOver())
        {
            EndGame();
        }
		
	}

    public void StartGame(bool withTime, float time, int repetitions, float spawnTime, bool withGrab,
        bool withFlexionExtension, bool withPronation, bool withBothHands, float flexion, float extension,
        bool rightHand)
    {
        this.withTime = withTime;

        totalTime = time;
        currentTime = totalTime;
        totalRepetitions = repetitions;
        remainingRepetitions = totalRepetitions;
        repetitionsText.text = remainingRepetitions.ToString();
        this.spawnTime = spawnTime;
        this.withGrab = withGrab;
        this.withFlexionExtension = withFlexionExtension;
        this.withPronation = withPronation;
        this.withBothHands = withBothHands;
        isRightHand = rightHand;
        this.flexion = flexion;
        this.extension = extension;

        changes = 0;

        int numChanges = 0;
        if (withGrab)
            numChanges++;
        if (withFlexionExtension)
            numChanges++;
        if (withPronation)
            numChanges++;

        if (withTime)
        {
            timeBetweenChange = totalTime / numChanges;
            repetitionsPanel.SetActive(false);
            timerPanel.SetActive(true);
        }
        else
        {
            timeBetweenChange = 20f;
            timerPanel.SetActive(false);
            repetitionsPanel.SetActive(true);
        }

        mainPanel.SetActive(true);
        parametrersPanel.SetActive(false);
        playing = true;
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        if (withTime)
        {
            totalRepetitions++;
        }else
        {
            UpdateRepetition(1);
        }
    }

    public void UpdateRepetition(int repetitionsDone)
    {
        remainingRepetitions -= repetitionsDone;
        repetitionsText.text = remainingRepetitions.ToString();

        if(remainingRepetitions <= 0)
        {
            playing = false;
            gameOver = true;
            state = PlayState.NONE;
        }
    }

    public void UpdatePlayState()
    {
        
        if (withGrab && totalTime - currentTime > timeBetweenChange*changes)
        {
            state = PlayState.ASTEROIDS;
            withGrab = false;
            changes++;
            Debug.Log(state);
        }else if (withFlexionExtension && totalTime - currentTime > timeBetweenChange * changes)
        {
            state = PlayState.STARS;
            withFlexionExtension = false;
            changes++;
            Debug.Log(state);
        }
        else if (withPronation && totalTime - currentTime > timeBetweenChange * changes)
        {
            state = PlayState.ENEMIES;
            withPronation = false;
            changes++;
            Debug.Log(state);
        }
    }

    public void EndGame()
    {
        mainPanel.SetActive(false);
        StartCoroutine(EndGameAnimator());
    }

    private IEnumerator EndGameAnimator()
    {
        shipAnimator.Play("ShipFinal");
        yield return new WaitForSeconds(8f);
        SaveAndShowResults();
    }

    public void SaveAndShowResults()
    {
        TherapySessionObject objTherapy = TherapySessionObject.tso;

        if (objTherapy != null)
        {
            objTherapy.fillLastSession(score, totalRepetitions, (int)totalTime, level.ToString());
            objTherapy.saveLastGameSession();
        }

        int finalScore;
        if(totalRepetitions == 0)
        {
            finalScore = 0;
        }
        else
        {
            finalScore = (int)(((float)score / totalRepetitions) * 100.0f);
        }
        resultsScoreText.text = "Desempeño: " + finalScore + "%";

        if (objTherapy != null)
            resultsBestScoreText.text = "Mejor: " + objTherapy.getGameRecord() + "%";

        if (finalScore <= 60)
        {
            //resultMessage.GetComponent<TextMesh>().text = "¡Muy bien!";
            star1.sprite = starOn;
            star2.sprite = starOff;
            star3.sprite = starOff;
        }
        else if (finalScore <= 90)
        {
            //resultMessage.GetComponent<TextMesh>().text = "¡Grandioso!";
            star1.sprite = starOn;
            star2.sprite = starOn;
            star3.sprite = starOff;
        }
        else if (finalScore <= 100)
        {
            //resultMessage.GetComponent<TextMesh>().text = "¡Increíble!";
            star1.sprite = starOn;
            star2.sprite = starOn;
            star3.sprite = starOn;
        }

        resultsPanel.SetActive(true);
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

    public PlayState GetState()
    {
        return state;
    }
}
