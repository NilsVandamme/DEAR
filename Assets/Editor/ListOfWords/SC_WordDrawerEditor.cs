using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Word))]
public class SC_WordDrawerEditor : PropertyDrawer
{
    /*
     * Définie la taille alouer a chache objet de l'inspecteur
     */
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        float numberOfLines = property.FindPropertyRelative("score").arraySize + 2;

        return lineHeight * numberOfLines;
    }

    /*
     * Display la structure des words créer
     */
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int i = 0, j, numberOfCritere = 1;

        float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        float space = 10f;
        float rectWight = (position.width - space);

        Rect motRect = new Rect(position.x, position.y, rectWight, lineHeight);
        
        SerializedProperty name = property.FindPropertyRelative("name");
        SerializedProperty critere = property.FindPropertyRelative("critere");
        

        EditorGUI.LabelField(new Rect(position.x, position.y + lineHeight * i + EditorGUIUtility.standardVerticalSpacing, rectWight * 0.5f, lineHeight), new GUIContent(name.GetArrayElementAtIndex(1).stringValue));

        if (SC_EnumerableCritere.myStaticEnum.key == name.GetArrayElementAtIndex(1).stringValue)
            for (int k = 0; k < SC_EnumerableCritere.myStaticEnum.value.Length; k++)
                if (SC_EnumerableCritere.myStaticEnum.value[k] == critere.stringValue)
                {
                    int selectIndex = 
                        EditorGUI.Popup(new Rect(position.x + rectWight * 0.5f, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight * 0.5f, lineHeight), k, SC_EnumerableCritere.myStaticEnum.value);
                    critere.stringValue = SC_EnumerableCritere.myStaticEnum.value[selectIndex];
                }


        SerializedProperty score = property.FindPropertyRelative("score");
        score.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight), score.isExpanded, "List of scores", true);

        if (score.isExpanded)
        {
            EditorGUI.indentLevel += 1;

            for (j = 0; j < score.arraySize; j++)
            {
                EditorGUI.LabelField(new Rect(position.x, position.y + lineHeight * i + EditorGUIUtility.standardVerticalSpacing, rectWight * 0.5f, lineHeight), new GUIContent(name.GetArrayElementAtIndex(j + numberOfCritere + 1).stringValue));
                score.GetArrayElementAtIndex(j).intValue = 
                    EditorGUI.IntField(new Rect(position.x + rectWight * 0.7f, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight * 0.3f, lineHeight), score.GetArrayElementAtIndex(j).intValue);
            }

            EditorGUI.indentLevel -= 1;
        }
    }
}
