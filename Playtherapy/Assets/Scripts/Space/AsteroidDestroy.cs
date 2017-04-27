using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroy : MonoBehaviour {

    Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || 
            other.gameObject.tag == "Wall" || 
            other.gameObject.tag == "Ball")
        {
            GameManagerSpace.gms.UpdateScore(1);
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
