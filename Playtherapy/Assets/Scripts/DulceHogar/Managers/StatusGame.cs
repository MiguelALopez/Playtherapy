using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CompleteProject
{
public class StatusGame : MonoBehaviour {
		public StatusGame status;
		public float timer_game; // Tiempo Global del Juego.
		public float healthPlayer = 100; // Salud Global del Juego.
		public float tiempoInicial;
        public string indicador;

	// Use this for initialization
		void Awake(){
		/*Crea una instancia del GameObject que no se destruye entre escenas*/	
			if (status == null) {
				/*Verifica que No exista previamente Otro StatusGame
				Antes de Crearse.*/
				DontDestroyOnLoad (gameObject);

			}else if(status != this){
				Destroy (gameObject);
			}

		}
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}