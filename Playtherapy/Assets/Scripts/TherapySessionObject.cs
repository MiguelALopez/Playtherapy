using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TherapySessionObject : MonoBehaviour
{
    private Patient patient;
    private Therapist therapist;
    private TherapySession therapySession;
    private List<GameSession> gameSessionList;

    public void Login()
    {
        GameObject input = GameObject.Find("Input ID Text");
        string id = input.GetComponent<Text>().text;

        patient = PatientDAO.ConsultPatient(id);
        therapist = TherapistDAO.ConsultTherapist("123");
        therapySession = new TherapySession(therapist.Id_num, patient.Id_num);      
    }

    public void addGameSession(GameSession gs)
    {
        if (gs != null)
        {
            gameSessionList.Add(gs);
        }
        else
        {
            Debug.Log("Null Game Session");
        }
    }

    public Patient Patient
    {
        get
        {
            return patient;
        }

        set
        {
            patient = value;
        }
    }

    public Therapist Therapist
    {
        get
        {
            return therapist;
        }

        set
        {
            therapist = value;
        }
    }

    public TherapySession TherapySession
    {
        get
        {
            return therapySession;
        }

        set
        {
            therapySession = value;
        }
    }

    public List<GameSession> GameSessionList
    {
        get
        {
            return gameSessionList;
        }

        set
        {
            gameSessionList = value;
        }
    }
}
