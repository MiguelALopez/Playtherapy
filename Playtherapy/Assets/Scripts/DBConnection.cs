using UnityEngine;
using System.Collections;
using Npgsql;
using System;

public class DBConnection : MonoBehaviour
{
    public string host = "127.0.0.1";
    public string username = "postgres";
    public string password = "postgres";
    public string database = "playtherapy";

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

			NpgsqlCommand dbcmd = dbconn.CreateCommand();
			string sql = string.Format("SELECT * FROM patient_patient ;");
			dbcmd.CommandText = sql;
			NpgsqlDataReader reader = dbcmd.ExecuteReader();

			if (reader.Read())
			{


			string id_num = (string)reader["id_num"];
			Debug.Log("Name: " + id_num + " ");
			}

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
