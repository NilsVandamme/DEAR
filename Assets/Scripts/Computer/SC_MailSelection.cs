using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_MailSelection : MonoBehaviour
{
    public Color SelectedColor;
    public GameObject mailTextsListParent;
    public GameObject mailButtonsListParent;
    public List<GameObject> mailTextsList;
    public List<Image> mailButtonsList;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in mailTextsListParent.transform)
        {
                mailTextsList.Add(child.gameObject);
        }

        foreach (Transform child in mailButtonsListParent.transform)
        {
                mailButtonsList.Add(child.gameObject.GetComponent<Image>());
        }
    }

    public void OpenMailText(int index)
    {
        for (int i = 0; i < mailTextsList.Count; i++)
        {
            mailTextsList[i].SetActive(false);
        }

        mailTextsList[index].SetActive(true);

        for (int j = 0; j < mailButtonsList.Count; j++)
        {
            mailButtonsList[j].color = Color.white;
        }

        mailButtonsList[index].color = SelectedColor;
    }
}
