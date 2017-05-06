using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldDestroy : MonoBehaviour {

    public float lifeTime = 10f;                // The lifetime of the force field
    private float savedTime;                    // Temporal variable for save the init of the life

	// Use this for initialization
	void Start () {
        savedTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.time - savedTime >= lifeTime)
        {
            gameObject.SetActive(false);
        }
	}

    // Used for set the beginnig of life
    private void OnEnable()
    {
        savedTime = Time.time;
    }
}
