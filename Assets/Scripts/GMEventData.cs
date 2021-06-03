using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class GMEventData : MonoBehaviour
{
    public static List<IEvent> eventsList;

    private void IncreaseSex(int sexSkillGain, string girlName1, string girlName2)
    {
        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if (girl.name == girlName1 || girl.name == girlName2)
                girl.AddToSex(sexSkillGain);
        }
    }

    private void IncreaseForeplay(int skillGain, string girlName1, string girlName2)
    {
        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if (girl.name == girlName1 || girl.name == girlName2)
                girl.AddToForeplay(skillGain);
        }
    }

    private void IncreaseGroup(int skillGain, string girlName1, string girlName2)
    {
        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if (girl.name == girlName1 || girl.name == girlName2)
                girl.AddToGroup(skillGain);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        eventsList = new List<IEvent>();

        //******************************************************************* NEW EVENT*******************************************//
        VideoClip[] clip = Resources.LoadAll<VideoClip>(StaticStrings.EVENT_FOLDER + StaticStrings.DUO_GIRL_EVENT_FOLDER +
    StaticStrings.DUO_GIRL_EVENTS_DIRECTORIES_NAMES[0]);

        int foreplaySkillGain = 5;
        int groupSkillGain = 5;

        int moneyGain = 100;

        string girl1 = "Asa Akira";
        string girl2 = "Madison Ivy";

        //Creates an event for Asa Akira and Madison Ivy, that increases their foreplay skill
        DuoGirlEvent dge = new DuoGirlEvent(girl1,girl2,
    "Practicing together", "Looks like Asa and Madison decided to practice together. " +
    "This will improve their foreplay skills!\n\nForeplay skill +" + foreplaySkillGain + ".", clip[0]
    ,delegate (){ IncreaseForeplay(foreplaySkillGain, girl1,girl2);},
    0, 0);
    
        eventsList.Add(dge);

        //******************************************************************* NEW EVENT*******************************************//
        clip = Resources.LoadAll<VideoClip>(StaticStrings.EVENT_FOLDER + StaticStrings.DUO_GIRL_EVENT_FOLDER +
StaticStrings.DUO_GIRL_EVENTS_DIRECTORIES_NAMES[1]);

        foreplaySkillGain = 5;
        groupSkillGain = 5;

        moneyGain = 100;

        girl1 = "Aletta Ocean";
        girl2 = "Madison Ivy";

        dge = new DuoGirlEvent(girl1,girl2,
    "Practicing together", "Looks like Aletta and Madison decided to practice together. " +
    "This will improve their foreplay skills!\n\nForeplay skill +" + foreplaySkillGain + ".", clip[0]
    , delegate () { IncreaseForeplay(foreplaySkillGain, girl1,girl2);},
    GMGlobalNumericVariables.gnv.MIN_OPENNESS_FOREPLAY, GMGlobalNumericVariables.gnv.MIN_OPENNESS_FOREPLAY);

        eventsList.Add(dge);

        //******************************************************************* NEW EVENT*******************************************//
        clip = Resources.LoadAll<VideoClip>(StaticStrings.EVENT_FOLDER + StaticStrings.DUO_GIRL_EVENT_FOLDER +
StaticStrings.DUO_GIRL_EVENTS_DIRECTORIES_NAMES[2]);

        girl1 = "Madison Ivy";
        girl2 = "Rachel Starr";

        foreplaySkillGain = 5;
        groupSkillGain = 5;

        moneyGain = 100;

        dge = new DuoGirlEvent(girl1,girl2,
    "Practicing with friends", "Looks like Madison and Rachel decided to practice together with a couple of girlfriends. " +
    "This will improve their foreplay and group skills!\n\nForeplay skill +" + foreplaySkillGain + ".\nGroup skill +" + groupSkillGain + ".", clip[0]
    , delegate () {
        IncreaseForeplay(foreplaySkillGain, girl1,girl2);
        IncreaseGroup(groupSkillGain, girl1,girl2);
    },
    GMGlobalNumericVariables.gnv.MIN_OPENNESS_FOREPLAY, GMGlobalNumericVariables.gnv.MIN_OPENNESS_FOREPLAY);


        //******************************************************************* NEW EVENT*******************************************//
        clip = Resources.LoadAll<VideoClip>(StaticStrings.EVENT_FOLDER + StaticStrings.DUO_GIRL_EVENT_FOLDER +
StaticStrings.DUO_GIRL_EVENTS_DIRECTORIES_NAMES[3]);

        girl1 = "Bonnie Rotten";
        girl2 = "Kira Noir";

        foreplaySkillGain = 5;
        groupSkillGain = 2;

        moneyGain = 1000;

        dge = new DuoGirlEvent(girl1,girl2,
    "Working together", "Bonnie and Kira are working extra today. " +
    "They found one lucky client who accepted to pay a bonus!\n\n+" + moneyGain + StaticStrings.MONEY_SIGN +
    "\n\nThis will also improve their group skills!\n\nGroup skill +" + groupSkillGain + ".", clip[0]
    , delegate () {
        IncreaseGroup(groupSkillGain, girl1,girl2);
        GMClubData.EarnMoney(moneyGain);
    },
    GMGlobalNumericVariables.gnv.MIN_OPENNESS_GROUP, GMGlobalNumericVariables.gnv.MIN_OPENNESS_GROUP);

        eventsList.Add(dge);

        //******************************************************************* NEW EVENT*******************************************//
        clip = Resources.LoadAll<VideoClip>(StaticStrings.EVENT_FOLDER + StaticStrings.DUO_GIRL_EVENT_FOLDER +
StaticStrings.DUO_GIRL_EVENTS_DIRECTORIES_NAMES[4]);

        girl1 = "Abigail Mac";
        girl2 = "Kimmy Granger";

        foreplaySkillGain = 5;
        groupSkillGain = 2;

        moneyGain = 1000;

        dge = new DuoGirlEvent(girl1, girl2,
    "Working together", "Abigail and Kimmy were invited to a cosplay orgy. " +
    "They hapily agreed and got a bonus!\n\n+" + moneyGain + StaticStrings.MONEY_SIGN +
    "\n\nThis will also improve their group skills!\n\nGroup skill +" + groupSkillGain + ".", clip[0]
    , delegate () {
        IncreaseGroup(groupSkillGain, girl1, girl2);
        GMClubData.EarnMoney(moneyGain);
    },
    GMGlobalNumericVariables.gnv.MIN_OPENNESS_GROUP, GMGlobalNumericVariables.gnv.MIN_OPENNESS_GROUP);

        eventsList.Add(dge);

        //*********************************************************************************************************************//
    }

}
