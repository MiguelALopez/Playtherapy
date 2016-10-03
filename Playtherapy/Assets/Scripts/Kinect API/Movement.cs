using UnityEngine;
using System;
using System.Collections;

namespace MovementDetectionLibrary
{

	public class Movement {


		// name of the movement
		private string name;
		// angle initial of the movement
		private double initialAngle;
		// angle final of the movement
		private double finalAngle;
		//Body to get the position of the joints 
		private FullBody myBody;

		// joint to set the point one of the vector one 
		private BodyPointPosition pointOne;

		// joint vertex of the angle, point of intersection
		private BodyPointPosition pointCenter;

		//joint to set the point two of the vector two
		private BodyPointPosition pointTwo;

		// angle of movement 
		private double deltaAngle;

		// percent of the max movement
		private double percentaje;

		// bool to indicate if the movement start
		private bool initial;


		public double getAngleJoints(BodyPointPosition pointOne, BodyPointPosition pointCenter, BodyPointPosition pointTwo)
		{


			BodyPointPosition positionOne = pointOne;
			//cout << positionOne.x << ", " << positionOne.y << ", " << positionOne.z << endl;
			BodyPointPosition positionCenter = pointCenter;
			//cout << positionCenter.x << ", " << positionCenter.y << ", " << positionCenter.z << endl;
			BodyPointPosition positionTwo = pointTwo;
			//cout << positionTwo.x << ", " << positionTwo.y << ", " << positionTwo.z << endl;

			double[] vecAB = { positionOne.x - positionCenter.x, positionOne.y - positionCenter.y, positionOne.z - positionCenter.z };
			double[] vecBC = { positionTwo.x - positionCenter.x, positionTwo.y - positionCenter.y, positionTwo.z - positionCenter.z };

			double magAB = Math.Sqrt(vecAB[0] * vecAB[0] + vecAB[1] * vecAB[1] + vecAB[2] * vecAB[2]);
			double magBC = Math.Sqrt(vecBC[0] * vecBC[0] + vecBC[1] * vecBC[1] + vecBC[2] * vecBC[2]);

			double[] vecNormAB = { vecAB[0] / magAB, vecAB[1] / magAB, vecAB[2] / magAB };
			double[] vecNormBC = { vecBC[0] / magBC, vecBC[1] / magBC, vecBC[2] / magBC };

			double producto = vecNormAB[0] * vecNormBC[0] + vecNormAB[1] * vecNormBC[1] + vecNormAB[2] * vecNormBC[2];
			double angulo = Math.Acos(producto) * 180.0f / (Math.PI);

			return angulo;
		}



	}
}
