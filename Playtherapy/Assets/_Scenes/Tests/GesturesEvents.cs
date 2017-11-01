using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GesturesEvents : MonoBehaviour {




	public bool can_gesture = true;
	// Use this for initialization
	void Start () {
		

	}
	void OnEnable()
	{
		GestureSourceManager manager =FindObjectOfType<GestureSourceManager> ();
		if (manager!=null) {
			manager.OnGesture += onGestureActivated;
		}
	}
	void OnDisable()
	{
		GestureSourceManager manager =FindObjectOfType<GestureSourceManager> ();
		if (manager!=null) {
			manager.OnGesture -= onGestureActivated;
		}


	}
	void onGestureActivated (GestureSourceManager.EventArgs gesture)
	{
		if (gesture.name=="None") {
			can_gesture = true;
		}

		if (can_gesture==true) {

			if (gesture.confidence>0.2) {
				print ("name:"+gesture.name+", confidence:"+gesture.confidence);
				can_gesture = false;

			}


		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
