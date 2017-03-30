using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollideWithObjects : MonoBehaviour {


    public CameraShake.Properties testProperties;
    public GameObject emmiter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<CameraShake>().StartShake(testProperties);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.name == "Terrain Chunk")
        {
            emmiter.GetComponent<ParticleSystem>().Play(true) ;
            print("Hi Terrain");
            FindObjectOfType<CameraShake>().StartShake(testProperties);
        }
        switch (other.tag)
        {
            case "Clouds":
                print("hi clouds");
               
                break;
            case "Planes":
                emmiter.GetComponent<ParticleSystem>().Play(true);
                print("hi planes");
                FindObjectOfType<CameraShake>().StartShake(testProperties);
                Destroy(other.gameObject);
                break;
            case "Coins":
                print("hi gems");
                Destroy(other.gameObject);
                break;

            default:
                break;
        }

    }
}
