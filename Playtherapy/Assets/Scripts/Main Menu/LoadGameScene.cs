using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    private Minigame minigame;

    public void Load()
    {
        string sceneName = this.gameObject.GetComponentInChildren<Text>().text;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Debug.Log("Scene loaded");
    }

    public void createGameSession()
    {
        if (minigame != null)
        {
            GameObject tso = GameObject.Find("TherapySession");

            if (tso != null)
            {
                GameSession gs = new GameSession(minigame.Id);
                tso.GetComponent<TherapySessionObject>().addGameSession(gs);
            }
            else
            {
                Debug.Log("TherapySession not found");
            }
        }
    }

    public Minigame Minigame
    {
        get
        {
            return minigame;
        }

        set
        {
            minigame = value;
        }
    }
}
