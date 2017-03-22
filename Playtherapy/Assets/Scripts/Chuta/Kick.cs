using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementDetectionLibrary;

public class Kick : MonoBehaviour
{
    public GameObject ball;
    public float speed;

    public RUISSkeletonController skeleton;
    public FullBody mdl;

    public bool kicking;
    public bool kicked;

    private Vector3 ballInitialPosition;

    private float hipLeftAngle;
    private float hipRightAngle;
    private float kneeLeftAngle;
    private float kneeRightAngle;

    private float lastHipLeftAngle;
    private float lastHipRightAngle;
    private float lastKneeLeftAngle;
    private float lastKneeRightAngle;

    private float kneeLeftOritation;
    private float kneeRightOritation;

    private Vector3 eulerRotationTemp;
    private float tempFloat;

    private int calculatedTarget;

    // Use this for initialization
    void Start ()
    {
        ballInitialPosition = ball.transform.position;

        hipLeftAngle = 0f;
        hipRightAngle = 0f;
        kneeLeftAngle = 0f;
        kneeRightAngle = 0f;
        lastHipLeftAngle = 0f;
        lastHipRightAngle = 0f;
        lastKneeLeftAngle = 0f;
        lastKneeRightAngle = 0f;
        kneeLeftOritation = 0f;
        kneeRightOritation = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (kicking && mdl.bodyMovements.bodyPointsCollection != null)
        {
            setHipLeftAngle();
            setKneeLeftAngle();
            //setKneeLeftOrientation();
        }
        else if (kicked)
        {
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, 
                GameManagerChuta.gm.getCurrentTargets()[calculatedTarget].transform.position, speed * Time.deltaTime);

            //Debug.Log(GameManagerChuta.gm.getCurrentTargets()[calculatedTarget].name);
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("entra a trigger");

        if (other.gameObject.tag == "Foot")
        {
            Debug.Log("entra a kickball");
            KickBall();
        }
        else if (other.gameObject.tag == "Target")
        {
            Debug.Log("entra a target");
            TargetCollision();
        }
        else if (other.gameObject.tag == "Wall")
        {
            Debug.Log("entra a wall");
            WallCollision();
        }
    }

    public void KickBall()
    {
        kicking = false;        

        setKneeLeftOrientation();

        int pos1 = 0;
        int pos2 = 1;

        if (kneeLeftOritation > 7 && kneeLeftOritation < 180)
            pos1 = 2;
        else if (kneeLeftOritation < 353 && kneeLeftOritation > 180)
            pos1 = 0;
        else
            pos1 = 1;

        if (hipLeftAngle > 25)
            pos2 = 6;
        else if (hipLeftAngle > 16)
            pos2 = 3;
        else
            pos2 = 0;

        calculatedTarget = pos1 + pos2;
        Debug.Log(calculatedTarget);
        
        kicked = true;        
    }

    public void TargetCollision()
    {
        kicked = false;
        ball.transform.position = ballInitialPosition;
        lastHipLeftAngle = 0f;
        lastHipRightAngle = 0f;
        lastKneeLeftAngle = 0f;
        lastKneeRightAngle = 0f;
        kicking = true;
    }

    public void WallCollision()
    {
        kicked = false;
        ball.transform.position = ballInitialPosition;
        lastHipLeftAngle = 0f;
        lastHipRightAngle = 0f;
        lastKneeLeftAngle = 0f;
        lastKneeRightAngle = 0f;
        kicking = true;
    }

    public void setHipLeftAngle()
    {
        tempFloat = (float)mdl.bodyMovements.hipLeftExtMovement();

        if (tempFloat < 60)
        {
            if (lastHipLeftAngle < tempFloat)
                hipLeftAngle = tempFloat;

            lastHipLeftAngle = tempFloat;
        }
        else
        {
            hipLeftAngle = 0f;
        }        
    }

    public void setHipRightAngle()
    {
        tempFloat = (float)mdl.bodyMovements.hipRigthExtMovement();

        if (tempFloat < 60)
        {
            if (lastHipRightAngle < tempFloat)
                hipRightAngle = tempFloat;

            lastHipRightAngle = tempFloat;
        }
    }

    public void setKneeLeftAngle()
    {
        tempFloat = (float)mdl.bodyMovements.kneeLeftMovement();

        if (tempFloat < 60)
        {
            if (lastKneeLeftAngle < tempFloat)
                kneeLeftAngle = tempFloat;

            lastKneeLeftAngle = tempFloat;
        }
    }

    public void setKneeRightAngle()
    {
        tempFloat = (float)mdl.bodyMovements.kneeRigthMovement();

        if (tempFloat < 60)
        {
            if (lastKneeRightAngle < tempFloat)
                kneeRightAngle = tempFloat;

            lastKneeRightAngle = tempFloat;
        }
    }

    public void setKneeLeftOrientation()
    {
        eulerRotationTemp = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].leftKnee.rotation.eulerAngles;

        //kneeLeftOritation = Quaternion.Angle(Quaternion.Euler(0, eulerRotationTemp.y, 0), Quaternion.identity);
        kneeLeftOritation = eulerRotationTemp.y;
    }

    public void setKneeRightOrientation()
    {
        eulerRotationTemp = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].rightKnee.rotation.eulerAngles;

        //kneeRightOritation = Quaternion.Angle(Quaternion.Euler(0, eulerRotationTemp.y, 0), Quaternion.identity);
        kneeRightOritation = eulerRotationTemp.y;
    }
}
