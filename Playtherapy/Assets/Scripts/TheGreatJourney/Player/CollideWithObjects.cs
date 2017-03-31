using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollideWithObjects : MonoBehaviour {

    ScoreHandler handler;
    public CameraShake.Properties testProperties;
    public GameObject emmiterCrash;
    public GameObject emmiterCoin;
    AudioSource fireSound;
    AudioSource coinSound;
    void Start ()
    {
        handler = FindObjectOfType<ScoreHandler>();
        emmiterCrash = GameObject.Find("EmmiterCrash");
        
        emmiterCoin = GameObject.Find("EmmiterCoin");
        fireSound = GameObject.Find("FireSound").GetComponent<AudioSource>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }
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
            emmiterCrash.transform.position = this.transform.position;
            emmiterCrash.GetComponent<ParticleSystem>().Play(true) ;
            fireSound.Play();
            FindObjectOfType<CameraShake>().StartShake(testProperties);
            handler.sum_score(-1);
        }
        switch (other.tag)
        {
            case "Clouds":
                print("hi clouds");
               
                break;
            case "Planes":
                emmiterCrash.transform.position = other.transform.position;
                emmiterCrash.GetComponent<ParticleSystem>().Play(true);
                fireSound.Play();
                FindObjectOfType<CameraShake>().StartShake(testProperties);
                Destroy(other.gameObject);
                handler.sum_score(-2);
                break;
            case "Airballoon":
                emmiterCrash.transform.position = other.transform.position;
                emmiterCrash.GetComponent<ParticleSystem>().Play(true);
                fireSound.Play();
                FindObjectOfType<CameraShake>().StartShake(testProperties);
                Destroy(other.gameObject);

                handler.sum_score(-1);
                break;                
            case "Coins":
                
                coinSound.Play();
                emmiterCoin.transform.position = other.transform.position;
                emmiterCoin.GetComponent<ParticleSystem>().Play(true);
                Destroy(other.gameObject);
                handler.sum_score(5);
                break;

            default:
                break;
        }

    }
}
