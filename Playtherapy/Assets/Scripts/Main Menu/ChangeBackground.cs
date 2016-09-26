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

        GameObject g = GameObject.Find("Bosque");
        g.SetActive(false);
        GameObject go = GameObject.FindGameObjectWithTag("Text");
        go.GetComponentInChildren<TextMesh>().text = name;
        go.SetActive(true);
    }
}
