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
}
