using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GuerraMedieval
{
    public class GameManagerMedieval : MonoBehaviour
    {
        public static GameManagerMedieval gmm;

        // Panels used in the scene
        public GameObject mainPanel;
        public GameObject parametrersPanel;
        public GameObject resultsPanel;

        // Used for states of the game
        public enum GameState
        {
            PLAYING,
            GAMEOVER,
            PAUSE,
            STARTING
        }
        private GameState gameState;
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
        public Text scoreText;                              // Current score on the screen

        public Text resultsScoreText;
        public Text resultsBestScoreText;
        public Sprite starOn;
        public Sprite starOff;
        public Image star1;
        public Image star2;
        public Image star3;

        // Parameters of Parameters panel
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

        private PlayState playState;

        // Use this for initialization
        void Start()
        {
            if (gmm == null)
            {
                gmm = this.gameObject.GetComponent<GameManagerMedieval>();
            }
            currentTime = totalTime;
            timeMillis = 1000f;

            gameState = GameState.STARTING;
            score = 0;
            parametrersPanel.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            switch (gameState)
            {
                case GameState.PLAYING:
                    {
                        if (withTime)
                        {
                            currentTime -= Time.deltaTime;

                            if (currentTime >= 0)
                            {
                                timeMillis -= Time.deltaTime * 1000f;
                                if (timeMillis < 0)
                                {
                                    timeMillis = 1000f;
                                }
                                currentTimeText.text = (((int)currentTime) / 60).ToString("00") + ":"
                                    + (((int)currentTime) % 60).ToString("00") + ":"
                                    + ((int)(timeMillis * 60 / 1000)).ToString("00");
                                sliderCurrentTime.value = currentTime * 100 / totalTime;
                            }
                            else
                            {
                                gameState = GameState.GAMEOVER;
                                currentTimeText.text = "00:00:00";
                                //state = PlayState.NONE;
                            }
                        }
                        else
                        {
                            totalTime += Time.deltaTime;
                        }
                    }
                    break;
                case GameState.GAMEOVER:
                    {
                        EndGame();
                    }
                    break;
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
            gameState = GameState.PLAYING;
        }

        public void UpdateScore(int points)
        {
            score += points;
            scoreText.text = score.ToString();

            if (withTime)
            {
                totalRepetitions++;
            }
            else
            {
                UpdateRepetition(1);
            }
        }

        public void UpdateRepetition(int repetitionsDone)
        {
            remainingRepetitions -= repetitionsDone;
            repetitionsText.text = remainingRepetitions.ToString();

            if (remainingRepetitions <= 0)
            {
                gameState = GameState.GAMEOVER;

                playState = PlayState.NONE;
            }
        }

        public void EndGame()
        {
            mainPanel.SetActive(false);
            StartCoroutine(EndGameAnimator());
        }

        private IEnumerator EndGameAnimator()
        {
            //shipAnimator.Play("ShipFinal");
            yield return new WaitForSeconds(0f);
            SaveAndShowResults();
        }

        public void SaveAndShowResults()
        {
            TherapySessionObject objTherapy = TherapySessionObject.tso;

            //if (objTherapy != null)
            //{
            //    objTherapy.fillLastSession(score, totalRepetitions, (int)totalTime, level.ToString());
            //    objTherapy.saveLastGameSession();
            //}

            int finalScore;
            if (totalRepetitions == 0)
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

        public GameState GetGameState()
        {
            return gameState;
        }

        public bool IsPlaying()
        {
            if (gameState == GameState.PLAYING)

                return true;
            else
                return false;
        }

        public bool IsGameOver()
        {
            if (gameState == GameState.GAMEOVER)
                return true;
            else
                return false;
        }


    }
}