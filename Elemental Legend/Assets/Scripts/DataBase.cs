using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DataBase
{
    private static string conn = "URI=file:" + Application.dataPath + "/DataBase/ElementalLegend.s3db";

    public static int GetDinero(int id)
    {
        int dinero=0;
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT dinero FROM Dinero WHERE id = " +id;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            dinero = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return dinero;
    }

    public static void SetDinero(int id, int dinero)
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "UPDATE Dinero SET dinero = " +dinero +" WHERE id = " + id;
        dbcmd.CommandText = sqlQuery;
        if (dbcmd.ExecuteNonQuery() > 0)
        {
            Debug.Log("Se modifico, dinero = " + dinero);
        }
        else
        {
            Debug.Log("No se modifico");
        }
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public static int GetPuntuacion(int idNivel)
    {
        int dinero = 0;
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT puntuacion FROM Puntuacion WHERE id_nivel = " + idNivel;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            dinero = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return dinero;
    }

    public static void SetPuntuacion(int idNivel, int puntuacion)
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "UPDATE Puntuacion SET puntuacion = " + puntuacion + " WHERE id_nivel = " + idNivel;
        dbcmd.CommandText = sqlQuery;
        if (dbcmd.ExecuteNonQuery() > 0)
        {
            Debug.Log("Se modifico, puntuacion = " + puntuacion);
        }
        else
        {
            Debug.Log("No se modifico");
        }
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
