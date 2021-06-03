using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImprovementsData
{
    public bool gym = false;
    public bool bar = false;
    public bool cigarettesDispenser = false;
    public bool pharmacy = false;
    public bool condomDispenser = false;
    public bool stage = false;
    public bool photostudio = false;
    public bool foreplayBooth = false;
    public bool oralBooth = false;
    public bool sexBooth = false;
    public bool groupRoom = false;
    public bool interactionRoom = false;

    //True if the auto condom dispenser is owned
    public bool autoCondom = false;    
    //True if the auto bar is owned
    public bool autoDrink = false;    
    //True if the auto drug dispenser is owned
    public bool autoDrug = false;    
    //True if the auto cigarette dispenser is owned
    public bool autoCigs = false;

    ///An array representing which booth are modular
    ///Since there are only 6 booths, there are 6 booleans in the array, on for each booth, in the same order ([0] is the first booth...)
    private bool[] modularBooths = new bool[]{ false, false, false, false, false, false };

    public bool[] GetModularBooths()
    {
        if(modularBooths == null)
        {
            modularBooths = new bool[] { false, false, false, false, false, false };
            
        }
        return modularBooths;
    }

    public void SetModularBooths(int index)
    {
        if (modularBooths == null)
        {
            modularBooths = new bool[] { false, false, false, false, false, false };

        }
        modularBooths[index] = true;
    }

}

public static class GMImprovementsData
{

    public static ImprovementsData improvementsData = new ImprovementsData();

    public static List<Improvement> boughtImprovements = new List<Improvement>();

    public static bool IsImprovementOwned(string s, int level)
    {
        foreach (Improvement imp in boughtImprovements)
        {
            if (s.ToLower().Equals(imp.name.ToLower()))
            {
                return level <= imp.currentLevel;
            }
        }
        return false;
    }

}

