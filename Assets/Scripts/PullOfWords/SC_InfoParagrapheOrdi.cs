using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi info;
    public TextMeshProUGUI button;

    public void OnClickParagrapheOrdi()
    {
        button.text = info.champLexical.fichierWords.name;
        button.gameObject.SetActive(true);
    }

    public void OnClickButtonConfirm()
    {
        bool[] tabBool = new bool[SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].score.Length];
        for (int i = 0; i < info.motAccepter.Length; i++)
            if (info.motAccepter[i])
            {
                SC_WordInPull elem = new SC_WordInPull(info.champLexical.fichierWords.name, info.champLexical.words[i], tabBool);

                foreach (SC_WordInPull wordPull in SC_GM_Master.gm.choosenWords)
                    if (wordPull.GetWord().titre == elem.GetWord().titre)
                        return;

                SC_GM_Master.gm.choosenWords.Add(elem);
            }
    }
}
