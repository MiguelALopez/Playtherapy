using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

    public float lifeTime = 5f;

    Rigidbody m_rigidbody;
    private float savedTime;
    

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Time.time - savedTime >= lifeTime)
        {
            ResetObject();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
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

    public void SetInitialTime(float time)
    {
        savedTime = time;
    }

    public void OnEnable()
    {
        savedTime = Time.time;
    }
}
