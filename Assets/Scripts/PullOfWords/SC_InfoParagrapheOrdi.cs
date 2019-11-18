using UnityEngine;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi info;

    public void OnClickParagrapheOrdi()
    {
        bool[] tabBool = new bool[SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].score.Length];
        for (int i = 0; i < info.motAccepter.Length; i++)
            if (info.motAccepter[i])
            {
                (string, Word, bool[]) elem = (info.champLexical.fichierWords.name, info.champLexical.words[i], tabBool);
                if (!SC_GM_Master.gm.choosenWords.Contains(elem))
                    SC_GM_Master.gm.choosenWords.Add(elem);
            }
    }
}
