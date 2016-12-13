using UnityEngine;
using System.Collections;
using Npgsql;

public class PerformanceDAO
{
	public static bool InsertPerformance(Performance performance)
    {
        bool exito = false;

        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            try
            {
                string sql = string.Format("INSERT INTO start_performance (angle, game_session_id, movement_id) VALUES ({0}, {1}, '{2}');",
                    performance.Angle, performance.Game_session_id, performance.Movement_id);

                Debug.Log(sql);

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
