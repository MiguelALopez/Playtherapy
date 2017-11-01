using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolesManager : MonoBehaviour
{
    public GameObject[] moles;
    private int currentMole;

    public void NextMole()
    {
        if (GameManagerMoles.gm != null && GameManagerMoles.gm.isPlaying)
        {
            StartCoroutine(NextMoleRoutine());
        }        
    }

    private IEnumerator NextMoleRoutine()
    {
        currentMole = Random.Range(0, moles.Length);

        if (!moles[currentMole].GetComponentInChildren<MoleBehaviour>().isUp)
            moles[currentMole].GetComponentInChildren<MoleBehaviour>().Up();

        yield return new WaitForSeconds(GameManagerMoles.gm.timeBetweenMoles);

        NextMole();
    }
}
