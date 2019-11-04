using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ListWords))]
public class SC_ListWordsEditor : Editor
{
    private SC_ListWords listOfWords;

    private bool foldoutListOfWord;
    private bool generate = false;

    SerializedProperty myListOfWords;

    private void OnEnable()
    {
        listOfWords = target as SC_ListWords;

        myListOfWords = serializedObject.FindProperty("words");
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        listOfWords.fichierWords = EditorGUILayout.ObjectField("File words : ", listOfWords.fichierWords, typeof(TextAsset), false) as TextAsset;

        if (listOfWords.fichierWords != null)
        {
            if (GUILayout.Button("Load BD"))
                if (listOfWords.words != null)
                    GenerateBD();

            //if (GUILayout.Button("Rewrite BD"))
                if (generate)
                    RewriteBD();

            foldoutListOfWord = EditorGUILayout.Foldout(foldoutListOfWord, "List of words", true);
            if (foldoutListOfWord)
            {
                EditorGUI.indentLevel += 1;
                foreach (SerializedProperty elem in myListOfWords)
                {
                    elem.isExpanded = EditorGUILayout.Foldout(elem.isExpanded, "Words", true);
                    if (elem.isExpanded)
                    {
                        EditorGUI.indentLevel += 1;
                        EditorGUILayout.PropertyField(elem);
                        EditorGUI.indentLevel -= 1;
                    }
                }
                EditorGUI.indentLevel -= 1;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    /*
     * Lis le csv du level
     */
    private void GenerateBD()
    {
        generate = true;
        Undo.RecordObject(listOfWords, "undo");

        string rawContent = listOfWords.fichierWords.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        string[] separator = new string[] { "," };
        string[] cells;
        string[] name = lineList[0].Split(separator, System.StringSplitOptions.None); 

        List<Word> wordInfos = new List<Word>();
        Word word;
        
        for (int i = 1; i < lineList.Length; i++)
        {
            cells = lineList[i].Split(separator, System.StringSplitOptions.None);
            word = new Word();

            word.mot = cells[0];
            word.champLexical = cells[1];
            
            word.score = new int[cells.Length - 2];
            for (int j = 2; j < cells.Length; j++)
                int.TryParse(cells[j], out word.score[j - 2]);

            word.name = new string[cells.Length];
            for (int j = 0; j < cells.Length; j++)
                word.name[j] = name[j];

            wordInfos.Add(word);
        }

        listOfWords.words = wordInfos;
    }

    /*
     * Réécris le csv du level
     */
    private void RewriteBD()
    {
        string newCSV = "";

        string rawContent = listOfWords.fichierWords.text;
        string[] lineList = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        
        newCSV += lineList[0];

        foreach (SerializedProperty elem in myListOfWords)
        {
            newCSV += "\n";

            newCSV += elem.FindPropertyRelative("mot").stringValue + ",";
            newCSV += elem.FindPropertyRelative("champLexical").stringValue;

            for (int j = 0; j < elem.FindPropertyRelative("score").arraySize; j++)
                newCSV += "," + elem.FindPropertyRelative("score").GetArrayElementAtIndex(j).intValue;

        }

        System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "\\Assets\\CSV\\Dears.csv", newCSV);
    }
}
