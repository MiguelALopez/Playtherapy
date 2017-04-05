using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
public class SpannerOfMovements : MonoBehaviour {

	GameObject PlanesParentArray;
	GameObject GemsParentArray;
	[Range (0,40)]
	public int maxPlanesInScreen;
	[Range (1,60)]
	public float SecondsPerPlane;
	public GameObject[] planes_types;
	public GameObject[] gems_types;
	// Use this for initialization
	public float distanceFromCenter =15;
	float timer =0;
	void Start () {
		PlanesParentArray = GameObject.Find ("PlanesArray");
		GemsParentArray = GameObject.Find ("GemsArray");
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		int actual_numbers_planes= PlanesParentArray.transform.childCount;

		if (timer<=0 && actual_numbers_planes<maxPlanesInScreen) {

			timer = SecondsPerPlane;
			releaseObject ();

		}
	}
	public void releaseObject()
	{
		sendMoveToRight (0);

	}
	/// <summary>
	/// Sends the move to right.
	/// The planes gone to be send for the left line to avoid
	/// </summary>
	/// <param name="time">Time.</param> used to know how much have the patient in this potition
	/// <param name="speedPlane">Time.</param> used to put the speed of the planes
	public void sendMoveToRight(int time=1,float speed=5)
	{

		// we are goint to send the wave of planes or airballons
		switch (time) {
		case 0: 
			createAirBalloon (new Vector3 (-9, -0.5f, 0), speed);
			createAirBalloon (new Vector3 (-6, -0.5f, 0), speed);
			createAirBalloon (new Vector3 (0, -0.5f, 0), speed);
			createAirBalloon (new Vector3 (3, -0.5f, 0), speed);
			createGem (new Vector3 (9, -0.5f, 15), speed);
			break;
		case 1:
			createWarPlane (new Vector3 (-6, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (1, -0.5f, 15), speed);
			createGem (new Vector3 (9, -0.5f, 15), speed);
			break;
		case 2:
			createSmallPlane (new Vector3 (-3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-1, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 45), speed);
			createGem (new Vector3 (9, -0.5f, 45), speed);
			break;
		case 3:
			createSmallPlane (new Vector3 (-3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-1, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 45), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 45), speed);
			createWarPlane (new Vector3 (-1, -0.5f, 60), speed);
			createGem (new Vector3 (9, -0.5f, 60), speed);
			break;
		case 4:



			break;
		case 5:



			break;
		case 6:



			break;
		default:
			break;
		}




		//createWarPlane(new Vector3(-6,-0.5f,30),speed);
	}

	public void createSmallPlane(Vector3 initialPos=default(Vector3),float speed=5)
	{
		GameObject planeType = planes_types[0];// small plane

		GameObject plane = (GameObject)Instantiate(planeType, transform.position + initialPos, Quaternion.identity);
		SetVelocity velocity = plane.AddComponent<SetVelocity>();
		velocity.speed = speed;
		plane.transform.parent = PlanesParentArray.transform;
		plane.transform.GetChild(0).tag = "Planes";

	}
	public void createWarPlane(Vector3 initialPos=default(Vector3),float speed=5)
	{
		GameObject planeType = planes_types[1];// war plane

		GameObject plane = (GameObject)Instantiate(planeType, transform.position + initialPos, Quaternion.identity);
		SetVelocity velocity = plane.AddComponent<SetVelocity>();
		velocity.speed = speed;
		plane.transform.parent = PlanesParentArray.transform;
		plane.transform.GetChild(0).tag = "Planes";

	}
	public void createAirBalloon(Vector3 initialPos=default(Vector3),float speed=5)
	{
		GameObject planeType = planes_types[2];// war plane

		GameObject plane = (GameObject)Instantiate(planeType, transform.position + initialPos, Quaternion.identity);
		SetVelocity velocity = plane.AddComponent<SetVelocity>();
		velocity.speed = speed;
		plane.transform.parent = PlanesParentArray.transform;
		plane.tag = "Airballoon";

	}
	public void createGem(Vector3 initialPos=default(Vector3),float speed=5)
	{
		GameObject gemType = gems_types[0];// gem

		GameObject gem = (GameObject)Instantiate(gemType, transform.position + initialPos, Quaternion.identity);
		SetVelocity velocity = gem.AddComponent<SetVelocity>();
		velocity.speed = speed;
		gem.transform.parent = GemsParentArray.transform;
		gem.tag = "Coins";

	}
	private void TweenRotate()
	{
		float startAngle = this.transform.rotation.eulerAngles.z;
		float endAngle = startAngle + 360.0f;
		this.gameObject.Tween("RotateCircle", startAngle, endAngle, 2.0f, TweenScaleFunctions.CubicEaseInOut, (t) =>
			{
				// progress
				this.transform.rotation = Quaternion.identity;
				this.transform.Rotate(-Camera.main.transform.right, t.CurrentValue);
			}, (t) =>
			{
				// completion
				this.gameObject.Tween("Rotate2Circle", startAngle, endAngle, 2.0f, TweenScaleFunctions.CubicEaseInOut, (t2) =>
					{
						// progress
						this.transform.rotation = Quaternion.identity;
						this.transform.Rotate(-Camera.main.transform.right, t2.CurrentValue);
					}, (t2) =>
					{
						// completion
					});
			});
	}
}
