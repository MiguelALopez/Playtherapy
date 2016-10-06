using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    

    int uno = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (uno == 0)
        {
            float t = 3f;            

            this.GetComponent<Rigidbody>().AddForce(calcularVectorVelocidad(t), ForceMode.VelocityChange);
            uno = 1;
        }
	
	}

    Vector3 calcularVectorVelocidad(float t)
    {
        Vector3 velIni = new Vector3();

        float Vx = 0;
        float Vy = 0;
        float Vz = 0;
        float X0 = this.transform.position.x;
        float Z0 = this.transform.position.z;
        float X1 = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        float Z1 = GameObject.FindGameObjectWithTag("Player").transform.position.z;

        Vx = (X1 - X0) / t;
        Vz = (Z1 - Z0) / t;

        Vy = (9.8f * t) / 2;


        velIni.x = Vx;
        velIni.y = Vy;
        velIni.z = Vz;

        return velIni;
    }
}
