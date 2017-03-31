using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dodge : MonoBehaviour {

    ScoreHandler handler;
    void Start()
    {
        handler = FindObjectOfType<ScoreHandler>();
        print("dodge implemented");
    }
    void OnTriggerEnter(Collider other)
    {
        HasCollide collideWithOther;
        print("dodge collider:"+ other.tag);
        switch (other.tag)
        {
            case "Airballoon":
                collideWithOther = other.GetComponent<HasCollide>();
                if (collideWithOther.hasCollide==false)
                {
                    collideWithOther.hasCollide = true;
                    handler.sum_score(1);
                }
                
                
                break;
            case "Planes":
                collideWithOther = other.GetComponent<HasCollide>();
                if (collideWithOther.hasCollide == false)
                {
                    collideWithOther.hasCollide = true;
                    handler.sum_score(2);
                }
                break;

            default:
                break;
        }

    }
}
