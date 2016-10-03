using UnityEngine;
using System.Collections;
using System;

public class GameSession
{
    private string fecha;
    private int puntaje;
    private int repeticiones;
    private int tiempo;
    private string nivel;
    private string id_minigame;

    public GameSession(string id_minigame)
    {
        fecha = DateTime.Now.ToString("dd-MM-yyyy H:mm:ss");
        this.id_minigame = id_minigame;
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

    public int Puntaje
    {
        get
        {
            return puntaje;
        }

        set
        {
            puntaje = value;
        }
    }

    public int Repeticiones
    {
        get
        {
            return repeticiones;
        }

        set
        {
            repeticiones = value;
        }
    }

    public int Tiempo
    {
        get
        {
            return tiempo;
        }

        set
        {
            tiempo = value;
        }
    }

    public string Nivel
    {
        get
        {
            return nivel;
        }

        set
        {
            nivel = value;
        }
    }

    public string Id_minigame
    {
        get
        {
            return id_minigame;
        }

        set
        {
            id_minigame = value;
        }
    }
}
