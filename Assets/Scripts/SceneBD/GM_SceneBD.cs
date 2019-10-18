using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM_SceneBD : MonoBehaviour
{

    //Prefabs
    public GameObject BasePrefabs;
    public GameObject InsertObjectPrefabs;
    public GameObject UpdateObjectPrefabs;
    public GameObject DeleteObjectPrefabs;
    public GameObject ShowObjectPrefabs;


    //Clone
    private GameObject BaseClone;
    private GameObject InsertObjectClone;
    private GameObject UpdateObjectClone;
    private GameObject DeleteObjectClones;
    private GameObject ShowObjectClones;

    private Toggle Insert;
    private Toggle Update;
    private Toggle Delete;
    private GameObject Show;
    private Canvas Canvas;


    //Private
    private String sqlQuery;
    public static GM_SceneBD GM_BD = null;

    private void Awake()
    {
        if (GM_BD == null)
        {
            GM_BD = this;
        }
        else if (GM_BD != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BaseClone = Instantiate(this.BasePrefabs, this.BasePrefabs.transform.position, Quaternion.identity) as GameObject;

        Insert = BaseClone.transform.Find("Canvas/Insert").GetComponent<Toggle>();
        Delete = BaseClone.transform.Find("Canvas/Delete").GetComponent<Toggle>();
        Update = BaseClone.transform.Find("Canvas/Update").GetComponent<Toggle>();
        Show = BaseClone.transform.Find("Canvas/Show").GetComponent<Button>().gameObject;
        Canvas = BaseClone.transform.Find("Canvas").GetComponent<Canvas>();
    }

    public void CID(String toggle)
    {
        Show.SetActive(false);

        Insert.interactable = false;
        Update.interactable = false;
        Delete.interactable = false;


        if (toggle == "Insert")
        {
            /*
            INSERT INTO table
            VALUES ('valeur 1', 'valeur 2', ...)
            */
            sqlQuery = "INSERT INTO ";
            InsertObjectClone = Instantiate(this.InsertObjectPrefabs, this.InsertObjectPrefabs.transform.position, Quaternion.identity) as GameObject;
            InsertObjectClone.transform.SetParent(Canvas.transform, false);
        }
        else if (toggle == "Update")
        {
            /*
            UPDATE table
            SET colonne_1 = 'valeur 1', colonne_2 = 'valeur 2', colonne_3 = 'valeur 3'
            WHERE condition
            */
            sqlQuery = "UPDATE ";
            UpdateObjectClone = Instantiate(this.UpdateObjectPrefabs, this.UpdateObjectPrefabs.transform.position, Quaternion.identity) as GameObject;
            UpdateObjectClone.transform.SetParent(Canvas.transform, false);
        }
        else if (toggle == "Delete")
        {
            /*
            DELETE FROM `table`
            WHERE condition
            */
            sqlQuery = "DELETE FROM ";
            DeleteObjectClones = Instantiate(this.DeleteObjectPrefabs, this.DeleteObjectPrefabs.transform.position, Quaternion.identity) as GameObject;
            DeleteObjectClones.transform.SetParent(Canvas.transform, false);
        }
    }

    public void Stop()
    {
        if (Insert.isOn)
        {
            Destroy(InsertObjectClone);
            Insert.isOn = false;
            Destroy(InsertObjectClone);
        }
        else if (Update.isOn)
        {
            Destroy(UpdateObjectClone);
            Update.isOn = false;
            Destroy(UpdateObjectClone);
        }
        else if (Delete.isOn)
        {
            Destroy(DeleteObjectClones);
            Delete.isOn = false;
            Destroy(DeleteObjectClones);
        }

        Insert.interactable = true;
        Update.interactable = true;
        Delete.interactable = true;

        Show.SetActive(true);
        Destroy(ShowObjectClones);
    }

    public void Finish()
    {
        if (Insert.isOn)
        {
            sqlQuery += InsertObjectClone.transform.Find("InputFieldTableName").GetComponent<InputField>().text;
            sqlQuery += " (Name, ScorePers1, ScorePers2, ScorePers3, idChampLexical) VALUES (";
            sqlQuery += InsertObjectClone.transform.Find("InputFieldValues").GetComponent<InputField>().text;
            sqlQuery += ")";
        }
        else if (Update.isOn)
        {
            sqlQuery += UpdateObjectClone.transform.Find("InputFieldTableName").GetComponent<InputField>().text;
            sqlQuery += " SET ";
            sqlQuery += UpdateObjectClone.transform.Find("InputFieldSet").GetComponent<InputField>().text;
            sqlQuery += " WHERE ";
            sqlQuery += UpdateObjectClone.transform.Find("InputFieldWhere").GetComponent<InputField>().text;
        }
        else if (Delete.isOn)
        {
            sqlQuery += DeleteObjectClones.transform.Find("InputFieldTableName").GetComponent<InputField>().text;
            sqlQuery += " WHERE ";
            sqlQuery += DeleteObjectClones.transform.Find("InputFieldWhere").GetComponent<InputField>().text;
        }

        ConnectWrite();

        Stop();
    }

    public void ShowTable()
    {
        ShowObjectClones = Instantiate(this.ShowObjectPrefabs, this.ShowObjectPrefabs.transform.position, Quaternion.identity) as GameObject;
        ShowObjectClones.transform.SetParent(Canvas.transform, false);

        sqlQuery = "SELECT * FROM ChampLexical;";
        ConnectRead("table1");
        sqlQuery = "SELECT * FROM Mots;";
        ConnectRead("table2");

        Insert.interactable = false;
        Update.interactable = false;
        Delete.interactable = false;

        Show.SetActive(false);
    }

    private void ConnectRead(String choix)
    {
        String Connection = "URI=file:" + System.IO.Directory.GetCurrentDirectory() + "/BD/mot_emotions.db";
        IDbConnection dbConnection = new SqliteConnection(Connection); ;
        dbConnection.Open();
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = sqlQuery;
        IDataReader reader = dbCmd.ExecuteReader();

        while (reader.Read())
        {
            if (choix == "table1")
            {
                ShowObjectClones.transform.Find("ChampLexical/GameObject/Text/Id").GetComponent<Text>().text += reader["Id"].ToString() + "\n";
                ShowObjectClones.transform.Find("ChampLexical/GameObject/Text/Name").GetComponent<Text>().text += reader["Name"].ToString() + "\n";
            }
            else if (choix == "table2")
            {
                ShowObjectClones.transform.Find("Mots/GameObject/Text/Id").GetComponent<Text>().text += reader["Id"].ToString() + "\n";
                ShowObjectClones.transform.Find("Mots/GameObject/Text/Name").GetComponent<Text>().text += reader["Name"].ToString() + "\n";
                ShowObjectClones.transform.Find("Mots/GameObject/Text/ScorePers1").GetComponent<Text>().text += reader["ScorePers1"].ToString() + "\n";
                ShowObjectClones.transform.Find("Mots/GameObject/Text/ScorePers2").GetComponent<Text>().text += reader["ScorePers2"].ToString() + "\n";
                ShowObjectClones.transform.Find("Mots/GameObject/Text/ScorePers3").GetComponent<Text>().text += reader["ScorePers3"].ToString() + "\n";
                ShowObjectClones.transform.Find("Mots/GameObject/Text/IdChampLexical").GetComponent<Text>().text += reader["IdChampLexical"].ToString() + "\n";
            }
        }
        reader.Close();
        dbConnection.Dispose();
        dbConnection.Close();
    }

    private void ConnectWrite()
    {
        String Connection = "URI=file:" + System.IO.Directory.GetCurrentDirectory() + "/BD/mot_emotions.db";
        IDbConnection dbConnection = new SqliteConnection(Connection); ;
        dbConnection.Open();
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteReader();
        dbConnection.Dispose();
        dbConnection.Close();
    }
}
