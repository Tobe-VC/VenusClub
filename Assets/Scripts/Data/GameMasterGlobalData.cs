using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterGlobalData : MonoBehaviour
{

    public static List<GirlClass> girlsList;

    public static List<Improvement> clubImprovementList;

    public static List<Policy> policiesList;

    public static List<Costume> costumeList = new List<Costume>();

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GameMasterGlobalData s_Instance = null;


    // A static property that finds or creates an instance of the manager object and returns it.
    public static GameMasterGlobalData instance
    {
        get
        {
            if (s_Instance == null)
            {
                // FindObjectOfType() returns the first GMClubData object in the scene.
                s_Instance = FindObjectOfType(typeof(GameMasterGlobalData)) as GameMasterGlobalData;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("GameMasterGlobalData");
                s_Instance = obj.AddComponent<GameMasterGlobalData>();
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }

}
