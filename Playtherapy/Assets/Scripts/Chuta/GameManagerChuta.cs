using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerChuta : MonoBehaviour
{
    public static GameManagerChuta gm;

    public int currentScene;

    public GameObject[] targets1;
    public GameObject[] targets2;
    public GameObject[] targets3;

    private string gameState;

    public bool kickReady;

	// Use this for initialization
	void Start ()
    {
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManagerChuta>();

        //currentScene = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    public GameObject[] getCurrentTargets()
    {
        switch (currentScene)
        {
            case 1:
                return targets1;
            case 2:
                return targets2;
            case 3:
                return targets3;
            default:
                return targets1;
        }
    }
}
