using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour {

	// Use this for initialization
	public float speed = 2;
	public GameObject ObjetoGame;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ObjetoGame.transform.Rotate (new Vector3(1,45,1)* Time.deltaTime * speed);
	}
}
