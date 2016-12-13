using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class StartTherapySession : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject content; // grid container
    public GameObject buttonPrefab; // minigame frame

    public Text patient_name;
    public Text patient_id;
    public Text therapist_name;

    public TherapySessionObject tso;

    // used for transition between menus
    public GameObject canvasOld;
    public GameObject canvasNew;

    private List<Minigame> minigames = null;

	// Use this for initialization
	void Start ()
    {
        minigames = new List<Minigame>();

        minigames.Add(new Minigame("16", "Sushi Samurai", "tirar pura katana"));
        minigames.Add(new Minigame("17", "Atrapalo", "tirar puro tiki"));
        //minigames.Add(new Minigame("3", "Duro contra el Muro", "tirar pura katana"));
        //minigames.Add(new Minigame("4", "Ponchado", "tirar pura katana"));
    }

    public void StartTherapy()
    {
        if (tso != null)
        {
            tso.Login();
            DisplayPatientInfo();
            DisplayTherapistInfo();            
            LoadMinigames();
        }
        else
        {
            Debug.Log("No therapy session object found");
            LoadMinigames();
        }
    }

    public void DisplayPatientInfo()
    {
        if (tso.Patient != null)
        {
            patient_name.text = tso.Patient.Name + " " + tso.Patient.Lastname;
            patient_id.text = tso.Patient.Id_num;
        }
        else
        {
            Debug.Log("Patient not loaded");
        }
    }

    public void DisplayTherapistInfo()
    {
        if (tso.Therapist != null)
        {
            therapist_name.text = tso.Therapist.Name + " " + tso.Therapist.Lastname;
        }
        else
        {
            Debug.Log("Therapist not loaded");
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
                m.GetComponent<Image>().sprite = GameObject.Find(minigame.Name + " Image").GetComponent<Image>().sprite;
                m.GetComponent<LoadGameScene>().Minigame = minigame;
            }

            canvasOld.SetActive(false);
            canvasNew.SetActive(true);           
        }
    }
}
