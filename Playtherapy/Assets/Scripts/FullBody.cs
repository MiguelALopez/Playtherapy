using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MovementDetectionLibrary
{
    public class FullBody : MonoBehaviour
    {
        Dictionary<BodyParts, BodyPoint> bodyPointsCollection;
        
        //KinectTwoAdapter sensorTwoKinect;
        public GameObject KinectTAdapter;
        private KinectTwoAdapter _KinectTAdapter;
		private Movement movimiento;	

        public Text infoText;

        // Use this for initialization
        void Start()
        {
			movimiento = new Movement ();
            //sensorTwoKinect = new KinectTwoAdapter();
            bodyPointsCollection = new Dictionary<BodyParts, BodyPoint>();
            for (int i = 0; i < (int)BodyParts.ThumbRight; i++)
            {
                bodyPointsCollection.Add(((BodyParts) i), new BodyPoint((BodyParts) i));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (KinectTAdapter == null)
            {
                return;
            }

            _KinectTAdapter = KinectTAdapter.GetComponent<KinectTwoAdapter>();
            if (_KinectTAdapter == null)
            {
                return;
            }

            for (int i = 0; i < (int)BodyParts.ThumbRight; i++)
            {
                UpdateBodyPoint((BodyParts)i);
				angleMovement ();
            }
        }

        void UpdateBodyPoint(BodyParts joint)
        {
            BodyPointPosition position = _KinectTAdapter.ReturnPosition(joint);
           if(joint == BodyParts.FootRight)
            {
                infoText.text =("Posicion New "+position.name+": " + position.x + " " + position.y + " " + position.z + " ");

            }

            bodyPointsCollection[joint].setPosition(position);
        }

        public BodyPointPosition ReturnPointPosition(BodyParts joint)
        {
            return bodyPointsCollection[joint].getCurrentPosition();
        }

		public void angleMovement(){


			BodyPointPosition pointOne = bodyPointsCollection[BodyParts.ElbowLeft].getCurrentPosition();
			pointOne.y= (float)(pointOne.y - 0.2); 
			infoText.text = ("Angle " + movimiento.getAngleJoints (pointOne, bodyPointsCollection[BodyParts.ElbowLeft].getCurrentPosition(), bodyPointsCollection[BodyParts.ShoulderLeft].getCurrentPosition()));
		}
    }
}
