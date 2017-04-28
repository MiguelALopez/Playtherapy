﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the behavior of the bullets and control the shot.
/// </summary>
public class BulletBehavior : MonoBehaviour {

    public Vector3 bulletRight = new Vector3(0.94f, -0.05f, 0.15f);     // Relative right  position of the bullet depending of the ship
    public Vector3 bulletLeft = new Vector3(-0.94f, -0.05f, 0.15f);     // Relative left  position of the bullet depending of the ship
    public float bulletVelocity = 10.0f;                                // Velocity of the bullet

    private int currentBullet;                                          // Flag with the position of the next bullet for the shot
    private GameObject[] bullets;                                       // Array with the available bullets
    private GameObject player;                                          // Object with the player

	// Use this for initialization
	void Start () {

        // initialice the current bullet with 0
        currentBullet = 0;

        // Find all Bullets in the scene
        bullets = GameObject.FindGameObjectsWithTag("Ball");
        foreach(GameObject obj in bullets)
        {
            obj.SetActive(false); //Inactive all bullets
        }

        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GameManagerSpace.gms.IsPlaying())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Fire()
    {
        if (!bullets[currentBullet].activeSelf)
        {
            bullets[currentBullet].transform.position = player.transform.position + bulletRight;
            bullets[currentBullet].SetActive(true);
            bullets[currentBullet].GetComponent<Rigidbody>().velocity = Vector3.forward * bulletVelocity;
            currentBullet++;

            if(currentBullet >= bullets.Length)
            {
                currentBullet = 0;
            }
        }

        if (!bullets[currentBullet].activeSelf)
        {
            bullets[currentBullet].transform.position = player.transform.position + bulletLeft;
            bullets[currentBullet].SetActive(true);
            bullets[currentBullet].GetComponent<Rigidbody>().velocity = Vector3.forward * bulletVelocity;
            currentBullet++;

            if (currentBullet >= bullets.Length)
            {
                currentBullet = 0;
            }
        }
    }
}