using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class ConnectBD : MonoBehaviour
{
    private String connection;

    // Start is called before the first frame update
    void Start()
    {
        connection = "URI=file:" + Application.dataPath + "/BD/mot_emotions.db";
        GetElemBd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetElemBd()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connection))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                String sqlQuery = "SELECT * FROM Mots";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader.GetString(1));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

}
