using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerTiroLibre : MonoBehaviour
{
    public static GameManagerTiroLibre gm;

    public int currentScene;

    public GameObject mainPanel;
    public GameObject gameOverPanel;

    public int score;
    public Text scoreText;

    //public int scoreToWin;
    public bool isPlaying;
    public bool isGameOver;
    public bool withTime;

    public float totalTime;
    private float timeMillis;
    private float currentTime;

    public Text currentTimeText;
    public int totalRepetitions;
    public int remainingRepetitions;
    public Text repetitionsText;

    public GameObject timerPanel;
    public GameObject repetitionsPanel;

    public GameObject[] targets1;
    public GameObject[] targets2;
    public GameObject[] targets3;    

    public bool targetReady;
    public Collider ballCollider;
    public float timeBetweenTargets;

	// Use this for initialization
	void Start ()
    {
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManagerTiroLibre>();

        currentTime = totalTime;
        timeMillis = 1000f;

        remainingRepetitions = totalRepetitions;

        //currentScene = 1;
        ballCollider.enabled = false;

        StartGame();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (isPlaying)
        {
            if (!isGameOver)
            {
                if (withTime)
                {
                    currentTime -= Time.deltaTime;

                    if (currentTime >= 0)
                    {
                        timeMillis -= Time.deltaTime * 1000;
                        if (timeMillis < 0)
                            timeMillis = 1000f;
                        //Debug.Log(currentTime);
                        currentTimeText.text = (((int)currentTime) / 60).ToString("00") + ":" + (((int)currentTime) % 60).ToString("00") + ":" + ((int)(timeMillis * 60 / 1000)).ToString("00");
                    }
                    else
                    {
                        isPlaying = false;
                        isGameOver = true;
                        currentTimeText.text = "00:00:00";
                    }
                }
                else
                {
                    
                }
            }
            else
            {
                Debug.Log("not supposed to be seen");
            }
        }
        else if (isGameOver)
        {
            targetReady = false;
            ballCollider.enabled = false;

            mainPanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
	}

    public void StartGame()
    {
        if (withTime)
        {
            repetitionsPanel.SetActive(false);
            timerPanel.SetActive(true);
        }
        else
        {
            timerPanel.SetActive(false);
            repetitionsPanel.SetActive(true);
        }

        repetitionsText.text = remainingRepetitions.ToString();

        isPlaying = true;
        NextTarget();
        ballCollider.enabled = true;
    }

    public void BallHit(int points)
    {
        UpdateScore(points);

        if (!withTime)
            UpdateRepetitions(1);
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void NextTarget()
    {
        if (isPlaying && !isGameOver)
        {
            int nextTarget;

            switch (currentScene)
            {
                case 1:
                    {
                        nextTarget = Random.Range(0, targets1.Length);
                        StartCoroutine(targets1[nextTarget].GetComponent<TiroLibreTargetBehaviour>().DelayedShow(timeBetweenTargets));
                        break;
                    }
                case 2:
                    {
                        nextTarget = Random.Range(0, targets2.Length);
                        StartCoroutine(targets2[nextTarget].GetComponent<TiroLibreTargetBehaviour>().DelayedShow(timeBetweenTargets));
                        break;
                    }
                case 3:
                    {
                        nextTarget = Random.Range(0, targets3.Length);
                        StartCoroutine(targets3[nextTarget].GetComponent<TiroLibreTargetBehaviour>().DelayedShow(timeBetweenTargets));
                        break;
                    }
                default:
                    {
                        nextTarget = Random.Range(0, targets1.Length);
                        StartCoroutine(targets1[nextTarget].GetComponent<TiroLibreTargetBehaviour>().DelayedShow(timeBetweenTargets));
                        break;
                    }
            }

            targetReady = true;
        }
    }

    public void UpdateRepetitions(int repetitionsDone)
    {
        remainingRepetitions -= repetitionsDone;
        repetitionsText.text = remainingRepetitions.ToString();

        if (remainingRepetitions <= 0)
        {
            isPlaying = false;
            isGameOver = true;
        }
    }

    public GameObject[] getCurrentTargets()
    {
        switch (currentScene)
        {
            case 1:
                return targets1;
            case 2:
                return targets2;
            case 3:
                return targets3;
            default:
                return targets1;
        }
    }

    public Vector3 getCurrentTargetPosition(int target)
    {
        switch (currentScene)
        {
            case 1:
                return targets1[target].transform.position;
            case 2:
                return targets2[target].transform.position;
            case 3:
                return targets3[target].transform.position;
            default:
                return targets1[target].transform.position;
        }
    }
}
