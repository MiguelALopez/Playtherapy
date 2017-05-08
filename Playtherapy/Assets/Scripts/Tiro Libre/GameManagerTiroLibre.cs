using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerTiroLibre : MonoBehaviour
{
    public static GameManagerTiroLibre gm;
    public Kick kickScript;
    public GameObject ball;
    //public Vector3 currentBallStartPosition;

    public int currentScene;

    public int level;
    public bool useFrontPlane;
    public bool useBackPlane;
    public bool useShifts;
    public bool useSustained;
    public float timeSustained;

    public GameObject parametersPanel;
    public GameObject mainPanel;
    public GameObject gameOverPanel;
    public GameObject frontLeftLegPanel;
    public GameObject frontRightLegPanel;
    public GameObject backLeftLegPanel;
    public GameObject backRightLegPanel;
    public GameObject shiftLeftPanel;
    public GameObject shiftRightPanel;

    public int score;
    public Text scoreText;
    public TextMesh boardScoreText;

    //public int scoreToWin;
    public bool isPlaying;
    public bool isGameOver;
    public bool withTime;

    public float totalTime;
    private float timeMillis;
    private float currentTime;
    public Slider sliderCurrentTime;
    public Text currentTimeText;

    public int totalRepetitions;
    public int remainingRepetitions;
    public Text repetitionsText;

    public GameObject timerPanel;
    public GameObject repetitionsPanel;

    public int currentTarget;
    public GameObject[] targets1;
    public GameObject[] targets2;
    public GameObject[] targets3;    

    public bool targetReady;
    //public Collider ballCollider;
    public float timeBetweenTargets;
    public bool changeMovement;

    public enum LegMovements { FrontLeftLeg, FrontRightLeg, BackLeftLeg, BackRightLeg };
    private LegMovements[] frontPlane;
    private LegMovements[] backPlane;
    public LegMovements currentMovement;
    private System.Random random;

    public bool isAbleToMove;
    public float shiftsFrequency;
    private bool lastLegMovement;
    private bool lastShiftMovement;
    public enum ShiftPlatforms { LeftPlatform, CenterPlatform, RightPlatform };
    public ShiftPlatforms currentPlatform;
    public GameObject avatarPlatform;
    public GameObject leftShiftPlatform;
    public GameObject centerShiftPlatform;
    public GameObject rightShiftPlatform;

    public Animator cameraAnimator;
    public GameObject resultsPanel;
    public Text resultsScoreText;
    public Text resultsBestScoreText;
    public Sprite starOn;
    public Sprite starOff;
    public Image star1;
    public Image star2;
    public Image star3;    

    // Use this for initialization
    void Start ()
    {
        if (gm == null)
            gm = gameObject.GetComponent<GameManagerTiroLibre>();

        //currentBallStartPosition = ball.transform.position;

        currentTime = totalTime;
        timeMillis = 1000f;

        remainingRepetitions = totalRepetitions;

        frontPlane = new LegMovements[] {LegMovements.FrontLeftLeg, LegMovements.FrontRightLeg};
        backPlane = new LegMovements[] {LegMovements.BackLeftLeg, LegMovements.BackRightLeg};
        random = new System.Random();
        lastLegMovement = false;
        lastShiftMovement = false;
        currentPlatform = ShiftPlatforms.CenterPlatform;

        mainPanel.SetActive(false);
        parametersPanel.SetActive(true);

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
                        currentTimeText.text = (((int)currentTime) / 60).ToString("00") + ":" 
                            + (((int)currentTime) % 60).ToString("00") + ":" 
                            + ((int)(timeMillis * 60 / 1000)).ToString("00");
                        sliderCurrentTime.value = currentTime * 100 / totalTime;                        
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
            isGameOver = false;
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
        NextMovement();        
        //ballCollider.enabled = true;
    }

    public void StartGame(bool withTime, float time, int repetitions, float timeBetweenTargets, bool frontPlane, float frontAngle1, 
        float frontAngle2, float frontAngle3, bool backPlane, float backAngle1, float backAngle2, float backAngle3, bool shifts, 
        float shiftsFrequency, bool sustained, float timeSustained, bool changeMovement)
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

        totalTime = time;
        currentTime = totalTime;
        totalRepetitions = repetitions;
        remainingRepetitions = totalRepetitions;
        repetitionsText.text = remainingRepetitions.ToString();
        this.timeBetweenTargets = timeBetweenTargets;

        useFrontPlane = frontPlane;
        useBackPlane = backPlane;
        useShifts = shifts;
        this.shiftsFrequency = shiftsFrequency;
        useSustained = sustained;
        this.timeSustained = timeSustained;
        this.changeMovement = changeMovement;

        kickScript.firstFrontAngle = frontAngle1;
        kickScript.secondFrontAngle = frontAngle2;
        kickScript.thirdFrontAngle = frontAngle3;
        kickScript.firstBackAngle = backAngle1;
        kickScript.secondBackAngle = backAngle2;
        kickScript.thirdBackAngle = backAngle3;

        mainPanel.SetActive(true);
        isPlaying = true;
        NextMovement();
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
        boardScoreText.text = score.ToString();
    }

    public void EnableMovement(bool enable)
    {
        isAbleToMove = enable;
        kickScript.skeleton.updateRootX = enable;
    }

    public void NextMovement()
    {
        if (isPlaying && !isGameOver)
        {
            if (useShifts && (lastLegMovement || lastShiftMovement))
            {
                int choice = UnityEngine.Random.Range(1, 101);

                if (choice > (100 - shiftsFrequency))
                {
                    NextShiftMovement();
                }
                else
                {
                    NextLegMovement();
                } 
            }
            else
            {
                NextLegMovement();
            }
        }
    }

    public void NextShiftMovement()
    {
        frontLeftLegPanel.SetActive(false);
        frontRightLegPanel.SetActive(false);
        backLeftLegPanel.SetActive(false);
        backRightLegPanel.SetActive(false);

        ShiftPlatforms lastPlatform = currentPlatform;

        if (lastShiftMovement)
        {
            currentPlatform = ShiftPlatforms.CenterPlatform;
            lastShiftMovement = false;
            lastLegMovement = false;
        }
        else
        {
            ArrayList values = new ArrayList(Enum.GetValues(typeof(ShiftPlatforms)));
            values.Remove(ShiftPlatforms.CenterPlatform);
            currentPlatform = (ShiftPlatforms)values[(random.Next(values.Count))];
            lastShiftMovement = true;
        }

        if (lastPlatform == ShiftPlatforms.LeftPlatform)
            shiftRightPanel.SetActive(true);
        else if (lastPlatform == ShiftPlatforms.RightPlatform)
            shiftLeftPanel.SetActive(true);
        else if (currentPlatform == ShiftPlatforms.LeftPlatform)
            shiftLeftPanel.SetActive(true);
        else if (currentPlatform == ShiftPlatforms.RightPlatform)
            shiftRightPanel.SetActive(true);

        avatarPlatform.SetActive(true);

        switch (currentPlatform)
        {
            case ShiftPlatforms.LeftPlatform:
                {                    
                    leftShiftPlatform.SetActive(true);
                    centerShiftPlatform.SetActive(false);
                    rightShiftPlatform.SetActive(false);
                    //currentBallStartPosition.x = leftShiftPlatform.transform.position.x;
                    break;
                }
            case ShiftPlatforms.CenterPlatform:
                {
                    leftShiftPlatform.SetActive(false);
                    centerShiftPlatform.SetActive(true);
                    rightShiftPlatform.SetActive(false);
                    //currentBallStartPosition.x = centerShiftPlatform.transform.position.x;
                    break;
                }
            case ShiftPlatforms.RightPlatform:
                {
                    leftShiftPlatform.SetActive(false);
                    centerShiftPlatform.SetActive(false);
                    rightShiftPlatform.SetActive(true);
                    //currentBallStartPosition.x = rightShiftPlatform.transform.position.x;
                    break;
                }
            default:
                break;
        }

        //ball.transform.position = currentBallStartPosition;

        EnableMovement(true);
    }

    public void ShiftDone()
    {
        avatarPlatform.SetActive(false);
        leftShiftPlatform.SetActive(false);
        centerShiftPlatform.SetActive(false);
        rightShiftPlatform.SetActive(false);
        shiftLeftPanel.SetActive(false);
        shiftRightPanel.SetActive(false);
        EnableMovement(false);

        NextMovement();
    }

    public void NextLegMovement()
    {
        NextTarget();
        NextLeg();

        lastLegMovement = true;
    }

    public void NextTarget()
    {
        int nextTarget;

        switch (currentScene)
        {
            case 1:
                {
                    nextTarget = UnityEngine.Random.Range(0, targets1.Length);
                    StartCoroutine(targets1[nextTarget].GetComponent<TiroLibreTargetBehaviour>().DelayedShow(timeBetweenTargets));
                    break;
                }
            case 2:
                {
                    nextTarget = UnityEngine.Random.Range(0, targets2.Length);
                    StartCoroutine(targets2[nextTarget].GetComponent<TiroLibreTargetBehaviour>().DelayedShow(timeBetweenTargets));
                    break;
                }
            case 3:
                {
                    nextTarget = UnityEngine.Random.Range(0, targets3.Length);
                    StartCoroutine(targets3[nextTarget].GetComponent<TiroLibreTargetBehaviour>().DelayedShow(timeBetweenTargets));
                    break;
                }
            default:
                {
                    nextTarget = UnityEngine.Random.Range(0, targets1.Length);
                    StartCoroutine(targets1[nextTarget].GetComponent<TiroLibreTargetBehaviour>().DelayedShow(timeBetweenTargets));
                    break;
                }
        }

        currentTarget = nextTarget;
        targetReady = true;
    }

    public void NextLeg()
    {
        if (useFrontPlane && useBackPlane)
        {
            Array values = Enum.GetValues(typeof(LegMovements));
            currentMovement = (LegMovements)values.GetValue(random.Next(values.Length));
        }
        else if (useFrontPlane)
        {
            currentMovement = (LegMovements)frontPlane.GetValue(random.Next(frontPlane.Length));
        }
        else
        {
            currentMovement = (LegMovements)backPlane.GetValue(random.Next(backPlane.Length));
        }

        switch (currentMovement)
        {
            case LegMovements.FrontLeftLeg:
                {
                    frontLeftLegPanel.SetActive(true);
                    if (currentTarget == 2 || currentTarget == 5 || currentTarget == 8)
                        frontLeftLegPanel.GetComponent<Animator>().Play("FrontLeftLeg Panel Right");
                    else if (currentTarget == 1 || currentTarget == 4 || currentTarget == 7)
                        frontLeftLegPanel.GetComponent<Animator>().Play("FrontLeftLeg Panel Center");
                    else
                        frontLeftLegPanel.GetComponent<Animator>().Play("FrontLeftLeg Panel Left");
                    frontRightLegPanel.SetActive(false);
                    backLeftLegPanel.SetActive(false);
                    backRightLegPanel.SetActive(false);
                    break;
                }
            case LegMovements.FrontRightLeg:
                {
                    frontLeftLegPanel.SetActive(false);
                    frontRightLegPanel.SetActive(true);
                    if (currentTarget == 2 || currentTarget == 5 || currentTarget == 8)
                        frontRightLegPanel.GetComponent<Animator>().Play("FrontRightLeg Panel Right");
                    else if (currentTarget == 1 || currentTarget == 4 || currentTarget == 7)
                        frontRightLegPanel.GetComponent<Animator>().Play("FrontRightLeg Panel Center");
                    else
                        frontRightLegPanel.GetComponent<Animator>().Play("FrontRightLeg Panel Left");
                    backLeftLegPanel.SetActive(false);
                    backRightLegPanel.SetActive(false);
                    break;
                }
            case LegMovements.BackLeftLeg:
                {
                    frontLeftLegPanel.SetActive(false);
                    frontRightLegPanel.SetActive(false);
                    backLeftLegPanel.SetActive(true);
                    if (currentTarget == 2 || currentTarget == 5 || currentTarget == 8)
                        backLeftLegPanel.GetComponent<Animator>().Play("BackLeftLeg Panel Right");
                    else if (currentTarget == 1 || currentTarget == 4 || currentTarget == 7)
                        backLeftLegPanel.GetComponent<Animator>().Play("BackLeftLeg Panel Center");
                    else
                        backLeftLegPanel.GetComponent<Animator>().Play("BackLeftLeg Panel Left");
                    backRightLegPanel.SetActive(false);
                    break;
                }
            case LegMovements.BackRightLeg:
                {
                    frontLeftLegPanel.SetActive(false);
                    frontRightLegPanel.SetActive(false);
                    backLeftLegPanel.SetActive(false);
                    backRightLegPanel.SetActive(true);
                    if (currentTarget == 2 || currentTarget == 5 || currentTarget == 8)
                        backRightLegPanel.GetComponent<Animator>().Play("BackRightLeg Panel Right");
                    else if (currentTarget == 1 || currentTarget == 4 || currentTarget == 7)
                        backRightLegPanel.GetComponent<Animator>().Play("BackRightLeg Panel Center");
                    else
                        backRightLegPanel.GetComponent<Animator>().Play("BackRightLeg Panel Left");
                    break;
                }
            default:
                break;
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
        StartCoroutine(EndGameAnimation());       
    }

    private IEnumerator EndGameAnimation()
    {
        cameraAnimator.Play("Camera Final Animation");
        yield return new WaitForSeconds(4f);

        SaveAndShowResults();
    }

    public void SaveAndShowResults()
    {
        TherapySessionObject objTherapy = TherapySessionObject.tso;

        if (objTherapy != null)
        {
            objTherapy.fillLastSession(score, totalRepetitions, (int)totalTime, level.ToString());
            objTherapy.saveLastGameSession();

            objTherapy.savePerformance((int)kickScript.BestLeftHipFrontAngle, "4");
            objTherapy.savePerformance((int)kickScript.BestRightHipFrontAngle, "5");
        }

        //totalRepetitions = 10;
        int finalScore = (int)(((float)score / totalRepetitions) * 100.0f);
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

    public Vector3 getTargetPosition(int target)
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

    public void EnableTarget(int target, bool enable)
    {
        switch (currentScene)
        {
            case 1:
                {
                    targets1[target].GetComponent<TiroLibreTargetBehaviour>().EnableTarget(enable);
                    break;
                }
            case 2:
                {
                    targets2[target].GetComponent<TiroLibreTargetBehaviour>().EnableTarget(enable);
                    break;
                }
            case 3:
                {
                    targets3[target].GetComponent<TiroLibreTargetBehaviour>().EnableTarget(enable);
                    break;
                }
            default:
                break;
        }
    }
}
