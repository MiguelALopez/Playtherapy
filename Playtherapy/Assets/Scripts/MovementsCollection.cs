using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MovementDetectionLibrary
{

	public class MovementsCollection {


		Dictionary<BodyParts, BodyPoint> bodyPointsCollection;

		public void setBodyPointsCollection(Dictionary<BodyParts, BodyPoint> bodyPoints){

			bodyPointsCollection = bodyPoints;

		}

        public double headLateralAngle()
        {

            // lateral flexion
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.Neck].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.Head].getCurrentPosition();
            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);
        }

        public double headFrontalExtAngle()
        {

            //  frontal extension 

            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.Neck].getCurrentPosition(); 
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.Head].getCurrentPosition();
            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);
        }


        public double headFrontalFleAngle()
        {

            //  frontal flexion 

            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.Neck].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.Head].getCurrentPosition();
            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);
        }

    
        public double shoulderAbdRigthMovements()
        {

            
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double shoulderFlexRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double shoulderExtRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }



        public double shoulderRotIntRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.z = pointOne.z - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double shoulderRotExtRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.z = pointOne.z - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double elbowFleExtMovement()
        {

            // Flexion

            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double hipRigthAbMovement()
        {
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double hipRigthAdMovement()
        {
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double hipRigthFlexMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y + 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double hipRigthExtMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y + 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double hipRigthRotIntMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double hipRigthRotExtMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }







        public double kneeRigthMovement()
        {
            //Flexion
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleRight].getCurrentPosition() ;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);
        }


        public double spineLatovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.SpineBase].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();

            BodyPointPosition pointOne = pointCenter;
            pointOne.x = pointOne.x + 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double spineIncovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.SpineBase].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();

            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }







    }
}
