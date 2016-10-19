using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    public void Load()
    {
        string sceneName = this.gameObject.GetComponentInChildren<Text>().text;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Debug.Log("Tiki");
    }
}
