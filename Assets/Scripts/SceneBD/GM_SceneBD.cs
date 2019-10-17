using System;
using System.Collections;
using System.Collections.Generic;
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


    //Clone
    private GameObject BaseClone;
    private GameObject InsertObjectClone;
    private GameObject UpdateObjectClone;
    private GameObject DeleteObjectClones;


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
    }

    public void CID(string toggle)
    {
        BaseClone.transform.Find("Canvas/Insert").GetComponent<Toggle>().interactable = false;
        BaseClone.transform.Find("Canvas/Delete").GetComponent<Toggle>().interactable = false;
        BaseClone.transform.Find("Canvas/Update").GetComponent<Toggle>().interactable = false;


        if (toggle == "Insert")
        {
            /*
            INSERT INTO table
            VALUES ('valeur 1', 'valeur 2', ...)
            */
            sqlQuery = "INSERT INTO ";
            InsertObjectClone = Instantiate(this.InsertObjectPrefabs, this.InsertObjectPrefabs.transform.position, Quaternion.identity) as GameObject;
            InsertObjectClone.transform.SetParent(BaseClone.transform.Find("Canvas").GetComponent<Canvas>().transform, false);
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
            UpdateObjectClone.transform.SetParent(BaseClone.transform.Find("Canvas").GetComponent<Canvas>().transform, false);
        }
        else if (toggle == "Delete")
        {
            /*
            DELETE FROM `table`
            WHERE condition
            */
            sqlQuery = "DELETE FROM ";
            DeleteObjectClones = Instantiate(this.DeleteObjectPrefabs, this.DeleteObjectPrefabs.transform.position, Quaternion.identity) as GameObject;
            DeleteObjectClones.transform.SetParent(BaseClone.transform.Find("Canvas").GetComponent<Canvas>().transform, false);
        }
    }

    public void Stop()
    {
        if (BaseClone.transform.Find("Canvas/Insert").GetComponent<Toggle>().isOn)
        {
            Destroy(InsertObjectClone);
            BaseClone.transform.Find("Canvas/Insert").GetComponent<Toggle>().isOn = false;
            Destroy(InsertObjectClone);
        }
        else if (BaseClone.transform.Find("Canvas/Update").GetComponent<Toggle>().isOn)
        {
            Destroy(UpdateObjectClone);
            BaseClone.transform.Find("Canvas/Update").GetComponent<Toggle>().isOn = false;
            Destroy(UpdateObjectClone);
        }
        else if (BaseClone.transform.Find("Canvas/Delete").GetComponent<Toggle>().isOn)
        {
            Destroy(DeleteObjectClones);
            BaseClone.transform.Find("Canvas/Delete").GetComponent<Toggle>().isOn = false;
            Destroy(DeleteObjectClones);
        }

        BaseClone.transform.Find("Canvas/Insert").GetComponent<Toggle>().interactable = true;
        BaseClone.transform.Find("Canvas/Update").GetComponent<Toggle>().interactable = true;
        BaseClone.transform.Find("Canvas/Delete").GetComponent<Toggle>().interactable = true;
    }

    public void Finish()
    {
        if (BaseClone.transform.Find("Canvas/Insert").GetComponent<Toggle>().isOn)
        {
            sqlQuery += InsertObjectClone.transform.Find("InputFieldTableName").GetComponent<InputField>().text;
            sqlQuery += " (Name, ScorePers1, ScorePers2, ScorePers3, idChampLexical) VALUES (";
            sqlQuery += InsertObjectClone.transform.Find("InputFieldValues").GetComponent<InputField>().text;
            sqlQuery += ")";
        }
        else if (BaseClone.transform.Find("Canvas/Update").GetComponent<Toggle>().isOn)
        {
            sqlQuery += UpdateObjectClone.transform.Find("InputFieldTableName").GetComponent<InputField>().text;
            sqlQuery += " SET ";
            sqlQuery += UpdateObjectClone.transform.Find("InputFieldSet").GetComponent<InputField>().text;
            sqlQuery += " WHERE ";
            sqlQuery += UpdateObjectClone.transform.Find("InputFieldWhere").GetComponent<InputField>().text;
        }
        else if (BaseClone.transform.Find("Canvas/Delete").GetComponent<Toggle>().isOn)
        {
            sqlQuery += DeleteObjectClones.transform.Find("InputFieldTableName").GetComponent<InputField>().text;
            sqlQuery += " WHERE ";
            sqlQuery += DeleteObjectClones.transform.Find("InputFieldWhere").GetComponent<InputField>().text;
        }
        
        Debug.Log(sqlQuery);
    }
}
