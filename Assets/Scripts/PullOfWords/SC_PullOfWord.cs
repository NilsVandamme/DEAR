using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_PullOfWord : MonoBehaviour
{
    // Object de la fenetre
    public GameObject critere;
    public GameObject perso;
    public GameObject wheel;
    public GameObject cancelWheel;
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

    // Liste des criteres et perso
    private TextMeshProUGUI[] listOfCritere;
    private TextMeshProUGUI[] listOfPerso;
    private int idCurrentCritere = 0;
    private int idCurrentPerso = 0;
    private bool[][] hasWord;

    // Liste des CL, Words et infos les concernants
    private LayoutGroup[] allChampLexicaux;
    private TextMeshProUGUI[][] champsLexicauxAndWords;

    // Info sur les Words
    private Dictionary<string, int> critereOfWord = 
        new Dictionary<string, int>()
        {
            {"Verbs", 0},
            {"Nouns", 3},
            {"Adjectives", 4}
        };

    // Liste des mots de la wheel
    private (bool, int)[] hasWordInWheel;
    private TextMeshProUGUI[] listOfWheel;
    private TextMeshProUGUI[] listOfCancelWheel;
    private int currentChoosenCritere;

    private int[] valeurCritere = new int[] {1, 2, 3};
    private int[] numberOfWordPerCritere;

    public TextMeshProUGUI chooseXWord;


    //##############################################################################################################################################################
    //##############################################################################################################################################################
    //########################################################################        INIT           ###############################################################
    //##############################################################################################################################################################
    //##############################################################################################################################################################

    /*
     * Init les CL et les Dropdown
     */
    public void MyStart()
    {
        InitCritereAndPerso();
        InitWheel();
        InitCLAndWordInCL();
        WriteWordAndCL();
    }

    /*
     * Recupere la liste des critere et des perso. Met a jour les perso
     */
    private void InitCritereAndPerso()
    {
        listOfCritere = critere.GetComponentsInChildren<TextMeshProUGUI>();
        listOfPerso = perso.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].critere.Length + 1, j = 1;
            i < SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].name.Length;
            i++, j++)
            listOfPerso[j].text = SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].name[i];
    }

    /*
     * Recupere la liste des critere et des perso. Met a jour les perso
     */
    private void InitWheel()
    {
        listOfCancelWheel = cancelWheel.GetComponentsInChildren<TextMeshProUGUI>();
        listOfWheel = wheel.GetComponentsInChildren<TextMeshProUGUI>();
        hasWordInWheel = new (bool, int)[listOfCancelWheel.Length];

        numberOfWordPerCritere = new int[] { SC_GM.gm.numberOfWordForEachCritere.verb, SC_GM.gm.numberOfWordForEachCritere.noun, SC_GM.gm.numberOfWordForEachCritere.adjectif };

        Bonus();
    }

    /*
     * Recupere les objets de champs lexicaux et les mots qui sont dedans
     */
    private void InitCLAndWordInCL()
    {
        allChampLexicaux = myButtons.GetComponentsInChildren<LayoutGroup>(true);
        champsLexicauxAndWords = new TextMeshProUGUI[allChampLexicaux.Length][];
        for (int i = 0; i < allChampLexicaux.Length; i++)
            champsLexicauxAndWords[i] = allChampLexicaux[i].GetComponentsInChildren<TextMeshProUGUI>(true);
    }

    /*
     * Met a jour les zone de texte des CL et des mots inclus
     */
    private void WriteWordAndCL()
    {
        hasWord = new bool[allChampLexicaux.Length][];
        //Debug.Log("sc_gm_master = " + SC_GM_Master.gm.listChampsLexicaux.nameChampsLexicals.Length);
        if (allChampLexicaux.Length == SC_GM_Master.gm.listChampsLexicaux.nameChampsLexicals.Length)
            for (int i = 0; i < allChampLexicaux.Length; i++)
            {
                champsLexicauxAndWords[i][posElemCl].text = SC_GM_Master.gm.listChampsLexicaux.nameChampsLexicals[i];
                hasWord[i] = new bool[numberOfElemInCL];
                hasWord[i][posElemCl] = true;
            }
        else
            Debug.LogError("Il faut le même nombre de champs lexicaux dans GM_Master que de champs lexicaux à afficher dans le Pull");

        foreach (SC_WordInPull elem in SC_GM_Master.gm.choosenWords)
            for (int i = 0; i < champsLexicauxAndWords.Length; i++)
                if (elem.GetCL() == champsLexicauxAndWords[i][posElemCl].text)
                    champsLexicauxAndWords[i][GetFirstMotLibre(i)].text = elem.GetWord().titre;
    }

    /*
    * Trouve le premier champ non utilisé du CL, l'active et retoune sa position
    */
    private int GetFirstMotLibre(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl && !hasWord[index][i])
            {
                hasWord[index][i] = true;
                return i;
            }

        return -1;
    }

    private void Bonus(int val = 0)
    {
        if (SC_GM.gm.activeBonus)
        {
            for (int i = 0; i < numberOfWordPerCritere.Length; i++)
                if (numberOfWordPerCritere[i] != 0)
                {
                    numberOfWordPerCritere[i] -= val;
                    chooseXWord.text = "Choose " + numberOfWordPerCritere[i] + " " + listOfCritere[i + 1].text;
                    currentChoosenCritere = valeurCritere[i];
                    if (numberOfWordPerCritere[i] != 0)
                        return;
                }

            chooseXWord.text = "Selection Finish";
            currentChoosenCritere = 0;
        }
    }

    //##############################################################################################################################################################
    //##############################################################################################################################################################
    //#####################################################################          CRITERE           #############################################################
    //##############################################################################################################################################################
    //##############################################################################################################################################################

    /*
     * Récupère le click sur les critere et actualise en consequence
     */
    public void OnClickCritere (TextMeshProUGUI critere)
    {
        for (int i = 0; i < listOfCritere.Length; i++)
            if (listOfCritere[i].text == critere.text)
            {
                listOfCritere[idCurrentCritere].color = Color.white;
                idCurrentCritere = i;
                listOfCritere[i].color = Color.red;
            }

        FullEnableDisable();
    }

    /*
     * Gere l'interface losqu'on change de critère.
     */
    public void FullEnableDisable()
    {
        for (int i = 0; i < allChampLexicaux.Length; i++)
        {
            ClearAllWords(i);
            WordToDisplay(i);
        }
    }

    /*
     * Enleve l'affichage de tous les mots du CL
     */
    private void ClearAllWords(int index)
    {
        for (int i = 0; i < numberOfElemInCL; i++)
            if (i != posElemCl)
            {
                hasWord[index][i] = false;
                champsLexicauxAndWords[index][i].text = "";
            }
    }

    /*
     * Met à jour les text des CL avec les mot à afficher
     */
    private void WordToDisplay(int index)
    {
        int pos;

        foreach (SC_WordInPull elem in SC_GM_Master.gm.choosenWords)
            if (elem.GetCL() == champsLexicauxAndWords[index][posElemCl].text)
            {
                pos = GetFirstMotLibre(index);
                if (pos != -1)
                {
                    if (idCurrentCritere == 0) // Titre 
                    {
                        hasWord[index][pos] = true;
                        champsLexicauxAndWords[index][pos].text = elem.GetWord().titre;
                    }

                    foreach (KeyValuePair<string, int> item in critereOfWord)
                        if (listOfCritere[idCurrentCritere].text == item.Key) // Critere
                        {
                            hasWord[index][pos] = true;
                            champsLexicauxAndWords[index][pos].text = elem.GetWord().critere[item.Value];
                        }
                }
            }
    }

    //##############################################################################################################################################################
    //##############################################################################################################################################################
    //#######################################################################        PERSO           ###############################################################
    //##############################################################################################################################################################
    //##############################################################################################################################################################

    /*
     * Récupère le click sur les perso et actualise en consequence
     */
    public void OnClickPerso(TextMeshProUGUI perso)
    {
        for (int i = 0; i < listOfPerso.Length; i++)
            if (listOfPerso[i].text == perso.text)
            {
                listOfPerso[idCurrentPerso].color = Color.white;
                idCurrentPerso = i;
                listOfPerso[i].color = Color.red;
            }

        OnValueChangePerso();
    }

    /*
     * Met en couleur les mots en fonction de leur score aux personnages
     */
    private void OnValueChangePerso ()
    {
        // Remet tous les mots en noir
        if (idCurrentPerso == 0)
            foreach (TextMeshProUGUI[] liste in champsLexicauxAndWords)
                foreach (TextMeshProUGUI mot in liste)
                    mot.color = Color.white;

        // Met les mots en couleurs
        else
        {
            for (int i = 1; i < listOfPerso.Length; i++)
                if (listOfPerso[idCurrentPerso].text == listOfPerso[i].text) // Trouve l'indice pour le score du perso
                    for (int k = 0; k < champsLexicauxAndWords.Length; k++)
                        for (int l = 0; l < champsLexicauxAndWords[k].Length; l++)
                        {
                            SC_WordInPull mot = GetWordInPull(champsLexicauxAndWords[k][l].text);
                            if (mot != null && mot.GetUsed()[i]) champsLexicauxAndWords[k][l].color = color[mot.GetWord().score[i]];
                        }
        }
    }

    /*
     * Trouve le mot du pull correspondant au TMP_UGUI (champsLexicauxAndWords[i][j])
     */
    private SC_WordInPull GetWordInPull(string mot)
    {
        foreach (SC_WordInPull elem in SC_GM_Master.gm.choosenWords)
        {
            if (mot == elem.GetWord().titre)
                return elem;

            foreach (KeyValuePair<string, int> item in critereOfWord)
                if (mot == elem.GetWord().critere[item.Value])
                    return elem;
        }

        return null;

    }

    //##############################################################################################################################################################
    //##############################################################################################################################################################
    //#######################################################################          WHEEL           #############################################################
    //##############################################################################################################################################################
    //##############################################################################################################################################################

    /*
     * Ajoute le mot clicker dans la wheel si l'on est sur 'Général' et 'Titre'
     */
    public void AddWordInWheel(TextMeshProUGUI tmp)
    {
        if (SC_GM.gm.activeBonus && currentChoosenCritere != idCurrentCritere)
            return;

        int k = GetFirstMotInWheelLibre();
        if (k == -1)
            return;

        for (int i = 0; i < champsLexicauxAndWords.Length; i++)
            for (int j = 0; j < champsLexicauxAndWords[i].Length; j++)
                if (champsLexicauxAndWords[i][j] == tmp) // cherche le TMP_UGUI sur lequel on a clicker
                {
                    SC_WordInPull mot = GetWordInPull(champsLexicauxAndWords[i][j].text);
                    if (mot != null)
                    {
                        if (SC_GM.gm.wheelOfWords.Contains(mot.GetWord()))
                            return;

                        hasWordInWheel[k] = (true, idCurrentCritere);
                        listOfCancelWheel[k].text = "X";
                        listOfWheel[k].text = mot.GetWord().titre;
                        SC_GM.gm.wheelOfWords.Add(mot.GetWord());

                        Bonus(1);

                        return;
                    }

                }
    }

    /*
    * Trouve le premier mot de la wheel disponible et retoune sa position
    */
    private int GetFirstMotInWheelLibre()
    {
        for (int i = 0; i < listOfCancelWheel.Length; i++)
            if (!hasWordInWheel[i].Item1)
                return i;

        return -1;
    }

    /*
     * Remove le mot de la wheel
     */
     public void OnClickRemoveWordInWheel(TextMeshProUGUI tmp)
    {
        Debug.Log("remove word running");
        for (int i = 0; i < listOfCancelWheel.Length; i++)
            if (listOfCancelWheel[i] == tmp)
            {
                numberOfWordPerCritere[Array.IndexOf(valeurCritere, hasWordInWheel[i].Item2)]++;
                Bonus();

                hasWordInWheel[i] = (false, 0);
                SC_GM.gm.wheelOfWords.Remove(GetWordInPull(listOfWheel[i].text).GetWord());
                listOfWheel[i].text = "";
                listOfCancelWheel[i].text = "";
            }

    }

    //##############################################################################################################################################################
    //##############################################################################################################################################################
    //##################################################################          START WRITTING           #########################################################
    //##############################################################################################################################################################
    //##############################################################################################################################################################

    public void StartWritting()
    {
        // TO-DO
    }
}
