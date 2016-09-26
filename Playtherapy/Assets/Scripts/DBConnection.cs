using UnityEngine;
using System.Collections;
using Npgsql;
using System;

public class DBConnection : MonoBehaviour
{
    public string host = "127.0.0.1";
    public string username = "postgres";
    public string password = "postgres";
    public string database = "test";

    public Animator cameraAnimator;

    public static NpgsqlConnection dbconn = null;

    // Use this for initialization
    void Start()
    {
        string connectionString =
          "Host=" + host + ";" +
          "Username=" + username + ";" +
          "Password=" + password + ";" +
          "Database=" + database;

        try
        {
            dbconn = new NpgsqlConnection(connectionString);
            dbconn.Open();
            Debug.Log("Succesfully connected to the database");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        
        if (cameraAnimator != null)
        {
            cameraAnimator.enabled = true;
        }  
    }

    public void CloseConnection()
    {
        if (dbconn != null)
        {
            dbconn.Close();
            dbconn = null;
        }
    }
}
