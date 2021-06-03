using System.Collections.Generic;

[System.Serializable]
public class Costume
{
    public string name;

    public string[] subNames;

    public string description;
    //public string[] subDescriptions;

    //public string detailedDescription;
    public string[] subDetailedDescriptions;

    public int[] pricesMoney;
    public int[] pricesInfluence;
    public int[] pricesConnection;

    public int maxLevel = 3;

    public int currentLevel = -1;

    public string[] imagesPaths;

    //False if the costume should not be shown in the list
    public bool showInList = true;
}