using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public float speed = 7;
	public float rotacion = 0.27f;
	public float sensitiveRotate = 1.5f;
	Vector3 movement;
	public GameObject personaje;
	Animator anim;
	int idleHash;
	int runStateHash;

	private Rigidbody playerRigidbody;

	void Start ()
	{
		anim = personaje.GetComponent<Animator> ();
		idleHash = Animator.StringToHash("Idle_B");
		runStateHash = Animator.StringToHash("Run");
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float vision = personaje.GetComponent<Transform> ().rotation.y;
		//Aplicacion de fuerza
		Move (moveHorizontal, moveVertical);
	
		/*Controla la rotacion del personaje para un desplasamieto realista*/
		float orientacion = 0; 
		float angulorotation = personaje.GetComponent<Transform> ().rotation.y;

		//Movimieto Rotatorio a la derecha
		if (moveHorizontal == 1) {
			//Limites de la Rotacion
			if (angulorotation < rotacion) {
				orientacion = sensitiveRotate;
			}
		}

		//Movimiento Rotatorio a la Izquierda
		if(moveHorizontal == -1){
			//Limites de la rotacion
			if (angulorotation > -rotacion) {
				orientacion = -sensitiveRotate;
			}
		}
//Aplica la rotacion al personaje
		personaje.GetComponent<Transform> ().Rotate (0.0f,orientacion,0.0f);

// Ejecuta la Animacion de correr y detencion
		if (moveHorizontal == 0 && moveVertical == 0) {
			anim.Play (idleHash);
		} else {
			anim.Play (runStateHash);
		}


	}
void Move (float h, float v)
	{
// Set the movement vector based on the axis input.
	movement.Set (h, 0f, v);
// Normalise the movement vector and make it proportional to the speed per second.
	movement = movement.normalized * speed * Time.deltaTime;
// Move the player to it's current position plus the movement.
	playerRigidbody.MovePosition (transform.position + movement);
	}
}
