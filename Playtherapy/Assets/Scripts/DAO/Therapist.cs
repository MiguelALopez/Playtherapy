using UnityEngine;
using System.Collections;

public class Therapist
{
    private string numero_doc;
    private string tipo_doc;
    private string nombre;
    private string apellido;
    private string genero;
    private string password;

    public Therapist(string numero_doc, string tipo_doc, string nombre, string apellido, string genero, string password)
    {
        this.numero_doc = numero_doc;
        this.tipo_doc = tipo_doc;
        this.nombre = nombre;
        this.apellido = apellido;
        this.genero = genero;
        this.password = password;
    }

    public string Numero_doc
    {
        get
        {
            return numero_doc;
        }

        set
        {
            numero_doc = value;
        }
    }

    public string Tipo_doc
    {
        get
        {
            return tipo_doc;
        }

        set
        {
            tipo_doc = value;
        }
    }

    public string Nombre
    {
        get
        {
            return nombre;
        }

        set
        {
            nombre = value;
        }
    }

    public string Apellido
    {
        get
        {
            return apellido;
        }

        set
        {
            apellido = value;
        }
    }

    public string Genero
    {
        get
        {
            return genero;
        }

        set
        {
            genero = value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }

        set
        {
            password = value;
        }
    }
}
