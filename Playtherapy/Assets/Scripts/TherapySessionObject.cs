using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TherapySessionObject : MonoBehaviour
{
    private Patient patient;
    private Therapist therapist;
    private TherapySession ts;

    public void Login()
    {
        GameObject input = GameObject.Find("Input ID Text");
        string id = input.GetComponent<Text>().text;

        patient = PatientDAO.ConsultPatient(id);
        therapist = TherapistDAO.ConsultTherapist("123");
        ts = new TherapySession(therapist.Id_num, patient.Id_num);      
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
}
