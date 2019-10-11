using System.Collections;
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
        Connection = "URI=file:" + Application.dataPath + "/BD/mot_emotions.db";
        ListeMots = new List<InputField>();
        ListeMots.Add(Mot1);
        ListeMots.Add(Mot2);
        ListeMots.Add(Mot3);
    }

    public void GetElemBd()
    {
        using (IDbConnection dbConnection = new SqliteConnection(Connection))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                foreach (InputField elem in ListeMots)
                {
                    String sqlQuery = "SELECT * FROM Mots WHERE Name = '" + elem.text + "'";

                    dbCmd.CommandText = sqlQuery;

                    using (IDataReader reader = dbCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.Log(elem.text + " = " + reader["ScorePers1"]);
                        }

                        reader.Close();
                    }
                }

                dbConnection.Close();
            }
        }
    }
}
