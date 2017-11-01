using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Contact : MonoBehaviour {

	public int scorevalue;
	private GameController gameController;
	public GameObject ball_particles; 

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {

			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {

			Debug.Log("Cannot find GameController script");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Character")
		{
			Instantiate (ball_particles, transform.position, transform.rotation);
			GameController.gc.AddScore (scorevalue);
			Destroy (gameObject);

		}



		//Destroy (gameObject);
	}
}
