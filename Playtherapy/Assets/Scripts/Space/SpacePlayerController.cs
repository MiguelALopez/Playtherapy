using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin = -6;
    public float xMax = 6;
}
/// <summary>
/// Script used for controll the movements of the ship
/// </summary>

public class SpacePlayerController : MonoBehaviour
{
    public float horizontalSpeed;                           // Velocity of the horizontal move
    public float tilt;                                      // Max rotation of the ship
    public float rotateSpeed = 5f;                          // Velocity of the rotation

    public Boundary boundary;                               // Boundaries of the ship movement


    private Rigidbody m_rigidbody;                          // Rigidbody of the Ship
    private float moveHorizontal;                           // Amount of horizontal movement


    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameManagerSpace.gms.IsPlaying())
        {
            HorizontalMove();
            CalculateRotation();
            CalculateBoundary();

            if (Input.GetButtonDown("Fire1"))
            {
                BulletBehavior.bbh.Fire();
            }
        }
    }

    /// <summary>
    /// Calculates the horizontal move of the ship
    /// </summary>
    public void HorizontalMove()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
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
}
