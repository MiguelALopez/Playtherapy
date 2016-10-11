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
        if (sushiCount % 8 < 4)
        {
            multZ = 0;
        } else
        {
            multZ = 1;
        }
        Instantiate(roll, new Vector3(-0.50f + (0.40f * (sushiCount % 4)) + posParent.x, 0.15f + (((sushiCount) / 8) * 0.29f) + posParent.y, 0.14f - (0.40f * multZ) + posParent.z), roll.transform.rotation, GameObject.Find("SushiContainer").transform);
        sushiCount++;
    }
}
