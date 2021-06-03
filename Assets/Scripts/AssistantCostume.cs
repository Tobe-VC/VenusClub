using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssistantCostume
{
    public string name;

    public List<string> subNames;
    public List<string> description;

    public int currentLevel;

    public int maxLevel;

    public List<string> imagesPaths;

    //False if the improvement should not be shown in list
    public bool bought = false;

    public string directoryName;

    [System.NonSerialized]
    public List<Sprite> sprites = new List<Sprite>();

    public bool MaxLevelWillBeReached()
    {
        return currentLevel == maxLevel - 1;
    }

    public Sprite GetCurrentCostume()
    {
        return sprites[StaticAssistantData.data.currentCostumeLevel];
    }

    public void IncreaseLevel()
    {
        if(currentLevel < maxLevel - 1)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = maxLevel - 1;
        }
    }

    public static AssistantCostume CreateFromJSON(string path)
    {
        AssistantCostume costume = JsonUtility.FromJson<AssistantCostume>(Resources.Load<TextAsset>(path).text);

        if(costume.maxLevel != costume.imagesPaths.Count)
        {
            Debug.LogError("Costume max level different from the number of images");
        }

        costume.sprites = new List<Sprite>();

        for(int i = 0; i < costume.maxLevel; i++)
        {
            costume.sprites.Add(Resources.Load<Sprite>(
                StaticStrings.ASSISTANT_RESSOURCES_FOLDER + 
                StaticStrings.ASSISTANT_IMAGES_FOLDER + 
                StaticStrings.ASSISTANT_OUTFITS_FOLDER + 
                costume.directoryName + costume.imagesPaths[i]));
        }

        return costume;
    }
}
