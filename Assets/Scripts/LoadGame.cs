using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    
    public void Load()
    {
        SceneManager.LoadScene(StaticStrings.LOAD_SCENE);
    }
    
}
