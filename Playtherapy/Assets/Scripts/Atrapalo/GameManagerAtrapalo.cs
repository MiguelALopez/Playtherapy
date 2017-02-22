﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerAtrapalo : MonoBehaviour {

    // make game manager public static so can access this from other scripts
    public static GameManagerAtrapalo gms;

    // public variables
    public int score = 0;
    public int level = 1;

    public bool canBeatLevel = false;
    public int beatLevelScore = 0;

    public float startTime = 5.0f;

    public int repetitions = 0;
    public int currentReps = 0;
    public int remainingReps = 0;
	public float launchTime = 0;

    public Text mainScoreDisplay;
    public GameObject mainScoreDisplayObj;
    public Text mainTimerDisplay;
    public GameObject mainTimerDisplayObj;
    public GameObject countdownDisplayObject;
    private Text countdownDisplay;

    public GameObject canvasScoreText;
    public GameObject canvasBestScoreText;
    public GameObject bronzeTrophy;
    public GameObject silverTrophy;
    public GameObject goldTrophy;
    public GameObject canvasResults;

    public GameObject gameOverScoreOutline;

    public AudioSource musicAudioSource;

    public bool countdownStarted = false;
    public bool gameIsStarted = false;
    public bool gameIsOver = false;
    private bool lastSeconds = false;
    public int countBallPlane;


    public bool side; 
    public bool planeShootLat ;
    public bool planeShootFront;
    public int shootOpt;

    public AudioSource countdownSound;

    public GameObject playAgainButtons;
    public string playAgainLevelToLoad;

    public GameObject nextLevelButtons;
    public string nextLevelToLoad;

	//Necessary elements for capturing the best angle in a section
	public GameObject FullBodyObject;
	private MovementDetectionLibrary.FullBody fBodyObject;
	private float bestPartialLeftShoulderAngle;
	private float bestTotalLeftShoulderAngle;
	private float bestPartialRightShoulderAngle;
	private float bestTotalRightShoulderAngle;

    public float currentTime;
    private float countdownTime = 0.0f;
	public int ballsAlive;

    public string plane;

    public Toggle toggleFronPla;
    public Toggle toggleLatPlan;

    private SpawnGameObjectsBall spawner;

    public bool withTime = false;

	public void StartGame(int levelToLoad, bool time, int value, int launchTime)
    {
        withTime = time;

        if (time)
        {
            startTime = (float)value;
            currentTime = (float)value;
            mainTimerDisplay.text = "Tiempo: " + (((int)startTime) / 60).ToString() + ":" + (((int)startTime) % 60).ToString("00");
        }
        else
        {
            repetitions = value;
            remainingReps = repetitions;
            mainTimerDisplay.text = "Repeticiones: " + repetitions.ToString();
        }

        level = levelToLoad;
		this.launchTime = launchTime/2f; 

        countdownStarted = true;
        if (countdownSound)
        {
            countdownSound.Play();
        }
        //gameIsStarted = true;
        mainScoreDisplay.text = "0";
        countdownDisplayObject.SetActive(true);
        lastSeconds = false;
        planeShootFront = toggleFronPla.isOn;
        planeShootLat = toggleLatPlan.isOn;
        countBallPlane = 0;

        calculateOptionShoot();
        setPlane();
        if (!planeShootFront && !planeShootLat)
        {
            toggleLatPlan.isOn = true;
            planeShootLat = true;
        }

    }

    // setup the game
    void Start()
    {
        side = true;
		spawner = GameObject.Find("Spawner").GetComponent<SpawnGameObjectsBall>();
		spawner.enabled = false;
        // set the current time to the startTime specified
        currentTime = startTime;

        // get a reference to the GameManager component for use by other scripts
        if (gms == null)
            gms = this.gameObject.GetComponent<GameManagerAtrapalo>();

        // init scoreboard to 0
        mainScoreDisplay.text = "";
		spawner.enabled = false;

        // inactivate the gameOverScoreOutline gameObject, if it is set
        if (gameOverScoreOutline)
            gameOverScoreOutline.SetActive(false);

        // inactivate the playAgainButtons gameObject, if it is set
        if (playAgainButtons)
            playAgainButtons.SetActive(false);

        // inactivate the nextLevelButtons gameObject, if it is set
        if (nextLevelButtons)
            nextLevelButtons.SetActive(false);

        if (countdownDisplayObject)
            countdownDisplay = countdownDisplayObject.GetComponent<Text>();
		initiateAngleSystem ();

        
    }

    // this is the main game event loop
    void Update()
    {


        if (gameIsStarted)
        {
            if (!gameIsOver)
            {
                if (canBeatLevel && (score >= beatLevelScore))
                {  // check to see if beat game
                    BeatLevel();
                }
                else
                {
                    if (withTime)
                    {
                        if (currentTime <= 0)
                        { // check to see if timer has run out
							if (ballsAlive == 0) {
								EndGame();
							}
                        }
                        else
                        { // game playing state, so update the timer
                            currentTime -= Time.deltaTime;
                            mainTimerDisplay.text = "Tiempo: " + (((int)currentTime) / 60).ToString() + ":" + (((int)currentTime) % 60).ToString("00");
                        }
                        if (!lastSeconds && currentTime <= 3.0f)
                        {
                            lastSeconds = true;
                            if (countdownSound)
                            {
                                countdownSound.Play();
                            }
                            mainTimerDisplay.fontStyle = FontStyle.Bold;
                            mainTimerDisplay.color = Color.red;
                        }
                    }
                    else
                    {
                        if (remainingReps <= 0)
                        { // check to see if timer has run out
							if (ballsAlive == 0) {
								EndGame();
							}
                            
                        }
                        else
                        { // game playing state, so update the timer
                            //currentTime -= Time.deltaTime;
                        }
                    }

					setCurrentMaximunAngle ();
                }
            }
        }
        else if (countdownStarted)
        {
            if (3.0f - countdownTime > 2.0f)
            {
                countdownDisplay.text = "3";
                countdownDisplay.fontSize = 10 + (int)(90.0f * countdownTime);
            }
            else if (3.0f - countdownTime > 1.0f)
            {
                countdownDisplay.text = "2";
                countdownDisplay.fontSize = 10 + (int)(90.0f * (countdownTime - 1.0f));
            }
            else if (3.0f - countdownTime > 0.0f)
            {
                countdownDisplay.text = "1";
                countdownDisplay.fontSize = 10 + (int)(90.0f * (countdownTime - 2.0f));
            }
            else if (3.0f - countdownTime > -1.0f)
            {
                countdownDisplay.text = "¡ADELANTE!";
                countdownDisplay.fontSize = 30 + (int)(70.0f * (countdownTime - 3.0f));
            }
            else
            {
                mainScoreDisplayObj.SetActive(true);
                mainTimerDisplayObj.SetActive(true);
                countdownDisplay.text = "";
                gameIsStarted = true;
                countdownDisplayObject.SetActive(false);
				spawner.enabled = true;

            }
            countdownTime += Time.deltaTime;

        }

    }

    public void NewRepetition()
    {
        currentReps++;
        remainingReps--;
		mainTimerDisplay.text = "Repeticiones: " + remainingReps.ToString();
		setBestMaximunAngleRepetition ();

    }

    public int GetRepetitions()
    {
        return remainingReps;
    }

    void EndGame()
    {
        // game is over
        gameIsOver = true;
		StartCoroutine(waitBall ());
        // repurpose the timer to display a message to the player
        mainTimerDisplay.text = "GAME OVER";


        // activate the gameOverScoreOutline gameObject, if it is set 
        if (gameOverScoreOutline)
            gameOverScoreOutline.SetActive(true);

        // activate the playAgainButtons gameObject, if it is set 
        if (playAgainButtons)
            playAgainButtons.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (musicAudioSource)
            musicAudioSource.pitch = 0.5f; // slow down the music

        //GameObject.Find("CanvasInfoManos").SetActive(false);
        GameObject.Find("Canvas").SetActive(false);

        double finalScore = score / repetitions * 100;
        finalScore = 61;

        canvasScoreText.GetComponentInChildren<TextMesh>().text = finalScore + "%";

        if (finalScore <= 60)
        {
            bronzeTrophy.SetActive(true);
        }
        else if (finalScore <= 90)
        {
            bronzeTrophy.SetActive(false);
            silverTrophy.SetActive(true);
        }
        else if (finalScore <= 100)
        {
            bronzeTrophy.SetActive(false);
            goldTrophy.SetActive(true);
        }

        canvasResults.SetActive(true);
    }

    void BeatLevel()
    {
        // game is over
        gameIsOver = true;

        // repurpose the timer to display a message to the player
        mainTimerDisplay.text = "LEVEL COMPLETE";

        // activate the gameOverScoreOutline gameObject, if it is set 
        if (gameOverScoreOutline)
            gameOverScoreOutline.SetActive(true);

        // activate the nextLevelButtons gameObject, if it is set 
        if (nextLevelButtons)
            nextLevelButtons.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (musicAudioSource)
            musicAudioSource.pitch = 0.5f; // slow down the music
    }

    // public function that can be called to update the score or time
    public void targetHit(int scoreAmount)
    {
        // increase the score by the scoreAmount and update the text UI
        score += scoreAmount;
        mainScoreDisplay.text = score.ToString();

        // don't let it go negative
        if (currentTime < 0)
            currentTime = 0.0f;

        // update the text UI
        //mainTimerDisplay.text = currentTime.ToString("0.00");
    }

    // public function that can be called to restart the game
    public void RestartGame()
    {
        // we are just loading a scene (or reloading this scene)
        // which is an easy way to restart the level
        //Application.LoadLevel (playAgainLevelToLoad);
    }

    // public function that can be called to go to the next level of the game
    public void NextLevel()
    {
        // we are just loading the specified next level (scene)
        //Application.LoadLevel (nextLevelToLoad);
    }

    // set the option to shoot the ball, only lat 1, only front 2, both 3
    public void calculateOptionShoot()
    {
        if (toggleLatPlan.isOn)
        {
            shootOpt = 1;
        }
        if (toggleFronPla.isOn)
        {
            shootOpt = 2;
        }
        if (toggleFronPla.isOn && toggleLatPlan.isOn)
        {
            shootOpt = 3;
        }
    }

    // Change the plane from x to z or from z to x
    public void changePlane()
    {
        if (plane == "x")
        {
            plane = "z";
        }else
        {
            plane = "x";
        }
    }

    //set the plane to shoot, according to the toggle select
    public void setPlane()
    {
        if (toggleFronPla.isOn)
        {
            plane = "x";
        }else
        {
            plane  = "z";
        }
    }


	IEnumerator waitBall() {
		yield return new WaitForSeconds(10);
	}


	/**
	 * Function to initiate the system of mesuare of angel to calculate the maximu	
	 * */

	private void initiateAngleSystem(){


		spawner = GameObject.Find("Spawner").GetComponent<MovementDetectionLibrary.SpawnGameObjects>();

		if (FullBodyObject)
			fBodyObject = FullBodyObject.GetComponent<MovementDetectionLibrary.FullBody>();

		//Initialize angle values
		bestPartialLeftShoulderAngle = 0.0f;
		bestTotalLeftShoulderAngle = 0.0f;
		bestPartialRightShoulderAngle = 0.0f;
		bestTotalRightShoulderAngle = 0.0f;

		//Initialize time values for final animation
		animEnded = false;

	}


	/**
	 * Function to set the maximun current angle do by the patient
	 * */
	private void setCurrentMaximunAngle(){

		spawner = GameObject.Find("Spawner").GetComponent<MovementDetectionLibrary.SpawnGameObjects>();

		float currentLeftShoulderAngle = 0.0f;
		float currentRightShoulderAngle = 0.0f;

		if (fBodyObject)
		{
			currentLeftShoulderAngle = fBodyObject.getAngle("shoulderAbdLeft");
			if (currentLeftShoulderAngle > bestPartialLeftShoulderAngle)
				bestPartialLeftShoulderAngle = currentLeftShoulderAngle;

			currentRightShoulderAngle = fBodyObject.getAngle("shoulderAbdRight");
			if (currentRightShoulderAngle > bestPartialRightShoulderAngle)
				bestPartialRightShoulderAngle = currentRightShoulderAngle;
		}
	}



	/**
	 * Function to set best maximum angle 
	 * */
	private void setBestMaximunAngleRepetition(){

		if (bestPartialLeftShoulderAngle > bestTotalLeftShoulderAngle)
		{
			bestTotalLeftShoulderAngle = bestPartialLeftShoulderAngle;
		}
		bestPartialLeftShoulderAngle = 0.0f;

		if (bestPartialRightShoulderAngle > bestTotalRightShoulderAngle)
		{
			bestTotalRightShoulderAngle = bestPartialRightShoulderAngle;
		}
		bestPartialRightShoulderAngle = 0.0f;

	}


	/**
	 * Function to save the information of a game session and a performance
	 * */
	public void saveGameSessionInfo(){

		TherapySessionObject objTherapy = GameObject.Find ("TherapySession").GetComponent<TherapySessionObject> ();
		objTherapy.fillLastSession(score, repetitions, (int)startTime, level.ToString());
		objTherapy.saveLastGameSession ();

		objTherapy.savePerformance((int)bestTotalLeftShoulderAngle, "4");
		objTherapy.savePerformance((int)bestTotalRightShoulderAngle, "5");

	}

}
