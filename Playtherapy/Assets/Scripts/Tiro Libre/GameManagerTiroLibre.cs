using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerTiroLibre : MonoBehaviour
{
    public static GameManagerTiroLibre gm;
    public Kick kickScript;

    public int currentScene;

    public int level;
    public bool useFrontPlane;
    public bool useBackPlane;
    public bool useShifts;
    public bool useSustained;
    public float timeSustained;

    public GameObject mainPanel;
    public GameObject gameOverPanel;
    public GameObject leftLegPanel;
    public GameObject rightLegPanel;

    public TextMesh resultsScoreText;
    public TextMesh resultsBestScoreText;
    public GameObject bronzeTrophy;
    public GameObject silverTrophy;
    public GameObject goldTrophy;
    public GameObject resultsPanel;

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
    //public Collider ballCollider;
    public float timeBetweenTargets;

    public bool leftLegActive;

	// Use this for initialization
	void Start ()
    {
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManagerTiroLibre>();

        currentTime = totalTime;
        timeMillis = 1000f;

        remainingRepetitions = totalRepetitions;

        //currentScene = 1;
        //ballCollider.enabled = false;

        //StartGame();
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
                    totalTime += Time.deltaTime;
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

            EndGame();            
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
        //ballCollider.enabled = true;
    }

    public void StartGame(bool withTime, float time, int repetitions, float timeBetweenTargets, bool frontPlane, float frontAngle1, float frontAngle2, float frontAngle3, bool backPlane, float backAngle1, float backAngle2, float backAngle3, bool shifts, bool sustained, float timeSustained)
    {
        this.withTime = withTime;

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
        
        currentTime = time;
        totalRepetitions = repetitions;
        remainingRepetitions = totalRepetitions;
        repetitionsText.text = remainingRepetitions.ToString();

        useFrontPlane = frontPlane;
        useBackPlane = backPlane;
        useShifts = shifts;
        useSustained = sustained;
        this.timeSustained = timeSustained;

        kickScript.firstFlexionAngle = frontAngle1;
        kickScript.secondFlexionAngle = frontAngle2;
        kickScript.thirdFlexionAngle = frontAngle3;
        kickScript.firstExtensionAngle = backAngle1;
        kickScript.secondExtensionAngle = backAngle2;
        kickScript.thirdExtensionAngle = backAngle3;

        isPlaying = true;
        NextTarget();
    }

    public void BallHit(int points)
    {
        UpdateScore(points);

        if (!withTime)
            UpdateRepetitions(1);
        else
            totalRepetitions += 1;
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void NextLeg()
    {
        int choice = Random.Range(0, 2);

        if (choice == 0)
        {
            leftLegActive = false;
            leftLegPanel.SetActive(false);
            rightLegPanel.SetActive(true);
        }
        else
        {
            leftLegActive = true;
            leftLegPanel.SetActive(true);
            rightLegPanel.SetActive(false);
        }
    }

    public void NextTarget()
    {
        if (isPlaying && !isGameOver)
        {
            NextLeg();

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

    public void EndGame()
    {
        mainPanel.SetActive(false);
        
        TherapySessionObject objTherapy = TherapySessionObject.tso;

        if (objTherapy != null)
        {
            objTherapy.fillLastSession(score, totalRepetitions, (int)totalTime, level.ToString());
            objTherapy.saveLastGameSession();

            objTherapy.savePerformance((int)kickScript.BestLeftHipFrontAngle, "4");
            objTherapy.savePerformance((int)kickScript.BestRightHipFrontAngle, "5");
        }       

        int finalScore = (int)(((float)score / totalRepetitions) * 100.0f);
        resultsScoreText.text = finalScore + "%";

        if (objTherapy != null)
            resultsBestScoreText.text = objTherapy.getGameRecord() + "%";

        if (finalScore <= 60)
        {
            //resultMessage.GetComponent<TextMesh>().text = "¡Muy bien!";
            silverTrophy.SetActive(false);
            goldTrophy.SetActive(false);
            bronzeTrophy.SetActive(true);
        }
        else if (finalScore <= 90)
        {
            //resultMessage.GetComponent<TextMesh>().text = "¡Grandioso!";
            bronzeTrophy.SetActive(false);
            goldTrophy.SetActive(false);
            silverTrophy.SetActive(true);
        }
        else if (finalScore <= 100)
        {
            //resultMessage.GetComponent<TextMesh>().text = "¡Increíble!";
            bronzeTrophy.SetActive(false);
            silverTrophy.SetActive(false);
            goldTrophy.SetActive(true);
        }

        resultsPanel.SetActive(true);
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
