using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BossHelp : MonoBehaviour
{
    public static SC_BossHelp instance;

    public GameObject Close;
    public GameObject Rings;
    public GameObject Bubbles;

    public List<GameObject> bossHelpClose;
    public List<GameObject> bossHelpOpen;
    public List<GameObject> helpbubbles;

    private bool tutoInfosDone;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //Debug.Log("start boss help");
        for (int i = 0; i < Rings.transform.childCount; i++)
        {
            bossHelpOpen.Add(Rings.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Close.transform.childCount; i++)
        {
            bossHelpClose.Add(Close.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Bubbles.transform.childCount; i++)
        {
            helpbubbles.Add(Bubbles.transform.GetChild(i).gameObject);
        }

    }

    public void OpenBossHelp(int index)
    {
        
        for(int i = 0; i < bossHelpOpen.Count; i++)
        {
            bossHelpOpen[i].SetActive(false);
        }

        if (index >= 0)
        {
            if(index != 1)
            {
                Debug.Log("OpenBossHelp running " + bossHelpOpen[index].name);
                bossHelpOpen[index].SetActive(true);
            }
            else
            {
                if (tutoInfosDone == false)
                {
                    Debug.Log("OpenBossHelp running " + bossHelpOpen[index].name);
                    bossHelpOpen[index].SetActive(true);
                }
            }

        }

    }

    public void CloseBossHelp(int index)
    {
       
        for (int i = 0; i < bossHelpClose.Count; i++)
        {
            bossHelpClose[i].SetActive(false);
        }

        if (index >= 0)
        {
            if (index != 1)
            {
                Debug.Log("CloseBossHelp running " + bossHelpClose[index].name);
                bossHelpClose[index].SetActive(true);
            }
            else
            {
                if (tutoInfosDone == false)
                {
                    Debug.Log("CloseBossHelp running " + bossHelpClose[index].name);
                    bossHelpClose[index].SetActive(true);
                }
            }

        }
        

    }

    public void OpenBossBubble(int index)
    {
       
        for (int i = 0; i < helpbubbles.Count; i++)
        {
            helpbubbles[i].SetActive(false);
        }

        if(index >= 0)
        {
            if (index != 1)
            {
                Debug.Log("OpenBossBubble running " + helpbubbles[index].name);
                helpbubbles[index].SetActive(true);
            }
            else
            {
                if (tutoInfosDone == false)
                {
                    Debug.Log("OpenBossBubble running " + helpbubbles[index].name);
                    helpbubbles[index].SetActive(true);
                }
            }
        }      
    }

    public void TutoInfoIsDone()
    {
        tutoInfosDone = true;
    }
}
