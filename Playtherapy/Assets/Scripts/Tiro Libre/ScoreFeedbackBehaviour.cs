using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFeedbackBehaviour : MonoBehaviour
{
    public MeshRenderer mesh;
    public float travelTime;
    public float speed;

    private bool isMoving;
    private float elapsedTime;
    private Vector3 currentPosition;

	// Use this for initialization
	void Start ()
    {
        isMoving = false;
        elapsedTime = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isMoving)
        {
            if (elapsedTime < travelTime)
            {
                elapsedTime += Time.deltaTime;
                currentPosition.y += speed * Time.deltaTime;
                gameObject.transform.position = currentPosition;
            }
            else
            {
                Hide();
            }
        }
	}

    public void Show(Vector3 startPosition)
    {
        gameObject.transform.position = startPosition;
        currentPosition = startPosition;
        elapsedTime = 0f;
        mesh.enabled = true;
        isMoving = true;
    }

    public void Hide()
    {
        isMoving = false;
        mesh.enabled = false;
    }
}
