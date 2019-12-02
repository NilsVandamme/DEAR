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
        float numberOfLines = 2;

        SerializedProperty score = property.FindPropertyRelative("score");
        SerializedProperty critere = property.FindPropertyRelative("critere");

        if (critere.isExpanded && score.isExpanded)
            numberOfLines += score.arraySize + critere.arraySize;
        else if (critere.isExpanded)
            numberOfLines += critere.arraySize;
        else if (score.isExpanded)
            numberOfLines += score.arraySize;

        return lineHeight * numberOfLines;
    }

    /*
     * Display la structure des words créer
     */
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int i = 0, j, numberOfCritere = 5;

        float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        float space = 10f;
        float rectWight = (position.width - space) * 0.6f;
        
        SerializedProperty name = property.FindPropertyRelative("name");

        SerializedProperty critere = property.FindPropertyRelative("critere");
        critere.isExpanded = 
            EditorGUI.Foldout(new Rect(position.x, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight), critere.isExpanded, "List of criteres", true);

        if (critere.isExpanded)
        {
            EditorGUI.indentLevel += 1;

            for (j = 0; j < critere.arraySize; j++)
            {
                EditorGUI.LabelField(new Rect(position.x, position.y + lineHeight * i + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                    new GUIContent(name.GetArrayElementAtIndex(j + 1).stringValue));

                EditorGUI.LabelField(new Rect(position.x + rectWight, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                    critere.GetArrayElementAtIndex(j).stringValue);
            }

            EditorGUI.indentLevel -= 1;
        }


        SerializedProperty score = property.FindPropertyRelative("score");
        score.isExpanded = 
            EditorGUI.Foldout(new Rect(position.x, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight), score.isExpanded, "List of scores", true);

        if (score.isExpanded)
        {
            EditorGUI.indentLevel += 1;

            for (j = 0; j < score.arraySize; j++)
            {
                EditorGUI.LabelField(new Rect(position.x, position.y + lineHeight * i + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                    new GUIContent(name.GetArrayElementAtIndex(j + numberOfCritere + 1).stringValue));

                EditorGUI.LabelField(new Rect(position.x + rectWight, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                    (score.GetArrayElementAtIndex(j).intValue).ToString());
            }

            EditorGUI.indentLevel -= 1;
        }
    }
}
