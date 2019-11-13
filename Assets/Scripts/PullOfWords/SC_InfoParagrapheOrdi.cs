using UnityEngine;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi info;

    public void OnClickParagrapheOrdi()
    {
        for (int i = 0; i < info.motAccepter.Length; i++)
            if (info.motAccepter[i])
            {
                (string, Word, bool) elem = (info.champLexical.fichierWords.name, info.champLexical.words[i], false);
                if (!SC_GM_Master.gm.choosenWordInMail.Contains(elem))
                {
                    SC_GM_Master.gm.choosenWordInMail.Add(elem);
                    Debug.Log(elem.Item2.titre);
                }
            }
    }
}
