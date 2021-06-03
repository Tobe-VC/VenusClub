using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogLine
{
    public string talker;
    public string line;

    public string background = "";
    public string portrait = "";
    public string portraitPosition = "";
    public PortraitPosition portraitPositionEnum;


    public DialogLine(string talker, string line, string background, string portrait, PortraitPosition portraitPosition)
    {
        this.talker = talker;
        this.line = line;
        this.background = background ?? ""; //Means that background and portrait are either set to the corresponding parameter or ""
        this.portrait = portrait ?? "";
        this.portraitPositionEnum = portraitPosition;
    }
}

[System.Serializable]
public class BackgroundLine
{
    public string backgroundImageName;
    public List<int> lines;

    public BackgroundLine(string backgroundImageName, List<int> lines)
    {
        this.backgroundImageName = backgroundImageName;
        this.lines = lines;
    }
}

[System.Serializable]
public class PortraitLine
{
    public string portraitImageName;
    public List<int> lines;

    public PortraitLine(string portraitImageName, List<int> lines)
    {
        this.portraitImageName = portraitImageName;
        this.lines = lines;
    }
}

[System.Serializable]
public class PortraitLineSprite
{
    public Sprite portraitSprite;
    public List<int> lines;

    public PortraitLineSprite(Sprite portraitSprite, List<int> lines)
    {
        this.portraitSprite = portraitSprite;
        this.lines = lines;
    }
}

[System.Serializable]
public class BackgroundLineSprite
{
    public Sprite backgroundImage;
    public List<int> lines;

    public BackgroundLineSprite(Sprite backgroundImage, List<int> lines)
    {
        this.backgroundImage = backgroundImage;
        this.lines = lines;
    }
}

[System.Serializable]
public class ImprovementNameAndLevel
{
    public string name;
    public int level;

    public ImprovementNameAndLevel(string name, int level)
    {
        this.name = name;
        this.level = level;
    }
}

public class GirlDialog
{
    public int id;

    /// <summary>
    /// The girl who uses this dialog
    /// </summary>
    [NonSerialized]
    public GirlClass girl;

    public float minOpenness = GMGlobalNumericVariables.gnv.MIN_OPENNESS_VALUE;
    public float maxOpenness = GMGlobalNumericVariables.gnv.MAX_OPENNESS_VALUE;

    public float minAffection = GMGlobalNumericVariables.gnv.MIN_AFFECTION;
    public float maxAffection = GMGlobalNumericVariables.gnv.MAX_AFFECTION;

    public int minDance;
    public int maxDance;

    public int minPose;
    public int maxPose;

    public int minForeplay;
    public int maxForeplay;

    public int minOral;
    public int maxOral;

    public int minSex;
    public int maxSex;

    public int minGroup;
    public int maxGroup;

    public float minEnergy;
    public float maxEnergy;

    public int minInteractionSeen;
    public int maxInteractionSeen;

    public bool seenOnce;

    public List<int> needInteractionsSeen = new List<int>();

    public List<ImprovementNameAndLevel> needImprovements = new List<ImprovementNameAndLevel> { new ImprovementNameAndLevel("",0) };
    public List<string> needPolicies = new List<string> { "" };
    public string needCostumeEquipped = "";
    public List<string> needCostumesUnlocked = new List<string> { "" };
    public List<string> needServicesUnlocked = new List<string> { "" };
    public List<string> needServicesSubscribed = new List<string> { "" };


    public List<DialogLine> dialog;

    public bool displayPortrait;

    /// <summary>
    /// This path assumes that it starts from the Talk/ folder
    /// </summary>
    public string videoName;

    public int firstLineWithVideo;
    public int lastLineWithVideo;

    public bool isVideoFullScreen;

    public bool seen = false;

    public List<BackgroundLine> backgrounds = null;

    [NonSerialized]
    public List<BackgroundLineSprite> backgroundsSprite = null;

    public List<PortraitLine> portraits = null;

    [NonSerialized]
    public List<PortraitLineSprite> portraitsSprites = null;



