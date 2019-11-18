using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_PullOfWord : MonoBehaviour
{
    // Nombre d'elem dans un CL
    private int numberOfElemInCL = 9;
    private int posElemCl = 4;

    // Liste des dropdown
    private List<string> listCritere = new List<string> {"Titre", "Verb", "Noun", "Adjectif"};
    private List<string> listPerso = new List<string> {"General"};

    // Object de la fenetre
    public TMP_Dropdown critere;
    public TMP_Dropdown perso;
    public GameObject myButtons;

    // Liste des CL, Words et infos les concernants
    private LayoutGroup[] allChampLexicaux;
    private bool[] isDisplay;
    private TextMeshProUGUI[][] champsLexicauxAndWords;
    private bool[][] toDisplay;

    /*
     * Init les CL et les Dropdown
     */
    private void Start()
    {
        for (int i = SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].critere.Length + 1; i < SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].name.Length; i++)
            listPerso.Add(SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].name[i]);

        critere.AddOptions(listCritere);
        perso.AddOptions(listPerso);


        allChampLexicaux = myButtons.GetComponentsInChildren<LayoutGroup>(true);

        champsLexicauxAndWords = new TextMeshProUGUI[allChampLexicaux.Length][];
        toDisplay = new bool[allChampLexicaux.Length][];
        for (int i = 0; i < allChampLexicaux.Length; i++)
        {
            toDisplay[i] = new bool[numberOfElemInCL];
            for (int j = 0; j < numberOfElemInCL; j++)
            {
                champsLexicauxAndWords[i] = allChampLexicaux[i].GetComponentsInChildren<TextMeshProUGUI>(true);
                toDisplay[i][j] = false;
            }
        }


        if (allChampLexicaux.Length == SC_GM_Master.gm.listChampsLexicaux.nameChampsLexicals.Length)
            for (int i = 0; i < allChampLexicaux.Length; i++)
                champsLexicauxAndWords[i][posElemCl].text = SC_GM_Master.gm.listChampsLexicaux.nameChampsLexicals[i];
        else
            Debug.LogError("Il faut le même nombre de champs lexicaux dans GM_Master que de champs lexicaux à afficher dans le Pull");


        isDisplay = new bool[allChampLexicaux.Length];
        for (int i = 0; i < allChampLexicaux.Length; isDisplay[i++] = false);
    }

    /*
     * Met a jour le pull de mot lorsqu'on click sur des paragraphe a l'ordi
     */
    private void Update()
    {
        foreach ((string, Word, bool[]) elem in SC_GM_Master.gm.choosenWords)
            for (int i = 0; i < champsLexicauxAndWords.Length; i++)
                if (champsLexicauxAndWords[i][posElemCl].gameObject.activeSelf == false && champsLexicauxAndWords[i][posElemCl].text == elem.Item1)
                    champsLexicauxAndWords[i][posElemCl].gameObject.SetActive(true);

    }

    /*
     * Récupère et traite le click sur un CL
     */
    public void OnClickNameChampLexical (LayoutGroup lg)
    {
        int index = -1;

        for (int i = 0; i < allChampLexicaux.Length; i++)
            if (allChampLexicaux[i] == lg)
            {
                index = i;
                break;
            }

        if (!isDisplay[index])
            WordToDisplay(index);
        else
            ClearAllWords(index);

        EnableDisable(index);
    }

    /*
     * Met à jour les text des CL avec les mot à afficher
     */
    private void WordToDisplay(int index)
    {
        int pos;
        isDisplay[index] = true;

        foreach ((string, Word, bool[]) elem in SC_GM_Master.gm.choosenWords)
            if (elem.Item1 == champsLexicauxAndWords[index][posElemCl].text)
                if (critere.captionText.text == listCritere[0] && elem.Item2.titre != "none") // Titre
                {
                    pos = GetFirstMotLibre(index);
                    if (pos != -1)
                        champsLexicauxAndWords[index][pos].text = elem.Item2.titre;
                }
                else if (critere.captionText.text == listCritere[1] && elem.Item2.critere[0] != "none") // Verb
                {
                    pos = GetFirstMotLibre(index);
                    if (pos != -1)
                        champsLexicauxAndWords[index][pos].text = elem.Item2.critere[0];
                }
                else if (critere.captionText.text == listCritere[2] && elem.Item2.critere[3] != "none") // Noun
                {
                    pos = GetFirstMotLibre(index);
                    if (pos != -1)
                        champsLexicauxAndWords[index][pos].text = elem.Item2.critere[3];
                }
                else if (critere.captionText.text == listCritere[3] && elem.Item2.critere[4] != "none") // Adjectif
                {
                    pos = GetFirstMotLibre(index);
                    if (pos != -1)
                        champsLexicauxAndWords[index][pos].text = elem.Item2.critere[4];
                }
    }

    /*
    * Trouve le premier champ non utilisé du CL, l'active et retoune sa position
    */
    private int GetFirstMotLibre(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl && !toDisplay[index][i])
            {
                toDisplay[index][i] = true;
                return i;
            }

        return -1;
    }

    /*
     * Enleve l'affichage de tous les mots du CL
     */
    private void ClearAllWords(int index)
    {
        isDisplay[index] = false;

        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl)
                toDisplay[index][i] = false;
    }

    /*
    * Affiche ou non les mots d'un CL
    */
    private void EnableDisable(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl)
                champsLexicauxAndWords[index][i].gameObject.SetActive(toDisplay[index][i]);

    }

    /*
     * Gere l'interface losqu'on change de critère.
     */
    public void FullEnableDisable()
    {
        for (int i = 0; i < allChampLexicaux.Length; i++)
            if (isDisplay[i])
            {
                ClearAllWords(i);
                WordToDisplay(i);
                EnableDisable(i);
            }
    }

    /*
     * Ajoute le mot clicker dans la wheel si l'on est sur 'Général' et 'Titre'
     */
     public void AddWordInWheel (TextMeshProUGUI tmp)
    {
        if (critere.captionText.text != listCritere[0] || perso.captionText.text != listPerso[0])
            return;

        for (int i = 0; i < champsLexicauxAndWords.Length; i++)
            for (int j = 0; j < champsLexicauxAndWords[i].Length; j++)
                if (champsLexicauxAndWords[i][j] == tmp) // cherche le TMP_UGUI sur lequel on a clicker
                    foreach ((string, Word, bool[]) elem in SC_GM_Master.gm.choosenWords)
                        if (elem.Item1 == champsLexicauxAndWords[i][posElemCl].text &&
                            ((champsLexicauxAndWords[i][j].text == elem.Item2.titre) || (champsLexicauxAndWords[i][j].text == elem.Item2.critere[0]) ||
                            (champsLexicauxAndWords[i][j].text == elem.Item2.critere[3]) || (champsLexicauxAndWords[i][j].text == elem.Item2.critere[4]))) // cherche quel mot de la BD il correpond
                        {
                            SC_GM.gm.wheelOfWords.Add(elem.Item2);
                            return;
                        }

    }
}
