using System.IO;
using UnityEngine.Video;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.Serialization;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public static class StaticFunctions
{

    //Associates each girl (by her name) to a number that represents the number of the last video of her that has been played
    private static Hashtable lastVideoSceneForGirl = new Hashtable();

    private static int autosaveSlotNumber = 1;

    public static short crimeOfficeAvailable = 0;

    public static string ReadJSONString(string path)
    {
        string result;
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        result = reader.ReadToEnd();
        reader.Close();
        return result;
    }

    public static string ToLowerCaseExceptFirst(string s)
    {
        string aux = "";

        aux = (s.ToString().Substring(1, s.ToString().Length - 1)).ToLower();
        aux = aux.Replace('_', ' ');
        return s.Substring(0, 1).ToUpper() + aux;
    }

    public static bool IsCompatibleVideoFile(FileInfo file)
    {
        return file.Extension.ToLower().Equals(".avi") || file.Extension.ToLower().Equals(".m4v")
            || file.Extension.ToLower().Equals(".mov") || file.Extension.ToLower().Equals(".mp4")
           || file.Extension.ToLower().Equals(".ogv") || file.Extension.ToLower().Equals(".webm") 
           || file.Extension.ToLower().Equals(".wmv");
    }

    public static void SelectRandomVideoClipFromExternalFolder(string folder, string girlName, VideoPlayer clip, GirlClass girl)
    {
        //Load the video clip from the available clips in the girl's folder
        List<string> clips = new List<string>();
        try
        {
            DirectoryInfo d = new DirectoryInfo(StaticStrings.GIRLPACKS_DIRECTORY + girlName + "/" + StaticStrings.VIDEOS_FOLDER +
                StaticStrings.PERFORMANCES_FOLDER + folder);

            foreach (FileInfo file in d.GetFiles())
            {
                if (IsCompatibleVideoFile(file))
                    clips.Add(file.FullName);
            }

            int random = 0;

            if (clips == null || clips.Count <= 0)
            {
                //If this girl doesn't have the type of clip we are looking for then use the generic girl folder
                VideoClip[] tmpClips = Resources.LoadAll<VideoClip>(StaticStrings.GIRLS_FOLDER +
                StaticStrings.GENERIC_GIRL_NAME + "/" + StaticStrings.VIDEOS_FOLDER +
                StaticStrings.PERFORMANCES_FOLDER + folder);

                random = 0;
                if (lastVideoSceneForGirl.ContainsKey(girlName))
                {
                    random = RandomExcept((int)lastVideoSceneForGirl[girlName], tmpClips.Length - 1);
                }
                else
                {
                    random = Random(tmpClips.Length - 1);
                }
                clip.clip = tmpClips[random];
                return;
            }

            //This code is to create a pseudo random that avoids repeating the same video twice in a row for a girl
            random = 0;
            if (lastVideoSceneForGirl.ContainsKey(girlName))
            {
                random = RandomExcept((int)lastVideoSceneForGirl[girlName], clips.Count - 1);
            }
            else
            {
                random = Random(clips.Count - 1);
            }

            lastVideoSceneForGirl[girlName] = random;
            clip.url = clips[random];
            string[] splitURL = clip.url.Split('\\');
            Debug.Log(splitURL[splitURL.Length-1]);
            girl.videosSeen.Add(splitURL[splitURL.Length - 1]);
        }
        catch
        {
            //If this girl doesn't have the type of clip we are looking for then use the generic girl folder
            VideoClip[] tmpClips = Resources.LoadAll<VideoClip>(StaticStrings.GIRLS_FOLDER +
            StaticStrings.GENERIC_GIRL_NAME + "/" + StaticStrings.VIDEOS_FOLDER +
            StaticStrings.PERFORMANCES_FOLDER + folder);

            int random = 0;
            if (lastVideoSceneForGirl.ContainsKey(girlName))
            {
                random = RandomExcept((int)lastVideoSceneForGirl[girlName], tmpClips.Length - 1);
            }
            else
            {
                random = Random(tmpClips.Length - 1);
            }
            clip.clip = tmpClips[random];
            return;
        }
    }

    public static void SelectRandomVideoClipFromFolder(string folder, string girlName, GirlClass girl, VideoPlayer clip)
    {
        //Load the video clip from the available clips in the girl's folder
        VideoClip[] clips = Resources.LoadAll<VideoClip>(StaticStrings.GIRLS_FOLDER +
            girlName + "/" + StaticStrings.VIDEOS_FOLDER +
            StaticStrings.PERFORMANCES_FOLDER + folder);

        if (clips == null || clips.Length <= 0)
        {
            //If this girl doesn't have the type of clip we are looking for then use the generic girl folder
            clips = Resources.LoadAll<VideoClip>(StaticStrings.GIRLS_FOLDER +
            StaticStrings.GENERIC_GIRL_NAME + "/" + StaticStrings.VIDEOS_FOLDER +
            StaticStrings.PERFORMANCES_FOLDER + folder);
        }

        //This code is to create a pseudo random that avoids repeating the same video twice in a row for a girl
        int random = 0;
        if (lastVideoSceneForGirl.ContainsKey(girlName))
        {
            random = RandomExcept((int)lastVideoSceneForGirl[girlName], clips.Length - 1);
        }
        else
        {
            random = Random(clips.Length - 1);
        }

        lastVideoSceneForGirl[girlName] = random;
        clip.clip = clips[random];
        girl.videosSeen.Add(clip.clip.name);
        Debug.Log(clip.clip.name);
    }

    //Returns a random number between 0 and max (excluded), excluding exception.
    private static int RandomExcept(int exception, int max)
    {
        if (max == 1)
            return 0;

        int random = Random(max);
        if (random == exception)
        {
            if (random == max)
                return 0;
            else
                return random + 1;
        }
        else
            return random;
    }

    /// <summary>
    /// Returns true if the girl can perform the performance corresponding to the folder
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="girl"></param>
    /// <returns></returns>
    private static bool CanSeeVideo(string folder, GirlClass girl)
    {
        return 
            (
            (folder.Equals(StaticStrings.DANCE_SUBFOLDER) && girl.doDance)
            || (folder.Equals(StaticStrings.CLOSER_DANCE_SUBFOLDER) && girl.doDanceCloser)
            || (folder.Equals(StaticStrings.TOPLESS_DANCE_SUBFOLDER) && girl.doDanceTopless)
            || (folder.Equals(StaticStrings.NAKED_SUBFOLDER) && girl.doPoseNaked)
            || (folder.Equals(StaticStrings.HAND_MASTURBATION_SUBFOLDER) && girl.doSoloFingering)
            || (folder.Equals(StaticStrings.TOY_MASTURBATION_SUBFOLDER) && girl.doToysMasturbation)
            || (folder.Equals(StaticStrings.HANDJOB_SUBFOLDER) && girl.doHandjob)
            || (folder.Equals(StaticStrings.FOOTJOB_SUBFOLDER) && girl.doFootjob && PlayerPrefs.GetInt(StaticStrings.PLAYER_PREFS_ACTIVATE_FOOTJOBS) != 0)
            || (folder.Equals(StaticStrings.TITSJOB_SUBFOLDER) && girl.doTitsjob)
            || (folder.Equals(StaticStrings.BLOWJOB_SUBFOLDER) && girl.doBlowjob)
            || (folder.Equals(StaticStrings.DEEPTHROAT_SUBFOLDER) && girl.doDeepthroat)
            || (folder.Equals(StaticStrings.FACEFUCK_SUBFOLDER) && girl.doFacefuck)
            || (folder.Equals(StaticStrings.MISSIONARY_SUBFOLDER) && girl.doMissionary) //Here to ensure retrocompatibility
            || (folder.Equals(StaticStrings.VAGINAL_FACING_SUBFOLDER) && girl.doFacingVaginal)
            || (folder.Equals(StaticStrings.DOGGYSTYLE_SUBFOLDER) && girl.doDoggystyle) //Here to ensure retrocompatibility
            || (folder.Equals(StaticStrings.VAGINAL_BACK_SUBFOLDER) && girl.doBackVaginal)
            || (folder.Equals(StaticStrings.ANAL_SEX_SUBFOLDER) && girl.doAnal)
            || (folder.Equals(StaticStrings.THREESOME_SUBFOLDER) && girl.doThreesome)
            || (folder.Equals(StaticStrings.FOURSOME_SUBFOLDER) && girl.doFoursome)
            || (folder.Equals(StaticStrings.ORGY_SUBFOLDER) && girl.doOrgy)
            || (folder.Equals(StaticStrings.BODY_FINISH_SUBFOLDER) && girl.doBodyCumshot)
            || (folder.Equals(StaticStrings.TITS_FINISH_SUBFOLDER) && girl.doTitsCumshot)
            || (folder.Equals(StaticStrings.FACE_FINISH_SUBFOLDER) && girl.doFacial)
            || (folder.Equals(StaticStrings.SWALLOW_FINISH_SUBFOLDER) && girl.doSwallow)
            || (folder.Equals(StaticStrings.CREAMPIE_FINISH_SUBFOLDER) && girl.doCreampie)
            || (folder.Equals(StaticStrings.ANAL_CREAMPIE_FINISH_SUBFOLDER) && girl.doAnalCreampie)
            || (folder.Equals(StaticStrings.THREESOME_FINISH_SUBFOLDER) && girl.doThreesomeFinish)
            || (folder.Equals(StaticStrings.FOURSOME_FINISH_SUBFOLDER) && girl.doGroupFinish)
            );
    }

    private static int ChooseVideoSubFolderIndex(string subfolder1, string subfolder2, string subfolder3, GirlClass girl)
    {
        int random = Random(2);
        if (CanSeeVideo(subfolder1, girl) && !CanSeeVideo(subfolder2, girl) && !CanSeeVideo(subfolder3, girl))
            random = 0;
        else if (!CanSeeVideo(subfolder1, girl) && CanSeeVideo(subfolder2, girl) && !CanSeeVideo(subfolder3, girl))
            random = 1;
        else if (!CanSeeVideo(subfolder1, girl) && !CanSeeVideo(subfolder2, girl) && CanSeeVideo(subfolder3, girl))
            random = 2;
        else if (CanSeeVideo(subfolder1, girl) && CanSeeVideo(subfolder2, girl) && !CanSeeVideo(subfolder3, girl))
            random = UnityEngine.Random.Range(0, 2);
        else if (CanSeeVideo(subfolder1, girl) && !CanSeeVideo(subfolder2, girl) && CanSeeVideo(subfolder3, girl))
        {
            random = UnityEngine.Random.Range(0, 2);
            if (random == 1) random = 2;
        }
        else if (!CanSeeVideo(subfolder1, girl) && CanSeeVideo(subfolder2, girl) && CanSeeVideo(subfolder3, girl))
        {
            random = UnityEngine.Random.Range(1, 3);
        }
        return random;
    }

    /// <summary>
    ///Selects the correct video clip for the corresponding work
    ///Used for all selection of this type, to include the policies in the choice
    ///policy1 is the last policy in the hierarchy (e.g. topless for the dance policies)
    ///policy2 is the first policy in the hierarchy (e.g. closer for the dance policies)
    ///mainFolder is the main prestation folder (e.g. Dance for the dance prestations)
    ///subfolder1 is the basic sub prestation subfolder (e.g. Dance for the dance prestations)
    ///subfolder2 is the second prestation subfolder (e.g. Closer for the dance prestations)
    ///subfolder3 is the third prestation subfolder (e.g. Topless for the dance prestations)
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="work"></param>
    /// <param name="girlName"></param>
    /// <param name="policy1"></param>
    /// <param name="policy2"></param>
    /// <param name="mainFolder"></param>
    /// <param name="subfolder1"></param>
    /// <param name="subfolder2"></param>
    /// <param name="subfolder3"></param>
    /// <param name="girl"></param>
    /// <param name="clip"></param>
    /// <returns>The nale of the subfolder the video is chosen from</returns>
    private static string SelectWorkVideoClipGeneric(string folder, Work work, string girlName,
        bool policy1, bool policy2, string mainFolder, string subfolder1, string subfolder2, string subfolder3, GirlClass girl, VideoPlayer clip)
    {
        if (!girl.external)
        {
            if (policy1)
            {

                switch (ChooseVideoSubFolderIndex(subfolder1,subfolder2,subfolder3, girl))
                {
                    case 0:
                        SelectRandomVideoClipFromFolder(folder
                            + mainFolder
                            + subfolder1, girlName, girl, clip);
                        return subfolder1;
                    case 1:
                        SelectRandomVideoClipFromFolder(folder
                            + mainFolder
                            + subfolder2, girlName, girl, clip);
                        return subfolder2;
                    case 2:
                        SelectRandomVideoClipFromFolder(folder
                            + mainFolder
                            + subfolder3, girlName, girl, clip);
                        return subfolder3;
                }
            }
            else if (policy2)
            {
                switch (Random(1))
                {
                    case 0:
                        if (CanSeeVideo(subfolder1, girl))
                        {
                            SelectRandomVideoClipFromFolder(folder
                                + mainFolder
                                + subfolder1, girlName, girl, clip);
                            return subfolder1;
                        }
                        else
                        {
                            SelectRandomVideoClipFromFolder(folder
                            + mainFolder
                            + subfolder2, girlName, girl, clip);
                            return subfolder2;
                        }

                    case 1:
                        if (CanSeeVideo(subfolder2, girl))
                        {
                            SelectRandomVideoClipFromFolder(folder
                                + mainFolder
                                + subfolder2, girlName, girl, clip);
                            return subfolder2;
                        }
                        else
                        {
                            SelectRandomVideoClipFromFolder(folder
                            + mainFolder
                            + subfolder1, girlName, girl, clip);
                            return subfolder1;
                        }
                }
            }
            else
            {
                SelectRandomVideoClipFromFolder(folder
                    + mainFolder
                    + subfolder1, girlName, girl, clip);
                return subfolder1;
            }
        }
        else
        {
            if (policy1)
            {
                switch (ChooseVideoSubFolderIndex(subfolder1, subfolder2, subfolder3, girl))
                {
                    case 0:
                        SelectRandomVideoClipFromExternalFolder(folder
                            + mainFolder
                            + subfolder1, girlName, clip, girl);
                        return subfolder1;
                    case 1:
                        SelectRandomVideoClipFromExternalFolder(folder
                            + mainFolder
                            + subfolder2, girlName, clip, girl);
                        return subfolder2;
                    case 2:
                        SelectRandomVideoClipFromExternalFolder(folder
                            + mainFolder
                            + subfolder3, girlName, clip, girl);
                        return subfolder3;
                }
            }
            else if (policy2)
            {
                switch (Random(1))
                {
                    case 0:
                        if (CanSeeVideo(subfolder1, girl))
                        {
                            SelectRandomVideoClipFromExternalFolder(folder
                            + mainFolder
                            + subfolder1, girlName, clip, girl);
                            return subfolder1;
                        }
                        else
                        {
                            SelectRandomVideoClipFromExternalFolder(folder
                            + mainFolder
                            + subfolder2, girlName, clip, girl);
                            return subfolder2;
                        }

                            
                    case 1:
                        if (CanSeeVideo(subfolder2, girl))
                        {
                            SelectRandomVideoClipFromExternalFolder(folder
                            + mainFolder
                            + subfolder2, girlName, clip, girl);
                            return subfolder2;
                        }
                        else
                        {
                            SelectRandomVideoClipFromExternalFolder(folder
                            + mainFolder
                            + subfolder1, girlName, clip, girl);
                            return subfolder1;
                        }

                }
            }
            else
            {
                SelectRandomVideoClipFromExternalFolder(folder
                    + mainFolder
                    + subfolder1, girlName, clip, girl);
                return subfolder1;
            }
        }
        return null;
    }
    /*
    private static VideoClip SelectWorkVideoClipDance(string folder, Work work, string girlName)
    {
        if (GMPoliciesData.policies.danceTopless)
        {
            switch (Random(3))
            {
                case 0:
                    return SelectRandomVideoClipFromFolder(folder
                        + StaticStrings.DANCE_FOLDER
                        + StaticStrings.DANCE_SUBFOLDER, girlName);
                case 1:
                    return SelectRandomVideoClipFromFolder(folder
                        + StaticStrings.DANCE_FOLDER
                        + StaticStrings.CLOSER_DANCE_SUBFOLDER, girlName);
                case 2:
                    return SelectRandomVideoClipFromFolder(folder
                        + StaticStrings.DANCE_FOLDER
                        + StaticStrings.TOPLESS_DANCE_SUBFOLDER, girlName);
            }
        }
        else if (GMPoliciesData.policies.danceCloser)
        {
            switch (Random(2))
            {
                case 0:
                    return SelectRandomVideoClipFromFolder(folder
                        + StaticStrings.DANCE_FOLDER
                        + StaticStrings.CLOSER_DANCE_SUBFOLDER, girlName);
                default:
                    return SelectRandomVideoClipFromFolder(folder
                        + StaticStrings.DANCE_FOLDER
                        + StaticStrings.DANCE_SUBFOLDER, girlName);
            }
        }

        return SelectRandomVideoClipFromFolder(folder
            + StaticStrings.DANCE_FOLDER
            + StaticStrings.DANCE_SUBFOLDER, girlName);
    }
    */
    //Selects a work video clip, according to what the girl can do and the current policies
    public static void SelectWorkVideoClip(string folder, Work work, string girlName, GirlClass girl, VideoPlayer clip, Booth booth)
    {
        switch (work)
        {
            case Work.DANCE:
                booth.nameOfFolderForPerformanceType = SelectWorkVideoClipGeneric(folder, work, girlName, GMPoliciesData.policies.danceTopless,
   GMPoliciesData.policies.danceCloser, StaticStrings.DANCE_FOLDER, StaticStrings.DANCE_SUBFOLDER,
   StaticStrings.CLOSER_DANCE_SUBFOLDER, StaticStrings.TOPLESS_DANCE_SUBFOLDER, girl, clip);
                break;

            case Work.POSE:
                booth.nameOfFolderForPerformanceType = SelectWorkVideoClipGeneric(folder, work, girlName, GMPoliciesData.policies.mastToy,
    GMPoliciesData.policies.soloHand, StaticStrings.POSE_FOLDER, StaticStrings.NAKED_SUBFOLDER,
    StaticStrings.HAND_MASTURBATION_SUBFOLDER, StaticStrings.TOY_MASTURBATION_SUBFOLDER, girl, clip); break;

            case Work.FOREPLAY:
                booth.nameOfFolderForPerformanceType = SelectWorkVideoClipGeneric(folder, work, girlName, GMPoliciesData.policies.titsjob,
GMPoliciesData.policies.footjob, StaticStrings.FOREPLAY_FOLDER, StaticStrings.HANDJOB_SUBFOLDER, StaticStrings.FOOTJOB_SUBFOLDER,
StaticStrings.TITSJOB_SUBFOLDER, girl, clip); break;

            case Work.ORAL:
                booth.nameOfFolderForPerformanceType = SelectWorkVideoClipGeneric(folder, work, girlName, GMPoliciesData.policies.facefuck,
    GMPoliciesData.policies.deepthroat, StaticStrings.ORAL_FOLDER, StaticStrings.BLOWJOB_SUBFOLDER,
    StaticStrings.DEEPTHROAT_SUBFOLDER, StaticStrings.FACEFUCK_SUBFOLDER, girl, clip); break;

            case Work.SEX:
                if (girl.isBeforeMissionaryRenamePack)
                {
                    booth.nameOfFolderForPerformanceType = SelectWorkVideoClipGeneric(folder, work, girlName, GMPoliciesData.policies.anal,
         GMPoliciesData.policies.backVaginal, StaticStrings.SEX_FOLDER, StaticStrings.MISSIONARY_SUBFOLDER,
         StaticStrings.DOGGYSTYLE_SUBFOLDER, StaticStrings.ANAL_SEX_SUBFOLDER, girl, clip);
                }
                else
                {
                    booth.nameOfFolderForPerformanceType = SelectWorkVideoClipGeneric(folder, work, girlName, GMPoliciesData.policies.anal,
        GMPoliciesData.policies.backVaginal, StaticStrings.SEX_FOLDER, StaticStrings.VAGINAL_FACING_SUBFOLDER,
        StaticStrings.VAGINAL_BACK_SUBFOLDER, StaticStrings.ANAL_SEX_SUBFOLDER, girl, clip);
                }
                break;


            case Work.GROUP:
                booth.nameOfFolderForPerformanceType = SelectWorkVideoClipGeneric(folder, work, girlName, GMPoliciesData.policies.orgy,
   GMPoliciesData.policies.foursome, StaticStrings.GROUP_FOLDER, StaticStrings.THREESOME_SUBFOLDER,
   StaticStrings.FOURSOME_SUBFOLDER, StaticStrings.ORGY_SUBFOLDER, girl, clip); break;
        }

    }

    /*
    public static VideoClip SelectOccupationVideoClip(Occupation occupation, string girlName, Work work = Work.NONE, GirlClass girl)
    {
        switch (occupation)
        {
            case Occupation.TRAIN: return SelectWorkVideoClip(StaticStrings.TRAIN_FOLDER, work, girlName);
            case Occupation.TALK: return SelectRandomVideoClipFromFolder(StaticStrings.TALK_FOLDER, girlName);
            case Occupation.WORK: return SelectWorkVideoClip(StaticStrings.WORK_FOLDER, work, girlName);
            case Occupation.REST: return SelectRandomVideoClipFromFolder(StaticStrings.REST_FOLDER, girlName);
            default: return null;
        }
    }
    */


    //Returns a random integer between 0 and max (included)
    public static int Random(int max)
    {
        return UnityEngine.Random.Range(0, max + 1);
    }

    public static void Save(string saveFile)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFile);

        bf.Serialize(file, GMClubData.money);
        bf.Serialize(file, GMClubData.GetReputation());
        bf.Serialize(file, GMClubData.GetInfluence());
        bf.Serialize(file, GMClubData.GetConnection());
        bf.Serialize(file, GMClubData.day);

        bf.Serialize(file, GMRecruitmentData.recruitedGirlsList);



        bf.Serialize(file, GMPoliciesData.policies);
        bf.Serialize(file, GMPoliciesData.ownedPolicies);

        bf.Serialize(file, GMImprovementsData.improvementsData);
        bf.Serialize(file, GMImprovementsData.boughtImprovements);
        // bf.Serialize(file, GameMasterGlobalData.clubImprovementList);

        bf.Serialize(file, GMWardrobeData.costumesData);
        bf.Serialize(file, GMWardrobeData.ownedCostumes);
        bf.Serialize(file, GMWardrobeData.currentlyUsedCostume);
        //bf.Serialize(file, GameMasterGlobalData.costumeList);

        bf.Serialize(file, RegisterDialogs.dialogs);

        bf.Serialize(file, RegisterMissions.missions);
        bf.Serialize(file, RegisterMissions.availableMissions);
        bf.Serialize(file, RegisterMissions.activeMissions);

        bf.Serialize(file, StaticDialogElements.dialogData);

        bf.Serialize(file, GMCrimeServiceData.unlockedCrimeServices);

        bf.Serialize(file, GMRecruitmentData.firedGirlsList);

        bf.Serialize(file, InteractionSceneBehavior.availableTalks);

        bf.Serialize(file, StaticAssistantData.data);

        bf.Serialize(file, RegisterAssistantCostumes.assistantCostumes);

        bf.Serialize(file, RegisterDates.dates);

        bf.Serialize(file, RegisterAssistantStoreItems.assistantItemsList);

        bf.Serialize(file, NewPlanningBehaviors.boothsSpecialties);

        bf.Serialize(file, RegisterPersonalImprovements.personalImprovements);
        /*
    bf.Serialize(file, GMGlobalNumericVariables.gnv.EARN_MONEY_GENERIC_MULTIPLIER);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.REPUTATION_GAIN);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.TRAINING_MULTIPLIER);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.REST_ENERGY_GAIN);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.ASK_HELP_BONUS_MULTIPLIER);

    bf.Serialize(file, GMGlobalNumericVariables.gnv.EARN_MONEY_DRINK_HAPPINESS_BOOST);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.EARN_MONEY_FOOD_HAPPINESS_BOOST);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.EARN_MONEY_DRUG_HAPPINESS_BOOST);
    bf.Serialize(file, GMGlobalNumericVariables.gnv.EARN_MONEY_CONDOM_HAPPINESS_BOOST);

    bf.Serialize(file, GMGlobalNumericVariables.gnv.BOOTH_GAME_TIME);
    */
        //SaveStatics();

        //bf.Serialize(file, le);

        //bf.Serialize(file,GMGlobalNumericVariables.gnv);

        file.Close();
    }

    public static bool TestLoad(string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
        bool isOK = true;
        try
        {


                bf.Deserialize(file);
            bf.Deserialize(file);
            bf.Deserialize(file);
            bf.Deserialize(file);
            bf.Deserialize(file);

            bf.Deserialize(file);

            bf.Deserialize(file);
            bf.Deserialize(file);

            bf.Deserialize(file);
            bf.Deserialize(file);


            bf.Deserialize(file);
            bf.Deserialize(file);
            bf.Deserialize(file);

            try
            {
                //This is the deserialization of the dialogs (maybe ?)
                bf.Deserialize(file);

            }

            catch
            {
                Debug.Log("Dialog deserialization failed");
                //FindDialogWithID(GMGlobalNumericVariables.gnv.FOURTH_MISION_OPEN_DIALOG_ID);
            }

            bf.Deserialize(file);
            bf.Deserialize(file);
            bf.Deserialize(file);

            try
            {
                bf.Deserialize(file);

                bf.Deserialize(file);

                bf.Deserialize(file);

                bf.Deserialize(file);
            }
            catch
            {
                Debug.Log("Detected an old save that shouldn't pose problem with the loading.");
            }
        }
        catch(Exception e)
        {
            isOK = false;

            Debug.LogWarning(e.Message);
        }
        file.Close();

        return isOK;
    }

    //Returns true if the loading happened without error
    public static bool Load(string fileName)
    {
        bool isOK = true;

        StaticBooleans.tutorialIsOn = false;

        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            GMGlobalNumericVariables.gnv = new GlobalNumericVariables();
            StaticBooleans.InitializeBooleans();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);


            try
            {
                GMClubData.money = (int)bf.Deserialize(file);
                GMClubData.SetReputation((float)bf.Deserialize(file));
                GMClubData.SetInfluence((float)bf.Deserialize(file));
                GMClubData.SetConnection((float)bf.Deserialize(file));
                GMClubData.day = (int)bf.Deserialize(file);

                GMRecruitmentData.recruitedGirlsList = (List<GirlClass>)bf.Deserialize(file);

                List<GirlClass> toRemove = new List<GirlClass>();

               foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
                {
                    foreach (GirlClass g in GameMasterGlobalData.girlsList)
                    {
                        //In case the assets have been modified between now and last session
                        //update the various variables


                        if (girl.name.Equals(g.name)) {
                            girl.eyeColor = g.eyeColor;
                            girl.eyes = g.eyes;
                            girl.bust = g.bust;
                            girl.bustType = g.bustType;
                            girl.height = g.height;
                            girl.age = g.age;
                            girl.body = g.body;
                            girl.bodyType = g.bodyType;
                            girl.hair = g.hair;
                            girl.hairColor = g.hairColor;
                            girl.skin = g.skin;
                            girl.skinComplexion = g.skinComplexion;
                            girl.specialSkill = g.specialSkill;
                            girl.specialty = g.specialty;
                            //Necessary for the multipleFinish because a save file could contain an "old" girl for which the muiltipleFinish 
                            //does not exist and is therefore set to false
                            girl.doThreesomeFinish = g.doThreesomeFinish;
                            girl.doGroupFinish = g.doGroupFinish;

                            string sexFolder = StaticStrings.GIRLPACKS_DIRECTORY + girl.name + "/" + StaticStrings.VIDEOS_FOLDER + StaticStrings.PERFORMANCES_FOLDER + StaticStrings.WORK_FOLDER + StaticStrings.SEX_FOLDER;
                            string missioFolder = sexFolder + StaticStrings.MISSIONARY_SUBFOLDER;
                            string facingVagFolder = sexFolder + StaticStrings.VAGINAL_FACING_SUBFOLDER;
                            if (Directory.Exists(missioFolder) && !Directory.Exists(facingVagFolder))
                            {
                                //If the Missionary and/or Doggystyle folder exists AND the FacingVaginal and BackVaginal do not
                                girl.isBeforeMissionaryRenamePack = true;
                            }
                            else
                            {
                                girl.isBeforeMissionaryRenamePack = false;
                            }
                            if (g.isBeforeMissionaryRenamePack)
                            {
                                //Necessary because a girl could be from before the missionary to facing vaginal renaming update
                                girl.doFacingVaginal = g.doMissionary;
                                girl.doBackVaginal = g.doDoggystyle;
                            }
                            else
                            {
                                girl.doFacingVaginal = g.doFacingVaginal;
                                girl.doBackVaginal = g.doBackVaginal;
                            }

                            if (girl.external)
                            {
                                girl.SetPortrait(g.GetPortrait());
                                girl.closeupPortrait = g.closeupPortrait;

                            }

                            foreach (string s in StaticStrings.OLD_INTERNAL_GIRLS_NAMES) {
                                if (s.Equals(girl.name) && !girl.external)
                                {
                                    //If the girl is one of the "old" internal girls and it is still external, import the portrait
                                    girl.SetPortrait(g.GetPortrait());
                                    girl.closeupPortrait = g.closeupPortrait;
                                    girl.external = g.external;
                                }
                            }
                            
                                
                            //girl.girlDialogs = g.girlDialogs;

                            if (girl.girlLessons != null)
                            {
                                List<GirlLesson> toRemoveLesson = new List<GirlLesson>();
                                for (int i = 0; i < girl.girlLessons.Count; i++)
                                {
                                    GirlLesson recruitedGL = girl.girlLessons[i];
                                    bool found = false;
                                    foreach (GirlLesson globalGL in g.girlLessons)
                                    {

                                        if (recruitedGL.fileName == globalGL.fileName)
                                        {

                                            globalGL.done = recruitedGL.done;
                                            found = true;
                                            recruitedGL = globalGL;

                                            break;
                                        }
                                    }
                                    recruitedGL.girl = girl;
                                    if (!found)
                                    {
                                        toRemoveLesson.Add(recruitedGL);
                                    }
                                }
                                if (toRemoveLesson.Count > 0)
                                {
                                    foreach (GirlLesson gl in toRemoveLesson)
                                        girl.girlLessons.Remove(gl);
                                }
                            }
                            girl.girlLessons = g.girlLessons;
                        }
                    }
                    if (girl.folderName == "" || girl.folderName == null)
                    {
                        girl.folderName = girl.name;
                    }

                    if (girl.external && !Directory.Exists(StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + "/"))
                    {
                        toRemove.Add(girl);
                    }

                    if(girl.videosSeen == null)
                    {
                        girl.videosSeen = new List<string>();
                    }

                    if (GameMasterGlobalData.girlsList.Find(delegate (GirlClass g) { return g.name == girl.name; }) == null)
                    {
                        toRemove.Add(girl);
                    }
                }

                //Cleans the recruited girls list if a girl that used to be present as an external asset is removed
                if(toRemove.Count > 0)
                {
                    foreach(GirlClass g in toRemove)
                        GMRecruitmentData.recruitedGirlsList.Remove(g);
                }

                GMPoliciesData.policies = (PoliciesData)bf.Deserialize(file);
                List <Policy> tmpListPol = (List<Policy>)bf.Deserialize(file);

                foreach(Policy pol in tmpListPol)
                {
                    foreach(Policy pol2 in GameMasterGlobalData.policiesList)
                    {
                        if (pol.name.Equals(pol2.name))
                        {
                            GMPoliciesData.ownedPolicies.Add(pol2);
                        }
                    }
                }

                GMImprovementsData.improvementsData = (ImprovementsData)bf.Deserialize(file);

                GMImprovementsData.boughtImprovements = (List<Improvement>)bf.Deserialize(file);
                //GameMasterGlobalData.clubImprovementList = (List<Improvement>)bf.Deserialize(file);

                for (int i = 0; i < GameMasterGlobalData.clubImprovementList.Count; i++)
                {
                    for (int j = 0; j < GMImprovementsData.boughtImprovements.Count; j++)
                    {
                        if (GameMasterGlobalData.clubImprovementList[i].name == GMImprovementsData.boughtImprovements[j].name)
                        {
                            GameMasterGlobalData.clubImprovementList[i].currentLevel = GMImprovementsData.boughtImprovements[j].currentLevel;
                            GMImprovementsData.boughtImprovements[j] = GameMasterGlobalData.clubImprovementList[i];
                        }
                    }
                }

                GMGlobalNumericVariables.gnv = new GlobalNumericVariables();
                StaticBooleans.InitializeBooleans();
                foreach (Improvement imp in GMImprovementsData.boughtImprovements)
                {
                    ReBuildImprovementsModifiers(imp.name,imp);
                }

                foreach (Policy pol in GMPoliciesData.ownedPolicies)
                {
                    BuildPoliciessModifiers(pol.name, pol);
                }

                GMWardrobeData.costumesData = (CostumesData)bf.Deserialize(file);
                GMWardrobeData.ownedCostumes = (List<Costume>)bf.Deserialize(file);
                GMWardrobeData.currentlyUsedCostume = (Costume)bf.Deserialize(file);

                for (int i = 0; i < GameMasterGlobalData.costumeList.Count; i++)
                {
                    for (int j = 0; j < GMWardrobeData.ownedCostumes.Count; j++)
                    {
                        if (GameMasterGlobalData.costumeList[i].name == GMWardrobeData.ownedCostumes[j].name)
                        {
                            GameMasterGlobalData.costumeList[i].currentLevel = GMWardrobeData.ownedCostumes[j].currentLevel;
                            GMWardrobeData.ownedCostumes[j] = GameMasterGlobalData.costumeList[i];
                        }
                    }
                }

                List<Dialog> tmpDial = (List<Dialog>)bf.Deserialize(file);

                for(int i = 0; i < tmpDial.Count; i++)
                {
                    for (int j = 0; j < RegisterDialogs.dialogs.Count; j++)
                    {
                        if (tmpDial[i].id == RegisterDialogs.dialogs[j].id)
                        {
                            RegisterDialogs.dialogs[j].done = tmpDial[i].done;
                            RegisterDialogs.dialogs[j].seen = tmpDial[i].seen;
                            //tmpDial[i].trigger = RegisterDialogs.dialogs[j].trigger;
                        }
                    }
                    //d.trigger = RegisterDialogs.dialogs
                }

                //RegisterDialogs.dialogs = tmpDial;

                List < Mission > tmpMission = (List<Mission>)bf.Deserialize(file);
                RegisterMissions.availableMissions = (List<Mission>)bf.Deserialize(file);
                RegisterMissions.activeMissions = (List<Mission>)bf.Deserialize(file);

                foreach (Mission m in RegisterMissions.missions)
                {
                    foreach (Mission mission in tmpMission)
                    {

                        if (mission.id == m.id)
                        {
                            m.isActive = mission.isActive;
                            m.isTriggered = mission.isTriggered;
                            m.isDone = mission.isDone;
                            /*
                            mission.checkDoable = m.checkDoable;
                            mission.effect = m.effect;
                            mission.effectWhenTriggered = m.effectWhenTriggered;
                            mission.trigger = m.trigger;
                            */
                        }
                    }
                    for (int i =0; i < RegisterMissions.availableMissions.Count; i++)
                    {
                        if(m.id == RegisterMissions.availableMissions[i].id)
                        {
                            RegisterMissions.availableMissions[i] = m;
                        }
                    }
                    for (int i = 0; i < RegisterMissions.activeMissions.Count; i++)
                    {
                        if (m.id == RegisterMissions.activeMissions[i].id)
                        {
                            RegisterMissions.activeMissions[i] = m;
                        }
                    }

                }

                //RegisterMissions.missions = tmpMission;
            }
            catch (Exception e)
            {
                isOK = false;
                Debug.Log(e);
            }

            try
            {
                StaticDialogElements.dialogData = (StaticDialogElements.DialogData)bf.Deserialize(file);
            }
            catch
            {
                Debug.LogWarning("DialogData does not exist.");
            }

            try
            {
                List<CrimeService> tmpCS = (List<CrimeService>)bf.Deserialize(file);
                foreach (CrimeService cs in GMCrimeServiceData.crimeServices)
                {
                    foreach (CrimeService newCS in tmpCS)
                    {
                        if (cs.id == newCS.id)
                        {
                            cs.isSubscribed = newCS.isSubscribed;
                            cs.isUnlocked = newCS.isUnlocked;
                            GMCrimeServiceData.unlockedCrimeServices.Add(cs);
                        }
                    }
                }
            }
            catch
            {
                Debug.Log("Pre CrimeService update savefile detected.");
                //Those loops are there to correct the new girls when detecting a pre crime service update savefile
                foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
                {
                    foreach (GirlClass g in GameMasterGlobalData.girlsList)
                    {
                        if (girl.folderName.Equals(g.folderName))
                        {
                            girl.CopyDos(g);
                        }
                    }
                }
            }

            try
            {
                List<GirlClass> toRemove = new List<GirlClass>();
                GMRecruitmentData.firedGirlsList = (List<GirlClass>)bf.Deserialize(file);
                foreach(GirlClass girl in GMRecruitmentData.firedGirlsList)
                {
                    bool girlExists = false;
                    foreach (GirlClass g in GameMasterGlobalData.girlsList)
                    {
                        if (girl.name.Equals(g.name))
                        {
                            girlExists = true;
                            g.CopyFiredGirl(girl);
                        }
                    }
                    if (!girlExists)
                    {
                        //If the girl doesn't exist, it means that she has been removed from the girlpacks, 
                        //and so she must be removed from the fired list
                        toRemove.Add(girl);
                    }
                }
                foreach(GirlClass girl in toRemove)
                {
                    GMRecruitmentData.firedGirlsList.Remove(girl);
                }

            }
            catch
            {
                Debug.Log("Pre firing girls update savefile detected.");
                GMRecruitmentData.firedGirlsList = new List<GirlClass>();
            }

            try
            {
                InteractionSceneBehavior.availableTalks = (int)bf.Deserialize(file);
            }
            catch
            {
                Debug.Log("Pre talking with girls update savefile detected.");
                InteractionSceneBehavior.availableTalks = GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY;
            }

            try
            {
                StaticAssistantData.data = (AssistantData)bf.Deserialize(file);

                List<AssistantCostume> listAC = (List<AssistantCostume>)bf.Deserialize(file);

                AssistantCostume ac = RegisterAssistantCostumes.FindAssistantCostumeWithName(StaticStrings.ASSISTANT_STARTING_OUTFIT);

                if (StaticAssistantData.data.currentCostume == null)
                {
                    StaticAssistantData.data.currentCostume = ac;
                }
                else
                {
                    StaticAssistantData.data.currentCostume = RegisterAssistantCostumes.FindAssistantCostumeWithName(StaticAssistantData.data.currentCostume.name);
                }

                if(StaticAssistantData.data.datesAvailablePerDay == 0)
                {
                    StaticAssistantData.data.datesAvailablePerDay = GMGlobalNumericVariables.gnv.MINIMUM_DATES_PER_DAY;
                    StaticAssistantData.data.datesAvailableToday = StaticAssistantData.data.datesAvailablePerDay;
                }

                if (FindDialogWithID(GMGlobalNumericVariables.gnv.FOURTH_MISION_OPEN_DIALOG_ID).seen && ac.currentLevel < 1)
                {
                    ac.currentLevel++;
                }

                for (int i = 0; i < RegisterAssistantCostumes.assistantCostumes.Count; i++)
                {
                    for (int j = 0; j < listAC.Count; j++)
                    {
                        if(RegisterAssistantCostumes.assistantCostumes[i].name == listAC[j].name)
                        {
                            RegisterAssistantCostumes.assistantCostumes[i].bought = listAC[j].bought;
                            RegisterAssistantCostumes.assistantCostumes[i].currentLevel = listAC[j].currentLevel;

                            //Debug.Log(RegisterAssistantCostumes.assistantCostumes[i].sprites.Count);
                        }
                    }
                }

                

            }
            catch
            {
                Debug.LogWarning("Assistant store data does not exist.");
                StaticAssistantData.data = new AssistantData();
                StaticAssistantData.data.currentCostume = RegisterAssistantCostumes.FindAssistantCostumeWithName(StaticStrings.ASSISTANT_STARTING_OUTFIT);
                if (StaticAssistantData.data.currentCostume == null)
                    StaticAssistantData.data.currentCostume = RegisterAssistantCostumes.FindAssistantCostumeWithName(StaticStrings.ASSISTANT_STARTING_OUTFIT);
                if (FindDialogWithID(GMGlobalNumericVariables.gnv.FOURTH_MISION_OPEN_DIALOG_ID).seen)
                {
                    RegisterAssistantCostumes.FindAssistantCostumeWithName(StaticStrings.ASSISTANT_STARTING_OUTFIT).currentLevel++;
                }

            }

            try
            {
                List<DateData> registeredDates = (List<DateData>)bf.Deserialize(file);
                for(int i = 0; i < registeredDates.Count; i++)
                {
                    for(int j = 0; j < RegisterDates.dates.Count; j++)
                    {
                        if(registeredDates[i].id == RegisterDates.dates[j].id)
                        {
                            RegisterDates.dates[j].bought = registeredDates[i].bought;
                            RegisterDates.dates[j].currentBackgroundLevel = registeredDates[i].currentBackgroundLevel;
                            RegisterDates.dates[j].currentPortraitLevel = registeredDates[i].currentPortraitLevel;
                            RegisterDates.dates[j].timesDone = registeredDates[i].timesDone;
                            RegisterDates.dates[j].topScore = registeredDates[i].topScore;
                            RegisterDates.dates[j].videoLevel = registeredDates[i].videoLevel;
                            RegisterDates.dates[j].currentVideoTokenCollected = registeredDates[i].currentVideoTokenCollected;
                        }
                    }
                }
            }

            catch
            {
                Debug.LogWarning("No dates data found.");
            }

            try
            {
                List<AssistantStoreItem> storeItems = (List<AssistantStoreItem>)bf.Deserialize(file);
                for (int i = 0; i < storeItems.Count; i++)
                {
                    for (int j = 0; j < RegisterAssistantStoreItems.assistantItemsList.Count; j++)
                    {
                        if (storeItems[i].name == RegisterAssistantStoreItems.assistantItemsList[j].name)
                        {
                            RegisterAssistantStoreItems.assistantItemsList[j].currentLevel = storeItems[i].currentLevel;
                        }
                    }
                }
            }
            catch
            {
                Debug.LogWarning("No store items data found.");
            }

            try
            {
                NewPlanningBehaviors.boothsSpecialties = (Work[])bf.Deserialize(file);
            }
            catch
            {
                Debug.LogWarning("No booth specialty found.");
                NewPlanningBehaviors.boothsSpecialties = new Work[]{Work.NONE, Work.NONE, Work.NONE, Work.NONE, Work.NONE, Work.NONE };
            }

            try
            {
                List<PersonalImprovement> personalImprovements = (List<PersonalImprovement>)bf.Deserialize(file);

                for(int i= 0; i < RegisterPersonalImprovements.personalImprovements.Count; i++)
                {
                    for(int j = 0; j < personalImprovements.Count; j++)
                    {
                        if(RegisterPersonalImprovements.personalImprovements[i].name == personalImprovements[j].name)
                        {
                            RegisterPersonalImprovements.personalImprovements[i].currentLevel = personalImprovements[j].currentLevel;
                        }
                    }
                }

                foreach (PersonalImprovement imp in personalImprovements)
                {
                    
                    int currentLevel = imp.currentLevel;
                    for (int i = 0; i < currentLevel; i++)
                    {
                        PersonalImprovement auxImp = imp;
                        int aux = i;
                        auxImp.currentLevel = aux;
                        BuyPersonalImprovement(auxImp);
                    }
                    imp.currentLevel = currentLevel;
                }

            }
            catch
            {
                Debug.LogWarning("No personal improvements list found.");
            }

            file.Close();

            StaticBooleans.gameIsLoaded = true;
        }


        
        return isOK;
    }


    private static void ReBuildImprovementsModifiers(string improvementName, Improvement imp)
    {
        if (improvementName == StaticStrings.ADVERTISEMENTS_IMPROVEMENT_NAME)
        {
            if (imp.currentLevel > 0)
            {
                GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER -= (GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER * 0.1f);
            }
            if (imp.currentLevel > 1)
            {
                GMGlobalNumericVariables.gnv.REPUTATION_GAIN += GMGlobalNumericVariables.gnv.BASIC_REPUTATION_GAIN * 0.1f;
            }
            if (imp.currentLevel > 2)
            {
                GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER -= (GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER * 0.1f);
            }
            if (imp.currentLevel > 3)
            {
                GMGlobalNumericVariables.gnv.REPUTATION_GAIN += GMGlobalNumericVariables.gnv.BASIC_REPUTATION_GAIN * 0.1f;
            }
        }
        //Action for the construction or improvement of the bar
        if (improvementName == StaticStrings.BAR_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.EARN_MONEY_DRINK_HAPPINESS_BOOST += (GMGlobalNumericVariables.gnv.IMPROVEMENT_HAPPINESS_BOOST * imp.currentLevel - 1);
        }
        //Action for the construction or improvement of the cigarette dispenser
        else if (improvementName == StaticStrings.CIGARETTES_DISPENSER_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.EARN_MONEY_CIGARETTES_HAPPINESS_BOOST += (GMGlobalNumericVariables.gnv.IMPROVEMENT_HAPPINESS_BOOST * imp.currentLevel - 1);
        }
        //Action for the construction or improvement of the pharmacy
        else if (improvementName == StaticStrings.PHARMACY_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.EARN_MONEY_DRUG_HAPPINESS_BOOST += (GMGlobalNumericVariables.gnv.IMPROVEMENT_HAPPINESS_BOOST * imp.currentLevel - 1);
        }
        //Action for the construction or improvement of the condom dispenser
        else if (improvementName == StaticStrings.CONDOM_DISPENSER_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.EARN_MONEY_CONDOM_HAPPINESS_BOOST += (GMGlobalNumericVariables.gnv.IMPROVEMENT_HAPPINESS_BOOST * imp.currentLevel - 1);
        }
        else if (improvementName == StaticStrings.PRESTATIONS_ROOMS_IMPROVEMENT_NAME)
        {
            //Nothing to do here
        }
        else if (improvementName == StaticStrings.UPGRADE_CLUB_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.CLUB_LEVEL += imp.currentLevel;
            if (imp.currentLevel >= imp.maxLevel)
                GMGlobalNumericVariables.gnv.CLUB_LEVEL = 100;
        }
        else if (improvementName == StaticStrings.LOUNGE_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY += GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY_INCREASE * (imp.currentLevel - 1 );
        }
        else if (improvementName == StaticStrings.MODULAR_BOOTHS_IMPROVEMENT_NAME)
        {
            for (int i = 0; i < imp.currentLevel; i++)
            {
                GMImprovementsData.improvementsData.SetModularBooths(i);
            }
        }
        else
        {
            //The standard case is to use the basic building function
            for (int i = 0; i < imp.currentLevel; i++)
            {
                BuildImprovementsModifiers(imp.name, imp);
            }
        }
    }

    /// <summary>
    /// Does whatever is needed when buying an item from the assistant store.
    /// Used when buying an item or loading a game.
    /// TODO
    /// </summary>
    /// <param name="item">The item bought</param>
    public static void BuyAssistantStoreItem(AssistantStoreItem item)
    {
        if(item.name == StaticStrings.UNLOCK_THIRD_NIGHT_MEETING_ITEM)
        {
            StaticAssistantData.data.thirdMeetingDialogBought = true;
        }
        else if (item.name == StaticStrings.ASSISTANT_UNLOCK_DANCING_ITEM)
        {
            GirlClass g = GameMasterGlobalData.girlsList.Find(
                    delegate (GirlClass girl) { return girl.name.Equals(StaticStrings.ASSISTANT_FULL_NAME); });

            GirlClass recruited = GMRecruitmentData.recruitedGirlsList.Find(
                    delegate (GirlClass girl) { return girl.name.Equals(StaticStrings.ASSISTANT_FULL_NAME); });

            switch (item.currentLevel)

            {
                case 0: GMRecruitmentData.recruitedGirlsList.Add(g); g.doDance = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockBasicDanceDialog = true;
                    break;
                case 1: g.doDanceCloser = true; recruited.doDanceCloser = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockCloserDanceDialog = true;
                    break;
                case 2: g.doDanceTopless = true; recruited.doDanceTopless = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockToplessDanceDialog = true;
                    break;
            }
        }
        else if (item.name == StaticStrings.ASSISTANT_UNLOCK_POSING_ITEM)
        {
            GirlClass g = GameMasterGlobalData.girlsList.Find(
                    delegate (GirlClass girl) { return girl.name.Equals(StaticStrings.ASSISTANT_FULL_NAME); });

            GirlClass recruited = GMRecruitmentData.recruitedGirlsList.Find(
                    delegate (GirlClass girl) { return girl.name.Equals(StaticStrings.ASSISTANT_FULL_NAME); });

            switch (item.currentLevel)

            {
                case 0:
                    g.doPoseNaked = true; recruited.doPoseNaked = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockPoseNakedDialog = true;
                    break;
                case 1:
                    g.doSoloFingering = true; recruited.doSoloFingering = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockSoloFingeringDialog = true;
                    break;
                case 2:
                    g.doToysMasturbation = true; recruited.doToysMasturbation = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockToysMasturbationDialog = true;
                    break;
            }
        }

        else if (item.name == StaticStrings.ASSISTANT_UNLOCK_FOREPLAY_ITEM)
        {
            GirlClass g = GameMasterGlobalData.girlsList.Find(
                    delegate (GirlClass girl) { return girl.name.Equals(StaticStrings.ASSISTANT_FULL_NAME); });

            GirlClass recruited = GMRecruitmentData.recruitedGirlsList.Find(
                    delegate (GirlClass girl) { return girl.name.Equals(StaticStrings.ASSISTANT_FULL_NAME); });

            switch (item.currentLevel)

            {
                case 0:
                    g.doHandjob = true; recruited.doHandjob = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockHandjobDialog = true;
                    break;
                case 1:
                    g.doFootjob = true; recruited.doFootjob = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockFootjobDialog = true;
                    break;
                case 2:
                    g.doTitsjob = true; recruited.doTitsjob = true;
                    StaticDialogElements.dialogData.launchAssistantUnlockTitsjobDialog = true;
                    break;
            }
        }

        else if (item.name == StaticStrings.ASSISTANT_UNLOCK_TENNIS_DATE_ITEM)
        {
            RegisterDates.dates.Find(x => x.directoryName == StaticStrings.DATE_TENNIS_NAME).bought = true;
            RegisterAssistantCostumes.FindAssistantCostumeWithName(StaticStrings.ASSISTANT_TENNIS_OUTFIT).bought = true;
        }

        else
        {
            AssistantCostume ac =  RegisterAssistantCostumes.FindAssistantCostumeWithName(item.name);
            if (ac.bought)
            {
                if (ac.name == StaticAssistantData.data.currentCostume.name &&
                    StaticAssistantData.data.currentCostumeLevel == ac.currentLevel)
                {
                    //If the current costume is the one bought and its level is the current maximum level,
                    //increase the displayed costume level
                    StaticAssistantData.data.currentCostumeLevel++;
                }
                ac.IncreaseLevel();
            }

            else
            {
                ac.bought = true;
            }
        }
    }

    public static void BuyPersonalImprovement(PersonalImprovement item)
    {
        if (item.name == StaticStrings.EYE_CONTACT_PERSONAL_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.MINIMUM_POINTS_KEPT_PER_DATE += GMGlobalNumericVariables.gnv.MINIMUM_POINTS_EARNED_PER_DATE_INCREASE;
        }
        else if (item.name == StaticStrings.FOCUS_TRAINING_PERSONAL_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.DATE_GAME_SPEED_MULTIPLIER -= GMGlobalNumericVariables.gnv.DATE_GAME_SPEED_MULTIPLIER_MODIFIER;
        }
        else if (item.name == StaticStrings.SHADES_PERSONAL_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.DATE_GAME_MAX_HP++;
        }
        else if(item.name == StaticStrings.LISTENING_PERSONAL_IMPROVEMENT_NAME)
        {
            if (item.currentLevel == 0){
                StaticBooleans.dateGameBetterScoreIncreaseBought = true;
            }
            else
            {
                GMGlobalNumericVariables.gnv.DATE_GAME_BETTER_SCORE_OBSTACLE_VALUE++;
            }
        }
        else if (item.name == StaticStrings.MAX_SCORE_INCREASE_PERSONAL_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.DATE_GAME_MAX_SCORE += GMGlobalNumericVariables.gnv.DATE_GAME_MAX_SCORE_INCREASE;
        }

        else if (item.name == StaticStrings.TIME_FREEZE_PERSONAL_IMPROVEMENT_NAME)
        {
            StaticBooleans.dateGameFreezeTimeUnlocked = true;
        }
    }

    //Builds, or rebuilds when loading, the data (global numbers) linked to the improvements
    public static void BuildImprovementsModifiers(string improvementName, Improvement imp, GameObject toClubBlocker = null)
    {
        //Action for the construction or improvement of the bar
        if (improvementName == StaticStrings.BAR_IMPROVEMENT_NAME)
        {
            if (imp.currentLevel == 0)
                GMImprovementsData.improvementsData.bar = true;
            else if (imp.MaxLevelWillBeReached())
            {
                GMImprovementsData.improvementsData.autoDrink = true;
            }
            else
                GMGlobalNumericVariables.gnv.EARN_MONEY_DRINK_HAPPINESS_BOOST += GMGlobalNumericVariables.gnv.IMPROVEMENT_HAPPINESS_BOOST;

        }
        //Action for the construction or improvement of the kitchen
        else if (improvementName == StaticStrings.KITCHEN_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.MAX_ENERGY += GMGlobalNumericVariables.gnv.MAX_ENERGY_GAIN;
        }
        //Action for the construction or improvement of the cigarette dispenser
        else if (improvementName == StaticStrings.CIGARETTES_DISPENSER_IMPROVEMENT_NAME)
        {
            if (imp.currentLevel == 0 )
                GMImprovementsData.improvementsData.cigarettesDispenser = true;
            else if (imp.MaxLevelWillBeReached())
            {
                GMImprovementsData.improvementsData.autoCigs = true;
            }
            else
                GMGlobalNumericVariables.gnv.EARN_MONEY_CIGARETTES_HAPPINESS_BOOST += GMGlobalNumericVariables.gnv.IMPROVEMENT_HAPPINESS_BOOST;
        }
        //Action for the construction or improvement of the pharmacy
        else if (improvementName == StaticStrings.PHARMACY_IMPROVEMENT_NAME)
        {
            if (imp.currentLevel == 0)
                GMImprovementsData.improvementsData.pharmacy = true;
            else if (imp.MaxLevelWillBeReached())
            {
                GMImprovementsData.improvementsData.autoDrug = true;
            }
            else
                GMGlobalNumericVariables.gnv.EARN_MONEY_DRUG_HAPPINESS_BOOST += GMGlobalNumericVariables.gnv.IMPROVEMENT_HAPPINESS_BOOST;
        }
        //Action for the construction or improvement of the condom dispenser
        else if (improvementName == StaticStrings.CONDOM_DISPENSER_IMPROVEMENT_NAME)
        {
            if (imp.currentLevel == 0)
                GMImprovementsData.improvementsData.condomDispenser = true;
            else if (imp.MaxLevelWillBeReached())
            {
                GMImprovementsData.improvementsData.autoCondom = true;
            }
            else
            {
                GMGlobalNumericVariables.gnv.EARN_MONEY_CONDOM_HAPPINESS_BOOST += GMGlobalNumericVariables.gnv.IMPROVEMENT_HAPPINESS_BOOST;
            }
        }
        //Action for the construction or improvement of the prestations rooms
        else if (improvementName == StaticStrings.PRESTATIONS_ROOMS_IMPROVEMENT_NAME)
        {
            switch (imp.currentLevel)
            {
                case 0:
                    GMImprovementsData.improvementsData.stage = true;
                    if (toClubBlocker != null && StaticBooleans.tutorialIsOn)
                    {
                        toClubBlocker.SetActive(false);
                        GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 4;
                        SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
                    }
                    break;
                case 1:
                    GMImprovementsData.improvementsData.photostudio = true;
                    break;
                case 2:
                    GMImprovementsData.improvementsData.foreplayBooth = true;
                    break;
                case 3:
                    GMImprovementsData.improvementsData.oralBooth = true;
                    break;
                case 4:
                    GMImprovementsData.improvementsData.sexBooth = true;
                    break;
                case 5:
                    GMImprovementsData.improvementsData.groupRoom = true;
                    break;
            }
        }
        //Action for the construction or improvement of the advertisements
        else if (improvementName == StaticStrings.ADVERTISEMENTS_IMPROVEMENT_NAME)
        {
            switch (imp.currentLevel)
            {
                case 0:
                    GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER -= (GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER * 0.1f);
                    break;
                case 1:
                    GMGlobalNumericVariables.gnv.REPUTATION_GAIN += GMGlobalNumericVariables.gnv.BASIC_REPUTATION_GAIN * 0.1f;
                    break;
                case 2:
                    GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER -= (GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER * 0.1f);
                    break;
                case 3:
                    GMGlobalNumericVariables.gnv.REPUTATION_GAIN += GMGlobalNumericVariables.gnv.BASIC_REPUTATION_GAIN * 0.1f;
                    break;
            }
        }
        //Action for the construction or improvement of the dressing room
        else if (improvementName == StaticStrings.DRESSING_ROOM_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.REST_ENERGY_GAIN += 5;
        }
        //Action for the construction or improvement of new booths
        else if (improvementName == StaticStrings.NEW_BOOTHS_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS += 1;
            GMGlobalNumericVariables.gnv.BOOTH_GAME_TIME += 30f;
        }
        //Action for the construction or improvement of maids
        else if (improvementName == StaticStrings.MAID_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_ENERGY_LOST -= GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_ENERGY_LOST_MODIFICATOR;
        }
        //Action for the construction or improvement of secretary
        else if (improvementName == StaticStrings.SECRETARY_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.REPUTATION_GAIN += GMGlobalNumericVariables.gnv.BASIC_REPUTATION_GAIN * 0.1f;
        }
        //Action for the construction or improvement of security
        else if (improvementName == StaticStrings.SECURITY_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER -= (GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER * 0.1f);
        }
        else if (improvementName == StaticStrings.SECURITY_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER -= (GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER * 0.1f);
        }
        else if (improvementName == StaticStrings.WARDROBE_IMPROVEMENT_NAME)
        {
            StaticBooleans.wardrobeAvailable = true;
            GMGlobalNumericVariables.gnv.COSTUME_LEVEL++;
        }
        else if (improvementName == StaticStrings.UPGRADE_CLUB_IMPROVEMENT_NAME)
        {
            if (StaticBooleans.tutorialIsOn)
            {
                GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 3;
            }
            GMGlobalNumericVariables.gnv.CLUB_LEVEL++;
            if (imp.currentLevel >= imp.maxLevel - 1)
                GMGlobalNumericVariables.gnv.CLUB_LEVEL = 100;
        }
        else if (improvementName == StaticStrings.LOUNGE_IMPROVEMENT_NAME)
        {
            GMImprovementsData.improvementsData.interactionRoom = true;
            if (imp.currentLevel > 0)
            {
                /*
                GMGlobalNumericVariables.gnv.INTERACTION_OPENNESS_GAIN += GMGlobalNumericVariables.gnv.INTERACTION_OPENNESS_GAIN_INCREASE;
                GMGlobalNumericVariables.gnv.INTERACTION_AFFECTION_GAIN += GMGlobalNumericVariables.gnv.INTERACTION_AFFECTION_GAIN_INCREASE;
                */
                GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY += GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY_INCREASE;
                InteractionSceneBehavior.availableTalks++;

            }
        }
        else if (improvementName == StaticStrings.PERSONAL_STUDY_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY += GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY_INCREASE;
            InteractionSceneBehavior.availableTalks++;
        }

        else if(improvementName == StaticStrings.MODULAR_BOOTHS_IMPROVEMENT_NAME)
        {
            GMImprovementsData.improvementsData.SetModularBooths(imp.currentLevel);
        }
        else if(improvementName == StaticStrings.PERSONAL_COACH_IMPROVEMENT_NAME)
        {
            GMGlobalNumericVariables.gnv.PERSONAL_IMPROVEMENT_STORE_LEVEL++;
        }

    }

    public static void BuildPoliciessModifiers(string policyName, Policy policy, GameObject toClubBlocker = null)
    {
        if (policyName == StaticStrings.ANAL_POLICY_NAME)
        {
            GMPoliciesData.policies.anal = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_SEX_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_SEX_INCREASE;
            GMGlobalNumericVariables.gnv.SEX_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.SEX_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.ANAL_CREAMPIE_POLICY_NAME)
        {
            GMPoliciesData.policies.analCreampie = true;
        }
        if (policyName == StaticStrings.BLOWJOB_POLICY_NAME)
        {
            GMPoliciesData.policies.blowjob = true;
        }
        if (policyName == StaticStrings.BODY_CUMSHOT_POLICY_NAME)
        {
            GMPoliciesData.policies.bodyFinish = true;
        }
        if (policyName == StaticStrings.CREAMPIE_POLICY_NAME)
        {
            GMPoliciesData.policies.creampie = true;
        }
        if (policyName == StaticStrings.DANCE_POLICY_NAME)
        {
            GMPoliciesData.policies.dance = true;
            if (StaticBooleans.tutorialIsOn)
            {
                toClubBlocker.SetActive(false);
                StaticBooleans.tutorialIsOnPolicies = false;
            }
        }
        if (policyName == StaticStrings.DANCE_CLOSER_POLICY_NAME)
        {
            GMPoliciesData.policies.danceCloser = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_DANCE_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_DANCE_INCREASE;
            GMGlobalNumericVariables.gnv.DANCE_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.DANCE_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.DANCE_TOPLESS_POLICY_NAME)
        {
            GMPoliciesData.policies.danceTopless = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_DANCE_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_DANCE_INCREASE;
            GMGlobalNumericVariables.gnv.DANCE_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.DANCE_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.DEEPTHROAT_POLICY_NAME)
        {
            GMPoliciesData.policies.deepthroat = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_ORAL_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_ORAL_INCREASE;
            GMGlobalNumericVariables.gnv.ORAL_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.ORAL_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.BACK_VAGINAL_POLICY_NAME)
        {
            GMPoliciesData.policies.backVaginal = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_SEX_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_SEX_INCREASE;
            GMGlobalNumericVariables.gnv.SEX_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.SEX_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.FACEFUCK_POLICY_NAME)
        {
            GMPoliciesData.policies.facefuck = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_ORAL_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_ORAL_INCREASE;
            GMGlobalNumericVariables.gnv.ORAL_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.ORAL_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.FACIAL_POLICY_NAME)
        {
            GMPoliciesData.policies.facial = true;
        }
        if (policyName == StaticStrings.FOOTJOB_POLICY_NAME)
        {
            GMPoliciesData.policies.footjob = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_FOREPLAY_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_FOREPLAY_INCREASE;
            GMGlobalNumericVariables.gnv.FOREPLAY_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.FOREPLAY_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.FOURSOME_POLICY_NAME)
        {
            GMPoliciesData.policies.foursome = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_GROUP_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_GROUP_INCREASE;
            GMGlobalNumericVariables.gnv.GROUP_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.GROUP_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.ORGY_POLICY_NAME)
        {
            GMPoliciesData.policies.orgy = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_GROUP_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_GROUP_INCREASE;
            GMGlobalNumericVariables.gnv.GROUP_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.GROUP_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.HANDJOB_POLICY_NAME)
        {
            GMPoliciesData.policies.handjob = true;
        }
        if (policyName == StaticStrings.VAGINAL_FACING_POLICY_NAME)
        {
            GMPoliciesData.policies.facingVaginal = true;
        }
        if (policyName == StaticStrings.POSE_NAKED_POLICY_NAME)
        {
            GMPoliciesData.policies.poseNaked = true;
        }
        if (policyName == StaticStrings.SOLO_FINGERING_POLICY_NAME)
        {
            GMPoliciesData.policies.soloHand = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_POSE_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_POSE_INCREASE;
            GMGlobalNumericVariables.gnv.POSE_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.POSE_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.SWALLOW_POLICY_NAME)
        {
            GMPoliciesData.policies.swallow = true;
        }
        if (policyName == StaticStrings.THREESOME_POLICY_NAME)
        {
            GMPoliciesData.policies.threesome = true;
        }
        if (policyName == StaticStrings.TITS_CUMSHOT_POLICY_NAME)
        {
            GMPoliciesData.policies.titsFinish = true;
        }
        if (policyName == StaticStrings.TITSJOB_POLICY_NAME)
        {
            GMPoliciesData.policies.titsjob = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_FOREPLAY_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_FOREPLAY_INCREASE;
        }
        if (policyName == StaticStrings.TOYS_MASTURBATING_POLICY_NAME)
        {
            GMPoliciesData.policies.mastToy = true;
            GMGlobalNumericVariables.gnv.EARN_MONEY_POSE_AMOUNT += GMGlobalNumericVariables.gnv.EARN_MONEY_POSE_INCREASE;
            GMGlobalNumericVariables.gnv.POSE_POLICIES_MULTIPLIER += GMGlobalNumericVariables.gnv.POSE_POLICIES_INCREASE;
        }
        if (policyName == StaticStrings.THREESOME_FINISH_POLICY_NAME)
        {
            GMPoliciesData.policies.threesomeFinish = true;
        }
        if (policyName == StaticStrings.FOURSOME_FINISH_POLICY_NAME)
        {
            GMPoliciesData.policies.foursomeFinish = true;
        }
    }


    //Random test of chance on a 1 to max probability
    public static bool Chance(int chance, int max)
    {
        return chance > UnityEngine.Random.Range(1, max + 1);
    }

    public static IEnumerator LoadSpriteFromURL(string MediaUrl, Sprite sprite)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            Texture tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            sprite = Sprite.Create(tex as Texture2D, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
    }

    public static IEnumerator LoadImageFromURL(string MediaUrl, string MediaUrl2, Image img, bool isBigPortrait, bool isMainPortrait = true)
    {
        MediaUrl = MediaUrl.Replace("#", "%23");
        MediaUrl2 = MediaUrl2.Replace("#", "%23");
        

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            request = UnityWebRequestTexture.GetTexture(MediaUrl2);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                if (isBigPortrait)
                {
                    if(isMainPortrait)
                    img.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + StaticStrings.GENERIC_GIRL_NAME + "/" + StaticStrings.IMAGES_FOLDER
                        + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
                    else
                    {
                        img.sprite = null;
                    }
                }
                else
                {
                    img.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + StaticStrings.GENERIC_GIRL_NAME + "/" + StaticStrings.IMAGES_FOLDER
                        + StaticStrings.CLOSEUP_PORTRAIT_FILE_NO_EXTENSION);
                }
                //Debug.Log(request.error);
            }
            else
            {
                Texture tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                img.sprite = Sprite.Create(tex as Texture2D, new Rect(0.0f, 0.0f, tex.width, tex.height), img.rectTransform.pivot);
            }
        }
        else
        {
            Texture tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            img.sprite = Sprite.Create(tex as Texture2D, new Rect(0.0f, 0.0f, tex.width, tex.height), img.rectTransform.pivot);
        }
    }

    public static IEnumerator LoadTextureFromURL(string MediaUrl, Texture2D tex)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            tex = (((DownloadHandlerTexture)request.downloadHandler).texture) as Texture2D;
        }
    }
    /// <summary>
    /// Tests if the number of girls working has reached the maximum
    /// </summary>
    /// <returns>True if the number of girls working has reached the maximum</returns>
    public static bool MaxGirlWorking()
    {
        int workingGirls = 0;
        foreach (GirlClass g in GMRecruitmentData.recruitedGirlsList)
        {
            if (g.morningOccupation == Occupation.WORK)
                workingGirls++;
        }
        return workingGirls >= GMGlobalNumericVariables.gnv.MAX_WORKING_GIRLS;

    }

    public static int DayCost()
    {
        int result = GMGlobalNumericVariables.gnv.DAILY_RENT;

        return result;
    }

    public static void PassADay(bool displayDayRecap, bool comeFromWork)
    {
        int dayCost = DayCost();

        if (StaticBooleans.tutorialIsOn)
        {
            GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 9;
        }

        foreach(GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if (girl.morningOccupation == Occupation.WORK)
            {
                //Put (1 - EARN_MONEY_GIRL_CUT_MULTIPLIER) of her earnings in the club's money
                GMClubData.EarnMoney(Mathf.RoundToInt((1 - GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_CUT_MULTIPLIER) * girl.moneyEarned));

                //Put the rest in her money
                girl.money += Mathf.RoundToInt(GMGlobalNumericVariables.gnv.EARN_MONEY_GIRL_CUT_MULTIPLIER * girl.moneyEarned);

                girl.moneyEarned = 0;
            }
            else
            {
                girl.AddToEnergy(GMGlobalNumericVariables.gnv.REST_ENERGY_GAIN);
            }
            dayCost += girl.DaySalary();
        }

        /*
        foreach (GirlClass girl in GMRecruitmentData.firedGirlsList)
        {
            girl.AddToEnergy(25);
            GameMasterGlobalData.girlsList.Find(x => x.name.Equals(girl.name)).AddToEnergy(25);
        }
        */

        //Add a day
        GMClubData.day++;
        //Empty the girl roster
        BoothGameData.girlsRoster = new List<GirlClass>();
        //This variable is true if the day recap should be displayed, so it is turned to true
        StaticBooleans.displayDayRecap = displayDayRecap;
        //This variable is true if the day recap is over so it is turned to false
        StaticBooleans.dayRecapOver = false;
        //Turn this variable to true to indicate that the club scene should test for an event
        StaticBooleans.comeFromWork = comeFromWork;
        //Pay the different costs
        GMClubData.SpendMoney(dayCost);
        //Reset the number of talks available
        InteractionSceneBehavior.availableTalks = GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY;
       
        //Reset the number of times the negotiator has been used
        BoothGameData.negotiatorUsage = 0;

        //Reset the music volume
        MusicManager.ReturnBaseMusicVolume();

        //Start testing for the second late night meeting with Nicole dialog, if it hasn't been already seen
        StaticDialogElements.dialogData.stopTestingSecondLateNightMeeting = false;

        //Reset the number of availables dates
        StaticAssistantData.data.datesAvailableToday = StaticAssistantData.data.datesAvailablePerDay;

        //Autosave
        Save("autosave" + autosaveSlotNumber + ".sav");
        autosaveSlotNumber = (autosaveSlotNumber % 2) + 1;

        //Then load the club scene
        SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
    }

    public static Mission FindMissionWithID(int id)
    {
        return RegisterMissions.missions.Find(x => x.id == id);
    }

    public static Dialog FindDialogWithID(int id)
    {
        return RegisterDialogs.dialogs.Find(x => x.id == id);
    }

    public static void ReinitializeVariables()
    {
        GMGlobalNumericVariables.gnv = new GlobalNumericVariables();
        StaticBooleans.InitializeBooleans();

        GameMasterGlobalData.clubImprovementList = new List<Improvement>();
        GameMasterGlobalData.costumeList = new List<Costume>();

        GameMasterGlobalData.policiesList = new List<Policy>();
        GMPoliciesData.policies = new PoliciesData();
        GMPoliciesData.ownedPolicies = new List<Policy>();


        foreach (string d in StaticStrings.IMPROVEMENTS_DIRECTORIES_NAMES)
        {
            Improvement imp = JsonUtility.FromJson<Improvement>(Resources.Load<TextAsset>(
             StaticStrings.IMPROVEMENTS_FOLDER + d + "/" + StaticStrings.IMPROVEMENT_DATA_FILE).text);
            if (imp.showInList)
            {
                GameMasterGlobalData.clubImprovementList.Add(imp);

            }
        }
        
        foreach (string d in StaticStrings.POLICIES_DIRECTORIES_NAMES)
        {
            Policy policy = JsonUtility.FromJson<Policy>(Resources.Load<TextAsset>(
             StaticStrings.POLICIES_FOLDER + d + "/" + StaticStrings.POLICY_DATA_FILE).text);
            GameMasterGlobalData.policiesList.Add(policy);
        }

        foreach (Policy pol in GameMasterGlobalData.policiesList)
        {
            pol.CreatePredecessors();
        }


        foreach (string d in StaticStrings.COSTUMES_DIRECTORIES_NAMES)
        {
            Costume c = JsonUtility.FromJson<Costume>(Resources.Load<TextAsset>(
             StaticStrings.COSTUMES_FOLDER + d + "/" + StaticStrings.COSTUME_DATA_FILE).text);
            if (c.showInList)
            {
                GameMasterGlobalData.costumeList.Add(c);

            }
        }

        RegisterDialogs.dialogs = new List<Dialog>();
        RegisterDialogs.CreateDialogs();
        RegisterDialogs2.CreateDialogs();
        DialogSystemBehavior.inDialog = false;

        RegisterMissions.missions = new List<Mission>();
        RegisterMissions.availableMissions = new List<Mission>();
        RegisterMissions.activeMissions = new List<Mission>();

        RegisterMissions.CreateMissions();

        GMCrimeServiceData.crimeServices = new List<CrimeService>();
        GMCrimeServiceData.unlockedCrimeServices = new List<CrimeService>();
        RegisterCrimeServices.CreateCrimeServices();

        StaticDialogElements.dialogData = new StaticDialogElements.DialogData();

        StaticAssistantData.data = new AssistantData();

        RegisterAssistantCostumes.assistantCostumes = new List<AssistantCostume>();
        RegisterAssistantCostumes.CreateAsssistantCostumes();

        crimeOfficeAvailable = 0;

        RegisterDates.dates = new List<DateData>();
        RegisterDates.CreateDates();

        RegisterAssistantStoreItems.assistantItemsList = new List<AssistantStoreItem>();
        RegisterAssistantStoreItems.CreateStoreItems();

        RegisterPersonalImprovements.personalImprovements = new List<PersonalImprovement>();
        RegisterPersonalImprovements.CreatePersonalImprovements();

        NewPlanningBehaviors.boothsSpecialties = new Work[] { Work.NONE, Work.NONE, Work.NONE, Work.NONE, Work.NONE, Work.NONE };

        //Resets the values of the fired girls
        if (GameMasterGlobalData.girlsList != null)
        {
            foreach (GirlClass girl in GameMasterGlobalData.girlsList)
            {
                /*
                foreach(GirlDialog gd in girl.girlDialogs)
                {
                    gd.seen = false;
                }
                */

                GirlClass g = null;
                if (!girl.external)
                {
                    g = GirlClass.CreateFromJSON((Resources.Load<TextAsset>(StaticStrings.GIRLS_FOLDER + girl.folderName + "/" +
                        StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE_NO_EXTENSION)).text);
                }
                else
                {
                    StreamReader reader = new StreamReader(StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + "/" +
                        StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE);
                    g = GirlClass.CreateFromJSON(reader.ReadToEnd());
                    reader.Close();
                }
                girl.CopyFiredGirl(g);
                //GameMasterGlobalData.girlsList.Find(x => x.name.Equals(girl.name)).CopyFiredGirl(g);
            }
        }
    }

    /// <summary>
    /// Returns true if there is at least one girl in the lottery pool that hasn't been recruited yet
    /// </summary>
    /// <returns></returns>
    public static bool GirlsStillInLotteryPool()
    {
        foreach(GirlClass girl in GameMasterGlobalData.girlsList)
        {
            if (girl.inLottery) {
                bool isRecruited = false;
                foreach (GirlClass hired in GMRecruitmentData.recruitedGirlsList)
                {
                    if (girl.name == hired.name)
                    {
                        isRecruited = true;
                    }
                }
                if (!isRecruited)
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns true if the only girls available are the lottery ones
    /// </summary>
    /// <returns></returns>
    public static bool OnlyLotteryGirlsInPool()
    {
        foreach (GirlClass girl in GameMasterGlobalData.girlsList)
        {
            if (!girl.inLottery)
            {
                bool isRecruited = false;
                foreach (GirlClass hired in GMRecruitmentData.recruitedGirlsList)
                {
                    if (girl.name == hired.name)
                    {
                        isRecruited = true;
                    }
                }
                if (!isRecruited)
                    return false;
            }
        }
        return true;
    }

    public static bool OnlyInfluenceOrLotteryGirlsInPool()
    {
        foreach (GirlClass girl in GameMasterGlobalData.girlsList)
        {
            if ((girl.influenceCost == 0 || IsCrimeOfficeAvailable()) && !girl.inLottery && !girl.doNotDisplay)
            {
                bool isRecruited = false;
                foreach (GirlClass hired in GMRecruitmentData.recruitedGirlsList)
                {
                    if (girl.name == hired.name)
                    {
                        isRecruited = true;
                        break;
                    }
                }
                if (!isRecruited)
                    return false;
            }
        }
        return true;
    }

    public static bool IsCrimeOfficeAvailable()
    {
        if(crimeOfficeAvailable == 0)
        {
            Dialog d = RegisterDialogs.dialogs.Find(x => x.id == GMGlobalNumericVariables.gnv.FOURTH_MISION_CLOSE_DIALOG_ID);
            if (d == null)
            {
                return false;
            }
            if (d.done) {
                crimeOfficeAvailable = 1;
                return true;
            }
            else
            {
                crimeOfficeAvailable = -1;
                return false;
            }
        }
        else
        {
                return crimeOfficeAvailable > 0;
        }
    }

    public static bool HasDoubleClicked(float lastTimeClicked)
    {
        if (Time.time - lastTimeClicked < GMGlobalNumericVariables.gnv.DOUBLE_CLICK_INTERVAL)
        {
            return true;
        }
        lastTimeClicked = Time.time;
        return false;
    }

    public static string WontDoDisplay(GirlClass girl)
    {
        string text = "";
        text += OnePerformanceWontDoDisplay(girl.doDance, StaticStrings.DANCE_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doDanceCloser,StaticStrings.DANCE_CLOSER_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doDanceTopless,StaticStrings.DANCE_TOPLESS_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doPoseNaked,StaticStrings.POSE_NAKED_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doSoloFingering,StaticStrings.SOLO_FINGERING_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doToysMasturbation,StaticStrings.TOYS_MASTURBATING_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doHandjob,StaticStrings.HANDJOB_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doFootjob,StaticStrings.FOOTJOB_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doTitsjob,StaticStrings.TITSJOB_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doBlowjob,StaticStrings.BLOWJOB_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doDeepthroat,StaticStrings.DEEPTHROAT_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doFacefuck,StaticStrings.FACEFUCK_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doFacingVaginal,StaticStrings.VAGINAL_FACING_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doBackVaginal,StaticStrings.BACK_VAGINAL_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doAnal,StaticStrings.ANAL_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doThreesome,StaticStrings.THREESOME_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doFoursome,StaticStrings.FOURSOME_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doOrgy,StaticStrings.ORGY_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doBodyCumshot,StaticStrings.BODY_CUMSHOT_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doTitsCumshot,StaticStrings.TITS_CUMSHOT_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doFacial,StaticStrings.FACIAL_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doSwallow,StaticStrings.SWALLOW_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doCreampie,StaticStrings.CREAMPIE_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doAnalCreampie,StaticStrings.ANAL_CREAMPIE_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doThreesomeFinish,StaticStrings.THREESOME_FINISH_POLICY_NAME);
        text += OnePerformanceWontDoDisplay(girl.doGroupFinish,StaticStrings.FOURSOME_FINISH_POLICY_NAME);
        if(text.EndsWith(", "))
        {
            text = text.Remove(text.Length - 2, 2);
        }
        return text;
    }

    private static string OnePerformanceWontDoDisplay(bool doX, string policyName)
    {
        if (!doX)
            return policyName + ", ";
        return "";
    }

    /*
    private static AudioSource FindMusicManager()
    {
        return MusicManager.musicPlayer;
    }
    */

    /// <summary>
    /// Used to reset a girl
    /// </summary>
    /// <param name="girl">The girl to reset</param>
    /// <returns>The resetted girl</returns>
    public static GirlClass ResetGirl(GirlClass girl)
    {
        GirlClass g;
        if (!girl.external)
        {
            g = GirlClass.CreateFromJSON((Resources.Load<TextAsset>(StaticStrings.GIRLS_FOLDER + girl.name + "/" +
                StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE_NO_EXTENSION)).text);
            g.folderName = girl.folderName;
            g.external = false;

            TextAsset[] textAssets = Resources.LoadAll<TextAsset>(StaticStrings.GIRLS_FOLDER + girl.name + "/" +
    StaticStrings.TEXTS_FOLDER + StaticStrings.DIALOGS_FOLDER);

            foreach (TextAsset ta in textAssets)
            {
                GirlLesson gl = new GirlLesson(g, ta.text);
                g.girlLessons.Add(gl);
                gl.fileName = ta.name;
            }
        }
        else
        {
            StreamReader reader = new StreamReader(StaticStrings.GIRLPACKS_DIRECTORY +
                girl.folderName + "/" + StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE);
            g = GirlClass.CreateFromJSON(reader.ReadToEnd());
            reader.Close();

            DirectoryInfo dialogDir = new DirectoryInfo(StaticStrings.GIRLPACKS_DIRECTORY +
                girl.folderName + "/" + StaticStrings.TEXTS_FOLDER + StaticStrings.DIALOGS_FOLDER);

            foreach (FileInfo file in dialogDir.GetFiles())
            {
                reader = new StreamReader(file.FullName);
                GirlLesson gl = new GirlLesson(g, reader.ReadToEnd());
                g.girlLessons.Add(gl);
                gl.fileName = file.Name.Substring(0, (file.Name.Length - 5));
                reader.Close();
            }

            g.external = true;
            g.folderName = girl.folderName;
            g.SetPortrait(girl.GetPortrait());
            g.closeupPortrait = girl.closeupPortrait;
        }

        return g;
    }

}



