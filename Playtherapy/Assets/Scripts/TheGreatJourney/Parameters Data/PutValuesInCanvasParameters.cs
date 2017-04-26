using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PutValuesInCanvasParameters : MonoBehaviour {



	Dropdown jugabilidad;
	Text txt_jugabilidad;
	Slider slider_jugabilidad;

	Slider angulo_dificultad;
	Text txt_nivel;

	Slider sostener_movimiento;
	Text txt_sostener;

	Slider tiempo_descanso;
	Text txt_descanso;
	GameObject timerUI;

	Dropdown lados_utilizar;


	// Use this for initialization
	void Start () {
		timerUI = GameObject.Find ("timerUI");
		jugabilidad = GameObject.Find ("DropdownJugabilidad").GetComponent<Dropdown>();
		txt_jugabilidad = GameObject.Find ("txt_jugabilidad").GetComponent<Text> ();
		slider_jugabilidad = GameObject.Find ("SliderJugabilidad").GetComponent<Slider>();



	
		HoldParametersGreatJourney.use_time = (jugabilidad.value==0);


		if (HoldParametersGreatJourney.use_time == false) {
			slider_jugabilidad.minValue = HoldParametersGreatJourney.min_repeticiones;
			slider_jugabilidad.maxValue = HoldParametersGreatJourney.max_repeticiones;
		}
		else 
		{
			slider_jugabilidad.minValue = HoldParametersGreatJourney.min_tiempo;
			slider_jugabilidad.maxValue = HoldParametersGreatJourney.max_tiempo;
		}


		angulo_dificultad = GameObject.Find ("SliderNivel").GetComponent<Slider>();
		txt_nivel = GameObject.Find ("txt_nivel").GetComponent<Text> ();;


		angulo_dificultad.minValue = HoldParametersGreatJourney.min_angle;
		angulo_dificultad.maxValue = HoldParametersGreatJourney.max_angle;

		sostener_movimiento = GameObject.Find ("SliderSostener").GetComponent<Slider>();
		txt_sostener = GameObject.Find ("txt_sostener").GetComponent<Text> ();;

		sostener_movimiento.minValue = HoldParametersGreatJourney.min_sostener;
		sostener_movimiento.maxValue = HoldParametersGreatJourney.max_sostener;


		tiempo_descanso = GameObject.Find ("SliderDescanso").GetComponent<Slider>();
		txt_descanso = GameObject.Find ("txt_descanso").GetComponent<Text> ();;

		tiempo_descanso.minValue = HoldParametersGreatJourney.min_descanso;
		tiempo_descanso.maxValue = HoldParametersGreatJourney.max_descanso;


		lados_utilizar = GameObject.Find ("dropdownLados").GetComponent<Dropdown>();

	}
	
	// Update is called once per frame
	void Update () {

		HoldParametersGreatJourney.use_time = (jugabilidad.value==1);
		HoldParametersGreatJourney.lados_involucrados = lados_utilizar.value;
		if (HoldParametersGreatJourney.use_time == false) {
			slider_jugabilidad.minValue = HoldParametersGreatJourney.min_repeticiones;
			slider_jugabilidad.maxValue = HoldParametersGreatJourney.max_repeticiones;
			timerUI.SetActive (false);
		}
		else 
		{
			timerUI.SetActive (true);
			slider_jugabilidad.minValue = HoldParametersGreatJourney.min_tiempo;
			slider_jugabilidad.maxValue = HoldParametersGreatJourney.max_tiempo;
		}



		HoldParametersGreatJourney.select_jugabilidad = slider_jugabilidad.value;
		if (HoldParametersGreatJourney.use_time == false) {
			txt_jugabilidad.text = "" + HoldParametersGreatJourney.select_jugabilidad+" rep";
		} else {


			string minutos_s="";
			string segundos_s = "00";

			if (HoldParametersGreatJourney.select_jugabilidad < 10) {
				minutos_s = "0" + HoldParametersGreatJourney.select_jugabilidad;
			} else {
				minutos_s = "" + HoldParametersGreatJourney.select_jugabilidad;
			}


			minutos_s = minutos_s.Substring (0, 2);

			int min = int.Parse(minutos_s);
			float segundos = HoldParametersGreatJourney.select_jugabilidad - min;


			//print (min+":"+segundos+" s");





			if (segundos > 0) {
				segundos= segundos*60;
				if (segundos < 10) {
					segundos_s = "0" + segundos;
				} else {
					segundos_s = "" + segundos;
				}

				segundos_s =segundos_s.Substring (0, 2);
			} 


			txt_jugabilidad.text = minutos_s + ":"+ segundos_s+" min";
		}

		HoldParametersGreatJourney.select_angle = angulo_dificultad.value;
		txt_nivel.text = "" + HoldParametersGreatJourney.select_angle+"º";

		HoldParametersGreatJourney.select_sostener = sostener_movimiento.value;
		txt_sostener.text = "" + HoldParametersGreatJourney.select_sostener+" seg";

		HoldParametersGreatJourney.select_descanso = tiempo_descanso.value;
		txt_descanso.text = "" + HoldParametersGreatJourney.select_descanso+" seg";

	}
}
