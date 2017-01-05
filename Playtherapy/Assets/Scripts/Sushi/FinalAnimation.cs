using UnityEngine;
using System.Collections;

public class FinalAnimation : MonoBehaviour {

    GameObject cam;
    GameObject table;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera");
        table = GameObject.Find("SushiContainer");
        cam.transform.Translate(table.transform.position - cam.transform.position - new Vector3(0.0f, 0.0f, 2.0f));

    }
	
	// Update is called once per frame
	void Update () {
        cam.transform.Translate(new Vector3(0.0f, 0.2f * Time.deltaTime, 0.0f));
        
	}
}
