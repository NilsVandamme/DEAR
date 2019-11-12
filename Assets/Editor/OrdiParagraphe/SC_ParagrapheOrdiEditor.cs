using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SC_ParagrapheOrdi))]
public class SC_ParagrapheOrdiEditor : Editor
{
    private SC_ParagrapheOrdi paragrapheOrdi;

    private void OnEnable()
    {
        paragrapheOrdi = target as SC_ParagrapheOrdi;
    }

    /*
     * Display l'inspector de l'asset créer
     */
    public override void OnInspectorGUI()
    {
        paragrapheOrdi.listChampLexicaux = EditorGUILayout.ObjectField("File words : ", paragrapheOrdi.listChampLexicaux, typeof(SC_ListChampLexicaux), false) as SC_ListChampLexicaux;

        if (paragrapheOrdi.listChampLexicaux != null)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Champ Lexical");
            paragrapheOrdi.nameChampLexical = paragrapheOrdi.listChampLexicaux.nameChampsLexicals[EditorGUILayout.Popup(0, paragrapheOrdi.listChampLexicaux.nameChampsLexicals)];

            EditorGUILayout.EndHorizontal();
        }

        if (paragrapheOrdi.nameChampLexical != null)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Champ Lexical");
            //EditorGUILayout.Toggle

            paragrapheOrdi.nameChampLexical = paragrapheOrdi.listChampLexicaux.nameChampsLexicals[EditorGUILayout.Popup(0, paragrapheOrdi.listChampLexicaux.nameChampsLexicals)];

            EditorGUILayout.EndHorizontal();
        }
    }
}
