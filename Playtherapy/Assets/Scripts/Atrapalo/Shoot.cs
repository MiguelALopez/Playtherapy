﻿using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{

    public class Shoot : MonoBehaviour
    {


        bool flag = true;
        float angle;
        GameAngles calc;
        bool flagSide = true;
		private GameManagerAtrapalo gameM;
		private float timeLaunch;
		public int type;


        // Use this for initialization
        void Start()
        {
            calc = new GameAngles();
			gameM = GameObject.Find("GameManager").GetComponent<GameManagerAtrapalo>();
			gameM.ballsAlive++;
			angle = (180*gameM.level)/6;
			timeLaunch = gameM.launchTime;
        }

        // Update is called once per frame
        void Update()
        {

			if (type == 3) {
				timeLaunch = timeLaunch / 2;
			}

            if (flag)
            {

                int side = Mathf.RoundToInt(Random.Range(1,3));
                if (side==1)
                {
                    this.shoot("ShoulderRigth", "HandRigth", "left");

                }else
                {
                    this.shoot("ShoulderLeft", "HandLeft", "rigth");
                }

                flag = false;
            }

        }



        Vector3 calculateSpeedVector(float t, Vector3 point)
        {
            Vector3 initSpeed = new Vector3();

            float Vx = 0;
            float Vy = 0;
            float Vz = 0;
            float X0 = this.transform.position.x;
            float Z0 = this.transform.position.z;
            float Y1 = point.y;
            float X1 = point.x;
            float Z1 = point.z;

            Vx = (X1 - X0) / t;
            Vz = (Z1 - Z0) / t;

            Vy = ((9.8f * t) / 2) + Y1/t;


            initSpeed.x = Vx;
            initSpeed.y = Vy;
            initSpeed.z = Vz;

            return initSpeed;
        }


		public void shoot(string jointOneName, string jointTwoName, string side)
        {


            float angleRad = calc.setRamdomAngle(angle, side);

            Vector3 pointOne = GameObject.FindGameObjectWithTag(jointOneName).transform.position;
            Vector3 pointTwo = GameObject.FindGameObjectWithTag(jointTwoName).transform.position;


            Vector3 pointFin = calc.getPosition(pointOne, calc.createPointTwoShoulderAF(pointOne, pointTwo), angleRad);

            //this.transform.position = pointFin;
            this.GetComponent<Rigidbody>().AddForce(calculateSpeedVector(timeLaunch, pointFin), ForceMode.VelocityChange);
			gameM.NewRepetition ();


        }

		void OnDestroy(){
			gameM.ballsAlive--;
		}
    }
}
