using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public GameObject canvasOld;
    public GameObject canvasNew;

    public void ChangeSceneBackground()
    {
        string name = GetComponentInChildren<Text>().text;
        Debug.Log(name);

        if (canvasOld != null)
        {
            canvasOld.SetActive(false);
        }

        if (canvasNew != null)
        {
            canvasNew.SetActive(true);
        }

        GameManager.gm.currentBackground.SetActive(false);

        GameObject go = GameManager.gm.findBackground(name);
        GameManager.gm.currentBackground = go;
        GameManager.gm.currentBackground.SetActive(true);
    }
}
