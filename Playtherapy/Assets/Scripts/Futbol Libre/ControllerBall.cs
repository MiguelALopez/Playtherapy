using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBall : MonoBehaviour {

	public Rigidbody ball;
	public float velocity= 10f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		print ("ballsaldlsadlsald");
		if (Input.anyKeyDown) {
			if (ball!=null) {
				ball.velocity= (Vector3.up*velocity);
				print ("ball kicked");
			}
		}
	}
}
