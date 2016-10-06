using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartTherapySession : MonoBehaviour
{
    public GameObject content; // grid container
    public GameObject buttonPrefab; // minigame frame

    // used for transition between menus
    public GameObject canvasOld;
    public GameObject canvasNew;

    List<Minigame> minigames = null;

	// Use this for initialization
	void Start ()
    {
        minigames = new List<Minigame>();

        minigames.Add(new Minigame("1", "Sushi Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("2", "Tiki", "tirar puro tiki"));
        minigames.Add(new Minigame("3", "Sushi", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("4", "Samurai", "tirar pura katana"));
    }

    public void LoadMinigames()
    {
        if (minigames != null && content != null)
        {
            foreach (Minigame minigame in minigames)
            {
                GameObject m = Instantiate(buttonPrefab, content.transform) as GameObject;
                m.GetComponentInChildren<Text>().text = minigame.Name;
            }

            canvasOld.SetActive(false);
            canvasNew.SetActive(true);           
        }
    }
}
