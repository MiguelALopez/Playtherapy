using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaylistManager : MonoBehaviour
{
    public Toggle toggle;
    public GameObject startButton;

    private GameObject[] checks;

    private struct ListItem
    {
        public string name;
        public int times;

        public ListItem(string name, int times)
        {
            this.name = name;
            this.times = times;
        }
    }

    private ArrayList listItems;

    public void Start()
    {
        listItems = new ArrayList();
    }

    public void LoadChecks()
    {
        checks = GameObject.FindGameObjectsWithTag("Playlist Check");

        foreach (GameObject go in checks)
        {
            go.SetActive(toggle.isOn);
        }
    }

	public void Toggle()
    {
        foreach (GameObject go in checks)
        {
            go.SetActive(toggle.isOn);
            Debug.Log("entra al for");
        }

        startButton.SetActive(toggle.isOn);
    }

    public void SelectMinigames()
    {
        if (checks != null)
        {
            listItems.Clear();

            foreach (GameObject go in checks)
            {
                if (go.GetComponentInChildren<Toggle>().isOn)
                {
                    listItems.Add(new ListItem(go.GetComponentInParent<Text>().text, Int32.Parse(go.GetComponentInChildren<InputField>().text)));
                }

                go.SetActive(toggle.isOn);
            }

            for (int i = 0; i < listItems.Count; i++)
            {
                Debug.Log(((ListItem)listItems[i]).name + " -- " + ((ListItem)listItems[i]).times);
            }
        }
    }
}
