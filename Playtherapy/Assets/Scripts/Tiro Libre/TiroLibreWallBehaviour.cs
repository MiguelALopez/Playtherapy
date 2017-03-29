using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroLibreWallBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            GameManagerTiroLibre.gm.BallHit(0);
        }
    }
}
