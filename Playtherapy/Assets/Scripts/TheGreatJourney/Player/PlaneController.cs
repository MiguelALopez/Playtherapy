using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using MovementDetectionLibrary;
using UnityEngine.UI;
public class PlaneController : MonoBehaviour {

    public float speedForward = 90;
    public float speed = 20;

    public float distanceMovementX = 10;
    public float initialY;
    public bool useRestitution = false;
    Rigidbody rig;
    //connection with the kinect

    BodyFrameReader reader;
    KinectSensor sensor;
    MovementsCollection bodyMovements;
    Dictionary<BodyParts, BodyPoint> bodyPointsCollection;
    KinectTwoAdapter adapter;


    //parametros de angulos de la cadera
	public double minAngle = HoldParametersGreatJourney.min_angle-2;
	public double maxAngle = HoldParametersGreatJourney.select_angle;

	Text txt_prueba;
	GameObject prueba;
    // Use this for initialization
    void Start() {
        rig = GetComponent<Rigidbody>();
        initialY = transform.position.y;
		prueba = GameObject.Find ("angle_test");
		prueba.SetActive (false);
		txt_prueba = prueba.GetComponent<Text>();
        connectWithSensor();
		this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, speedForward*Time.deltaTime);

    }
    void connectWithSensor()
    {
        adapter = gameObject.AddComponent<KinectTwoAdapter>();
        sensor = KinectSensor.GetDefault();

        if (sensor != null)
        {
			prueba.SetActive (true);
            if (!sensor.IsOpen)
            {
                sensor.Open();
            }
			bodyMovements = new MovementsCollection();
			bodyPointsCollection = new Dictionary<BodyParts, BodyPoint>();
			for (int i = 0; i < (int)BodyParts.ThumbRight; i++)
			{
				bodyPointsCollection.Add(((BodyParts)i), new BodyPoint((BodyParts)i));
			}

        }
    }
    MovementAxis moveWithKinect(double hAxis,double vAxis)
    {
		
		if (sensor != null && sensor.IsOpen)
        {

            // se actualiza cada parte del cuerpo 
            for (int i = 0; i < (int)BodyParts.ThumbRight; i++)
            {
                BodyPointPosition position = adapter.ReturnPosition((BodyParts)i);
                bodyPointsCollection[(BodyParts)i].setPosition(position);
            }
            bodyMovements.setBodyPointsCollection(bodyPointsCollection);


            //transform.position += transform.forward * Time.deltaTime * speedForward;


            double angleMovement;
            angleMovement = bodyMovements.hipLeftAbMovement();

			if (angleMovement >= minAngle) {
				//print ("movio a la izquierda" + angleMovement);
				hAxis = -(float)(angleMovement / (maxAngle + minAngle));
			} else {

				angleMovement = bodyMovements.hipRigthAbMovement();
				if (angleMovement >= minAngle)
				{
					//print("movio a la derecha" + angleMovement);
					hAxis = (float)(angleMovement / (maxAngle + minAngle));
				}
				// falta hacia abajo (probar)

			}
			txt_prueba.text = "angle: " +angleMovement+"º";
           

			if (angleMovement>HoldParametersGreatJourney.best_angle_left) {
				HoldParametersGreatJourney.best_angle_left = angleMovement;
            }


        }
        return new MovementAxis(hAxis,vAxis);
    }
    // Update is called once per frame
    void Update () {


		float vAxis = Input.GetAxis ("Vertical");
		float hAxis = Input.GetAxis ("Horizontal");
        
        // de estar conectado el kinect vera el moviento y retornara lo que ha movido
        // de no ser asi pasara los valores como estan
        MovementAxis movement_axis = moveWithKinect(hAxis,vAxis);
        hAxis = (float)movement_axis.xAxis;
        vAxis = (float)movement_axis.yAxis;
        
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

		
        //esto es para controlar ña rotacion apenas se mueva el avion en cualquiera de las dos direcciones (X,y)
        if (hAxis == 0 || vAxis == 0)
        {
            transform.Rotate(-vAxis * 5, 0, -hAxis * 5);

        }

        if (useRestitution) {
			if (hAxis == 0 && vAxis == 0) {

                Vector3 startPos = transform.position;
                Vector3 endPos = new Vector3(0, initialY, startPos.z);
                transform.position=Vector3.Lerp(startPos, endPos, 0.05f);
			}
		}
        
        Quaternion newRotation = Quaternion.AngleAxis(0, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, .05f);

        


		  
	}
    class MovementAxis
    {
        public double xAxis;
        public double yAxis;
        
        public MovementAxis(double x,double y)
        {
            xAxis = x;
            yAxis = y;
        }
        
    }
}
