using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetBehaviorBall : MonoBehaviour
{

	// target impact on game
	public int scoreAmount;
	public float timeAmount = 0.0f;

	// explosion when hit?
	public GameObject explosionPrefab;
    public TextMesh textPoint;
    public Material mat;
    public Font font;

    // information when hit?
    public GameObject informationPrefab;


	private MovementDetectionLibrary.SpawnGameObjects spawner;
	private GameManagerAtrapalo gameM;

	private SpawnGameObjectsBall sSpawner;

	void Start()
	{
		spawner = GameObject.Find("Spawner").GetComponent<MovementDetectionLibrary.SpawnGameObjects>();
		sSpawner = GameObject.Find("Spawner").GetComponent<SpawnGameObjectsBall>();
		gameM = GameObject.Find("GameManager").GetComponent<GameManagerAtrapalo>();
        if (scoreAmount > 0)
        {
            textPoint.text = "+"+scoreAmount.ToString();
        }
        else
        {
            textPoint.text =  scoreAmount.ToString();

        }

        textPoint.color = getColorBall(scoreAmount);
	}

	// when collided with another gameObject
	void OnTriggerEnter  (Collider newCollision)
	{

		// exit if there is a game manager and the game is over
		if (GameManagerSushi.gms) {
			if (GameManagerSushi.gms.gameIsOver)
				return;
		}
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "HandRight"||newCollision.gameObject.tag == "HandLeft") {
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
                Instantiate(textPoint, transform.position, textPoint.transform.rotation);
                // create 3d text mesh


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

    private Color getColorBall(int score)
    {
        Color ballColor;

        switch (score)
        {
            case 1:
                ballColor = new Color(0, 95, 195, 1);
                break;
            case -2:
                ballColor = new Color(255, 0, 0, 1);
                break;
           case 3:
                ballColor = new Color(233, 255, 98, 1);
                break;
            default:
                ballColor = new Color(253, 0, 0, 1);
                break;
        }
        return ballColor;
    }
}
