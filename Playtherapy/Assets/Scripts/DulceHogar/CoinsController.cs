using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CompleteProject
{
	public class CoinsController : MonoBehaviour {

		// Use this for initialization
		public GameObject start; 
		public int scoreValue = 10;
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnTriggerEnter(Collider other) 
		{
			if (other.gameObject.CompareTag ("Player"))
			{
				ScoreManager.score += scoreValue;
				start.SetActive (false);
			}
		}
	}
}