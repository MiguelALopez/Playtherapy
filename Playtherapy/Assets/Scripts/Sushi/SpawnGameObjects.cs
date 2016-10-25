﻿using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{
    public class SpawnGameObjects : MonoBehaviour
    {
        // public variables
        //public float secondsBetweenSpawning = 0.1f;
        public float xMinRange = -25.0f;
        public float xMaxRange = 25.0f;
        public float yMinRange = 8.0f;
        public float yMaxRange = 25.0f;
        public float zMinRange = -25.0f;
        public float zMaxRange = 25.0f;
        public GameObject[] spawnObjects; // what prefabs to spawn

        //private float nextSpawnTime;

        float angle;
        public GameAngles calc;
        bool flagSide = true;
        private Vector3 pointFin;
        private bool side;
        GameManagerSushi gameM;


        // Use this for initialization
        void Start()
        {
            // determine when to spawn the next object
            //nextSpawnTime = Time.time+secondsBetweenSpawning;
            gameM = GameObject.Find("GameManager").GetComponent<GameManagerSushi>();
            if (gameM)
            {
                Debug.Log("Entra a mirar gms");
                angle = (180 * gameM.level) / 6;
                calc = new GameAngles(angle, true, true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // exit if there is a game manager and the game is over
            if (gameM)
            {
                if (gameM.gameIsOver || !gameM.gameIsStarted)
                    return;
            }
            /*
            // if time to spawn a new game object
            if (Time.time  >= nextSpawnTime) {
                // Spawn the game object through function below
                MakeThingToSpawn ();

                // determine the next time to spawn the object
                nextSpawnTime = Time.time+secondsBetweenSpawning;
            }*/
        }

        public void MakeThingToSpawn()
        {
            Vector3 spawnPosition;

            System.Random rnd = new System.Random();
            int sideNumber = rnd.Next(1);
            if (sideNumber == 0)
            {
                side = true;
            }
            else
            {
                side = false;
            }

            if (side)
            {
                Debug.Log("lado der");
                shootPosition("ShoulderRight", "HandRight", "left");
            }
            else
            {
                Debug.Log("lado izq");
                shootPosition("ShoulderLeft", "HandLeft", "right");
            }

            // get a random position between the specified ranges
            //      spawnPosition.x = Random.Range (xMinRange, xMaxRange);
            //spawnPosition.y = Random.Range (yMinRange, yMaxRange);
            //spawnPosition.z = Random.Range (zMinRange, zMaxRange);

            // determine which object to spawn
            int objectToSpawn = Random.Range(0, spawnObjects.Length);

            // actually spawn the game object
            GameObject spawnedObject = Instantiate(spawnObjects[objectToSpawn], pointFin, spawnObjects[objectToSpawn].transform.rotation) as GameObject;

            // make the parent the spawner so hierarchy doesn't get super messy
            spawnedObject.transform.parent = gameObject.transform;

            Debug.Log("Spawner: " + pointFin.ToString());
            spawnedObject.GetComponent<TargetMover>().StartMoving(pointFin);

        }

        public void shootPosition(string jointOneName, string jointTwoName, string side)
        {
            float angleRad = 0.0f;
            if (calc != null)
            {
                angleRad = calc.setRamdomAngle(side);
            }
            else
            {
                Debug.Log("no existe");
            }


            Vector3 pointOne = GameObject.FindGameObjectWithTag(jointOneName).transform.position;
            Debug.Log("PointOne: " + pointOne);
            Vector3 pointTwo = GameObject.FindGameObjectWithTag(jointTwoName).transform.position;
            Debug.Log("PointTwo: " + pointTwo);


            pointFin = calc.getPosition(pointOne, calc.createPointTwoShoulderAF(pointOne, pointTwo), angleRad);
            Debug.Log("PointFin: " + pointFin);


        }

    }
}


