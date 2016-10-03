using UnityEngine;
using System.Collections;
using System;

public class TherapySession
{
    //private string id;
    private string fecha;
    private string objetivos;
    private string observaciones;
    private string numero_doc_Therapist;
    private string numero_doc_Patient;

    public TherapySession(string numero_doc_Therapist, string numero_doc_Patient)
    {
        fecha = DateTime.Now.ToString("dd-MM-yyyy H:mm:ss");
        this.numero_doc_Therapist = numero_doc_Therapist;
        this.numero_doc_Patient = numero_doc_Patient;
    }

    public string Fecha
    {
        get
        {
            return fecha;
        }

        set
        {
            fecha = value;
        }
    }

    public string Objetivos
    {
        get
        {
            return objetivos;
        }

        set
        {
            objetivos = value;
        }
    }

    public string Observaciones
    {
        get
        {
            return observaciones;
        }

        set
        {
            observaciones = value;
        }
    }

    public string Numero_doc_Therapist
    {
        get
        {
            return numero_doc_Therapist;
        }

        set
        {
            numero_doc_Therapist = value;
        }
    }

    public string Numero_doc_Patient
    {
        get
        {
            return numero_doc_Patient;
        }

        set
        {
            numero_doc_Patient = value;
        }
    }
}
