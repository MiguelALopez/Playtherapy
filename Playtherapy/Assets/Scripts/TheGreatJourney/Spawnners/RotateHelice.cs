using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHelice : MonoBehaviour {

	public int  spinSpeed = 30;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
	}
}
