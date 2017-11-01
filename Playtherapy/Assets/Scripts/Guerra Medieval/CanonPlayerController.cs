using UnityEngine;
using LeapAPI;
using System;

namespace GuerraMedieval
{
    [System.Serializable]
    public static class Boundary
    {
        public static float xMin = -6;
        public static float xMax = 6;
    }

    public class CanonPlayerController : MonoBehaviour
    {

        public float minAngleHorizontal = 10f;
        public float maxAngleHorizontal = 90f;

        public float minAngleVertical = 5f;
        public float maxAngleVertical = 60f;



        public float horizontalSpeed = 10f;                     // Velocity of the horizontal move
        public float tilt = 5f;                                 // Max rotation of the ship
        public float rotateSpeed = 5f;                          // Velocity of the rotation

        

        public Vector2 boundary = new Vector2(-6, 6);

        public bool withKeyboard = true;

        private float horizontalMove;                           // Amount of horizontal movement
        private float verticalMove;

        private bool destroyed;

        public GameObject canonStructure;
        public GameObject canon;

        public AudioSource canonRecoil;
        public float canonBallVelocityMagnitude = 10f;
        public Vector3 canonBallVelocity = new Vector3(0, 0, 0);
        private Vector3 canonBallPosition;
        private Vector3 relativeCanonBallPosition = new Vector3(0, 0f, 3f);
        float trayectoryTime = 0;


        private GameObject[] trayectoryBalls;

        // Use this for initialization
        void Start()
        {
            trayectoryBalls = GameObject.FindGameObjectsWithTag("Airballoon");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            switch (GameManagerMedieval.gmm.GetGameState())
            {
                case GameManagerMedieval.GameState.PLAYING:
                    {
                        
                        CalculateHorizontalMove();
                        CalculateVerticalMove();
                        CalculateCanonBallPosition();
                        Move();
                        CalculateBoundary();
                        CalculateVelocity();
                        CalculateShoot();
                        CalculateTrayectory();
                    }
                    break;
                case GameManagerMedieval.GameState.GAMEOVER:
                    {

                    }
                    break;
            }
        }

        /// <summary>
        /// Calculates the move of the canon
        /// </summary>
        public void Move()
        {
            transform.Rotate(0f, horizontalMove, 0f);
            canon.transform.Rotate(verticalMove, 0f, 0f);

        }

        /// <summary>
        /// Limist the rotation of the canon
        /// </summary>
        public void CalculateBoundary()
        {
            float angleY = transform.eulerAngles.y;
            if(angleY > 180)
            {
                angleY = angleY - 360;
            }
            angleY = Mathf.Clamp(angleY, -30, 30);
            if(angleY < 0)
            {
                angleY += 360;
            }
            transform.eulerAngles = new Vector3
                (
                transform.eulerAngles.x,
                angleY,
                transform.eulerAngles.z
                );


            float angleX = canon.transform.localEulerAngles.x;
            if (angleX > 180)
            {
                angleX = angleY - 360;
            }
            angleX = Mathf.Clamp(angleX, 0, 45);
            if (angleX < 0)
            {
                angleX += 360;
            }
            canon.transform.localEulerAngles = new Vector3
                (
                angleX,
                canon.transform.localEulerAngles.y,
                canon.transform.localEulerAngles.z
                );

        }

        public void CalculateHorizontalMove()
        {
            if (withKeyboard)
            {
                horizontalMove = Input.GetAxis("Horizontal");
                if(horizontalMove == 0)
                {
                    if (canonRecoil && canonRecoil.isPlaying)
                        canonRecoil.Stop();
                }
                else
                {
                    if (canonRecoil && !canonRecoil.isPlaying)
                        canonRecoil.Play();
                }
            }
            else
            {
                float angle = (float)new Movements().UlnarRadial();

                if (Math.Abs(angle) > minAngleHorizontal)
                {
                    angle = Math.Min(Math.Abs(angle), maxAngleHorizontal) * Math.Sign(angle);
                    horizontalMove = angle / maxAngleHorizontal;
                    if(canonRecoil && canonRecoil.isPlaying)
                        canonRecoil.Play();
                }
                else
                {
                    horizontalMove = 0;
                    if(canonRecoil && !canonRecoil.isPlaying)
                        canonRecoil.Stop();
                }
            }
        }

