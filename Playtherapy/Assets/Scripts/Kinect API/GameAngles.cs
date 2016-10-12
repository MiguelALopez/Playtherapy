using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{
    public class GameAngles
    {

        // Use this for initialization
        public Vector3 getPosition(Vector3 pointOne, Vector3 pointTwo)
        {

            Vector3 pointFin = pointTwo - pointOne;
            Vector3 pointOneD = pointTwo - pointOne;

            pointFin.x = Mathf.Cos(Mathf.PI * 0) * (pointTwo - pointOne).magnitude * 2;
            pointFin.y = Mathf.Sin(Mathf.PI * 0) * (pointTwo - pointOne).magnitude * 2;

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

    }
}
