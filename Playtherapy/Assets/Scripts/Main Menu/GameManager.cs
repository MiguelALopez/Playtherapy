using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject[] minigameBackgrounds;
    public GameObject currentBackground;

    // Use this for initialization
    void Start()
    {
        if (gm == null)
        {
            gm = this.gameObject.GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject findBackground(string name)
    {
        GameObject r = null;

        foreach (GameObject go in minigameBackgrounds)
        {
            if (go.name == name)
            {
                r = go;
                break;
            }
        }

        return r;
    }
}
