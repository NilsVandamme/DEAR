using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

struct Click
{
    public TMP_LinkInfo linkInfo;
    public int pos;
}

public class AutoComplete : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI myText;
    private TMP_InputField myInputField;
    private GameObject myButtons;

    private Click currentClick;
    private bool toClick = false;


    /*
     * Récupère les objets nécessaires
     */
    void Start()
    {
        myText = this.GetComponentInChildren<TextMeshProUGUI>();
        myInputField = this.GetComponentInChildren<TMP_InputField>();
        myButtons = myInputField.transform.Find("Buttons").GetComponent<GameObject>();
        
        myInputField.gameObject.SetActive(false);
        //myButtons.SetActive(false);
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
            if (toClick)
            {

            }
            else
            {
                toClick = true;
                linkInfo = myText.textInfo.linkInfo[linkIndex];

                while ((++pos) < linkInfo.linkIdFirstCharacterIndex)
                    pos = myText.text.IndexOf(linkInfo.GetLinkText(), pos);

                OpenInputField(linkInfo.GetLinkID());

                currentClick = new Click();
                currentClick.linkInfo = linkInfo;
                currentClick.pos = pos;
            }
        }
        else
        {
            myText.text = myText.text.Remove((--currentClick.pos), currentClick.linkInfo.GetLinkText().Length);
            myText.text = myText.text.Insert(currentClick.pos, myInputField.text);
            toClick = false;
        }
    }

    private void OpenInputField (string id)
    {
        myInputField.gameObject.SetActive(true);
    }
}
