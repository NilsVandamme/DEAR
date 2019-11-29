using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesToLoad : MonoBehaviour
{
    public Animator BossAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene() => SC_LoadingScreen.Instance.LoadThisScene();
}
