using System.Linq;
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
        int save = paragrapheOrdi.nameChampLexical;
        paragrapheOrdi.listChampLexicaux = EditorGUILayout.ObjectField("File words : ", paragrapheOrdi.listChampLexicaux, typeof(SC_ListChampLexicaux), false) as SC_ListChampLexicaux;

        if (paragrapheOrdi.listChampLexicaux != null)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("Champ Lexical");
            paragrapheOrdi.nameChampLexical = EditorGUILayout.Popup(paragrapheOrdi.nameChampLexical, paragrapheOrdi.listChampLexicaux.nameChampsLexicals);
            paragrapheOrdi.champLexical =  paragrapheOrdi.listChampLexicaux.listChampsLexicals.Where(x => x.fichierWords.name == paragrapheOrdi.listChampLexicaux.nameChampsLexicals[paragrapheOrdi.nameChampLexical]).First();

            EditorGUILayout.EndHorizontal();


            if (paragrapheOrdi.nameChampLexical != save || paragrapheOrdi.motAccepter == null)
            {
                paragrapheOrdi.motAccepter = new bool[paragrapheOrdi.champLexical.words.Count];
                for (int i = 0; i < paragrapheOrdi.champLexical.words.Count; i++)
                    paragrapheOrdi.motAccepter[i] = false;
            }


            for (int i = 0; i < paragrapheOrdi.champLexical.words.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(paragrapheOrdi.champLexical.words[i].titre);
                paragrapheOrdi.motAccepter[i] = EditorGUILayout.Toggle(paragrapheOrdi.motAccepter[i]);

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
