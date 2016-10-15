using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{
    public class GameAngles
    {

        // Use this for initialization
        public Vector3 getPosition(Vector3 pointOne, Vector3 pointTwo, float angle)
        {

            Vector3 pointFin = pointTwo - pointOne;
            Vector3 pointOneD = pointTwo - pointOne;

            pointFin.z = Mathf.Cos(angle) * (pointTwo - pointOne).magnitude*1.5f ;
            pointFin.y = Mathf.Sin(angle) * (pointTwo - pointOne).magnitude*1.5f;

            pointFin = pointFin + pointOne;

            return pointFin;

        }

        // Return the second vector for the angle
        public Vector3 createPointTwoShoulderAF(Vector3 pointOne, Vector3 pointTwo)
        {
            
            pointTwo.y = pointOne.y - (pointTwo - pointOne).magnitude;
            pointTwo.x = pointOne.x;
            pointTwo.z = pointOne.z;

            return pointTwo;
        }

        //Return an random angle between 0 and the angle in randians, angle is a degree
        public float setRamdomAngle(float angle)
        {

            float angleRad = Mathf.Deg2Rad * Random.Range(0, angle);
            return angleRad;
        }


    }
}
