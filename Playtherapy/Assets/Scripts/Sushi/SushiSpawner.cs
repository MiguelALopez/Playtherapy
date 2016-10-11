using UnityEngine;
using System.Collections;

public class SushiSpawner : MonoBehaviour {

    int sushiCount;
    public GameObject roll;

	// Use this for initialization
	void Start () {
        sushiCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnRoll()
    {
        Debug.Log("Instancia roll " + (sushiCount + 1));
        if (!roll)
            return;
        Vector3 posParent = GameObject.Find("SushiContainer").transform.position;
        int multZ = 0;
        if (sushiCount % 6 < 3)
        {
            multZ = 0;
        } else
        {
            multZ = 1;
        }
        Instantiate(roll, new Vector3(-0.48f + (0.51f * (sushiCount % 3)) + posParent.x, 0.2345f + (((sushiCount) / 6) * 0.38f) + posParent.y, 0.27f - (0.51f * multZ) + posParent.z), roll.transform.rotation, GameObject.Find("SushiContainer").transform);
        sushiCount++;
    }
}
