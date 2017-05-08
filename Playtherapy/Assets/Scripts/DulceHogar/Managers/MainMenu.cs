using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace CompleteProject
{
	public class MainMenu : MonoBehaviour {

		// Use this for initialization
		private bool parametro = false;
		private Text uiParametro;
		private Slider slider_jugabilidad;
		private Dropdown jugabilidad;
		public static MainMenu status;
		public float timer_game;
		private StatusGame statusGame; // Referacia Al objeto de status que es enviado a todas las escenas

		void Awake (){

			uiParametro = GameObject.Find ("Parametro").GetComponent<Text>();
			slider_jugabilidad = GameObject.Find ("SliderJugabilidad").GetComponent<Slider>();
			jugabilidad = GameObject.Find ("DropdownJugabilidad").GetComponent<Dropdown>();
		}
		void Start(){
			slider_jugabilidad.minValue = 2;
			slider_jugabilidad.maxValue = 30;
			statusGame = GameObject.Find ("StatusGame").GetComponent<StatusGame> ();


		}
		// Update is called once per frame
		void Update () {
			CambioIndicador ();
		}

		/*Inicia el juego con los parametros de entrada*/
		public void StartButton(){
			statusGame.timer_game = (float)slider_jugabilidad.value * 60.0f; // Asigna el valor del Slider para ser llevado a otras escenas.
			statusGame.tiempoInicial = slider_jugabilidad.value;
			NGUIDebug.Log ("Timer SetUp :" + statusGame.timer_game + " Tiempo Inicial: " + statusGame.tiempoInicial);

            if (statusGame.indicador == "star")
            {
                /*Cuando es selecionada numero de estrellas convierte el tiempo de 60 minutos
                 * para que no termine hasta que el jugador recoja todos los items*/
                statusGame.timer_game = 3600;
                statusGame.tiempoInicial = 60;    
            }
 
			SceneManager.LoadScene ("Urban");//Carga la escena principal del juego.
		}
		//Esta funcion cambia el indicador que es mostrado en pantalla dependiendo si se realiza por tiempo o numero de Items 
		// Tomando Como referencia el DropDown
		private void CambioIndicador(){
			int valor = (int) slider_jugabilidad.value;

			if(jugabilidad.value == 0)
			{	
				uiParametro.text = valor + ": Minutos";
				slider_jugabilidad.minValue = 2;
				slider_jugabilidad.maxValue = 15;
                statusGame.indicador = "time";

			}else
			{
				uiParametro.text = valor + ": Estrellas";
				slider_jugabilidad.minValue = 5;
				slider_jugabilidad.maxValue = 20;
                statusGame.indicador = "star";
			}
		}
}
}