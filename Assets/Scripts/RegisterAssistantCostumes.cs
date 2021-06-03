using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterAssistantCostumes : MonoBehaviour
{
    public static List<AssistantCostume> assistantCostumes = new List<AssistantCostume>();

    public static void CreateAsssistantCostumes()
    {
        if (assistantCostumes.Count <= 0)
        {
            foreach (string s in StaticStrings.ASSISTANT_COSTUMES_DIRECTORIES_NAMES)
            {
                assistantCostumes.Add(AssistantCostume.CreateFromJSON(StaticStrings.ASSISTANT_RESSOURCES_FOLDER +
                    StaticStrings.ASSISTANT_WARDROBE_FOLDER +
                    s + StaticStrings.ASSISTANT_COSTUME_JSON_FILE_NO_EXTENSION));
            }
            StaticAssistantData.data.currentCostume = FindAssistantCostumeWithName(StaticStrings.ASSISTANT_STARTING_OUTFIT);
        }
    }

    public static AssistantCostume FindAssistantCostumeWithName(string name)
    {
        foreach(AssistantCostume ac in assistantCostumes)
        {
            if (ac.name.Equals(name))
            {
                return ac;
            }
        }
        Debug.LogError("This assistant costume name does not exist.");
        return null;
    }
}
