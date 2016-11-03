using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public string levelToLoad;

	// respond on collisions
	void OnCollisionEnter(Collision newCollision)
	{
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			// call the NextLevel function in the game manager
			//Application.LoadLevel (levelToLoad);
		}
	}

    public void LoadLevelSelected()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
    }
}
