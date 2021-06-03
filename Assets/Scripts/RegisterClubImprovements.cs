using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterClubImprovements : MonoBehaviour
{

    public List<ClubImprovement> clubImprovements = new List<ClubImprovement>();


    void Awake()
    {
        if(GameMasterGlobalData.clubImprovementList == null)
        {

        }
    }
}
