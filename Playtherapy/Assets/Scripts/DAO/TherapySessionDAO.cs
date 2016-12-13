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

			Debug.Log ("Patient id:" + therapy.Patient_id);

            try
            {
                string sql = string.Format("INSERT INTO start_therapysession (date, objective, description, patient_id, therapist_id) VALUES ('{0}', '{1}', '{2}', (SELECT id FROM patient_patient WHERE id_num = '{3}'), (SELECT user_ptr_id FROM therapist_therapist, auth_user WHERE therapist_therapist.user_ptr_id = auth_user.id and auth_user.username = '{4}'));",
					therapy.Date, therapy.Objective, therapy.Description, therapy.Patient_id, therapy.Therapist_id);

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

	public static int GetLastTherapyId(string id_patient)
	{
		if (DBConnection.dbconn != null)
		{
			NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

			string sql = ("SELECT id FROM start_therapysession WHERE id = (SELECT max(id) FROM start_therapysession WHERE patient_id = (SELECT id FROM patient_patient WHERE id_num = '" + id_patient + "'));");
			dbcmd.CommandText = sql;

			NpgsqlDataReader reader = dbcmd.ExecuteReader();
			if (reader.Read())
			{
				//string numero_doc = (int)reader["id_num"];
				int id = (int)reader["id"];

				// clean up
				reader.Close();
				reader = null;
				dbcmd.Dispose();
				dbcmd = null;

				return id;                
			}
			else
			{
				// clean up
				reader.Close();
				reader = null;
				dbcmd.Dispose();
				dbcmd = null;

				Debug.Log("Error de consulta o elemento no encontrado");
				return 0;
			}            
		}
		else
		{
			Debug.Log("Database connection not established");
			return 0;
		}
	}
}
