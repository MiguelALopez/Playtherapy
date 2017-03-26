using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTerrain : MonoBehaviour {

	void OnTriggerEnter(Collider other) {

		Debug.Log ("GASKDFASKDASKD KASDKASKD KASKD AKSK DAKSEB KAWEK");
		switch (other.gameObject.name) {
		case "Terrain Chunk":
			Destroy (other.gameObject);
			break;
		}



	}
}
