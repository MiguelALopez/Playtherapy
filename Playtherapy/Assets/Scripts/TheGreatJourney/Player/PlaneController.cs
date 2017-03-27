using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

	public float speedForward = 90;
	public float speed = 20;

	public float distanceMovementX=10;
	public float initialY;
	public bool useRestitution=false;
	Rigidbody rig;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
		initialY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, speedForward*Time.deltaTime);

		//transform.position += transform.forward * Time.deltaTime * speedForward;
		float vAxis = Input.GetAxis ("Vertical");
		float hAxis = Input.GetAxis ("Horizontal");







		Vector3 movement = new Vector3 (hAxis, vAxis, 0)*speed*Time.deltaTime;

		if (transform.position.x+movement.x>distanceMovementX || transform.position.x+movement.x<-distanceMovementX) 
		{
			hAxis = 0;

		}
		if (transform.position.y+movement.y>initialY+2 || transform.position.y+movement.y<5) 
		{
			vAxis = 0;

		}
		movement=new Vector3 (hAxis, vAxis, 0)*speed*Time.deltaTime;





		rig.MovePosition (transform.position + movement);

		

        if (hAxis == 0 || vAxis == 0)
        {
            transform.Rotate(-vAxis * 5, 0, -hAxis * 5);

        }

        if (useRestitution) {
			if (hAxis == 0 && vAxis == 0) {

                Vector3 startPos = transform.position;
                Vector3 endPos = new Vector3(0, initialY, startPos.z);

                //transform.position = Vector3.MoveTowards(startPos, endPos, speed*Time.deltaTime);
                transform.position=Vector3.Lerp(startPos, endPos, 0.05f);
			}
		}
        
        Quaternion newRotation = Quaternion.AngleAxis(0, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, .05f);

        


		  
	}

}
