﻿using UnityEngine;

namespace GuerraMedieval
{
    public class WarriorDestroy : MonoBehaviour
    {

        public AudioSource soundDying;
        private Rigidbody m_rigidbody;

        // Use this for initialization
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                ResetObject();
                if (soundDying)
                    soundDying.Play();
            }
        }

        public void ResetObject()
        {
            m_rigidbody.velocity = Vector3.zero;
            m_rigidbody.angularVelocity = Vector3.zero;
            //transform.rotation = Quaternion.Euler(Vector3.zero);
            gameObject.SetActive(false);
        }
    }
}