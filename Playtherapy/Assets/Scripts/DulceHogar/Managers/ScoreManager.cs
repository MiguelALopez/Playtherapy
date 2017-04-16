using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;        // The player's score.
		public int nextLevel = 50; //Puntaje necesario para cambiar de nivel
		private string nameScene;

        Text text;                      // Reference to the Text component.


        void Awake ()
        {
            // Set up the reference.
            text = GetComponent <Text> ();
			Scene scene = SceneManager.GetActiveScene ();
			nameScene = scene.name;
			NGUIDebug.Log ("Escena Activa + " + nameScene + " ");
			// Reset the score.
            score = 0;
        }


        void Update ()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
            text.text = "Score: " + score;
			//Combio de Escena
			if (score >= nextLevel && "Urban".Equals(nameScene)) {
				print ("Cambio de Nivel");
				SceneManager.LoadScene ("ParkNatural");
			}
			if (score >= nextLevel && "ParkNatural".Equals(nameScene)) {
				print ("Cambio de Nivel");
				SceneManager.LoadScene ("ParkNaturalDark");
			}
        }
    }
}