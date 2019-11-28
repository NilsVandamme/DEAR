using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_Paragraphes))]
public class SC_ParagrapheEditor : Editor
{
    private SC_Paragraphes listOfText;

    SerializedProperty myListOfText;

    private void OnEnable()
    {
        listOfText = target as SC_Paragraphes;

        myListOfText = serializedObject.FindProperty("texte");
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        listOfText.fichierTexte = EditorGUILayout.ObjectField("File Text : ", listOfText.fichierTexte, typeof(TextAsset), false) as TextAsset;

        if (listOfText.fichierTexte != null)
        {
            if (GUILayout.Button("Load Paragraphe"))
                GenerateParagraphe();

            if (listOfText.fichierTexte != null)
                foreach (SerializedProperty text in myListOfText)
                    EditorGUILayout.PropertyField(text);

        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(listOfText);
    }

    /*
     * Lis le csv du paragraphe
     */
    private void GenerateParagraphe()
    {
        Undo.RecordObject(listOfText, "undo");

        string deb = "<link=\"", middle = "\">", fin = "</link>";

        string rawContent = listOfText.fichierTexte.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        string[] separator = new string[] { ";" };
        string[] cells;

        List<TextPart> textInfo = new List<TextPart>();
        TextPart temp = new TextPart();

        for (int i = 1; i < lineList.Length; i++)
        {
            cells = lineList[i].Split(separator, System.StringSplitOptions.None);
            

            if (cells[0] == "_____")
                if (i == lineList.Length - 1)
                    temp.partText = deb + cells[1] + middle + cells[0] + fin;
                else
                    temp.partText = deb + cells[1].Substring(0, cells[1].Length - 1) + middle + cells[0] + fin;
            else
                temp.partText = cells[0];


            textInfo.Add(temp);

        }

        listOfText.texte = textInfo;
    }
}
