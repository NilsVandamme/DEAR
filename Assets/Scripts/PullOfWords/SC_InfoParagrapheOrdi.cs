using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi info;
    public TextMeshProUGUI button;

    public void OnClickParagrapheOrdi()
    {
        Debug.Log(gameObject.name + " paragraph has been clicked");
        button.text = info.champLexical.fichierWords.name;
        button.gameObject.SetActive(true);
    }

    public void OnClickButtonConfirm()
    {
        Debug.Log(gameObject.name + " button has been clicked");
        bool[] tabBool = new bool[SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].score.Length];
        for (int i = 0; i < info.motAccepter.Length; i++)
            if (info.motAccepter[i])
            {
                SC_WordInPull elem = new SC_WordInPull(info.champLexical.fichierWords.name, info.champLexical.words[i], tabBool);
                if (!SC_GM_Master.gm.choosenWords.Contains(elem))
                    SC_GM_Master.gm.choosenWords.Add(elem);
            }
    }
}
