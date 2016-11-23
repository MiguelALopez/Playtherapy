using UnityEngine;
using System.Collections;

public class EndTherapySession : MonoBehaviour {

	// used for transition between menus
	public GameObject canvasOld;
	public GameObject canvasNew;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Tiki(){
		canvasOld.SetActive(false);
		canvasNew.SetActive(true);  
	}
}
