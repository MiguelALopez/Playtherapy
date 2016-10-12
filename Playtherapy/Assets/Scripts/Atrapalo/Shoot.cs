using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{

    public class Shoot : MonoBehaviour
    {


        int uno = 0;
        float angle;
        GameAngles calc;


        // Use this for initialization
        void Start()
        {
            calc = new GameAngles();
            angle = 90f;
        }

        // Update is called once per frame
        void Update()
        {

            if (uno == 0)
            {
                float t = 2f;
                float angleRad = calc.setRamdomAngle(angle);


                Vector3 pointOne = GameObject.FindGameObjectWithTag("ShoulderRigth").transform.position;
                Vector3 pointTwo = GameObject.FindGameObjectWithTag("WristRigth").transform.position;

                Vector3 pointFin = calc.getPosition(pointOne, calc.createPointTwoShoulderAF(pointOne, pointTwo), angleRad);

                //this.transform.position = pointFin;
                this.GetComponent<Rigidbody>().AddForce(calculateSpeedVector(t, pointFin), ForceMode.VelocityChange);



                uno = 1;
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

        void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = start;

            Quaternion moveAng = Quaternion.Euler(0, 30, 0);
            myLine.transform.localRotation = moveAng;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            lr.SetColors(color, color);
            lr.SetWidth(0.1f, 0.1f);
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);

            GameObject.Destroy(myLine, duration);
        }


    }
}
