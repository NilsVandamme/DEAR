using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Objects with this script attached will persist between scenes

public class SC_DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
