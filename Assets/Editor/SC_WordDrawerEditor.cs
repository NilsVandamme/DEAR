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
        float numberOfLines = 2 + property.FindPropertyRelative("score").arraySize + 1;

        return lineHeight * numberOfLines;
    }

    /*
     * Display la structure des words créer
     */
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int i = 2, j;
        float oldWidth = EditorGUIUtility.labelWidth;

        float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        float space = 10f;
        float rectWight = (position.width - space);

        Rect motRect = new Rect(position.x, position.y, rectWight, lineHeight);
        Rect champLexicalRect = new Rect(position.x, position.y + lineHeight + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight);
        
        SerializedProperty name = property.FindPropertyRelative("name");


        EditorGUIUtility.labelWidth = oldWidth * 0.5f;
        SerializedProperty motProp = property.FindPropertyRelative("mot");
        EditorGUI.PropertyField(motRect, motProp, new GUIContent(name.GetArrayElementAtIndex(0).stringValue));

        EditorGUIUtility.labelWidth = oldWidth;
        SerializedProperty champLexicalProp = property.FindPropertyRelative("champLexical");
        EditorGUI.PropertyField(champLexicalRect, champLexicalProp, new GUIContent(name.GetArrayElementAtIndex(1).stringValue));

        SerializedProperty score = property.FindPropertyRelative("score");
        score.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                             score.isExpanded, "List of scores", true);
        if (score.isExpanded)
        {
            EditorGUI.indentLevel += 1;

            for (j = 0; j < score.arraySize; j++) 
                EditorGUI.PropertyField(new Rect(position.x, position.y + lineHeight * (i++) + EditorGUIUtility.standardVerticalSpacing, rectWight, lineHeight),
                                        score.GetArrayElementAtIndex(j),new GUIContent(name.GetArrayElementAtIndex(j + 2).stringValue));

            EditorGUI.indentLevel -= 1;
        }
    }
}
