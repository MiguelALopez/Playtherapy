using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JointDataViewer : MonoBehaviour
{
    public Text text;

    public RUISSkeletonController skeleton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            //text.text = "" + Quaternion.Angle(Quaternion.Euler(0, -45, 0), Quaternion.identity);
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].leftAnkle.rotation.eulerAngles.ToString();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].rightAnkle.rotation.eulerAngles.ToString();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].leftFoot.rotation.eulerAngles.ToString();
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].rightFoot.rotation.eulerAngles.ToString();
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].leftKnee.rotation.eulerAngles.ToString();
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].rightKnee.rotation.eulerAngles.ToString();
        }
    }
}
