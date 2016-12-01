using UnityEngine;
using System.Collections;

public class LoadLevelSushi : MonoBehaviour {

	public string levelToLoad;

	public void LoadLevelSelected()
	{
		TherapySessionObject objTherapy = GameObject.Find ("TherapySession").GetComponent<TherapySessionObject> ();
		objTherapy.restartLastSession ();
		UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
	}
}
