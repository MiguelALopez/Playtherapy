using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroy : MonoBehaviour {

    private Rigidbody m_rigidbody;
    public AudioSource explosionSound;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || 
            other.gameObject.tag == "Wall" || 
            other.gameObject.tag == "Ball" ||
            other.gameObject.name == "ForceField")
        {
            explosionSound.Play();
            //GameManagerSpace.gms.UpdateScore(1);
            ResetObject();
        }
    }

    public void ResetObject()
    {
        m_rigidbody.velocity = new Vector3(0f, 0f, 0f);
        m_rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        gameObject.SetActive(false);
    }
}
