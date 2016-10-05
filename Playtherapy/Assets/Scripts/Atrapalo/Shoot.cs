using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.GetComponent<Rigidbody>().AddForce(10, 50, 0, ForceMode.Acceleration);
        }
	
	}
}
