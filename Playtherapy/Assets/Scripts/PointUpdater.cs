using UnityEngine;
using System.Collections;
using System;

namespace MovementDetectionLibrary
{
    public class PointUpdater : MonoBehaviour
    {
        public GameObject FullBodyObject;
        private FullBody _FullBodyObject;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (FullBodyObject == null)
            {
                return;
            }

            _FullBodyObject = FullBodyObject.GetComponent<FullBody>();
            if (_FullBodyObject == null)
            {
                return;
            }

            BodyPointPosition pos = _FullBodyObject.ReturnPointPosition((BodyParts)Enum.Parse(typeof(BodyParts), gameObject.transform.name.ToString()));
            transform.position = new Vector3(pos.x, pos.y, pos.z);
            
            //Debug.Log("Elbow: x:"+pos.x + ", " + pos.y + "" + pos.z);
        }
    }
}

