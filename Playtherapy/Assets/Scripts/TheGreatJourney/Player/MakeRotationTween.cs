using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
public class MakeRotationTween : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TweenRotate();
		TweenMovement ();
    }
	private void TweenMovement()
	{
		TweenFactory.Tween("MovementAirplane",0f,2f,2f,TweenScaleFunctions.SineEaseInOut,(t)=>
			{
				this.transform.position= new Vector3(0,this.transform.position.y+(t.CurrentValue*0.1f),this.transform.position.z+t.CurrentValue);
			},(t)=>
			{

			});

	}

    private void TweenRotate()
    {
        float startAngle = this.transform.rotation.eulerAngles.z;
        float endAngle = startAngle + 360.0f;
        this.gameObject.Tween("RotateCircle", startAngle, endAngle, 2.0f, TweenScaleFunctions.CubicEaseInOut, (t) =>
        {
            // progress
            this.transform.rotation = Quaternion.identity;
				this.transform.Rotate(-Camera.main.transform.right, t.CurrentValue);
        }, (t) =>
        {
            // completion
        });
    }
    // Update is called once per frame
    void Update () {
		
	}
}
