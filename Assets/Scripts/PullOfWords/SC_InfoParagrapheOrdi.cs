using UnityEngine;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi info;

    public void OnClickParagrapheOrdi()
    {
        for (int i = 0; i < info.motAccepter.Length; i++)
        {
            if (info.motAccepter[i])
            {
                Debug.Log(info.champLexical.words[i].titre);
            }

        }
    }
}
