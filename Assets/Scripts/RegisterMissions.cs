using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RegisterMissions : MonoBehaviour
{
    /// <summary>
    /// Contains all the missions, past, present and future
    /// </summary>
    public static List<Mission> missions = new List<Mission>();

    /// <summary>
    /// Contains only the missions that are available right now and not active
    /// </summary>
    public static List<Mission> availableMissions = new List<Mission>();

    /// <summary>
    /// Contains all the active missions
    /// </summary>
    public static List<Mission> activeMissions = new List<Mission>();

    public static void CreateMissions()
    {


        Mission m;

        m = new Mission(GMGlobalNumericVariables.gnv.FIRST_MISSION_ID, "Recruiting", "Recruit 3 girls",
            delegate () { return !StaticBooleans.tutorialIsOn; },
            delegate () { },
            delegate () { return GMRecruitmentData.recruitedGirlsList.Count >= 3; },
            delegate () { GMClubData.EarnMoney(500); });

        missions.Add(m);

        m = new Mission(GMGlobalNumericVariables.gnv.SECOND_MISSION_ID, "Buying dispensers", "Buy all four basic dispensers",
            delegate () { return StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.FIRST_MISSION_ID).IsDone(); }, //Triggers if the first mission is done
            delegate () { },
            delegate () {
                return GMImprovementsData.improvementsData.cigarettesDispenser && GMImprovementsData.improvementsData.condomDispenser &&
                    GMImprovementsData.improvementsData.bar && GMImprovementsData.improvementsData.pharmacy;
                },
            delegate () { GMClubData.EarnReputation(10); });

        missions.Add(m);

        m = new Mission(GMGlobalNumericVariables.gnv.THIRD_MISSION_ID, "Openning up girls", "Bring a girl to 35 openness or more",
    delegate () { return StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.SECOND_MISSION_ID).IsDone(); }, //Triggers if the second mission is done
    delegate () { },
    delegate () {//The mission is accomplished if one girl has an openness of 35 or more
        foreach(GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if(girl.GetOpenness() >= 35)
            {
                return true;
            }
        }
        return false;
    },
    delegate () { });

        missions.Add(m);


        m = new Mission(GMGlobalNumericVariables.gnv.FOURTH_MISSION_ID, "Gaining in reputation", "Get up to 35 reputation or more",
            delegate () { return StaticFunctions.FindMissionWithID(GMGlobalNumericVariables.gnv.THIRD_MISSION_ID).IsDone(); }, //Triggers if the third mission is done
            delegate () { },
            delegate () {return GMClubData.GetReputation() >= 35;//The mission is accomplished if reputation is 35 or more
                        },
            delegate () { });

        missions.Add(m);

        m = new Mission(GMGlobalNumericVariables.gnv.UNLOCK_ANY_SERVICE_MISSION_ID, "Unlocking a service", "Unlock any service",
    delegate () { return StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.CRIME_SERVICE_TUTORIAL_DIALOG_ID).done; }, //Triggers if the crime service tutorial is done
    delegate () { },
    delegate () {
        return GMCrimeServiceData.unlockedCrimeServices.Count > 0;//The mission is accomplished if there is at least one service unlocked
            },
    delegate () {

    });

        missions.Add(m);

    }



}
