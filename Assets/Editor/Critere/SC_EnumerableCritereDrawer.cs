using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Enum))]
public class SC_EnumerableCritereDrawer : PropertyDrawer
{
    /*
     * Définie la taille alouer a chache objet de l'inspecteur
     */
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        return lineHeight;
    }

    /*
     * Display la structure des words créer
     */
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        float space = 10f;
        float rectWight = (position.width - space) * 0.5f;

        Rect fieldRect = new Rect(position.x, position.y, rectWight, lineHeight);
        Rect popUpRect = new Rect(position.x + fieldRect.width, position.y, rectWight, lineHeight);
        
        SerializedProperty keyProp = property.FindPropertyRelative("key");
        EditorGUI.LabelField(fieldRect, keyProp.stringValue);

        SerializedProperty valueProp = property.FindPropertyRelative("value");

        string[] temp = new string[valueProp.arraySize];
        for (int i = 0; i < valueProp.arraySize; i++)
            temp[i] = valueProp.GetArrayElementAtIndex(i).stringValue;

        EditorGUI.Popup(popUpRect, 0, temp);
    }
}
