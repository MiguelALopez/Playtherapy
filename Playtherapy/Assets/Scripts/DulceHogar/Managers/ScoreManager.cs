using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;
        // The player's score.
        private int nextLevel;
        //Puntaje necesario para cambiar de nivel
        private string nameScene;
        Text text;
        // Reference to the Text component.
        private StatusGame statusGame;
        // Referacia Al objeto de status que es enviado a todas las escenas.
        private Slider timeSlider;
        private float timer_game;
        public GameObject player;
        PlayerHealth playerHealth;



        void Awake()
        {
            // Set up the reference.

            timeSlider = GameObject.Find("SliderTime").GetComponent<Slider>(); //Contador de Tiempo
            text = GetComponent <Text>();
            statusGame = GameObject.Find("StatusGame").GetComponent<StatusGame>();
            this.timer_game = statusGame.timer_game; // Tiempo Global
            this.nextLevel = (int)statusGame.tiempoInicial * 10; // Reutilizo la variable del tiempo para indicar el numero de estrellas
            Scene scene = SceneManager.GetActiveScene();
            nameScene = scene.name;

            score = 0;

            //Busca Jugador
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent <PlayerHealth>();
            relationTimeStar(); //Relecion de estrellas con cantidad de tiempo.
            NGUIDebug.Log ("**************** Numero de Objetos a Capturar: " + nextLevel);
        }


        void Update()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
            text.text = "Puntaje: " + score;
            //Cambio de Escena
            if (score >= nextLevel && "Urban".Equals(nameScene))
            {
                SceneManager.LoadScene("ParkNatural");
            }
            if (score >= nextLevel && "ParkNatural".Equals(nameScene))
            {
                SceneManager.LoadScene("ParkNaturalDark");
            }
            if (score >= nextLevel && "ParkNaturalDark".Equals(nameScene))
            {
                SceneManager.LoadScene("MenuMain");
            }

            //Contador de Tiempo
            TimerController();
        }

        void TimerController()
        {
		
            if (timer_game > 0)
            {

                timer_game -= Time.deltaTime;
                statusGame.timer_game = timer_game;
                float inicial = statusGame.tiempoInicial * 60;

                //NGUIDebug.Log ("Timer:  "+ timer_game + "Status Time "+ inicial);
                timeSlider.value = (timer_game / inicial) * 100;
                //NGUIDebug.Log ("Slider Value:  " + timeSlider.value);
            }
            else
            {
                //Termina el juego cuando el contador llega a 0
                playerHealth.currentHealth = 0;
            }
        }
        /*Relacion de Tiempo Con respecto al numero de estrellas*/
        void relationTimeStar()
        {
            if (statusGame.indicador == "time")
            {
                if (statusGame.tiempoInicial < 5)
                {
                    nextLevel = 80;
                }
                if (statusGame.tiempoInicial >= 5)
                {
                    nextLevel = 150;
                }
                if (statusGame.tiempoInicial >= 10 )
                {
                    nextLevel = 200;
                }
               
            }
        }
    }
}