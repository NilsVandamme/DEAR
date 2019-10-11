using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;


public class AutoComplete : MonoBehaviour
{
    public int LenghtAutoComplete = 3;

    private InputField mot;
    private String Connection;

    // Start is called before the first frame update
    void Start()
    {
        Connection = "URI=file:" + Application.dataPath + "/BD/mot_emotions.db";
        mot = GetComponent<InputField>();
    }

    public void AutoComp ()
    {
        if (mot.text.Length >= LenghtAutoComplete)
        {
            using (IDbConnection dbConnection = new SqliteConnection(Connection))
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
                            Debug.Log(mot.text + "  " + reader["Name"]);
                        }

                        dbConnection.Close();
                        reader.Close();
                    }

                }
            }
        }
        
    }

}