    /****************************Variables used to have an effect on the game***********************/
    public bool unlocksDance = false;
    public bool unlocksDanceCloser = false;
    public bool unlocksDanceTopless = false;
    public bool unlocksPoseNaked = false;
    public bool unlocksSoloFingering = false;
    public bool unlocksToysMasturbation = false;
    public bool unlocksHandjob = false;
    public bool unlocksFootjob = false;
    public bool unlocksTitsjob = false;
    public bool unlocksBlowjob = false;
    public bool unlocksDeepthroat = false;
    public bool unlocksFacefuck = false;
    public bool unlocksMissionary = false;
    public bool unlocksDoggystyle = false;
    public bool unlocksAnal = false;
    public bool unlocksThreesome = false;
    public bool unlocksFoursome = false;
    public bool unlocksOrgy = false;
    public bool unlocksBodyCumshot = false;
    public bool unlocksTitsCumshot = false;
    public bool unlocksFacial = false;
    public bool unlocksSwallow = false;
    public bool unlocksCreampie = false;
    public bool unlocksAnalCreampie = false;
    /*****************************************************************************************************/

    //Check if the necessary dialogs have been seen
    private bool AreDialogsSeen()
    {
        foreach (GirlDialog d in girl.girlDialogs)
        {
            foreach (int i in needInteractionsSeen)
                if (d.id == i)
                {
                    if (!d.seen)
                        return false;
                }
        }
        return true;
    }

