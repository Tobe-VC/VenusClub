using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GirlClass : IComparable<GirlClass>
{
    //**************************** Girl's attributes *************************************//

    public string name;
    public int height;
    public BustType bust;
    public EyeColor eyes;
    public HairColor hair;
    public BodyType body;
    public int age = 18;
    public SkinComplexion skin;
    public Specialty specialSkill;
    //************************************************************************************//
    //**************************** Girl's enums as strings *************************************//
    public string bustType;
    public string eyeColor;
    public string hairColor;
    public string bodyType;
    public string skinComplexion;
    public string specialty;
    //************************************************************************************//

    //********************************** Girl's skills ***********************************//
    [SerializeField]
    private int dancing; //For dance and strip acts
    [SerializeField]
    private int oral; //For blowjob and gloryhole acts
    [SerializeField]
    private int posing; //For naked, hand and toy masturbation acts
    [SerializeField]
    private int foreplay; //For handjob, footjob and titjob acts
    [SerializeField]
    private int sex; //For vaginal and anal sex acts
    [SerializeField]
    private int group; //For group sex acts (threesome, foursome, gangbang)
    //************************************************************************************//

    //****************************** Other ***********************************************//
    [SerializeField]
    private int popularity;
    public Occupation morningOccupation = Occupation.REST;
    public Occupation eveningOccupation = Occupation.REST;
    public Occupation nightOccupation = Occupation.REST;
    public Work morningWork = Work.NONE;
    public Work eveningWork = Work.NONE;
    public Work nightWork = Work.NONE;

    public int moneyEarned = 0;
    public int money = 0;

    [SerializeField]
    private float energy = 100f;

    //The money required to hire this girl
    public int moneyCost = 300;
    //The minimum reputation required to hire this girl
    public float reputationCost = 0;
    //The minimum influence required to hire this girl
    public float influenceCost = 0;
    //The minimum connection required to hire this girl
    public float connectionCost = 0;

    //The openness of the girl, it dictates what prestations she will or will not accept
    [SerializeField]
    private float openness = 0;

    public bool external = false;

    public bool inLottery = false;

    public string folderName;//The name of the folder containing the girl

    [NonSerialized]
    private Sprite portrait = null;

    [NonSerialized]
    private Sprite portrait25To50 = null;

    [NonSerialized]
    private Sprite portrait50To75 = null;

    [NonSerialized]
    private Sprite portrait75To100 = null;

    [NonSerialized]
    public Sprite closeupPortrait = null;

    //******** Variables to check if a girl will accept to do a specific performance ******//
    public bool doDance = true;
    public bool doDanceCloser = true;
    public bool doDanceTopless = true;

    public bool doPoseNaked = true;
    public bool doSoloFingering = true;
    public bool doToysMasturbation = true;
    public bool doHandjob = true;
    public bool doFootjob = true;
    public bool doTitsjob = true;
    public bool doBlowjob = true;
    public bool doDeepthroat = true;
    public bool doFacefuck = true;
    public bool doFacingVaginal = true;
    public bool doBackVaginal = true;
    public bool doAnal = true;
    public bool doThreesome = true;
    public bool doFoursome = true;
    public bool doOrgy = true;
    public bool doBodyCumshot = true;
    public bool doTitsCumshot = true;
    public bool doFacial = true;
    public bool doSwallow = true;
    public bool doCreampie = true;
    public bool doAnalCreampie = true;
    public bool doThreesomeFinish = true;
    public bool doGroupFinish = true;
    public bool doMissionary = true;
    public bool doDoggystyle = true;
    //************************************************************************************//



    public bool isFavorite = false;

    public bool isBeforeMissionaryRenamePack = false;

    [NonSerialized]
    public List<GirlDialog> girlDialogs = new List<GirlDialog>();

    private float affection = GMGlobalNumericVariables.gnv.MIN_AFFECTION;

    public List<GirlLesson> girlLessons = new List<GirlLesson>();

    [SerializeField]
    public int interactionSeen;

    public bool doNotDisplay = false;

    public List<string> videosSeen = new List<string>();

    public int GetDancing()
    {
        return dancing;
    }

    public void AddToDancing(int toAdd)
    {
        dancing = NotAboveMax(dancing, toAdd, GMGlobalNumericVariables.gnv.MAX_STATS_VALUE, GMGlobalNumericVariables.gnv.MIN_STATS_VALUE);
    }

    public int GetPosing()
    {
        return posing;
    }

    public void AddToPosing(int toAdd)
    {
        posing = NotAboveMax(posing, toAdd, GMGlobalNumericVariables.gnv.MAX_STATS_VALUE, GMGlobalNumericVariables.gnv.MIN_STATS_VALUE);
    }

    public int GetForeplay()
    {
        return foreplay;
    }

    public void AddToForeplay(int toAdd)
    {
        foreplay = NotAboveMax(foreplay, toAdd, GMGlobalNumericVariables.gnv.MAX_STATS_VALUE, GMGlobalNumericVariables.gnv.MIN_STATS_VALUE);
    }

    public int GetOral()
    {
        return oral;
    }

    public void AddToOral(int toAdd)
    {
        oral = NotAboveMax(oral, toAdd, GMGlobalNumericVariables.gnv.MAX_STATS_VALUE, GMGlobalNumericVariables.gnv.MIN_STATS_VALUE);
    }

    public int GetSex()
    {
        return sex;
    }

    public void AddToSex(int toAdd)
    {
        sex = NotAboveMax(sex, toAdd, GMGlobalNumericVariables.gnv.MAX_STATS_VALUE, GMGlobalNumericVariables.gnv.MIN_STATS_VALUE);
    }

    public int GetGroup()
    {
        return group;
    }

    public void AddToGroup(int toAdd)
    {
        group = NotAboveMax(group, toAdd, GMGlobalNumericVariables.gnv.MAX_STATS_VALUE, GMGlobalNumericVariables.gnv.MIN_STATS_VALUE);
    }

    public int GetPopularity()
    {
        return popularity;
    }

    public void AddToPopularity(int toAdd)
    {
        popularity = NotAboveMax(popularity, toAdd, GMGlobalNumericVariables.gnv.MAX_POPULARITY_VALUE,
            GMGlobalNumericVariables.gnv.MIN_POPULARITY_VALUE);
    }

    public float GetOpenness()
    {
        return openness;
    }

    public void AddToOpenness(float toAdd)
    {
        openness = NotAboveMax(openness, toAdd, GMGlobalNumericVariables.gnv.MAX_OPENNESS_VALUE, GMGlobalNumericVariables.gnv.MIN_OPENNESS_VALUE);
    }

    public float GetEnergy()
    {
        return energy;
    }

    public void AddToEnergy(float toAdd)
    {
        energy = NotAboveMax(energy, toAdd, GMGlobalNumericVariables.gnv.MAX_ENERGY, GMGlobalNumericVariables.gnv.MIN_ENERGY);
    }

    private int NotAboveMax(int stat, int toAdd, int max, int min)
    {
        stat += toAdd;
        if (stat >= max)
            stat = max;
        else if (stat < min)
            stat = min;
        return stat;
    }

    private float NotAboveMax(float stat, float toAdd, float max, float min)
    {
        stat += toAdd;
        if (stat >= max)
            stat = max;
        else if (stat < min)
            stat = min;
        return stat;
    }

    private int RandomRange(int skillValue, float randomRangePercentage)
    {
        float aux = randomRangePercentage / 100;
        return Mathf.RoundToInt(skillValue * aux);
    }

    public float GetAffection()
    {
        return affection;
    }

    public void AddToAffection(float toAdd)
    {
        affection = NotAboveMax(affection, toAdd, GMGlobalNumericVariables.gnv.MAX_AFFECTION, GMGlobalNumericVariables.gnv.MIN_AFFECTION);
    }

    public static GirlClass CreateFromJSON(string json)
    {
        //Extract the girl's data from a JSON
        GirlClass girl = JsonUtility.FromJson<GirlClass>(json);

        girl.folderName = girl.name + "/";

        //These are here to ensure that the recorded values are not above the max
        girl.AddToDancing(0);
        girl.AddToPosing(0);
        girl.AddToForeplay(0);
        girl.AddToOral(0);
        girl.AddToSex(0);
        girl.AddToGroup(0);
        girl.AddToEnergy(0);
        girl.AddToOpenness(0);
        girl.AddToPopularity(0);

        if (girl.moneyCost < 0)
            girl.moneyCost = 0;

        if (girl.reputationCost < 0)
            girl.reputationCost = 0;

        if (girl.influenceCost < 0)
            girl.influenceCost = 0;

        if (girl.connectionCost < 0)
            girl.connectionCost = 0;
        //Convert the string data into enums when necessary (for bust, eyes, hair, body, skin and specialty)
        //If the conversion cathes an error, the default value will be used and the corresponding string will be set accordingly
        try
        {
            girl.bust = (BustType)System.Enum.Parse(typeof(BustType), girl.bustType.ToUpper());
        }
        catch (Exception)
        {
            girl.bustType = girl.bust.ToString();
        }

        try
        {
            girl.eyes = (EyeColor)System.Enum.Parse(typeof(EyeColor), girl.eyeColor.ToUpper());
        }
        catch (Exception)
        {
            girl.eyeColor = girl.eyes.ToString();
        }

        try
        {
            string toParse = girl.hairColor.Replace(' ', '_');
            girl.hair = (HairColor)System.Enum.Parse(typeof(HairColor), toParse.ToUpper());
        }
        catch (Exception)
        {
            girl.hairColor = girl.hair.ToString(); ;
        }

        try
        {
            girl.body = (BodyType)System.Enum.Parse(typeof(BodyType), girl.bodyType.ToUpper());
        }
        catch (Exception)
        {
            girl.bodyType = girl.body.ToString();
        }

        try
        {
            girl.skin = (SkinComplexion)System.Enum.Parse(typeof(SkinComplexion), girl.skinComplexion.ToUpper());
        }
        catch (Exception)
        {
            girl.skinComplexion = girl.skin.ToString();
        }

        try
        {
            girl.specialSkill = (Specialty)System.Enum.Parse(typeof(Specialty), girl.specialty.ToUpper());
        }
        catch (Exception)
        {
            girl.specialty = girl.specialSkill.ToString();
        }

        if (girl.age < 18)
            girl.age = 18;

        //These lines are here to ensure retrocompatibility with old packs that use doMissionary and doDoggystyle
        //If those variables are at false, it means they were specified as false and therefore the "vaginal" variables
        //should be set to the same value
        //Otherwise, they are either absent or set as true, and should therefore be ignored in favor of the new variables
        if (!girl.doMissionary)
            girl.doFacingVaginal = false;

        if (!girl.doDoggystyle)
            girl.doBackVaginal = false;

        string sexFolder = StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + StaticStrings.VIDEOS_FOLDER + StaticStrings.PERFORMANCES_FOLDER + StaticStrings.WORK_FOLDER +  StaticStrings.SEX_FOLDER;
        string missioFolder = sexFolder + StaticStrings.MISSIONARY_SUBFOLDER;
        string doggyFolder = sexFolder + StaticStrings.DOGGYSTYLE_SUBFOLDER;
        string facingVagFolder = sexFolder + StaticStrings.VAGINAL_FACING_SUBFOLDER;
        string backVagFolder = sexFolder + StaticStrings.VAGINAL_BACK_SUBFOLDER;
        if (System.IO.Directory.Exists(missioFolder) && !System.IO.Directory.Exists(facingVagFolder))
        {
            //If the Missionary and/or Doggystyle folder exists AND the FacingVaginal and BackVaginal do not
            girl.isBeforeMissionaryRenamePack = true;
        }
        else
        {
            girl.isBeforeMissionaryRenamePack = false;
        }

        return girl;

    }

    //Computes the daily cost of a girl
    public int DaySalary()
    {
        //At the most basic, she costs BASIC_GIRL_DAY_COST per day
        int result = GMGlobalNumericVariables.gnv.BASIC_GIRL_DAY_COST;

        //Then add some amount depending on the girl's popularity
        float aux = popularity * GMGlobalNumericVariables.gnv.GIRL_DAY_COST_POPULARITY_MULTIPLIER;

        result += Mathf.FloorToInt(aux);

        return result;
    }
    /*
    //Computes the earnings of a girl for a given period of time
    //The formulae is basic BASIC_DAY_EARNINGS + random percentage value based on skill + random percentage value based on popularity
    private int PeriodEarnings(Occupation occupation, Work work)
    {
        if (occupation != Occupation.WORK)
            return 0;
        else
        {
            int randomRangeSkillBonus = 0;
            int randomRangePopularity = RandomRange(popularity, GMGlobalNumericVariables.gnv.DAY_COST_RANDOM_RANGE_PERCENTAGE) / 100; ;
            int randomPopularityBonus = UnityEngine.Random.Range(-randomRangePopularity, randomRangePopularity + 1);
            switch (work)
            {
                case Work.DANCE: randomRangeSkillBonus = RandomRange(dancing, GMGlobalNumericVariables.gnv.DAY_EARNINGS_RANDOM_RANGE_PERCENTAGE);
                    return GMGlobalNumericVariables.gnv.BASIC_DAY_EARNINGS +
                        UnityEngine.Random.Range(-randomRangeSkillBonus, randomRangeSkillBonus + 1) + randomPopularityBonus;

                case Work.POSE:
                    randomRangeSkillBonus = RandomRange(posing, GMGlobalNumericVariables.gnv.DAY_EARNINGS_RANDOM_RANGE_PERCENTAGE);
                    return GMGlobalNumericVariables.gnv.BASIC_DAY_EARNINGS +
                        UnityEngine.Random.Range(-randomRangeSkillBonus, randomRangeSkillBonus + 1) + randomPopularityBonus;

                case Work.FOREPLAY:
                    randomRangeSkillBonus = RandomRange(foreplay, GMGlobalNumericVariables.gnv.DAY_EARNINGS_RANDOM_RANGE_PERCENTAGE);
                    return GMGlobalNumericVariables.gnv.BASIC_DAY_EARNINGS +
                        UnityEngine.Random.Range(-randomRangeSkillBonus, randomRangeSkillBonus + 1) + randomPopularityBonus;

                case Work.ORAL:
                    randomRangeSkillBonus = RandomRange(oral, GMGlobalNumericVariables.gnv.DAY_EARNINGS_RANDOM_RANGE_PERCENTAGE);
                    return GMGlobalNumericVariables.gnv.BASIC_DAY_EARNINGS +
                        UnityEngine.Random.Range(-randomRangeSkillBonus, randomRangeSkillBonus + 1) + randomPopularityBonus;

                case Work.SEX:
                    randomRangeSkillBonus = RandomRange(sex, GMGlobalNumericVariables.gnv.DAY_EARNINGS_RANDOM_RANGE_PERCENTAGE);
                    return GMGlobalNumericVariables.gnv.BASIC_DAY_EARNINGS +
                        UnityEngine.Random.Range(-randomRangeSkillBonus, randomRangeSkillBonus + 1) + randomPopularityBonus;

                case Work.GROUP:
                    randomRangeSkillBonus = RandomRange(group, GMGlobalNumericVariables.gnv.DAY_EARNINGS_RANDOM_RANGE_PERCENTAGE);
                    return GMGlobalNumericVariables.gnv.BASIC_DAY_EARNINGS +
                        UnityEngine.Random.Range(-randomRangeSkillBonus, randomRangeSkillBonus + 1) + randomPopularityBonus;

                default: return GMGlobalNumericVariables.gnv.BASIC_DAY_EARNINGS;
            }
        }

    }
    
    //Computes the daily salary of a girl
    //The formulae is basic BASIC_DAY_COST + random percentage value based on popularity
    public int DaySalary()
    {
        int randomRange = RandomRange(popularity, GMGlobalNumericVariables.gnv.DAY_COST_RANDOM_RANGE_PERCENTAGE) / 100;
        return GMGlobalNumericVariables.gnv.BASIC_GIRL_DAY_COST + UnityEngine.Random.Range(-randomRange, randomRange + 1);
    }
    */
    public override string ToString()
    {
        return "Name: " + name + "\n" + "Height: " + height + "\n" + "Bust type: " + bust + "\n" + "Eye color: " + eyes +
            "\n" + "Hair color: " + hair + "\n" + "Body type: " + body + "\n" + "Age: " + age + "\n" + "Skin complexion: " + skin + "\n" +
            "\n" + "Specialty: " + specialSkill + "\n" + "Dancing skill: " + dancing + "\n" + "Oral skill: " + oral + "\n" +
            "Posing skill: " + posing + "\n" + "Foreplay skill: " + foreplay + "\n" + "Sex skill: " + sex + "\n" +
            "Group sex skill: " + group;
    }

    /*
    public override bool Equals(object obj)
    {
        return (obj as GirlClass).folderName.Equals(folderName);;
    }
    */

    public bool CanDoPerformance(Work performance)
    {
        switch (performance)
        {
            case Work.DANCE: //True if the girl can do at least one type of dancing performance
                return (doDance && GMPoliciesData.policies.dance)
                    || (doDanceCloser && GMPoliciesData.policies.danceCloser)
                    || (doDanceTopless && GMPoliciesData.policies.danceTopless);

            case Work.POSE:
                return (doPoseNaked && GMPoliciesData.policies.poseNaked)
                    || (doSoloFingering && GMPoliciesData.policies.soloHand)
                    || (doToysMasturbation && GMPoliciesData.policies.mastToy);


            case Work.FOREPLAY:
                return (doHandjob && GMPoliciesData.policies.handjob)
                   || (doFootjob && GMPoliciesData.policies.footjob)
                   || (doTitsjob && GMPoliciesData.policies.titsjob);

            case Work.ORAL:
                return (doBlowjob && GMPoliciesData.policies.blowjob)
                    || (doDeepthroat && GMPoliciesData.policies.deepthroat)
                    || (doFacefuck && GMPoliciesData.policies.facefuck);

            case Work.SEX:
                return (doFacingVaginal && GMPoliciesData.policies.facingVaginal)
                    || (doBackVaginal && GMPoliciesData.policies.backVaginal)
                    || (doAnal && GMPoliciesData.policies.anal);

            case Work.GROUP:
                return (doThreesome && GMPoliciesData.policies.threesome)
                    || (doFoursome && GMPoliciesData.policies.foursome)
                    || (doOrgy && GMPoliciesData.policies.orgy);

            default: return true;
        }
    }

    public bool AcceptsPerformance(Work performance)
    {
        if (CanDoPerformance(performance))
        {
            switch (performance)
            {
                case Work.DANCE: //True if the girl can do at least one type of dancing performance
                    return true;

                case Work.POSE:
                    return openness >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_POSE;


                case Work.FOREPLAY:
                    return openness >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_FOREPLAY;

                case Work.ORAL:
                    return openness >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_ORAL;

                case Work.SEX:
                    return openness >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_SEX;

                case Work.GROUP:
                    return openness >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_GROUP;

                default: return true;
            }
        }
        else
        {
            return false;
        }
    }

    //Returns the integer value of a stat corresponding to a type of work
    public int StatsValue(Work work)
    {
        switch (work)
        {
            case Work.DANCE:
                return dancing;
            case Work.POSE:
                return posing;
            case Work.FOREPLAY:
                return foreplay;
            case Work.ORAL:
                return oral;
            case Work.SEX:
                return sex;
            case Work.GROUP:
                return group;
            default: return dancing;
        }
    }

    //For the moment, sort by openness
    int IComparable<GirlClass>.CompareTo(GirlClass other)
    {
        if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.OPENNESS)
        {
            if (this.openness < other.openness) return 1;
            else if (this.openness > other.openness) return -1;
            else return 0;
        }
        else if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.ENERGY)
        {
            if (this.energy < other.energy) return 1;
            else if (this.energy > other.energy) return -1;
            else return 0;
        }
        else if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.NAME)
        {
            return this.name.CompareTo(other.name);
        }
        else if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.DANCING)
        {
            if (this.dancing < other.dancing) return 1;
            else if (this.dancing > other.dancing) return -1;
            else return 0;
        }
        else if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.POSING)
        {
            if (this.posing < other.posing) return 1;
            else if (this.posing > other.posing) return -1;
            else return 0;
        }
        else if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.FOREPLAY)
        {
            if (this.foreplay < other.foreplay) return 1;
            else if (this.foreplay > other.foreplay) return -1;
            else return 0;
        }
        else if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.ORAL)
        {
            if (this.oral < other.oral) return 1;
            else if (this.oral > other.oral) return -1;
            else return 0;
        }
        else if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.SEX)
        {
            if (this.sex < other.sex) return 1;
            else if (this.sex > other.sex) return -1;
            else return 0;
        }
        else if (GMGlobalNumericVariables.gnv.SORT_TYPE == SortType.GROUP)
        {
            if (this.group < other.group) return 1;
            else if (this.group > other.group) return -1;
            else return 0;
        }
        else
            return 0;
    }

    //Only used for debug purposes
    public void SetGirlStatsToValue(int stat)
    {
        this.AddToDancing(-1000);
        this.AddToForeplay(-1000);
        this.AddToOral(-1000);
        this.AddToPosing(-1000);
        this.AddToSex(-1000);
        this.AddToGroup(-1000);

        this.AddToDancing(stat);
        this.AddToForeplay(stat);
        this.AddToOral(stat);
        this.AddToPosing(stat);
        this.AddToSex(stat);
        this.AddToGroup(stat);
    }

    public void CopyDos(GirlClass g)
    {
        doDance = g.doDance;
        doDanceCloser = g.doDanceCloser;
        doDanceTopless = g.doDanceTopless;
        doPoseNaked = g.doPoseNaked;
        doSoloFingering = g.doSoloFingering;
        doToysMasturbation = g.doToysMasturbation;
        doHandjob = g.doHandjob;
        doFootjob = g.doFootjob;
        doTitsjob = g.doTitsjob;
        doBlowjob = g.doBlowjob;
        doDeepthroat = g.doDeepthroat;
        doFacefuck = g.doFacefuck;
        if (g.isBeforeMissionaryRenamePack)
        {
            //For retrocompatibility
            doFacingVaginal = g.doMissionary;
            doBackVaginal = g.doDoggystyle;
        }
        else
        { 
            doFacingVaginal = g.doFacingVaginal;
            doBackVaginal = g.doBackVaginal;
        }
        doAnal = g.doAnal;
        doThreesome = g.doThreesome;
        doFoursome = g.doFoursome;
        doOrgy = g.doOrgy;
        doBodyCumshot = g.doBodyCumshot;
        doTitsCumshot = g.doTitsCumshot;
        doFacial = g.doFacial;
        doSwallow = g.doSwallow;
        doCreampie = g.doCreampie;
        doAnalCreampie = g.doAnalCreampie;
        doThreesomeFinish = g.doThreesomeFinish;
        doGroupFinish = g.doGroupFinish;
    }

    public void CopyFiredGirl(GirlClass g)
    {
        dancing = g.dancing;
        posing = g.posing;
        foreplay = g.foreplay;
        oral = g.oral;
        sex = g.sex;
        group = g.group;
        popularity = g.popularity;
        energy = g.energy;
        money = g.money;
        float tmpOpenness = (g.openness * 100);
        openness = Mathf.Round(tmpOpenness) / 100;
        moneyCost = g.moneyCost;
        reputationCost = g.reputationCost;
        influenceCost = g.influenceCost;
        connectionCost = g.connectionCost;
        interactionSeen = g.interactionSeen;
        CopyDos(g);
    }

    public List<GirlLesson> PossibleLessons()
    {
        List<GirlLesson> possibleLessons = new List<GirlLesson>();
        foreach (GirlLesson lesson in girlLessons)
        {
            if (lesson.CheckTrigger())
            {
                possibleLessons.Add(lesson);
            }
        }
        return possibleLessons;
    }

    /// <summary>
    /// Returns the list of possible dialogs for this girl
    /// </summary>
    /// <returns></returns>
    public List<GirlDialog> PossibleDialogs()
    {
        List<GirlDialog> result = new List<GirlDialog>();
        foreach(GirlDialog gd in girlDialogs)
        {
            if (gd.CanTrigger())
            {
                result.Add(gd);
            }
        }
        if(result.Count > 1)
        {
            //Removes the default talk if there is at least one other possibility
            //Assumes that the default dialog is always last in the list
            //This should be ensured by how the dialogs are registered
            result.RemoveAll(x=> x.id == int.MaxValue);
        }
        return result;
    }

    public Sprite GetPortrait()
    {
        if (openness >= 75)
            if(portrait75To100 != null)
                return portrait75To100;
            else 
                return portrait;

        else if (openness >= 50)
            if (portrait50To75 != null)
                return portrait50To75;
            else
                return portrait;

        else if (openness >= 25)
            if (portrait25To50 != null)
                return portrait25To50;
            else
                return portrait;

        else
            return portrait;

    }

    public void SetPortrait(Sprite portrait)
    {
        this.portrait = portrait;
    }

    public void SetPortrait25(Sprite portrait25)
    {
        portrait25To50 = portrait25;
    }

    public void SetPortrait50(Sprite portrait50)
    {
        portrait50To75 = portrait50;
    }

    public void SetPortrait75(Sprite portrait75)
    {
        portrait75To100 = portrait75;
    }

}
