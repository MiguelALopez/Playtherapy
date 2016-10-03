using UnityEngine;
using System.Collections;

public class Patient
{
    private string numero_doc;
    private string tipo_doc;
    private string nombre;
    private string apellido;
    private string genero;
    private string ocupacion;
    private string fecha_nacimiento;
    //private string codigo_entidad;
    //private string codigo_regimen;
    //private string codigo_diagnostico;

    public Patient(string numero_doc, string tipo_doc, string nombre, string apellido, string genero, string ocupacion, string fecha_nacimiento)
    {
        this.numero_doc = numero_doc;
        this.tipo_doc = tipo_doc;
        this.nombre = nombre;
        this.apellido = apellido;
        this.genero = genero;
        this.ocupacion = ocupacion;
        this.fecha_nacimiento = fecha_nacimiento;
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

    public string Ocupacion
    {
        get
        {
            return ocupacion;
        }

        set
        {
            ocupacion = value;
        }
    }

    public string Fecha_nacimiento
    {
        get
        {
            return fecha_nacimiento;
        }

        set
        {
            fecha_nacimiento = value;
        }
    }
}
