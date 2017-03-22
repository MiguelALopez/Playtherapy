using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChutaTargetBehaviour : MonoBehaviour
{
    public MeshRenderer mesh;
    public Collider coll;
    public GameObject particle;

	// Use this for initialization
	void Start ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            EnableTarget(false);
            ShowHitParticles();
            StartCoroutine(DelayedShow());
        }
    }

    public void ShowHitParticles()
    {
        //particle.SetActive(true);
        particle.transform.position = gameObject.transform.position;
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void EnableTarget(bool enabled)
    {
        mesh.enabled = enabled;
        coll.enabled = enabled;
    }

    private IEnumerator DelayedShow()
    {
        yield return new WaitForSeconds(2f);
        EnableTarget(true);
    }
}
