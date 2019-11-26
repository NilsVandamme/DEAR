using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_PullOfWord : MonoBehaviour
{
    // Object de la fenetre
    public TMP_Dropdown critere;
    public TMP_Dropdown perso;
    public GameObject myButtons;

    // Couleur des mots en fonction des points
    private Dictionary<int, Color> color = 
        new Dictionary<int, Color>()
        {
            {-3, Color.red},
            {-2, new Color32(255, 128, 0, 255)},
            {-1, Color.yellow},
            {1, new Color32(128, 255, 0, 255)},
            {2, Color.green},
            {3, new Color32(0, 255, 128, 255)}
        };

    // Nombre d'elem dans un CL
    private int numberOfElemInCL = 9;
    private int posElemCl = 4;

    // Liste des dropdown
    private List<string> listCritere = new List<string> {"Titre", "Verb", "Noun", "Adjectif"};
    private List<string> listPerso = new List<string> {"General"};

    // Liste des CL, Words et infos les concernants
    private LayoutGroup[] allChampLexicaux;
    private bool[] isDisplay;
    private TextMeshProUGUI[][] champsLexicauxAndWords;
    private bool[][] toDisplay;

    // Info sur les Words
    private string[] namePerso;
    private Dictionary<string, int> critereOfWord = 
        new Dictionary<string, int>()
        {
            {"Verb", 0},
            {"Noun", 3},
            {"Adjectif", 4}
        };


    /*
     * Init les CL et les Dropdown
     */
    private void Start()
    {
        int x = 0;
        namePerso = new string[SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].score.Length];
        for (int i = SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].critere.Length + 1; i < SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].name.Length; i++)
        {
            listPerso.Add(SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].name[i]);
            namePerso[x++] = SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].name[i];
        }

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
        foreach (SC_WordInPull elem in SC_GM_Master.gm.choosenWords)
            for (int i = 0; i < champsLexicauxAndWords.Length; i++)
                if (champsLexicauxAndWords[i][posElemCl].gameObject.activeSelf == false && champsLexicauxAndWords[i][posElemCl].text == elem.GetCL())
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

        foreach (SC_WordInPull elem in SC_GM_Master.gm.choosenWords)
            if (elem.GetCL() == champsLexicauxAndWords[index][posElemCl].text)
            {
                pos = GetFirstMotLibre(index);
                if (pos != -1)
                {
                    if (critere.captionText.text == listCritere[0] && elem.GetWord().titre != "none") // Titre
                    {
                        toDisplay[index][pos] = true;
                        champsLexicauxAndWords[index][pos].text = elem.GetWord().titre;
                    }

                    foreach (KeyValuePair<string, int> item in critereOfWord)
                        if (critere.captionText.text == item.Key && elem.GetWord().critere[item.Value] != "none") // Critere
                        {
                            toDisplay[index][pos] = true;
                            champsLexicauxAndWords[index][pos].text = elem.GetWord().critere[item.Value];
                        }
                }
            }
    }

    /*
    * Trouve le premier champ non utilisé du CL, l'active et retoune sa position
    */
    private int GetFirstMotLibre(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl && !toDisplay[index][i])
                return i;

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
                {
                    SC_WordInPull mot = getWordInPull(i, j);
                    if (mot != null)
                    {
                        SC_GM.gm.wheelOfWords.Add(mot.GetWord());
                        return;
                    }

                }
    }

    /*
     * Met en couleur les mots en fonction de leur score aux personnages
     */
     public void OnValueChangePerso ()
    {
        // Remet tous les mots en noir
        if (perso.captionText.text == listPerso[0])
            foreach (TextMeshProUGUI[] liste in champsLexicauxAndWords)
                foreach (TextMeshProUGUI mot in liste)
                    mot.color = Color.black;

        // Met les mots en couleurs
        else
        {
            string perso = GetPersoInPull();
            for (int i = 0; i < namePerso.Length; i++)
                if (perso == namePerso[i]) // Trouve l'indice pour le score du perso
                    for (int k = 0; k < champsLexicauxAndWords.Length; k++)
                        for (int l = 0; l < champsLexicauxAndWords[k].Length; l++)
                        {
                            SC_WordInPull mot = getWordInPull(k, l);
                            if (mot != null && mot.GetUsed()[i]) champsLexicauxAndWords[k][l].color = color[mot.GetWord().score[i]];
                        }
        }
    }

    /*
     * Récupere le perso selectionner dans le pull
     */
    private string GetPersoInPull ()
    {
        for (int i = 1; i < listPerso.Count; i++)
            if (perso.captionText.text == listPerso[i])
                return listPerso[i];

        return null;
    }

    /*
     * Trouve le mot du pull correspondant au TMP_UGUI (champsLexicauxAndWords[i][j])
     */
    private SC_WordInPull getWordInPull(int i, int j)
    {
        foreach (SC_WordInPull elem in SC_GM_Master.gm.choosenWords)
            if (elem.GetCL() == champsLexicauxAndWords[i][posElemCl].text)
            {
                if (champsLexicauxAndWords[i][j].text == elem.GetWord().titre)
                    return elem;

                foreach (KeyValuePair<string, int> item in critereOfWord)
                    if (champsLexicauxAndWords[i][j].text == elem.GetWord().critere[item.Value])
                        return elem;
            }

        return null;

    }
}
