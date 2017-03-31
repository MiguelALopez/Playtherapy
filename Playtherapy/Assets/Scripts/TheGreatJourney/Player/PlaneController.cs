using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using MovementDetectionLibrary;
public class PlaneController : MonoBehaviour {

	public float speedForward = 90;
	public float speed = 20;

	public float distanceMovementX=10;
	public float initialY;
	public bool useRestitution=false;
	Rigidbody rig;
	//connection with the kinect

	BodyFrameReader reader;
	KinectSensor sensor;
	MovementsCollection bodyMovements;
	Dictionary<BodyParts,BodyPoint> bodyPointsCollection;
	KinectTwoAdapter adapter;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
		initialY = transform.position.y;
		adapter = gameObject.AddComponent<KinectTwoAdapter> ();
		sensor = KinectSensor.GetDefault ();

		if (sensor!=null) {

			if (!sensor.IsOpen) {
				sensor.Open ();
				bodyMovements = new MovementsCollection ();
				bodyPointsCollection = new Dictionary<BodyParts, BodyPoint> ();
				for (int i = 0; i < (int) BodyParts.ThumbRight; i++) {
					bodyPointsCollection.Add (((BodyParts)i), new BodyPoint ((BodyParts)i));
				}

			
			}

		}



	}
	
	// Update is called once per frame
	void Update () {

		this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, speedForward*Time.deltaTime);
		float vAxis = Input.GetAxis ("Vertical");
		float hAxis = Input.GetAxis ("Horizontal");
		if (sensor!=null) {


			for (int i = 0; i < (int) BodyParts.ThumbRight; i++) {
				BodyPointPosition position = adapter.ReturnPosition ((BodyParts)i);
				bodyPointsCollection [(BodyParts)i].setPosition (position);
			}
			bodyMovements.setBodyPointsCollection (bodyPointsCollection);


			//transform.position += transform.forward * Time.deltaTime * speedForward;


			double angleMovement;
			var maxAngle = 10;
			angleMovement= bodyMovements.hipLeftAbMovement ();
			if ( angleMovement> maxAngle) 
			{
				print ("movio a la izquierda"+angleMovement);
				hAxis = -(float)(angleMovement / maxAngle);
			} 
			else 
			{
				angleMovement = bodyMovements.hipRigthAbMovement ();
				if ( angleMovement> maxAngle) {
					print ("movio a la derecha"+angleMovement);
					hAxis = (float)(angleMovement / maxAngle);
				}
			}


		}












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
