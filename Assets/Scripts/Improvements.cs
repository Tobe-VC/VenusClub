using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Improvement
{
    public string name;

    public string description;

    public List<string> detailedDescription;

    public List<int> priceMoney;
    public List<int> priceInfluence;
    public List<int> priceConnection;

    public int currentLevel;

    public int maxLevel;

    public List<string> imagesPaths;

    public List<string> subNames;

    //False if the improvement should not be shown in list
    public bool showInList = true;

    public bool MaxLevelWillBeReached()
    {
        return currentLevel == maxLevel - 1;
    }

    /*
    public Improvement(Improvement imp)
    {
        this.name = imp.name;
        this.description = imp.description;
        this.detailedDescription = imp.detailedDescription;
        this.priceMoney = imp.priceMoney;
        this.priceInfluence = imp.priceInfluence;
        this.priceConnection = imp.priceConnection;
        this.currentLevel = imp.currentLevel;
        this.maxLevel = imp.maxLevel;
        this.imagesPaths = imp.imagesPaths;
        this.subNames = imp.subNames;
        this.showInList = true;
    }
    */
}
