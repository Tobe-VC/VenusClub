using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPersonalImprovements : MonoBehaviour
{
    public static List<PersonalImprovement> personalImprovements;

    public static void CreatePersonalImprovements()
    {
        if (personalImprovements.Count <= 0)
        {
            foreach (string d in StaticStrings.PERSONAL_IMPROVEMENTS_DIRECTORIES_NAMES)
            {
                PersonalImprovement imp = JsonUtility.FromJson<PersonalImprovement>(
                    Resources.Load<TextAsset>(StaticStrings.PERSONAL_IMPROVEMENTS_FOLDER + d + "/" + 
                    StaticStrings.PERSONAL_IMPROVEMENT_DATA_FILE).text);

                personalImprovements.Add(imp);
            }
        }

    }
}
