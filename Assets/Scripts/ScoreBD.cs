using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

struct Scoring
{
    public InputField Mot;
    public float Mult;
}

public class ScoreBD : MonoBehaviour
{
    
    public InputField Mot1;
    public InputField Mot2;
    public InputField Mot3;
    
    public float Mult1;
    public float Mult2;
    public float Mult3;

    private String Connection;
    private List<InputField> ListeMots;
    private List<float> ListeMults;

    Scoring[] scoreStruct = new Scoring[3];

    public static float Total;

    // Start is called before the first frame update
    void Start()
    {
        Connection = "URI=file:" + System.IO.Directory.GetCurrentDirectory() + "/BD/mot_emotions.db";
        ListeMots = new List<InputField>();
        ListeMults = new List<float>();
        
        ListeMots.Add(Mot1);
        ListeMots.Add(Mot2);
        ListeMots.Add(Mot3);

        ListeMults.Add(Mult1);
        ListeMults.Add(Mult2);
        ListeMults.Add(Mult3);

        for(int i = 0; i < 3; i++)
        {
            scoreStruct[i].Mot = ListeMots[i];
            scoreStruct[i].Mult = ListeMults[i];
        }
    }

    public void GetElemBd()
    {
        float total = 0;
        using (IDbConnection dbConnection = new SqliteConnection(Connection))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                foreach (Scoring elem in scoreStruct)
                {
                    //String sqlQuery = "SELECT * FROM sqlite_master";
                    String sqlQuery = "SELECT * FROM Mots WHERE Name = '" + elem.Mot.text + "'";
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
                            
                            total += elem.Mult * int.Parse(reader["ScorePers1"].ToString());
                        }

                        reader.Close();
                        
                    }
                }

                dbConnection.Close();
                Total = total;
                Debug.Log("Your score is: " + total);
            }
        }
    }
}
