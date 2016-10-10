using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{

	// target impact on game
	public int scoreAmount = 0;
	public float timeAmount = 0.0f;

	// explosion when hit?
	public GameObject explosionPrefab;

	// information when hit?
	public GameObject informationPrefab;

	private SpawnGameObjects spawner;
	private GameManagerSushi gameM;

	void Start()
	{
		spawner = GameObject.Find("Spawner").GetComponent<SpawnGameObjects>();
		gameM = GameObject.Find("GameManager").GetComponent<GameManagerSushi>();
	}

	// when collided with another gameObject
	void OnCollisionEnter (Collision newCollision)
	{
		// exit if there is a game manager and the game is over
		if (GameManagerSushi.gms) {
			if (GameManagerSushi.gms.gameIsOver)
				return;
		}

		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}

			if (informationPrefab) {
				//Intantiate an information dialog at the gameObjects position and rotation
				Instantiate (informationPrefab, transform.position, GameObject.FindWithTag("MainCamera").transform.rotation);
			}

			// if game manager exists, make adjustments based on target properties
			if (GameManagerSushi.gms) {
				GameManagerSushi.gms.targetHit (scoreAmount);
			}
				
			// destroy the projectile
			//Destroy (newCollision.gameObject);
				
			gameM.NewRepetition();
			if (gameM.GetRepetitions() >= 0)
			{
				spawner.MakeThingToSpawn();
			}

			// destroy self
			Destroy (gameObject);
		}
	}
}
