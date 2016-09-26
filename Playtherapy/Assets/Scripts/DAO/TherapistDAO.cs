using UnityEngine;
using System.Collections;
using Npgsql;

public class TherapistDAO
{
    // returns null if error
    public static Therapist ConsultTherapist(string numero_doc)
    {
        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            string sql = string.Format("SELECT * FROM therapist WHERE numero_doc = '{1}';", numero_doc);
            dbcmd.CommandText = sql;

            NpgsqlDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                //string numero_doc = (int)reader["numero_doc"];
                string tipo_doc = (string)reader["tipo_doc"];
                string nombre = (string)reader["nombre"];
                string apellido = (string)reader["apellido"];
                string genero = (string)reader["genero"];
                string password = (string)reader["password"];

                Therapist therapist = new Therapist(numero_doc, tipo_doc, nombre, apellido, genero, password);

                // clean up
                reader.Close();
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;

                Debug.Log("Name: " + nombre + " " + apellido);
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
