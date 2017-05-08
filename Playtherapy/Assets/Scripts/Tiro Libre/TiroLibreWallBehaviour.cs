using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroLibreWallBehaviour : MonoBehaviour
{
    public AudioSource hitSound;
    public ScoreFeedbackBehaviour scoreFeedback;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            hitSound.Play();
            ShowFeedback(other.transform.position);
            GameManagerTiroLibre.gm.BallHit(0);

            if (GameManagerTiroLibre.gm.changeMovement)
            {
                GameManagerTiroLibre.gm.EnableTarget(GameManagerTiroLibre.gm.currentTarget, false);
                GameManagerTiroLibre.gm.NextMovement();
            }
        }
    }

    public void ShowFeedback(Vector3 startPosition)
    {
        scoreFeedback.Show(startPosition);
    }
}
