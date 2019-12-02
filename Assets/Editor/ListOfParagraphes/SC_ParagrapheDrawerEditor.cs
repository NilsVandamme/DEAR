using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TextPart))]
public class SC_ParagrapheDrawerEditor : PropertyDrawer
{
    Dictionary<string, Vector2> scrollDict = new Dictionary<string, Vector2>();

    /*
     * Définie la taille alouer a chache objet de l'inspecteur
     */
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2;

        return lineHeight;
    }

    /*
     * Display la structure des words créer
     */
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUIStyle myTextAreaStyle = new GUIStyle(EditorStyles.textArea);
        myTextAreaStyle.wordWrap = true;
        myTextAreaStyle.alignment = TextAnchor.UpperLeft;

        GUIStyle myTextFieldStyle = new GUIStyle(EditorStyles.textField);
        myTextFieldStyle.alignment = TextAnchor.MiddleCenter;

        float lineHeight = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2;

        float space = 10f;
        float textRectWight = (position.width - space);

        Rect textRect = new Rect(position.x, position.y, textRectWight, lineHeight);

        if (!scrollDict.ContainsKey(label.text))
            scrollDict.Add(label.text, new Vector2());

        scrollDict[label.text] = GUI.BeginScrollView(textRect, scrollDict[label.text], new Rect(0, 0, textRectWight, lineHeight * 5), false, true, GUIStyle.none, GUI.skin.verticalScrollbar);

        SerializedProperty textProp = property.FindPropertyRelative("partText");
        textProp.stringValue = GUI.TextArea(new Rect(0, 0, textRectWight - space, lineHeight * 10), textProp.stringValue, myTextAreaStyle);

        GUI.EndScrollView();
    }
}
