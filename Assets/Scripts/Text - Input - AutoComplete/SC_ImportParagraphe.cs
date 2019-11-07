using TMPro;
using UnityEngine;

public class SC_ImportParagraphe : MonoBehaviour
{
    public SC_Paragraphes textParagraphe;

    private TextMeshProUGUI myText;

    void Start()
    {
        string text = "";

        myText = this.transform.Find("TextAutoComplete/Text").GetComponent<TextMeshProUGUI>();

        foreach (TextPart elem in textParagraphe.texte)
            text += elem.partText + " ";

        myText.text = text;
    }
}
