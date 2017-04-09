using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float sensitiveRotate = 1.5f;
	public GameObject personaje;
	Animator anim;
	int idleHash;
	int runStateHash;

	private Rigidbody rb;

	void Start ()
	{
		anim = personaje.GetComponent<Animator> ();
		idleHash = Animator.StringToHash("Idle_B");
		runStateHash = Animator.StringToHash("Run");
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float vision = personaje.GetComponent<Transform> ().rotation.y;
		print(" a:   " + personaje.GetComponent<Transform> ().rotation.y);
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//Aplicacion de fuerza
		rb.AddForce (movement * speed);
	
		/*Controla la rotacion del personaje para un desplasamieto realista*/
		float orientacion = 0; 

		//Movimieto A la derecha
		if (moveHorizontal == 1) {
			//Limites de la Rotacion
			orientacion = sensitiveRotate;
		}
		//Movimiento A la Izquierda
		if(moveHorizontal == -1){
			//Limites de la rotacion
			orientacion = -sensitiveRotate;
		}
		personaje.GetComponent<Transform> ().Rotate (0.0f,orientacion,0.0f);


		if (moveHorizontal == 0 && moveVertical == 0) {
			anim.Play (idleHash);
		} else {
			anim.Play (runStateHash);
		}


	}
}
