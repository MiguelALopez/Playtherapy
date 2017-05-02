using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    public GameObject plane;
    public float meteorVeocity = 10f;

    private GameObject[] meteors;

    public float minSecondsBetweenSpawning = 3.0f;
    public float maxSecondsBetweenSpawning = 6.0f;

    private Vector3 planeSize;

    private float savedTime;
    private float secondsBetweenSpawning;
    private int meteorCount;

    // Use this for initialization
    void Start () {
        meteors = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in meteors)
        {
            obj.SetActive(false);
        }

        meteorCount = 0;

        planeSize = plane.GetComponent<MeshCollider>().bounds.size;

        savedTime = Time.time;
        secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);

        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GameManagerSpace.gms.IsPlaying() && GameManagerSpace.gms.GetState() == GameManagerSpace.PlayState.ASTEROIDS)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        //if (Time.time - savedTime >= secondsBetweenSpawning && !meteorsRender[meteorCount].enabled)
        if (Time.time - savedTime >= secondsBetweenSpawning && !meteors[meteorCount].activeSelf)
        {
            meteors[meteorCount].transform.position = plane.transform.position;
            //meteors[meteorCount].transform.position = new Vector3(Random.Range(-planeSize.x / 2, planeSize.x / 2), 0f, plane.transform.position.z);
            //meteorsRender[meteorCount].enabled = true;
            meteors[meteorCount].SetActive(true);
            meteors[meteorCount].GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 7f;
            meteors[meteorCount].GetComponent<Rigidbody>().velocity = Vector3.back* meteorVeocity;

            meteorCount++;
            if(meteorCount >= meteors.Length)
            {
                meteorCount = 0;
            }

            savedTime = Time.time;
            secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
        }
    }
}
