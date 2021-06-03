using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssistantCostumesLevels
{
    public int basicCostume;
    public int workoutClothes;
    public int teacherCostume;
    public int sexyCasualClothes;
}

[System.Serializable]
public class AssistantData
{
    public int assistantPoints = 0;
    public bool thirdMeetingDialogBought = false;
    public bool assistantStoreUnlocked = false;
    public int datesAvailableToday;
    public int datesAvailablePerDay = GMGlobalNumericVariables.gnv.MINIMUM_DATES_PER_DAY;

    public AssistantCostume currentCostume;
    public int currentCostumeLevel;
}

public static class StaticAssistantData
{
    public static AssistantData data;

    public static List<AssistantStoreItem> boughtStoreItems = new List<AssistantStoreItem>();
}
