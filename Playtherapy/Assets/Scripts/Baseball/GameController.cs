using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController gc;

    public GameObject ParametersPanel;
    public GameObject MainPanel;
	public GameObject ResultPanel;

	public Button boton;

    public  bool InGame;
	public  bool GameOver;

    public GameObject Cannon;

	public GameObject Ball;

    public Animator pitcher;

	public GameObject catcher;

	public float rate = 0f;

	float force = 50;

	float shootTime=5.2f;

	float _time_game;

	public float time_default {

		get
		{
			return _time_game ;
		}
		set
		{
			_time_game = value;

			if (sliderText!=null) {
				sliderText.text = ( ((int)_time_game % 60).ToString("00") + ":" + ((int)_time_game / 60).ToString("00") + " mins");
					
			}
		}


	}

	float _velocity_game;
	public float velocity {

		get
		{
			return _velocity_game ;
		}
		set
		{
			_velocity_game = value;

			if (current_velocity!=null) {

				if (velocity == 1){

					current_velocity.text = "Fácil";
				}
				if (velocity == 2){

					current_velocity.text = "Medio";
				}
				if (velocity == 3){

					current_velocity.text = "Dificil";
				}


			}
		}


	}


	public Slider slider_velocity;
	public Text current_velocity;



	public Slider slider_range;
	public Text current_range;


	float _range_game;
	public float range {

		get
		{
			return _range_game ;
		}
		set
		{
			_range_game = value;

			if (current_range!=null) {

				current_range.text = ((int)_range_game).ToString("") + "m";


			}
		}


	}



	public Text textCurrentTime;
	public Slider sliderCurrentTime;
	public Text sliderText;

	private float time;


	private float currentTime;
	private float timeMillis;
	public float totalTime;

	PutDataResults results;
	public Text finalResult;
	public float lanzamiento;
	public Text total;

	public AudioClip catcher_sound;


	float maxTime = 2;

	public Text scoretext;

	private int score;

	// Use this for initialization
	void Start () {
		gc = gameObject.GetComponent<GameController> ();
		score= 0;
		UpdateScore();
        InGame = false;
        MainPanel.SetActive(false);
		ResultPanel.SetActive (false);
		time_default = 1;

		results = ResultPanel.GetComponent<PutDataResults> ();
		//results = FindObjectOfType<PutDataResults> ();
		//Button btn = boton.GetComponent<Button> ();
		//btn.onClick.AddListener(StartGame);
		
		//sliderCurrentTime.onValueChanged.AddListener(delegate {SlideTime(); });
    }
	
	// Update is called once per frame
	void Update () {
		
		if (InGame) {
			Time.timeScale = 1;

			if (Time.time > shootTime) {
				
				InvokeRepeating ("Lanzar", 0f, 0f);
				shootTime = shootTime + rate;
			}
			print (shootTime);

			if (currentTime < 0) {
				EndGame ();

			}

		} else {

			Time.timeScale = 0;
		}
		currentTime -= Time.deltaTime;
		if (currentTime > 0)
		{
			timeMillis -= Time.deltaTime * 1000;
			if (timeMillis < 0)
				timeMillis = 1000f;
			textCurrentTime.text = (((int)currentTime) / 60).ToString("00") + ":"
				+ (((int)currentTime) % 60).ToString("00") + ":"
				+ ((int)(timeMillis * 60 / 1000)).ToString("00");
			sliderCurrentTime.value = currentTime * 100 / totalTime;
		}
		else
		{
			
			
			textCurrentTime.text = "00:00:00";
		}


		
	}
	public void EndGame()
	{
		
		MainPanel.SetActive (false);
		ResultPanel.SetActive (true);
		StopAllCoroutines();
		InGame = false;
		int result = Mathf.RoundToInt ((score / lanzamiento) * 100);

		results.updateData (result, 0);



	}
	public void SlideTime(){

		time = sliderCurrentTime.value * 30f;

		UpdateSlide ();
	}

	public void UpdateSlide(){
		
		Debug.Log(sliderCurrentTime.value);
	}


	public void StartGame()
	{
		InGame = true;
		currentTime = time_default * 60;

		if (velocity == 1){

			force = 50;
		}
		if (velocity == 2){

			force = 70;
		}
		if (velocity == 3){

			force = 90;
		}
		_range_game = range * 10;;
		GameOver = false;
		MainPanel.SetActive (true);
		ParametersPanel.SetActive (false);

	}

    void Lanzar()
    {
		if (InGame) {
			StartCoroutine(Disparo());
		}

        
    }

	IEnumerator Disparo(){

        pitcher.Play("Throw");
        yield return new WaitForSeconds(2.7f);

        GameObject Temporary_Bullet_Handler;
		Temporary_Bullet_Handler = Instantiate (Ball, Cannon.transform.position, Cannon.transform.rotation) as GameObject;

		Temporary_Bullet_Handler.transform.Rotate (Vector3.left * 30);
		System.Random rany = new System.Random();
		System.Random ranx = new System.Random();

		//int pivy = rany.Next(80.0, 80.23);

		double pivy = rany.NextDouble () * (80.23 - 80) + 80;	


		float radius = 40 ;

		double posX;
		double posY;
		float posZ = catcher.transform.position.z;//posicion z del player


		double angleRandom = 20 +(-1)* ranx.NextDouble () * (230); 

		posX = Math.Cos (angleRandom * Math.PI / 180)*(double)_range_game;
		posY = Math.Sin (angleRandom * Math.PI / 180)*(double)_range_game;


        
        
		var vector = new Vector3 ((float)(catcher.transform.position.x - posX),(float)(catcher.transform.position.y - posY), (float)posZ).normalized * force;



		Temporary_Bullet_Handler.GetComponent<Rigidbody> ().velocity = vector ;
		lanzamiento = lanzamiento + 1;

		Destroy (Temporary_Bullet_Handler, 10.0f);


	}


	public void AddScore(int newscore)
	{
        score += newscore;
		UpdateScore();
	}
	void UpdateScore() {

        scoretext.text = ""+ score;
	}



}
