using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreHandler : MonoBehaviour {


    public int score_obtain;
    public int score_max;
    public Text txt_score;
    // Use this for initialization
    void Start () {
        score_obtain = 0;
        score_max = 0;
        txt_score = GameObject.Find("txt_score").GetComponent<Text>();
    }
	public void sum_score(int pts=0)
    {
        if (pts<0)
        {
            if (score_obtain+pts<0)
            {
                score_obtain = 0;
            }
            else
            {
                score_obtain += pts;
                
            }
        }
        else
        {
            score_max += pts;
            score_obtain += pts;
        }
    }
	// Update is called once per frame
	void Update () {
        txt_score.text = "Puntaje: " + score_obtain + "/" + score_max;

    }
}
