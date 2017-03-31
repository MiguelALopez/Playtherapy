using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
public class MakeRotationTween : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TweenRotate();

    }
    private void TweenRotate()
    {
        float startAngle = this.transform.rotation.eulerAngles.z;
        float endAngle = startAngle + 720.0f;
        this.gameObject.Tween("RotateCircle", startAngle, endAngle, 2.0f, TweenScaleFunctions.CubicEaseInOut, (t) =>
        {
            // progress
            this.transform.rotation = Quaternion.identity;
            this.transform.Rotate(Camera.main.transform.forward, t.CurrentValue);
        }, (t) =>
        {
            // completion
        });
    }
    // Update is called once per frame
    void Update () {
		
	}
}
