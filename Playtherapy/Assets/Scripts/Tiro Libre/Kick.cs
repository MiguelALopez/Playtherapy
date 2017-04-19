using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementDetectionLibrary;

public class Kick : MonoBehaviour
{
    public GameObject ball;
    public float speed;
    public AudioSource hitSound;

    public RUISSkeletonController skeleton;
    public FullBody mdl;
    public Transform leftHip;
    public Transform rightHip;
    public Transform leftFoot;
    public Transform rightFoot;

    public float firstFlexionAngle;
    public float secondFlexionAngle;
    public float thirdFlexionAngle;
    public float firstExtensionAngle;
    public float secondExtensionAngle;
    public float thirdExtensionAngle;

    public bool kicking;
    public bool kicked;

    public GameObject indicator;
    public float indicatorSpeed;
    public GameObject barRed;
    public GameObject barYellow;
    public GameObject barGreen;

    private Vector3 ballInitialPosition;
    private Vector3 indicatorInitialPosition;
    private RectTransform indicatorTransform;
    private RectTransform barRedTransform;
    private RectTransform barYellowTransform;
    private RectTransform barGreenTransform;

    private float hipLeftAngle;
    private float hipRightAngle;
    private float kneeLeftAngle;
    private float kneeRightAngle;

    private float legLeftOrientation;
    private float legRightOrientation;

    private Vector3 eulerRotationTemp;
    private Vector2 tempPosition;
    private float tempFloat;
    private Vector2 leftVector;
    private Vector2 rightVector;

    private int calculatedTarget;
    private Vector3 calculatedTargetPosition;

    private bool firstThreshold;
    private bool secondThreshold;
    private bool thirdThreshold;
    private bool firstOrientation;
    private bool secondOrientation;
    private bool thirdOrientation;

    // Use this for initialization
    void Start ()
    {
        ballInitialPosition = ball.transform.position;
        indicatorInitialPosition = indicator.GetComponent<RectTransform>().position;
        indicatorTransform = indicator.GetComponent<RectTransform>();
        barRedTransform = barRed.GetComponent<RectTransform>();
        barYellowTransform = barYellow.GetComponent<RectTransform>();
        barGreenTransform = barGreen.GetComponent<RectTransform>();

        hipLeftAngle = 0f;
        hipRightAngle = 0f;
        kneeLeftAngle = 0f;
        kneeRightAngle = 0f;
        legLeftOrientation = 0f;
        legRightOrientation = 0f;

        firstThreshold = false;
        secondThreshold = false;
        thirdThreshold = false;
        firstOrientation = false;
        secondOrientation = false;
        thirdOrientation = false;

        tempPosition = new Vector2();

        leftVector = new Vector2(leftHip.position.x, Mathf.Abs(leftHip.position.z));
        rightVector = new Vector2(rightHip.position.x, Mathf.Abs(rightHip.position.z));
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (kicking && mdl.bodyMovements.bodyPointsCollection != null && GameManagerTiroLibre.gm.isPlaying && GameManagerTiroLibre.gm.targetReady)
        {
            //Debug.Log("entra a kick update");
            if (GameManagerTiroLibre.gm.leftLegActive)
            {
                //Debug.Log("entra a kick left");
                setHipLeftAngle();

                if (hipLeftAngle > thirdFlexionAngle)
                {
                    thirdThreshold = true;
                    if (!thirdOrientation)
                    {
                        setLegLeftOrientation();
                        Debug.Log(legLeftOrientation);
                        thirdOrientation = true;                        
                    }
                }
                else if (hipLeftAngle > secondFlexionAngle)
                {
                    secondThreshold = true;
                    if (!thirdOrientation && !secondOrientation)
                    {
                        setLegLeftOrientation();
                        Debug.Log(legLeftOrientation);
                        secondOrientation = true;
                    }
                }
                else if (hipLeftAngle > firstFlexionAngle)
                {
                    firstThreshold = true;
                    if (!thirdOrientation && !secondOrientation && !firstOrientation)
                    {
                        setLegLeftOrientation();
                        Debug.Log(legLeftOrientation);
                        firstOrientation = true;
                    }
                }
                else if (thirdThreshold || secondThreshold || firstThreshold)
                {
                    KickBall();
                }
            }
            else
            {
                //Debug.Log("entra a kick right");
                setHipRightAngle();

                if (hipRightAngle > thirdFlexionAngle)
                {
                    thirdThreshold = true;
                    if (!thirdOrientation)
                    {
                        setLegRightOrientation();
                        Debug.Log(legRightOrientation);
                        thirdOrientation = true;
                    }
                }
                else if (hipRightAngle > secondFlexionAngle)
                {
                    secondThreshold = true;
                    if (!thirdOrientation && !secondOrientation)
                    {
                        setLegRightOrientation();
                        Debug.Log(legRightOrientation);
                        secondOrientation = true;
                    }
                }
                else if (hipRightAngle > firstFlexionAngle)
                {
                    firstThreshold = true;
                    if (!thirdOrientation && !secondOrientation && !firstOrientation)
                    {
                        setLegRightOrientation();
                        Debug.Log(legRightOrientation);
                        firstOrientation = true;
                    }
                }
                else if (thirdThreshold || secondThreshold || firstThreshold)
                {
                    KickBall();
                }
            }       
        }
        else if (kicked)
        {
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, calculatedTargetPosition, speed * Time.deltaTime);
            //Debug.Log(GameManagerChuta.gm.getCurrentTargets()[calculatedTarget].name);
        }