    private bool ElementsOwned(List<string> list, Func<string,bool> function)
    {
        if (list != null && list.Count > 0 && !(list.Count == 1 && list[0] == ""))
        {
            foreach (string s in list)
            {
                if (!function(s))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsCostumeEquiped()
    {
        if (needCostumeEquipped != null && needCostumeEquipped != "")
        {
            return GMWardrobeData.currentlyUsedCostume.name != null
                && GMWardrobeData.currentlyUsedCostume.subNames[GMWardrobeData.currentlyUsedCostume.currentLevel].
                    ToLower().Equals(needCostumeEquipped.ToLower());
        }
        return true;
    }

    private bool AreImprovementsOwned()
    {
        if (needImprovements != null && needImprovements.Count > 0 && !(needImprovements.Count == 1 && needImprovements[0].name == ""))
        {
            foreach (ImprovementNameAndLevel imp in needImprovements)
            {
                if (!GMImprovementsData.IsImprovementOwned(imp.name, imp.level))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool CanTrigger()
    {
        if (seenOnce && seen)
            return false;

        return
            (
            BetweenMinAndMax(minOpenness, maxOpenness, girl.GetOpenness())
            && BetweenMinAndMax(minAffection, maxAffection, girl.GetAffection())
            && BetweenMinAndMax(minDance, maxDance, girl.GetDancing())
            && BetweenMinAndMax(minPose, maxPose, girl.GetPosing())
            && BetweenMinAndMax(minForeplay, maxForeplay, girl.GetForeplay())
            && BetweenMinAndMax(minOral, maxOral, girl.GetOral())
            && BetweenMinAndMax(minSex, maxSex, girl.GetSex())
            && BetweenMinAndMax(minGroup, maxGroup, girl.GetGroup())
            && BetweenMinAndMax(minEnergy, maxEnergy, girl.GetEnergy())
            && BetweenMinAndMax(minInteractionSeen, maxInteractionSeen, girl.interactionSeen)
            && ElementsOwned(needPolicies, GMPoliciesData.IsPolicyOwned)
            && AreImprovementsOwned()
            && ElementsOwned(needCostumesUnlocked, GMWardrobeData.IsCostumeOwned)
            && ElementsOwned(needServicesUnlocked, GMCrimeServiceData.IsServiceUnlocked)
            && ElementsOwned(needServicesSubscribed, GMCrimeServiceData.IsServiceSubscribed)
            && IsCostumeEquiped()
            &&AreDialogsSeen()
            );
    }

    private void SwitchDos(bool girlDo, bool unlock)
    {
        if (unlock)
            girlDo = true;
    }
    /*
    public void Effects()
    {
        if(unlocksDance)
            girl.doDance = unlocksDance;

        if (unlocksDanceCloser)
            girl.doDanceCloser = unlocksDanceCloser;

        if (unlocksDanceTopless)
            girl.doDanceTopless = unlocksDanceTopless;

        if (unlocksPoseNaked)
            girl.doPoseNaked = unlocksPoseNaked;

        if (unlocksSoloFingering)
            girl.doSoloFingering = unlocksSoloFingering;

        if (unlocksToysMasturbation)
            girl.doToysMasturbation = unlocksToysMasturbation;

        if (unlocksHandjob)
            girl.doHandjob = unlocksHandjob;

        if (unlocksFootjob)
            girl.doFootjob = unlocksFootjob;

        if (unlocksTitsjob)
            girl.doTitsjob = unlocksTitsjob;

        if (unlocksBlowjob)
            girl.doBlowjob = unlocksBlowjob;

        if (unlocksDeepthroat)
            girl.doDeepthroat = unlocksDeepthroat;

        if (unlocksFacefuck)
            girl.doFacefuck = unlocksFacefuck;

        if (unlocksMissionary)
            girl.doMissionary = unlocksMissionary;

        if (unlocksDoggystyle)
            girl.doDoggystyle = unlocksDoggystyle;

        if (unlocksAnal)
            girl.doAnal = unlocksAnal;

        if (unlocksThreesome)
            girl.doThreesome = unlocksThreesome;

        if (unlocksFoursome)
            girl.doFoursome = unlocksFoursome;

        if (unlocksOrgy)
            girl.doOrgy = unlocksOrgy;

        if (unlocksBodyCumshot)
            girl.doBodyCumshot = unlocksBodyCumshot;

        if (unlocksTitsCumshot)
            girl.doTitsCumshot = unlocksTitsCumshot;

        if (unlocksFacial)
            girl.doFacial = unlocksFacial;

        if (unlocksSwallow)
            girl.doSwallow = unlocksSwallow;

        if (unlocksCreampie)
            girl.doCreampie = unlocksCreampie;

        if (unlocksAnalCreampie)
            girl.doAnalCreampie = unlocksAnalCreampie;
    }
    */
    private bool BetweenMinAndMax(int min, int max, int valueToTest)
    {
        return valueToTest >= min && valueToTest <= max;
    }

    private bool BetweenMinAndMax(float min, float max, float valueToTest)
    {
        return valueToTest >= min && valueToTest <= max;
    }

    public GirlDialog(GirlClass girl)
    {
        this.girl = girl;

        dialog = new List<DialogLine>();

        minOpenness = GMGlobalNumericVariables.gnv.MIN_OPENNESS_VALUE;
        maxOpenness = GMGlobalNumericVariables.gnv.MAX_OPENNESS_VALUE;

        minAffection = GMGlobalNumericVariables.gnv.MIN_AFFECTION;
        maxAffection = GMGlobalNumericVariables.gnv.MAX_AFFECTION;

        minDance = GMGlobalNumericVariables.gnv.MIN_STATS_VALUE;
        maxDance = GMGlobalNumericVariables.gnv.MAX_STATS_VALUE;

        minPose = GMGlobalNumericVariables.gnv.MIN_STATS_VALUE;
        maxPose = GMGlobalNumericVariables.gnv.MAX_STATS_VALUE;

        minForeplay = GMGlobalNumericVariables.gnv.MIN_STATS_VALUE;
        maxForeplay = GMGlobalNumericVariables.gnv.MAX_STATS_VALUE;

        minOral = GMGlobalNumericVariables.gnv.MIN_STATS_VALUE;
        maxOral = GMGlobalNumericVariables.gnv.MAX_STATS_VALUE;

        minSex = GMGlobalNumericVariables.gnv.MIN_STATS_VALUE;
        maxSex = GMGlobalNumericVariables.gnv.MAX_STATS_VALUE;

        minGroup = GMGlobalNumericVariables.gnv.MIN_STATS_VALUE;
        maxGroup = GMGlobalNumericVariables.gnv.MAX_STATS_VALUE;

        minEnergy = GMGlobalNumericVariables.gnv.MIN_ENERGY;
        maxEnergy = GMGlobalNumericVariables.gnv.MAX_ENERGY;

        displayPortrait = true;
        firstLineWithVideo = 0;
        lastLineWithVideo = -1;
        minInteractionSeen = 0;
        maxInteractionSeen = int.MaxValue;

        seenOnce = false;

        backgrounds = null;

        backgroundsSprite = null;

        isVideoFullScreen = false;

        seen = false;

        portraits = null;

        portraitsSprites = null;


        unlocksDance = false;
        unlocksDanceCloser = false;
        unlocksDanceTopless = false;
        unlocksPoseNaked = false;
        unlocksSoloFingering = false;
        unlocksToysMasturbation = false;
        unlocksHandjob = false;
        unlocksFootjob = false;
        unlocksTitsjob = false;
        unlocksBlowjob = false;
        unlocksDeepthroat = false;
        unlocksFacefuck = false;
        unlocksMissionary = false;
        unlocksDoggystyle = false;
        unlocksAnal = false;
        unlocksThreesome = false;
        unlocksFoursome = false;
        unlocksOrgy = false;
        unlocksBodyCumshot = false;
        unlocksTitsCumshot = false;
        unlocksFacial = false;
        unlocksSwallow = false;
        unlocksCreampie = false;
        unlocksAnalCreampie = false;

        needInteractionsSeen = new List<int>();

        needImprovements = new List<ImprovementNameAndLevel> { new ImprovementNameAndLevel("", 0) };
        needPolicies = new List<string> { "" };
        needCostumeEquipped = "";
        needCostumesUnlocked = new List<string> { "" };
        needServicesUnlocked = new List<string> { "" };
        needServicesSubscribed = new List<string> { "" };

}

    public GirlDialog(int id, GirlClass girl, List<DialogLine> dialog) : this(girl)
    {
        this.id = id;
        this.dialog = dialog;
    }

    /// <summary>
    /// Constructor specifically made for the default girl dialog
    /// </summary>
    /// <param name="id"></param>
    /// <param name="girl"></param>
    /// <param name="dialog"></param>
    /// <param name="portrait"></param>
    public GirlDialog(int id, GirlClass girl, List<DialogLine> dialog, Sprite portrait) : this(id, girl, dialog)
    {
        PortraitLineSprite pls = new PortraitLineSprite(portrait, new List<int> { 1 });
        portraitsSprites = new List<PortraitLineSprite> { pls };
    }

    public static GirlDialog CreateFromJSON(string json, GirlClass girl)
    {
        GirlDialog result = new GirlDialog(girl);
        JsonUtility.FromJsonOverwrite(json, result);
        if(result.lastLineWithVideo == -1)
        {
            result.lastLineWithVideo = result.dialog.Count + 1;
        }
        if(result.dialog == null || result.dialog.Count == 0)
        {
            result.dialog = new List<DialogLine> { new DialogLine("narrator", "You had a wonderful talk with her.","","", PortraitPosition.LEFT) };
        }
        result.girl = girl;

        return result;
    }

    public static IEnumerator LoadPortraits(GirlDialog dialog, Image loadingImage, GameObject loadingPopup)
    {
        dialog.portraitsSprites = new List<PortraitLineSprite>();

        foreach (PortraitLine pl in dialog.portraits)
        {
            if (dialog.girl.external)
            {
                if (File.Exists(StaticStrings.GIRLPACKS_DIRECTORY + dialog.girl.folderName + "/" +
                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                        pl.portraitImageName))
                {
                    yield return StaticFunctions.LoadImageFromURL("file:///" +
                        StaticStrings.GIRLPACKS_DIRECTORY + dialog.girl.folderName + "/" +
                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                        pl.portraitImageName, "", loadingImage, true, true);
                }
                else
                {
                    loadingImage.sprite = dialog.girl.GetPortrait();
                }

                dialog.portraitsSprites.Add(new PortraitLineSprite(loadingImage.sprite, pl.lines));
            }
            else
            {
                dialog.portraitsSprites.Add(new PortraitLineSprite(Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                                        dialog.girl.folderName + "/" +
                                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                                        pl.portraitImageName), pl.lines));
            }
        }
    }

    public static IEnumerator LoadBackgrounds(GirlDialog dialog, Image loadingImage, GameObject loadingPopup)
    {
        dialog.backgroundsSprite = new List<BackgroundLineSprite>();

        foreach (BackgroundLine bl in dialog.backgrounds)
        {
            if (dialog.girl.external)
            {
                if (File.Exists(StaticStrings.GIRLPACKS_DIRECTORY + dialog.girl.folderName + "/" +
                    StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                    bl.backgroundImageName))
                {
                    yield return StaticFunctions.LoadImageFromURL("file:///" +
                    StaticStrings.GIRLPACKS_DIRECTORY + dialog.girl.folderName + "/" +
                    StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                    bl.backgroundImageName, "", loadingImage, true, true);
                }
                else
                {
                    loadingImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + 
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB);
                }
                dialog.backgroundsSprite.Add(new BackgroundLineSprite(loadingImage.sprite, bl.lines));
            }
            else
            {
                dialog.backgroundsSprite.Add(new BackgroundLineSprite(Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                                        dialog.girl.folderName + "/" +
                                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                                        bl.backgroundImageName), bl.lines));
            }
        }
    }
}
