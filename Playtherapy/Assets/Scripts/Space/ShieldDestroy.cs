using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDestroy : MonoBehaviour {

    public GameObject bonusParticle;
    private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(bonusParticle != null)
            {
                Instantiate(bonusParticle, transform.position, Quaternion.identity);
            }
            ResetObject();
        }else if(other.gameObject.tag == "Wall")
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

}
