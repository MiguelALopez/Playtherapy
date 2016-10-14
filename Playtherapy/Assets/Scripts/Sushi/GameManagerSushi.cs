using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManagerSushi : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManagerSushi gms;

	// public variables
	public int score=0;
    public int level = 1;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public float startTime=5.0f;

    public int repetitions = 0;
    public int currentReps = 0;
    public int remainingReps = 0;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;

    public bool gameIsStarted = false;
	public bool gameIsOver = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

	public float currentTime;

	private SpawnGameObjects spawner;

    private bool withTime = false;

    public void StartGame(int levelToLoad, bool time, int value)
    {
        withTime = time; 

        if (time)
        {
            startTime = (float)value;
            currentTime = (float)value;
            mainTimerDisplay.text = "Tiempo: " + (((int)startTime)/60).ToString() + ":" + (((int)startTime) % 60).ToString("00");
        } else
        {
            repetitions = value;
            remainingReps = repetitions - 1;
            mainTimerDisplay.text = "Repeticiones: " + repetitions.ToString();
        }
        
        level = levelToLoad;

        gameIsStarted = true;
        mainScoreDisplay.text = "0";

		spawner.MakeThingToSpawn ();
    }

	// setup the game
	void Start () {

		// set the current time to the startTime specified
		currentTime = startTime;

		// get a reference to the GameManager component for use by other scripts
		if (gms == null) 
			gms = this.gameObject.GetComponent<GameManagerSushi>();

		// init scoreboard to 0
		mainScoreDisplay.text = "";

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons)
			playAgainButtons.SetActive (false);

		// inactivate the nextLevelButtons gameObject, if it is set
		if (nextLevelButtons)
			nextLevelButtons.SetActive (false);

		spawner = GameObject.Find("Spawner").GetComponent<SpawnGameObjects>();
	}

	// this is the main game event loop
	void Update () {

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
                        if (currentTime < 0)
                        { // check to see if timer has run out
                            EndGame();
                        }
                        else
                        { // game playing state, so update the timer
                            currentTime -= Time.deltaTime;
                            mainTimerDisplay.text = (((int)currentTime) / 60).ToString() + ":" + (((int)currentTime) % 60).ToString("00");
                        }
                    } else
                    {
                        if (remainingReps < 0)
                        { // check to see if timer has run out
                            EndGame();
                        }
                        else
                        { // game playing state, so update the timer
                            currentTime -= Time.deltaTime;
                            mainTimerDisplay.text = "Repeticiones: " + remainingReps.ToString();
                        }
                    }
                }
            }
        }
		
	}

    public void NewRepetition()
    {
        currentReps++;
        remainingReps--;
    }

    public int GetRepetitions()
    {
        return remainingReps;
    }

	void EndGame() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "GAME OVER";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);
	
		// activate the playAgainButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}
	
	void BeatLevel() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "LEVEL COMPLETE";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

		// activate the nextLevelButtons gameObject, if it is set 
		if (nextLevelButtons)
			nextLevelButtons.SetActive (true);
		
		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}

	// public function that can be called to update the score or time
	public void targetHit (int scoreAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();
		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;

		// update the text UI
		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		//Application.LoadLevel (playAgainLevelToLoad);
	}

	// public function that can be called to go to the next level of the game
	public void NextLevel ()
	{
		// we are just loading the specified next level (scene)
		//Application.LoadLevel (nextLevelToLoad);
	}
	

}
