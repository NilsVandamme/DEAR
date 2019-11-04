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
    public int autoCompileLenght = 3;

    // Liste des mots a stocker et a afficher
    private List<string> toStore;
    private List<string> toDisplay;

    // Camera de la scène
    private Camera cam;

    // Elements récupérer dans le canvas
    private TextMeshProUGUI myText;
    private TMP_InputField myInputField;
    private GameObject myButtons;
    private Button[] listButtons;
    private Tuple<Button, bool>[] tupleButtons;

    // Object regroupant les informations obtenue lors des clicks
    private SC_ClickObject currentClick;
    private Vector3 newPos;

    /*
     * Récupère les objets nécessaires
     */
    void Start()
    {
        // Initialise la caméra
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Initialise les listes de mots
        toDisplay = new List<string>();
        toStore = new List<string>();
        foreach (Word elem in SC_GM.gm.bd.words)
            toStore.Add(elem.mot);

        // Init des élements du canvas
        myText = this.GetComponentInChildren<TextMeshProUGUI>();
        myInputField = this.GetComponentInChildren<TMP_InputField>(true);
        myButtons = myInputField.transform.Find("Buttons").gameObject;
        listButtons = myButtons.GetComponentsInChildren<Button>(true);

        // Init le tab des buttons
        tupleButtons = new Tuple<Button, bool>[numberOfButtonToDisplay];
        for (int i = 0; i < listButtons.Length; i++)
            tupleButtons[i] = new Tuple<Button, bool>(listButtons[i], false);

        // Init le tab des inputs sauvegardées
        SC_GM.gm.tabInputStrings = new List<string>();
    }

    /*
     * Récupère si l'on click sur un lien et permet l'écriture à cette position
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(myText, Input.mousePosition, cam);

        RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), Input.mousePosition, cam, out newPos);
        myInputField.transform.position = new Vector3(newPos.x, myInputField.transform.position.y, newPos.z);

        //Debug.Log("camera position: " + newPos);
        //Debug.Log("inputfield position: " + myInputField.transform.position);

        if (linkIndex != -1)
        {
            if (currentClick == null)
                myInputField.gameObject.SetActive(true);
            else
                RewriteTextWithInputField();

            GetClickInfo(linkIndex);
        }
        else if (currentClick != null)
            RewriteAndReinit();
    }

    /*
     * Supprime le text courant et le remplace par le nouveau
     */
    private void RewriteTextWithInputField(string newString = null)
    {
        if (myInputField.text != "")
        {
            myText.text = myText.text.Remove((currentClick.getPosStart()), currentClick.getMot().Length);
            if (!SC_GM.gm.tabInputStrings.Contains(currentClick.getMot()))
                SC_GM.gm.tabInputStrings.Remove(currentClick.getMot());

            if (newString == null)
            {
                myText.text = myText.text.Insert(currentClick.getPosStart(), myInputField.text);
                if (!SC_GM.gm.tabInputStrings.Contains(myInputField.text))
                    SC_GM.gm.tabInputStrings.Add(myInputField.text);
            }
            else
            {
                myText.text = myText.text.Insert(currentClick.getPosStart(), newString);
                if (!SC_GM.gm.tabInputStrings.Contains(newString))
                    SC_GM.gm.tabInputStrings.Add(newString);
            }
        }
    }

    /*
     * Récupère les infos de la balise clicker
     */
    private void GetClickInfo(int linkIndex)
    {
        int pos = 0;
        TMP_LinkInfo linkInfo = myText.textInfo.linkInfo[linkIndex];

        while ((++pos) < linkInfo.linkIdFirstCharacterIndex)
            pos = myText.text.IndexOf(linkInfo.GetLinkText(), pos);

        myInputField.text = linkInfo.GetLinkText();

        currentClick = new SC_ClickObject(pos - 1, linkInfo.GetLinkText());
    }

    /*
     * Change les liste a afficher et a stocker quand la valeur de l'inputfield change
     */
    public void onInputFieldValueChange()
    {
        if (myInputField.text.Length >= autoCompileLenght)
        {
            // On ajoute une lettre donc on enlève les mots qui ne contiennent plus le text courant de l'inputfield
            IEnumerable<string> toRemove = toDisplay.Where(x => !x.Contains(myInputField.text)).ToArray();
            foreach (string mot in toRemove)
            {
                toDisplay.Remove(mot);
                toStore.Add(mot);
            }

            // On supprime une lettre donc on ajoute les mots qui contiennent le text courant de l'inputfield
            IEnumerable<string> toAddBack = toStore.Where(x => x.Contains(myInputField.text)).ToArray();
            foreach (string mot in toAddBack)
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

        foreach (string mot in toStore)
            for (int i = 0; i < tupleButtons.Length; i++)
                if (tupleButtons[i].Item2 == true && tupleButtons[i].Item1.GetComponentInChildren<TextMeshProUGUI>().text == mot)
                {
                    CloseButton(i);
                    break;
                }
        foreach (string mot in toDisplay)
        {
            find = false;
            for (int i = 0; i < tupleButtons.Length; i++)
                if (tupleButtons[i].Item2 == true && tupleButtons[i].Item1.GetComponentInChildren<TextMeshProUGUI>().text == mot)
                {
                    find = true;
                    break;
                }

            if (!find)
                for (int i = 0; i < tupleButtons.Length; i++)
                    if (tupleButtons[i].Item2 == false)
                    {
                        tupleButtons[i].Item1.GetComponentInChildren<TextMeshProUGUI>().text = mot;
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
