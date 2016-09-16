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
            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);
        }

        public double headFrontalExtAngle()
        {

            //  frontal extension 

            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.Neck].getCurrentPosition(); 
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.Head].getCurrentPosition();
            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);
        }


        public double headFrontalFleAngle()
        {

            //  frontal flexion 

            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.Neck].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.Head].getCurrentPosition();
            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);
        }

    
        public double shoulderAbdRigthMovements()
        {

            
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double shoulderFlexRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double shoulderExtRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);

        }



        public double shoulderRotIntRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.z = pointOne.z - 0.2f;

            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double shoulderRotExtRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.z = pointOne.z - 0.2f;

            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double elbowFleExtMovement()
        {

            // Flexion

            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();

            Movement headMovementFlex = new Movement();

            return headMovementFlex.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        void wristMovements()
        {

        }
        void hipMovements()
        {

        }
        void kneeMovements()
        {

        }
        void spineMovements()
        {

        }





    }
}
