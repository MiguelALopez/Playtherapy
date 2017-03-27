using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjectSpanned : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other) {




		if (other.name=="Terrain Chunk") {
			print ("Hi");
		}
		switch (other.tag) {
		case "Clouds":
			Destroy (other.transform.parent.gameObject);
			break;
		case"Planes":
			Destroy(other.gameObject);
			break;
		case"Coins":
			Destroy(other.gameObject);
			break;
		
		default:
			break;
		}

		//Destroy(other.gameObject);
	}
}
