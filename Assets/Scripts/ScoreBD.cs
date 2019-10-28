﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class ScoreBD : MonoBehaviour
{
    public InputField Mot1;
    public InputField Mot2;
    public InputField Mot3;

    private String Connection;
    private List<InputField> ListeMots;

    // Start is called before the first frame update
    void Start()
    {
        Connection = "URI=file:" + System.IO.Directory.GetCurrentDirectory() + "/BD/mot_emotions.db";
        ListeMots = new List<InputField>();
        ListeMots.Add(Mot1);
        ListeMots.Add(Mot2);
        ListeMots.Add(Mot3);
    }

    public void GetElemBd()
    {
        int total = 0;
        using (IDbConnection dbConnection = new SqliteConnection(Connection))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                foreach (InputField elem in ListeMots)
                {
                    //String sqlQuery = "SELECT * FROM sqlite_master";
                    String sqlQuery = "SELECT * FROM Mots WHERE Name = '" + elem.text + "'";
                    /*String sqlQuery = "CREATE TABLE test(" +
                                        "Field1    INTEGER," +
                                        "Field2    TEXT," +
                                        "PRIMARY KEY(Field1)" +
                                        ");";*/

                    dbCmd.CommandText = sqlQuery;

                    using (IDataReader reader = dbCmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            //Debug.Log(elem.text + " = " + reader["name"]);
                            
                            total += int.Parse(reader["ScorePers1"].ToString());
                        }

                        reader.Close();
                        
                    }
                }

                dbConnection.Close();
                Debug.Log("Your score is: " + total);
            }
        }
    }
}
