using UnityEngine;
using System.Collections;

public class CuboRotate : MonoBehaviour {
	/*It controls the behavior of the cube based on the color assigned by the class Shoot*/
	private Color defaultColor;
	// Use this for initialization
	void Start () {
		defaultColor = new Color (0.2f,0.2f,0.2f);
		GetComponent<MeshRenderer> ().material.color = defaultColor;
	}
	
	// Update is called once per frame
	void Update () {
		Color currentColor = GetComponent<MeshRenderer> ().material.color;

		if (defaultColor.Equals (currentColor)) {
			transform.Rotate ( Vector3.up * (120 * Time.deltaTime) );

		}
	}
}
