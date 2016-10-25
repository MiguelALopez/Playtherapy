using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{
    public class GameAngles
    {

        private float angleDegree;
        private ArrayList arrayAngles;
        private string side;
        private bool frontPos;
        private bool latPos;

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

            int plane = sideShootChoose();

            if (plane == 1)
            {
                pointFin.z = Mathf.Cos(angle) * (pointTwo - pointOne).magnitude * 1.2f;

            }
            else
            {
                pointFin.x = Mathf.Cos(angle) * (pointTwo - pointOne).magnitude * 1.2f;

            }

            pointFin.y = Mathf.Sin(angle) * (pointTwo - pointOne).magnitude * 1.2f;

            if (plane == 2)
            {
                if (side == "left")
                {
                    pointFin.z += 1.5f;
                }
                else
                {
                    pointFin.z -= 1.5f;
                }

            }
            else
            {
                if (side == "left")
                {
                    pointFin.x += 1.5f;
                }
                else
                {
                    pointFin.x -= 1.5f;
                }

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

                return angleRad - Mathf.PI / 2;

            }
            else
            {

                int position = Mathf.RoundToInt(Random.Range(0, arrayAngles.Count));
                float angle = (float)(arrayAngles[position]);

                arrayAngles.RemoveAt(position);

                float angleRad = Mathf.Deg2Rad * angle;


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



        private int sideShootChoose()
        {
            if (frontPos && latPos)
            {
                return Mathf.RoundToInt(Random.Range(1, 3));
            }
            if (frontPos)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }



        public float setRamdomAngle(float angle, string side)
        {

            if (side == "right")
            {
                float angleRad = Mathf.Deg2Rad * Random.Range(270 - angle, 270);
                return angleRad;
            }
            else
            {

                float angleRad = Mathf.Deg2Rad * Random.Range(270, 270 + angle);
                return angleRad;

            }

        }
    }
}

