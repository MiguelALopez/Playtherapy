using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Npgsql;

public class PatientDAO
{
    // returns null if error
    public static Patient ConsultPatient(string numero_doc)
    {
        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            string sql = string.Format("SELECT * FROM patient WHERE numero_doc = '{1}';", numero_doc);
            dbcmd.CommandText = sql;

            NpgsqlDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                //string numero_doc = (int)reader["numero_doc"];
                string tipo_doc = (string)reader["tipo_doc"];
                string nombre = (string)reader["nombre"];
                string apellido = (string)reader["apellido"];
                string genero = (string)reader["genero"];
                string ocupacion = (string)reader["ocupacion"];
                string fecha_nacimiento = (string)reader["fecha_nacimiento"];

                Patient patient = new Patient(numero_doc, tipo_doc, nombre, apellido, genero, ocupacion, fecha_nacimiento);

                // clean up
                reader.Close();
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;

                Debug.Log("Name: " + nombre + " " + apellido);
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

    // returns null if error
    public static List<Patient> ConsultPatients()
    {
        List<Patient> patients = null;

        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            string sql = "SELECT * FROM patient;";
            dbcmd.CommandText = sql;

            patients = new List<Patient>();

            NpgsqlDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string numero_doc = (string)reader["numero_doc"];
                string tipo_doc = (string)reader["tipo_doc"];
                string nombre = (string)reader["nombre"];
                string apellido = (string)reader["apellido"];
                string genero = (string)reader["genero"];
                string ocupacion = (string)reader["ocupacion"];
                string fecha_nacimiento = (string)reader["fecha_nacimiento"];

                Patient patient = new Patient(numero_doc, tipo_doc, nombre, apellido, genero, ocupacion, fecha_nacimiento);
                patients = new List<Patient>();
                patients.Add(patient);

                Debug.Log("Name: " + nombre + " " + apellido);                
            }

            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;

            return patients;
        }
        else
        {
            Debug.Log("Database connection not established");
            return null;
        }
    }
}
