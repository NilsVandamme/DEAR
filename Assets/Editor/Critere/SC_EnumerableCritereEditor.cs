using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_EnumerableCritere))]
public class SC_EnumerableCritereEditor : Editor
{
    private SC_EnumerableCritere listOfEnums;

    private bool foldoutListOfWord;

    SerializedProperty myListOfCritere;

    private void OnEnable()
    {
        listOfEnums = target as SC_EnumerableCritere;

        myListOfCritere = serializedObject.FindProperty("myEnum");
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        listOfEnums.fichierCritere = EditorGUILayout.ObjectField("File of Critere : ", listOfEnums.fichierCritere, typeof(TextAsset), false) as TextAsset;

        if (listOfEnums.fichierCritere != null)
        {
            if (GUILayout.Button("Load Enumerable Critere"))
                GenerateEnumerableCritere();
            
            EditorGUILayout.PropertyField(myListOfCritere);
        }

        serializedObject.ApplyModifiedProperties();
    }

    /*
     * Lis le csv des criteres
     */
    private void GenerateEnumerableCritere()
    {
        Undo.RecordObject(listOfEnums, "undo");

        string rawContent = listOfEnums.fichierCritere.text;

        string[] separator = new string[] { "," };
        string[] cells;

        Enum temp = new Enum();

        cells = rawContent.Split(separator, System.StringSplitOptions.None);
            
        temp.key = cells[0];
        temp.value = cells.Where(val => val != cells[0]).ToArray();

        listOfEnums.myEnum = temp;
        SC_EnumerableCritere.myStaticEnum = temp;
    }
}
