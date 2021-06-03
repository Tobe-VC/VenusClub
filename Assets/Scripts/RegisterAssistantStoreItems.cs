using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterAssistantStoreItems : MonoBehaviour
{
    public static List<AssistantStoreItem> assistantItemsList;

    public static void CreateStoreItems()
    {
        if (assistantItemsList.Count <= 0)
        {
            foreach (string d in StaticStrings.ASSISTANT_ITEMS_DIRECTORIES_NAMES)
            {
                AssistantStoreItem imp = JsonUtility.FromJson<AssistantStoreItem>(
                    Resources.Load<TextAsset>(StaticStrings.ASSISTANT_RESSOURCES_FOLDER +
                    StaticStrings.ASSISTANT_STORE_FOLDER + d + "/" + StaticStrings.ASSISTANT_STORE_ITEMS_DATA_FILE).text);

                assistantItemsList.Add(imp);
            }
        }

    }
}
