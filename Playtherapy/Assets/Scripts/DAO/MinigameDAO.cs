﻿using UnityEngine;
using System.Collections.Generic;
using Npgsql;

public class MinigameDAO : MonoBehaviour
{
    // returns null if error
    public static Minigame ConsultMinigame(string id)
    {
        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            string sql = string.Format("SELECT * FROM minigame WHERE id = {1};", id);
            dbcmd.CommandText = sql;

            NpgsqlDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                //string id = (int)reader["id"];
                string nombre = (string)reader["nombre"];
                string descripcion = (string)reader["descripcion"];

                Minigame minigame = new Minigame(id, nombre, descripcion);

                // clean up
                reader.Close();
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;

                Debug.Log("Minigame: " + nombre);
                return minigame;
            }
            else
            {
                // clean up
                reader.Close();
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;

                Debug.Log("Error de consulta o elemento no encontrado");
                return null;
            }
        }
        else
        {
            Debug.Log("Database connection not established");
            return null;
        }
    }

    // returns null if error
    public static List<Minigame> ConsultMinigames()
    {
        List<Minigame> minigames = null;

        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            string sql = "SELECT * FROM minigame;";
            dbcmd.CommandText = sql;

            minigames = new List<Minigame>();

            NpgsqlDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["id"];
                string nombre = (string)reader["nombre"];
                string descripcion = (string)reader["descripcion"];

                Minigame minigame = new Minigame(id, nombre, descripcion);
                minigames.Add(minigame);

                Debug.Log("Minigame: " + nombre);
            }

            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;

            return minigames;
        }
        else
        {
            Debug.Log("Database connection not established");
            return null;
        }
    }
}