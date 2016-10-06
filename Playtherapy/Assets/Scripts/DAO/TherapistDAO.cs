using UnityEngine;
using System.Collections;
using Npgsql;

public class TherapistDAO
{
    // returns null if error
    public static Therapist ConsultTherapist(string id_num)
    {
        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            string sql = string.Format("SELECT * FROM therapist WHERE numero_doc = '{1}';", id_num);
            dbcmd.CommandText = sql;

            NpgsqlDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                //string numero_doc = (int)reader["numero_doc"];
                string id_type = (string)reader["tipo_doc"];
                string name = (string)reader["nombre"];
                string lastname = (string)reader["apellido"];
                string genre = (string)reader["genero"];
                string password = (string)reader["password"];

                Therapist therapist = new Therapist(id_num, id_type, name, lastname, genre, password);

                // clean up
                reader.Close();
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;

                Debug.Log("Name: " + name + " " + lastname);
                return therapist;
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
