using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;

public class SC_AutoComplete : MonoBehaviour, IPointerClickHandler
{
    // Nombre de button a display
    public int numberOfButtonToDisplay = 12;

    // Taille de la chaine de l'inputfield minimum pour afficher l'autocompletion
    public int autoCompileLenght = 0;

    // Liste des mots a stocker et a afficher
    private List<(string, string)> toStore;
    private List<(string, string)> toDisplay;

    // Camera de la scène
    private Camera cam;

    // Elements récupérer dans le canvas
    public TextMeshProUGUI myText;
    public TMP_InputField myInputField;
    public GameObject myButtons;
    private Button[] listButtons;
    private Tuple<Button, bool>[] tupleButtons;
    private RectTransform rect;

    // Object regroupant les informations obtenue lors des clicks
    private SC_ClickObject currentClick;
    private Vector3 newPos;
    private string currentId;

    /*
     * Récupère les objets nécessaires
     */
    void Start()
    {
        // Initialise la caméra
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Initialise les listes de mots
        toDisplay = new List<(string, string)>();
        toStore = new List<(string, string)>();

        // Init des élements du canvas
        listButtons = myButtons.GetComponentsInChildren<Button>(true);
        rect = this.GetComponent<RectTransform>();

        // Init le tab des buttons
        tupleButtons = new Tuple<Button, bool>[numberOfButtonToDisplay];
        for (int i = 0; i < listButtons.Length; i++)
            tupleButtons[i] = new Tuple<Button, bool>(listButtons[i], false);

        // Init le tab des inputs sauvegardées
        SC_GM.gm.choosenWordInLetter = new List<string>();
    }

    public void Init()
    {
        foreach (Word elem in SC_GM.gm.wheelOfWords)
            for (int i = 0; i < elem.critere.Length; i++)
                if (elem.critere[i] != "none")
                    toStore.Add((elem.critere[i], elem.name[i + 1]));
    }

    /*
     * Récupère si l'on click sur un lien et permet l'écriture à cette position
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(myText, Input.mousePosition, cam);

        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, cam, out newPos);
        myInputField.transform.position = new Vector3(newPos.x, myInputField.transform.position.y, newPos.z);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = myText.textInfo.linkInfo[linkIndex];
            currentId = linkInfo.GetLinkID();

            if (currentClick == null)
            {
                myInputField.gameObject.SetActive(true);
                OnInputFieldValueChange();
            }
            else
                RewriteTextWithInputField();

            GetClickInfo(linkInfo);
        }
        else if (currentClick != null)
            RewriteAndReinit();
    }

    /*
     * Supprime le text courant et le remplace par le nouveau
     */
    private void RewriteTextWithInputField(string newString = null)
    {
        if (myInputField.text != "" || newString != null)
        {
            myText.text = myText.text.Remove((currentClick.getPosStart()), currentClick.getMot().Length);
            if (!SC_GM.gm.choosenWordInLetter.Contains(currentClick.getMot()))
                SC_GM.gm.choosenWordInLetter.Remove(currentClick.getMot());

            if (newString == null)
            {
                myText.text = myText.text.Insert(currentClick.getPosStart(), myInputField.text);
                if (!SC_GM.gm.choosenWordInLetter.Contains(myInputField.text))
                    SC_GM.gm.choosenWordInLetter.Add(myInputField.text);
            }
            else
            {
                myText.text = myText.text.Insert(currentClick.getPosStart(), newString);
                if (!SC_GM.gm.choosenWordInLetter.Contains(newString))
                    SC_GM.gm.choosenWordInLetter.Add(newString);
            }
        }
    }

    /*
     * Récupère les infos de la balise clicker
     */
    private void GetClickInfo(TMP_LinkInfo linkInfo)
    {
        int pos = 0;

        while ((++pos) < linkInfo.linkIdFirstCharacterIndex)
            pos = myText.text.IndexOf(linkInfo.GetLinkText(), pos);

        if (linkInfo.GetLinkText() == "_____")
            myInputField.text = "";
        else
            myInputField.text = linkInfo.GetLinkText();

        currentClick = new SC_ClickObject(pos - 1, linkInfo.GetLinkText());
    }

    /*
     * Change les liste a afficher et a stocker quand la valeur de l'inputfield change
     */
    public void OnInputFieldValueChange()
    {
        if (myInputField.text.Length >= autoCompileLenght)
        {
            // On ajoute une lettre donc on enlève les mots qui ne contiennent plus le text courant de l'inputfield
            IEnumerable<(string, string)> toRemove = toDisplay.Where(x => !x.Item1.Contains(myInputField.text) || x.Item2 != currentId).ToArray();
            foreach ((string, string) mot in toRemove)
            {
                toDisplay.Remove(mot);
                toStore.Add(mot);
            }
            
            // On supprime une lettre donc on ajoute les mots qui contiennent le text courant de l'inputfield
            IEnumerable<(string, string)> toAddBack = toStore.Where(x => x.Item1.Contains(myInputField.text) && x.Item2 == currentId).ToArray();
            foreach ((string, string) mot in toAddBack)
            {
                toStore.Remove(mot);
                toDisplay.Add(mot);
            }

            AfficheButton();
        }
        else
            for (int i = 0; i < tupleButtons.Length; i++)
                CloseButton(i);
    }

    /*
     * Calcul les mot d'autocompélation à afficher et a stocker et affiche les Button correspondant
     */
    private void AfficheButton()
    {
        bool find;

        foreach ((string, string) mot in toStore)
            for (int i = 0; i < tupleButtons.Length; i++)
                if (tupleButtons[i].Item2 == true && tupleButtons[i].Item1.GetComponentInChildren<TextMeshProUGUI>().text == mot.Item1)
                {
                    CloseButton(i);
                    break;
                }
        foreach ((string, string) mot in toDisplay)
        {
            find = false;
            for (int i = 0; i < tupleButtons.Length; i++)
                if (tupleButtons[i].Item2 == true && tupleButtons[i].Item1.GetComponentInChildren<TextMeshProUGUI>().text == mot.Item1)
                {
                    find = true;
                    break;
                }

            if (!find)
                for (int i = 0; i < tupleButtons.Length; i++)
                    if (tupleButtons[i].Item2 == false)
                    {
                        tupleButtons[i].Item1.GetComponentInChildren<TextMeshProUGUI>().text = mot.Item1;
                        tupleButtons[i] = new Tuple<Button, bool>(tupleButtons[i].Item1, true);
                        tupleButtons[i].Item1.gameObject.SetActive(true);
                        break;
                    }
        }
    }

    /*
     * Réinit et ferme le Button i
     */
    private void CloseButton(int i)
    {
        tupleButtons[i].Item1.GetComponentInChildren<TextMeshProUGUI>().text = "";
        tupleButtons[i] = new Tuple<Button, bool>(tupleButtons[i].Item1, false);
        tupleButtons[i].Item1.gameObject.SetActive(false);
    }

    /*
     * Gère le click d'un Button
     */
    public void OnClickButtonAutoComplete()
    {
        RewriteAndReinit(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }

    /*
     * Ecrit le text selectionné et réinit/close les params et les elems
     */
    private void RewriteAndReinit(string text = null)
    {
        RewriteTextWithInputField(text);
        currentClick = null;
        myInputField.gameObject.SetActive(false);

        for (int i = 0; i < tupleButtons.Length; i++)
            CloseButton(i);
    }
}
