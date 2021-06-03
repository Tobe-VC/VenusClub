using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssistantStoreItem
{
    public string name;

    public string description;

    public List<string> detailedDescription;

    public List<int> priceAssistantPoints;

    public int currentLevel;

    public int maxLevel;

    public List<string> imagesPaths;

    public List<string> subNames;

    //False if the item should not be shown in list
    public bool showInList = true;

    public string directoryName;

    public bool thirdLateNightMeetingRequired = false;

    public bool MaxLevelWillBeReached()
    {
        return currentLevel == maxLevel - 1;
    }
}
