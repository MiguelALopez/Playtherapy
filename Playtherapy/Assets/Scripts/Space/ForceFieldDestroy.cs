using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldDestroy : MonoBehaviour {

    public float lifeTime = 10f;                // The lifetime of the force field
    private float savedTime;                    // Temporal variable for save the init of the life
    private Animator animation;

	// Use this for initialization
	void Start () {
        animation = GetComponent<Animator>();
        savedTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.time - savedTime >= lifeTime)
        {
            //gameObject.SetActive(false);
            StartCoroutine(Deactivate());
        }
	}

    // Used for set the beginnig of life
    private void OnEnable()
    {
        savedTime = Time.time;
        animation.Play("ForceField", 0);
    }

    private IEnumerator Deactivate()
    {
        animation.Play("ForceFieldDeactivate", 0);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
