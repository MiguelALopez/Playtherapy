using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    public GameObject plane;
    public float meteorVeocity = 10f;


    private MeshCollider colliderPlane;
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

        colliderPlane = plane.GetComponent<MeshCollider>();
        planeSize = colliderPlane.bounds.size;

        savedTime = Time.time;
        secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);

        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GameManagerSpace.gms.IsPlaying())
        {
            Spawn();
        }
    }

    void Spawn()
    {
        //if (Time.time - savedTime >= secondsBetweenSpawning && !meteorsRender[meteorCount].enabled)
        if (Time.time - savedTime >= secondsBetweenSpawning && !meteors[meteorCount].activeSelf)
        {
            meteors[meteorCount].transform.position = plane.transform.position;
            //meteorsRender[meteorCount].enabled = true;
            meteors[meteorCount].SetActive(true);
            meteors[meteorCount].GetComponent<Rigidbody>().AddTorque(Vector3.up * 500f);
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
