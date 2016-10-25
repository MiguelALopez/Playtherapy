using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{
    public class GameAngles
    {
        private float angleDegree;
        private ArrayList arrayAngles;
        private string side;
        public GameAngles(float angle, bool front, bool lat)
        {

            this.angleDegree = angle;
            arrayAngles = new ArrayList();

        }

        // Use this for initialization
        public Vector3 getPosition(Vector3 pointOne, Vector3 pointTwo, float angle)
        {

            Vector3 pointFin = pointTwo - pointOne;
            Vector3 pointOneD = pointTwo - pointOne;

            //Debug.Log ("magnitude" + (pointTwo - pointOne).magnitude);




            pointFin.z = Mathf.Cos(angle) * (pointTwo - pointOne).magnitude * 1.2f;
            pointFin.y = Mathf.Sin(angle) * (pointTwo - pointOne).magnitude * 1.2f;

            if (side == "left")
            {
                pointFin.z += 1.5f;
            }
            else
            {
                pointFin.z -= 1.5f;
            }


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
        public float setRamdomAngle(string side)
        {
            Debug.Log("entra a tiki");
            this.side = side;
            if (arrayAngles.Count == 0)
            {
                setArrayAngles(this.angleDegree);
            }

            if (side == "left")
            {
                int position = Mathf.RoundToInt(Random.Range(0, arrayAngles.Count));
                float angle = (float)(arrayAngles[position]);
                arrayAngles.RemoveAt(position);

                float angleRad = Mathf.Deg2Rad * angle;

                Debug.Log("entra a tiki1");
                return angleRad - Mathf.PI / 2;

            }
            else
            {

                int position = Mathf.RoundToInt(Random.Range(0, arrayAngles.Count));
                float angle = (float)(arrayAngles[position]);

                arrayAngles.RemoveAt(position);

                float angleRad = Mathf.Deg2Rad * angle;

                Debug.Log("entra a tiki2");
                return 3 * Mathf.PI / 2 - angleRad;

            }

        }

        //Function to return a array with angles 
        private void setArrayAngles(float angle)
        {

            for (int i = 0; i < 6; i++)
            {
                this.arrayAngles.Add(angle - 5 * i);
            }
        }


    }
}