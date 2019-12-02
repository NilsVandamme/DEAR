using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SC_InfoParagrapheOrdi : MonoBehaviour
{
    public SC_ParagrapheOrdi info;
    public TextMeshProUGUI collect;
    public TextMeshProUGUI pull;
    public Image image;
    public Animator ArboAnim;
    public Color HighlightColor;
    public Color TakenColor;

    private bool validate = false;

    private void Start()
    {
        pull.text = SC_GM.gm.numberOfCLRecover.ToString() + "/" + SC_GM.gm.numberOfCLRecoverable.ToString();
    }

    public void OnClickParagrapheOrdi()
    {
        //Debug.Log("paragraph clicked running");
        if (!validate)
        {
            image.gameObject.SetActive(!image.IsActive());
            image.color = HighlightColor;
            //button.text = info.champLexical.fichierWords.name;
            collect.gameObject.SetActive(!collect.IsActive());

        }
    }

    public void OnClickButtonConfirm()
    {
        //Debug.Log("button confirm clicked");
        if (SC_GM.gm.numberOfCLRecover < SC_GM.gm.numberOfCLRecoverable)
        {
            Highlight();

            bool[] tabBool = new bool[SC_GM_Master.gm.listChampsLexicaux.listChampsLexicals[0].words[0].score.Length];
            for (int i = 0; i < info.motAccepter.Length; i++)
            {
                //Debug.Log("searching word loop running");
                if (info.motAccepter[i])
                {
                    //Debug.Log("confirm add word running");
                    SC_WordInPull elem = new SC_WordInPull(info.champLexical.fichierWords.name, info.champLexical.words[i], tabBool);

                    foreach (SC_WordInPull wordPull in SC_GM_Master.gm.choosenWords)
                        if (wordPull.GetWord().titre == elem.GetWord().titre)
                            return;

                    SC_GM_Master.gm.choosenWords.Add(elem);
                    //Debug.Log("added " + elem);
                }
            }

        }
    }

    private void Highlight ()
    {
        //Debug.Log("highlight running");
        SC_GM.gm.numberOfCLRecover++;
        pull.text = SC_GM.gm.numberOfCLRecover.ToString() + "/" + SC_GM.gm.numberOfCLRecoverable.ToString();
        validate = true;
        collect.gameObject.SetActive(!collect.IsActive());
        image.color = TakenColor;

        if (SC_GM.gm.numberOfCLRecover == SC_GM.gm.numberOfCLRecoverable)
        {
            ArboAnim.SetTrigger("ArboIsFull");
            SC_BossHelp.instance.CloseBossHelp(2);
            SC_BossHelp.instance.OpenBossBubble(2);
        }
    }
}
