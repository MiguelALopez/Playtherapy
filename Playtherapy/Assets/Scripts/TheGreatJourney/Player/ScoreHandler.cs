using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// for your own scripts make sure to add the following line:
using DigitalRuby.Tween;
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
				TweenColorIncorrecto ();
            }
        }
        else
        {
			TweenColorCorrecto ();
            score_max += pts;
            score_obtain += pts;
        }
    }
	// Update is called once per frame
	void Update () {
        txt_score.text = "Puntaje: " + score_obtain + "/" + score_max;

    }


	private void TweenColorIncorrecto()
	{
		Color endColor = Color.red;
		txt_score.gameObject.Tween("ColorCircle", txt_score.color, endColor, 0.25f, TweenScaleFunctions.QuadraticEaseOut, (t) =>
			{
				// progress
				txt_score.color = t.CurrentValue;
			}, (t) =>
			{
				// completion
				endColor= Color.white;
				txt_score.gameObject.Tween("ColorCircle", txt_score.color, endColor, 0.25f, TweenScaleFunctions.QuadraticEaseOut, (t2) =>
					{
						// progress
						txt_score.color = t2.CurrentValue;
					}, (t2) =>
					{
						// completion
					});
			});
	}
	private void TweenColorCorrecto()
	{
		Color endColor = Color.green;
		txt_score.gameObject.Tween("ColorCircle", txt_score.color, endColor, 0.25f, TweenScaleFunctions.QuadraticEaseOut, (t) =>
			{
				// progress
				txt_score.color = t.CurrentValue;
			}, (t) =>
			{
				// completion
				endColor= Color.white;
				txt_score.gameObject.Tween("ColorCircle", txt_score.color, endColor, 0.25f, TweenScaleFunctions.QuadraticEaseOut, (t2) =>
					{
						// progress
						txt_score.color = t2.CurrentValue;
					}, (t2) =>
					{
						// completion
					});
			});
	}
}
