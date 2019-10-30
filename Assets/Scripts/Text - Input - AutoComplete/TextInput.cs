using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextInput : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI myText;

    /*
     * Récupère l'objet Text courant
     */
    void Start()
    {
        myText = this.GetComponent<TextMeshProUGUI>();
    }

    /*
     * Récupère si l'on click sur un lien et permet l'écriture à cette position
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        int pos = 0, linkIndex = TMP_TextUtilities.FindIntersectingLink(myText, Input.mousePosition, null);
        TMP_LinkInfo linkInfo;

        if (linkIndex != -1)
        {
            linkInfo = myText.textInfo.linkInfo[linkIndex];
            
            while ((++pos) < linkInfo.linkIdFirstCharacterIndex)
                pos = myText.text.IndexOf(linkInfo.GetLinkText(), pos);
            
            myText.text = myText.text.Remove((--pos), linkInfo.GetLinkText().Length);
            myText.text = myText.text.Insert(pos, "seringue");
        }
    }
}
