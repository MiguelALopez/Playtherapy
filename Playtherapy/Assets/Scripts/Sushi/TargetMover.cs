using UnityEngine;
using System.Collections;

public class TargetMover : MonoBehaviour {

	// define the possible states through an enumeration
	public enum status {Up, Static, Down};
	
	// store the state
	private status motionState = status.Up;

	// motion parameters
	public float floatingTime = 5.0f;
	public float upTime = 0.5f;
	public float spinSpeed = 180.0f;
    private float initialHeight = 0.0f;
    public float objectiveHeight = 1.5f;
	private float aliveTime = 0.0f;

	private SpawnGameObjects spawner;
    private SushiSpawner sSpawner;

    private GameManagerSushi gameM;
    
    void Start()
    {
        initialHeight = transform.position.y;
		spawner = GameObject.Find("Spawner").GetComponent<SpawnGameObjects>();
        sSpawner = GameObject.Find("Spawner").GetComponent<SushiSpawner>();
        gameM = GameObject.Find("GameManager").GetComponent<GameManagerSushi>();
    }

	// Update is called once per frame
	void Update () {

		// do the appropriate motion based on the motionState
		switch(motionState) {
		case status.Up:
			// rotate around the up axix of the gameObject
			gameObject.transform.Translate (Vector3.up * ((objectiveHeight - initialHeight) / upTime) * Time.deltaTime, Space.World);

			break;
		case status.Static:
			// move up and down over time
			gameObject.transform.Rotate (Vector3.up * spinSpeed * Time.deltaTime, Space.World);
			gameObject.transform.Rotate (Vector3.right * (180.0f/floatingTime) * Time.deltaTime);
			break;
		case status.Down:
			// move up and down over time
			gameObject.transform.Translate (Vector3.down * ((objectiveHeight - initialHeight) / upTime) * Time.deltaTime, Space.World);
			break;
		}
		aliveTime += Time.deltaTime;
		if (aliveTime < upTime) {
			motionState = status.Up;
		} else if (aliveTime < upTime + floatingTime) {
			motionState = status.Static;
		} else if (aliveTime < upTime + floatingTime + 1.0f) {
			motionState = status.Down;
		} else {
			if (gameM.withTime) {
				if (gameM.currentTime > 0.0f) {
					spawner.MakeThingToSpawn ();
				}
			} else {
				gameM.NewRepetition ();
				if (gameM.GetRepetitions () >= 0) {
					spawner.MakeThingToSpawn ();
				}
			}

            
			Destroy (gameObject);
		}

	}

	void setObjHeight(float objHeight) {
		objectiveHeight = objHeight;
	}
}
