using UnityEngine;
using System.Collections;
using Npgsql;

public class TherapySessionDAO
{
    public static bool InsertTherapySession(TherapySession therapy)
    {
        bool exito = false;

        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            try
            {
                string sql = string.Format("INSERT INTO start_therapysession VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');",
                    therapy.Date, therapy.Objective, therapy.Description, therapy.Therapist_id, therapy.Patient_id);

                dbcmd.CommandText = sql;
                dbcmd.ExecuteNonQuery();

                //Debug.Log("");
                exito = true;                
            }
            catch (NpgsqlException ex)
            {
                Debug.Log(ex.Message);
            }

            // clean up
            dbcmd.Dispose();
            dbcmd = null;
        }
        else
        {
            Debug.Log("Database connection not established");
        }

        return exito;
    }

	public int GetLastTherapyId()
	{
		if (DBConnection.dbconn != null)
		{
			NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

			string sql = ("SELECT id FROM start_therapysession WHERE id = (SELECT max(id) FROM start_therapysession);");
			dbcmd.CommandText = sql;

			NpgsqlDataReader reader = dbcmd.ExecuteReader();
			if (reader.Read())
			{
				//string numero_doc = (int)reader["id_num"];
				string id_type = (string)reader["id_type"];
				string name = (string)reader["name"];
				string lastname = (string)reader["lastname"];
				string genre = (string)reader["genre"];
				string occupation = (string)reader["occupation"];
				string birthday = ((DateTime)reader["birthday"]).ToString();                

				Patient patient = new Patient(id_num, id_type, name, lastname, genre, occupation, birthday);

				// clean up
				reader.Close();
				reader = null;
				dbcmd.Dispose();
				dbcmd = null;

				Debug.Log("Name: " + name + " " + lastname);
				return patient;                
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
}
