using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class GM_1er_Mail : MonoBehaviour
{
    public Text PostitUp;
    public Text PostitDown;
    public Text MotRecup;

    //Private
    public static GM_1er_Mail GM_1_Mail = null;

    private void Awake()
    {
        if (GM_1_Mail == null)
        {
            GM_1_Mail = this;
        }
        else if (GM_1_Mail != this)
        {
            Destroy(gameObject);
        }
    }

    public void GetMot(string mot)
    {
        String Connection = "URI=file:" + System.IO.Directory.GetCurrentDirectory() + "/BD/mot_emotions.db";
        IDbConnection dbConnection = new SqliteConnection(Connection); ;
        dbConnection.Open();
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = "SELECT ScorePers1 FROM Mots WHERE Name = '" + mot + "'";
        IDataReader reader = dbCmd.ExecuteReader();

        while (reader.Read())
        {
            if (int.Parse(reader.GetValue(0).ToString()) > 0)
            {
                PostitUp.text += mot + "\n";
            }
            else if (int.Parse(reader.GetValue(0).ToString()) < 0)
            {
                PostitDown.text += mot + "\n";
            }

            MotRecup.text += mot + "\n";
        }

        reader.Close();
        dbConnection.Dispose();
        dbConnection.Close();
    }
}