        public void CalculateVerticalMove()
        {
            if (withKeyboard)
            {
                verticalMove = Input.GetAxis("Vertical");
            }
            else
            {
                float angle = (float)new Movements().FlexoExtention();

                if (Math.Abs(angle) > minAngleVertical)
                {
                    angle = Math.Min(Math.Abs(angle), maxAngleVertical) * Math.Sign(angle);
                    verticalMove = angle / maxAngleVertical;
                }
                else
                {
                    verticalMove = 0;
                }
            }
        }

        public void CalculateShoot()
        {
            if (withKeyboard)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    BulletBehavior.bbh.Fire(canonBallVelocity, canonBallPosition);
                }
            }
            else
            {
                if (new Movements().Grab() > 0.5f)
                {
                    BulletBehavior.bbh.Fire(canonBallVelocity, canonBallPosition);
                }
            }
        }

        public void CalculateTrayectory()
        {            
            float step = CalculateTime(canonBallVelocity.y, canonBallPosition) / (trayectoryBalls.Length - 1);

            if (trayectoryTime < step)
            {
                trayectoryTime += 0.001f;
            }
            else
            {
                trayectoryTime = 0;
            }

            float t = trayectoryTime;            
            
            foreach (GameObject obj in trayectoryBalls)
            {
                float x = ParabolicMovement(t, canonBallVelocity.x, canonBallPosition.x, 0);
                float y = ParabolicMovement(t, canonBallVelocity.y, canonBallPosition.y, Physics.gravity.y);
                float z = ParabolicMovement(t, canonBallVelocity.z, canonBallPosition.z, 0);
                
                obj.transform.position = new Vector3(x, y, z);
                t += step;
            }
        }

        public float ParabolicMovement(float t, float velocity, float position, float acceleration)
        {
            return position + velocity * t + (acceleration/2) * (float)Math.Pow(t, 2);
        }

        public float CalculateTime(float velocity, Vector3 position)
        {
            float acceleration = (Physics.gravity.y / 2f);
            return (float)(-velocity - Math.Sqrt(Math.Pow(velocity, 2) - 4f * acceleration * position.y)) / (2 * acceleration);
        }

        public void CalculateVelocity()
        {
            float angleA = canon.transform.localRotation.eulerAngles.x;
            float angleB = transform.localRotation.eulerAngles.y;

            canonBallVelocity.y = canonBallVelocityMagnitude * (float)Math.Sin(angleA * (Math.PI / 180.0));
            float elevationForce = canonBallVelocityMagnitude * (float)Math.Cos(angleA * (Math.PI / 180.0));

            canonBallVelocity.x = elevationForce * (float)Math.Sin(angleB * (Math.PI / 180.0));
            canonBallVelocity.z = elevationForce * (float)Math.Cos(angleB * (Math.PI / 180.0));
        }

        public void CalculateCanonBallPosition()
        {
            float angleA = canon.transform.localRotation.eulerAngles.x;
            float angleB = transform.localRotation.eulerAngles.y;
            Vector3 tempPosition = Vector3.zero;

            float magnitude = Vector3.Magnitude(relativeCanonBallPosition);
            tempPosition.y = (magnitude * (float)Math.Sin(angleA * (Math.PI / 180.0)));
            float elevationForce = magnitude * (float)Math.Cos(angleA * (Math.PI / 180.0));

            tempPosition.x = elevationForce * (float)Math.Sin(angleB * (Math.PI / 180.0));
            tempPosition.z = elevationForce * (float)Math.Cos(angleB * (Math.PI / 180.0));

            canonBallPosition =  tempPosition + canon.transform.position;
        }
    }
}
