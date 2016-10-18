using UnityEngine;
using System.Collections;

public class TargetBehaviorBall : MonoBehaviour
{

	// target impact on game
	public int scoreAmount = 10;
	public float timeAmount = 0.0f;

	// explosion when hit?
	public GameObject explosionPrefab;

	// information when hit?
	public GameObject informationPrefab;

	// when collided with another gameObject
	void OnCollisionEnter (Collision newCollision)
	{
		Debug.Log ("colision");

		// exit if there is a game manager and the game is over
		if (GameManagerSushi.gms) {
			if (GameManagerSushi.gms.gameIsOver)
				Debug.Log ("game over");
				return;
		}
		Debug.Log (newCollision.gameObject.tag);
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Player") {
			Debug.Log ("colision pla");
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}

			/*if (informationPrefab) {
				//Intantiate an information dialog at the gameObjects position and rotation
				Instantiate (informationPrefab, transform.position, GameObject.FindWithTag("MainCamera").transform.rotation);
			}*/

			// if game manager exists, make adjustments based on target properties
			if (GameManagerAtrapalo.gms) {
				GameManagerAtrapalo.gms.targetHit (scoreAmount);
			}
				
			// destroy the projectile
			//Destroy (newCollision.gameObject);
				
			// destroy self
			Destroy (gameObject);
		}
	}
}
