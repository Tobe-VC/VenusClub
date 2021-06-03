using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoliciesData
{
    public bool dance = false;
    public bool danceCloser = false;
    public bool danceTopless = false;

    public bool poseNaked = false;
    public bool soloHand = false;
    public bool mastToy = false;

    public bool handjob = false;
    public bool footjob = false;
    public bool titsjob = false;

    public bool blowjob = false;
    public bool deepthroat = false;
    public bool facefuck = false;

    public bool facingVaginal = false;
    public bool backVaginal = false;
    public bool anal = false;

    public bool threesome = false;
    public bool foursome = false;
    public bool orgy = false;

    public bool creampie = false;
    public bool analCreampie = false;

    public bool bodyFinish = false;
    public bool titsFinish = false;

    public bool facial = false;
    public bool swallow = false;

    public bool threesomeFinish = false;
    public bool foursomeFinish = false;
}

public class GMPoliciesData : MonoBehaviour
{
    public static PoliciesData policies = new PoliciesData();

    public static List<Policy> ownedPolicies = new List<Policy>();

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static GMPoliciesData s_Instance = null;


    // A static property that finds or creates an instance of the manager object and returns it.
    public static GMPoliciesData instance
    {
        get
        {
            if (s_Instance == null)
            {
                // FindObjectOfType() returns the first GMClubData object in the scene.
                s_Instance = FindObjectOfType(typeof(GMPoliciesData)) as GMPoliciesData;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("GMPoliciesData");
                s_Instance = obj.AddComponent<GMPoliciesData>();
            }

            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }

    public static bool IsPolicyOwned(string s)
    {
        foreach (Policy pol in ownedPolicies)
        {
            if (s.ToLower().Equals(pol.name.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

}
