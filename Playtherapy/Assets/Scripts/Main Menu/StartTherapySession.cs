using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartTherapySession : MonoBehaviour
{
    public GameObject content; // grid container
    public GameObject buttonPrefab; // minigame frame

    public Text patient_name;
    public Text patient_id;
    public Text therapist_name;

    // used for transition between menus
    public GameObject canvasOld;
    public GameObject canvasNew;

    private List<Minigame> minigames = null;
    private Patient patient;

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

    public void StartTherapy()
    {
        Login();
        therapist_name.text = "Random Therapist Name";
        LoadMinigames();
    }

    public void Login()
    {
        GameObject input = GameObject.Find("Input ID Text");
        string id = input.GetComponent<Text>().text;

        patient = PatientDAO.ConsultPatient(id);

        if (patient != null)
        {
            patient_name.text = patient.Name + " " + patient.Lastname;
            patient_id.text = patient.Id_num;
        }
        else
        {
            Debug.Log("Patient not loaded");
        }
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
