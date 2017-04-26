using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// for your own scripts make sure to add the following line:
using DigitalRuby.Tween;
public class ManagerGreatJourney : MonoBehaviour {

	List<MonoBehaviour> array_scrips_disabled;
	GameObject paramenters_canvas;
	GameObject results_canvas;
	PutDataResults results_script;
	ScoreHandler score_script;

	Slider timeSlider;
	GameObject timerUI;


	SpannerOfMovements spanner;
	bool game_over;
	bool hasStart;
	float timer_game=-1;

	// Use this for initialization
	void Start () {
		
		hasStart = false;
		game_over = false;

		spanner = FindObjectOfType<SpannerOfMovements> ();
		array_scrips_disabled = new List<MonoBehaviour> ();
		array_scrips_disabled.Add (spanner);
		array_scrips_disabled.Add (FindObjectOfType<SpanwClouds>());
		array_scrips_disabled.Add (FindObjectOfType<PlaneController>());
		paramenters_canvas = GameObject.Find ("parameters_canvas");
		results_canvas = GameObject.Find ("results_canvas");
		score_script = FindObjectOfType<ScoreHandler> ();

		timeSlider = GameObject.Find ("slideTimeUI").GetComponent<Slider>();


		results_script = FindObjectOfType<PutDataResults> ();

		TweenShowParameters ();
		results_canvas.transform.localScale = Vector3.zero;
	}
	public void EndGame()
	{
		saveData ();

		int performance_game = Mathf.RoundToInt (((float)score_script.score_obtain / (float)score_script.score_max) * 100);
		int performance_loaded_BD = 0;
		results_script.updateData (performance_game, performance_loaded_BD);

		
		hasStart = false;
		//paramenters_canvas.SetActive (true);
		foreach (MonoBehaviour behaviour in array_scrips_disabled)
		{
			behaviour.enabled = false;   
		}
		TweenShowResults ();

	}
	public void RetryGame()
	{
		TweenHideResults ();
		TweenShowParameters ();



	}
	public void StartGame()
	{
		timer_game = -1;
		game_over = false;
		hasStart = true;
		timeSlider.value = 100;
		score_script.score_obtain = 0;
		score_script.score_max = 0;
		//paramenters_canvas.SetActive (false);


		foreach (MonoBehaviour behaviour in array_scrips_disabled)
		{
			behaviour.enabled = true;   
		}

		if (HoldParametersGreatJourney.use_time == true) {
			timer_game = HoldParametersGreatJourney.select_jugabilidad * 60;

		} else
		{
			
			if (HoldParametersGreatJourney.lados_involucrados == HoldParametersGreatJourney.LADO_IZQ_DER) {
				HoldParametersGreatJourney.repeticiones_restantes =(int) HoldParametersGreatJourney.select_jugabilidad * 2+1;
			} else {
				HoldParametersGreatJourney.repeticiones_restantes =(int) HoldParametersGreatJourney.select_jugabilidad+1;
			}

		}
		spanner.setup ();
		TweenHideParameters ();

	}
	private void saveData()
	{
		GameObject tre = GameObject.Find ("TherapySession");

		if (tre!=null)
		{
			TherapySessionObject objTherapy = tre.GetComponent<TherapySessionObject> ();

			if (objTherapy!=null) 
			{

				objTherapy.fillLastSession(score_script.score_obtain, score_script.score_max, (int)0, "1");
				objTherapy.saveLastGameSession ();

				objTherapy.savePerformance((int)HoldParametersGreatJourney.best_angle_left, "14");



			}
		}




//		TherapySessionObject objTherapy = GameObject.Find ("TherapySession").GetComponent<TherapySessionObject> ();
//		objTherapy.fillLastSession(score, currentReps, (int)totalTime, level.ToString());
//		objTherapy.saveLastGameSession ();
//
//		objTherapy.savePerformance((int)bestTotalLeftShoulderAngle, "4");
//		objTherapy.savePerformance((int)bestTotalRightShoulderAngle, "5");

	}
	private void TweenShowResults()
	{
		results_canvas.transform.localScale = Vector3.zero;
		this.gameObject.Tween("ShowResults", Vector3.zero, Vector3.one, 0.75f, TweenScaleFunctions.QuadraticEaseOut, (t) =>
			{
				// progress
				results_canvas.transform.localScale =t.CurrentValue;

			}, (t) =>
			{
				//complete
			});



	}
	private void TweenHideResults()
	{
		results_canvas.transform.localScale = Vector3.one;
		this.gameObject.Tween("HideResults", Vector3.one, Vector3.zero, 0.75f, TweenScaleFunctions.QuadraticEaseOut, (t) =>
			{
				// progress
				results_canvas.transform.localScale =t.CurrentValue;

			}, (t) =>
			{
				//complete
			});



	}
	private void TweenShowParameters()
	{
		paramenters_canvas.transform.localScale = Vector3.zero;
		this.gameObject.Tween("ShowParameters", Vector3.zero, Vector3.one, 0.75f, TweenScaleFunctions.QuadraticEaseOut, (t) =>
			{
				// progress
				paramenters_canvas.transform.localScale =t.CurrentValue;

			}, (t) =>
			{
				//complete
			});



	}
	private void TweenHideParameters()
	{
		paramenters_canvas.transform.localScale = Vector3.one;
		this.gameObject.Tween("HideParameters", Vector3.one, Vector3.zero, 0.75f, TweenScaleFunctions.QuadraticEaseOut, (t) =>
			{
				// progress
				paramenters_canvas.transform.localScale =t.CurrentValue;

			}, (t) =>
			{
				//complete
			});



	}
	private void TweenFinishGame(float time=2f)
	{
		this.gameObject.Tween("FinishGameJourney", this.transform.position.x, this.transform.position.x, time, TweenScaleFunctions.QuadraticEaseOut, (t) =>
			{
				// progress


			}, (t) =>
			{
				//complete time
				EndGame();
			});
	}
	void Update () {


		if (hasStart == true) {

			if (game_over==false) 
			{
				if (HoldParametersGreatJourney.use_time == true)
				{
					if (timer_game > 0) {

						timer_game -= Time.deltaTime;

						timeSlider.value = (timer_game / (HoldParametersGreatJourney.select_jugabilidad * 60)) * 100;
					} else {
						timer_game = 0;
						timeSlider.value = 0;
						game_over = true;
						EndGame ();
					}

				} 
				else 
				{
					if (HoldParametersGreatJourney.repeticiones_restantes==0 && spanner.PlanesParentArray.transform.childCount==0) 
					{
						game_over = true;
						print ("termino gameplay");
						TweenFinishGame ();
					
					}
				}
			}







		}

	}
}

