using Leap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBodyBehaviour : MonoBehaviour
{
    [SerializeField] MoleBehaviour moleBehaviour;
    [SerializeField] GameObject fx;
    [SerializeField] AudioSource soundfx;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManagerMoles.gm != null && GameManagerMoles.gm.gameMode == GameManagerMoles.GameModeMoles.Touch
            && other.tag.Equals("FingerTip") && (moleBehaviour.isUp || (moleBehaviour.isMoving && !moleBehaviour.isUp)))
        {
            Collision();
            MoleTouched(other.GetComponent<FingerTipBehaviour>().isLeft, other.GetComponent<FingerTipBehaviour>().fingerType);
        }
    }

    public void Collision()
    {
        soundfx.Play();
        GameObject go = Instantiate(fx, transform) as GameObject;
        GameManagerMoles.gm.UpdateScore(1);
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().enabled = false;
        if (GetComponent<SkinnedMeshRenderer>() != null)
            GetComponent<SkinnedMeshRenderer>().enabled = false;
        moleBehaviour.feedback.Good(moleBehaviour.gameObject.transform);
        moleBehaviour.ResetMole();
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().enabled = true;
        if (GetComponent<SkinnedMeshRenderer>() != null)
            GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    public void MoleTouched(bool isLeft, Finger.FingerType fingerType)
    {
        string debug = "";

        if (isLeft)
            debug += "left";
        else
            debug += "right";

        switch (fingerType)
        {
            case Finger.FingerType.TYPE_INDEX:
                {
                    debug += " index";
                    break;
                }
            case Finger.FingerType.TYPE_MIDDLE:
                {
                    debug += " middle";
                    break;
                }
            case Finger.FingerType.TYPE_RING:
                {
                    debug += " ring";
                    break;
                }
            case Finger.FingerType.TYPE_PINKY:
                {
                    debug += " pinky";
                    break;
                }
            case Finger.FingerType.TYPE_THUMB:
                {
                    debug += " thumb";
                    break;
                }
            default:
                {
                    debug += " error";
                    break;
                }
        }

        Debug.Log(debug);
    }
}