        if (GameManagerTiroLibre.gm.leftLegActive)
        {
            if (hipLeftAngle > thirdFlexionAngle)
            {
                indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barRedTransform.position, indicatorSpeed * Time.deltaTime);
            }
            else if (hipLeftAngle > secondFlexionAngle)
            {
                indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barYellowTransform.position, indicatorSpeed * Time.deltaTime);
            }
            else if (hipLeftAngle > firstFlexionAngle)
            {
                indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barGreenTransform.position, indicatorSpeed * Time.deltaTime);
            }
            else
            {
                indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, indicatorInitialPosition, indicatorSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (hipRightAngle > thirdFlexionAngle)
            {
                indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barRedTransform.position, indicatorSpeed * Time.deltaTime);
            }
            else if (hipRightAngle > secondFlexionAngle)
            {
                indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barYellowTransform.position, indicatorSpeed * Time.deltaTime);
            }
            else if (hipRightAngle > firstFlexionAngle)
            {
                indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, barGreenTransform.position, indicatorSpeed * Time.deltaTime);
            }
            else
            {
                indicatorTransform.position = Vector3.MoveTowards(indicatorTransform.position, indicatorInitialPosition, indicatorSpeed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("entra a trigger");
        /*
        if (other.gameObject.tag == "Foot" && GameManagerTiroLibre.gm.targetReady)
        {
            Debug.Log("entra a kickball");
            //KickBall();
        }*/
        if (other.gameObject.tag == "Target")
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

        int pos1 = 0;
        int pos2 = 1;

        if (GameManagerTiroLibre.gm.leftLegActive)
        {
            if (legLeftOrientation > (leftHip.position.x + 0.02f))
                pos1 = 2;
            else if (legLeftOrientation < (leftHip.position.x - 0.11f))
                pos1 = 0;
            else
                pos1 = 1;
        }
        else
        {
            if (legRightOrientation > (rightHip.position.x + 0.11f))
                pos1 = 2;
            else if (legRightOrientation < (rightHip.position.x - 0.02f))
                pos1 = 0;
            else
                pos1 = 1;
        }

        if (thirdThreshold)
            pos2 = 6;
        else if (secondThreshold)
            pos2 = 3;
        else
            pos2 = 0;

        calculatedTarget = pos1 + pos2;
        calculatedTargetPosition = GameManagerTiroLibre.gm.getCurrentTargetPosition(calculatedTarget);
        Debug.Log(calculatedTarget);
                
        kicked = true;
        hitSound.Play();
        //GameManagerTiroLibre.gm.targetReady = false;
    }

    public void TargetCollision()
    {
        kicked = false;
        ball.transform.position = ballInitialPosition;
        hipLeftAngle = 0f;
        hipRightAngle = 0f;
        firstThreshold = false;
        secondThreshold = false;
        thirdThreshold = false;
        firstOrientation = false;
        secondOrientation = false;
        thirdOrientation = false;
        kicking = true;
    }

    public void WallCollision()
    {
        kicked = false;
        ball.transform.position = ballInitialPosition;
        hipLeftAngle = 0f;
        hipRightAngle = 0f;
        firstThreshold = false;
        secondThreshold = false;
        thirdThreshold = false;
        firstOrientation = false;
        secondOrientation = false;
        thirdOrientation = false;
        kicking = true;
    }

    public void setHipLeftAngle()
    {
        /*
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
        */
        
        hipLeftAngle = (float)mdl.bodyMovements.hipLeftExtMovement();
        //setLegLeftOrientation();
    }

    public void setHipRightAngle()
    {
        /*
        tempFloat = (float)mdl.bodyMovements.hipRigthExtMovement();

        if (tempFloat < 60)
        {
            if (lastHipRightAngle < tempFloat)
                hipRightAngle = tempFloat;

            lastHipRightAngle = tempFloat;
        }
        */

        hipRightAngle = (float)mdl.bodyMovements.hipRigthExtMovement();
        //setLegRightOrientation();
    }

    public void setKneeLeftAngle()
    {
        tempFloat = (float)mdl.bodyMovements.kneeLeftMovement();        
    }

    public void setKneeRightAngle()
    {
        tempFloat = (float)mdl.bodyMovements.kneeRigthMovement();
    }

    public void setLegLeftOrientation()
    {
        //eulerRotationTemp = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].leftKnee.rotation.eulerAngles;

        //kneeLeftOritation = Quaternion.Angle(Quaternion.Euler(0, eulerRotationTemp.y, 0), Quaternion.identity);
        //kneeLeftOritation = eulerRotationTemp.y;
        /*
        tempPosition.x = leftFoot.position.x;
        tempPosition.y = Mathf.Abs(leftFoot.position.z);

        legLeftOrientation = Vector2.Angle(leftVector, tempPosition);

        if (tempPosition.x < leftVector.x)
            legLeftOrientation *= -1;
            */
        legLeftOrientation = leftFoot.position.x;
    }

    public void setLegRightOrientation()
    {
        //eulerRotationTemp = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].rightKnee.rotation.eulerAngles;

        //kneeRightOritation = Quaternion.Angle(Quaternion.Euler(0, eulerRotationTemp.y, 0), Quaternion.identity);
        //kneeRightOritation = eulerRotationTemp.y;
        /*
        tempPosition.x = rightFoot.position.x;
        tempPosition.y = Mathf.Abs(rightFoot.position.z);

        legRightOritation = Vector2.Angle(rightVector, tempPosition);

        if (tempPosition.x < rightVector.x)
            legRightOritation *= -1;
            */
        legRightOrientation = rightFoot.position.x;
    }
}
