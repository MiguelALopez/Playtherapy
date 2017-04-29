using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour {

    public GameObject shieldParticle;


	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shield")
        {
            Instantiate(shieldParticle, transform.position, Quaternion.identity);
        }
    }
}
