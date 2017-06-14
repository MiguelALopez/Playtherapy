using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DroidBehavior : MonoBehaviour {

    public float horizontalSpeed = 10f;                     // Velocity of the horizontal move
    public float tilt = 5f;                                 // Max rotation of the droid
    public float rotateSpeed = 5f;                          // Velocity of the rotation
    public float timeBetweenMove = 6f;                      //
    public float timeBetweenFire = 2f;                      //
    private float savedTime;
    private float movementTime;
    private float randomMove;

    public Boundary boundary;                               // Boundaries of the ship movement

    private Rigidbody m_rigidbody;                          // Rigidbody of the Ship
    //private float moveHorizontal;                           // Amount of horizontal movement



    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        ResetMovement();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GameManagerSpace.gms.IsPlaying())
        {
            if(Time.time - savedTime >= timeBetweenMove)
            {
                HorizontalMove(randomMove);
                if(Time.time - savedTime >= timeBetweenMove + movementTime)
                {
                    ResetMovement();
                    HorizontalMove(0);
                }
            }
            CalculateRotation();
            CalculateBoundary();
        }
    }

    /// <summary>
    /// Calculates the horizontal move of the ship
    /// </summary>
    public void HorizontalMove(float moveHorizontal)
    {
        //moveHorizontal = Input.GetAxis("Horizontal");
        m_rigidbody.velocity = new Vector3(moveHorizontal * horizontalSpeed, 0.0f, m_rigidbody.velocity.z);
    }

    /// <summary>
    /// Calculates the effect of the ship rotation
    /// </summary>
    public void CalculateRotation()
    {
        transform.rotation = Quaternion.Slerp(m_rigidbody.rotation,
            Quaternion.Euler(m_rigidbody.rotation.x, m_rigidbody.rotation.y, m_rigidbody.velocity.x * -tilt),
            rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Limist the border of the ship movement
    /// </summary>
    public void CalculateBoundary()
    {
        m_rigidbody.position = new Vector3
            (
            Mathf.Clamp(m_rigidbody.position.x, boundary.xMin, boundary.xMax),
            m_rigidbody.position.y,
            m_rigidbody.position.z
            );
    }

    private void OnEnable()
    {
        savedTime = Time.time;
    }

    public void ResetMovement()
    {
        savedTime = Time.time;
        movementTime = Random.Range(1, 2);
        randomMove = Random.Range(-1, 2);
        Debug.Log("Reset move " + randomMove);
    }
}
