using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ClientSelection : MonoBehaviour
{
    public GameObject WindowInfos;
    public SC_WindowTopBar wtb;
    public List<GameObject> CharactersProfilesList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenClientInfos(int index)
    {
        WindowInfos.SetActive(true);
        wtb.MaximizeWindow();
        WindowInfos.transform.SetAsLastSibling();
        for (int i =0; i < CharactersProfilesList.Count; i++)
        {
            CharactersProfilesList[i].SetActive(false);
        }

        CharactersProfilesList[index].SetActive(true);
    }

}
