using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMRecruitmentData : MonoBehaviour
{
    public static string currentGirlName; 
    public static int currentGirlNumber;

    public static List<GirlClass> recruitedGirlsList = new List<GirlClass>();

    public static List<GirlClass> firedGirlsList = new List<GirlClass>();



    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GMRecruitmentData s_Instance = null;


    // A static property that finds or creates an instance of the manager object and returns it.
    public static GMRecruitmentData instance
    {
        get
        {
            if (s_Instance == null)
            {
                // FindObjectOfType() returns the first GMClubData object in the scene.
                s_Instance = FindObjectOfType(typeof(GMRecruitmentData)) as GMRecruitmentData;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("GMRecruitmentData");
                s_Instance = obj.AddComponent<GMRecruitmentData>();
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(recruitedGirlsList == null)
            recruitedGirlsList = new List<GirlClass>();

        if(currentGirlName == null)
        {
            currentGirlName = GameMasterGlobalData.girlsList[0].name;
            currentGirlNumber = 0;
        }
        
    }
}
