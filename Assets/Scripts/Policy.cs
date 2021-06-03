using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Policy
{
    //The name of this policy
    public string name;

    //The basic description of this policy
    public string description;

    //The detailed description of this policy
    public string detailedDescription;

    //The cost of this policy, in money units
    public int priceMoney;

    //The cost of this policy, in influence
    public float priceInfluence;

    //The cost of this policy, in connection
    public float priceConnection;

    //The cost of this policy, in reputation
    public float priceReputation;

    //The name of the image
    public string imgPath;

    //The minimum openness required that at least one girl must have to buy this policy
    public float minOpenness;

    //The list of predecessors required to pass this policy
    [System.NonSerialized]
    public List<Policy> predecessors = new List<Policy>();

    //The list of predecessors required to pass this policy, in the form of a list of names, so that they can be generated via JSON
    public List<string> predecessorsName;

    //Creates the predecessors of this policy in terms of objects
    public void CreatePredecessors()
    {
        foreach (string s in predecessorsName) {
            
            foreach (Policy pol in GameMasterGlobalData.policiesList)
            {
                if (pol.name == s)
                {
                    predecessors.Add(pol);
                }
            }
        }
    }
}
