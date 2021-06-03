//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Video;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class DayResultScript : MonoBehaviour
//{
//    /*
//    public VideoPlayer videoPlayer;

//    public Text girlNameText;
//    //public Text dayPhaseText;
//    public Text occupationText;
//    public Text workCategoryText;
//    public Text workSubCategoryText;

//    public int girlPopularityIncreaseBaseValue;
//    public int girlPopularityIncreaseRandomRange;

//    public int girlSkillIncreaseBaseValue;
//    public int girlSkillIncreaseRandomRange;

//    private int currentGirlIndex = 0;

//    //Phase of the day for the current girl
//    private DayPhase dayPhase = DayPhase.MORNING;


//    /*
//    private VideoClip SelectRandomVideoClipFromFolder(string folder)
//    {
//        //Load the video clip from the available clips in the girl's folder
//        VideoClip[] clips = Resources.LoadAll<VideoClip>(StaticStrings.GIRLS_FOLDER +
//            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].name + "/" + StaticStrings.VIDEOS_FOLDER + 
//            StaticStrings.PRESTATIONS_FOLDER + folder);

//        if(clips == null || clips.Length <= 0)
//        {
//            //If this girl doesn't have the type of clip we arte looking for then use the generic girl folder
//            clips = Resources.LoadAll<VideoClip>(StaticStrings.GIRLS_FOLDER +
//            StaticStrings.GENERIC_GIRL_NAME + "/" + StaticStrings.VIDEOS_FOLDER +
//            StaticStrings.PRESTATIONS_FOLDER + folder);
            
//        }
//        return clips[Random.Range(0, clips.Length)];
//    }
    
//    private VideoClip SelectWorkVideoClip(string folder,Work work)
//    {
//        switch (work)
//        {
//            case Work.DANCE:return SelectRandomVideoClipFromFolder(folder
//                + StaticStrings.DANCE_FOLDER 
//                + StaticStrings.DANCE_SUBFOLDER);

//            case Work.POSE: return SelectRandomVideoClipFromFolder(folder + StaticStrings.POSE_FOLDER);
//            case Work.FOREPLAY: return SelectRandomVideoClipFromFolder(folder + StaticStrings.FOREPLAY_FOLDER);
//            case Work.ORAL: return SelectRandomVideoClipFromFolder(folder + StaticStrings.ORAL_FOLDER);
//            case Work.SEX: return SelectRandomVideoClipFromFolder(folder + StaticStrings.SEX_FOLDER);
//            case Work.GROUP: return SelectRandomVideoClipFromFolder(folder + StaticStrings.GROUP_FOLDER);
//            default: return null;
//        }
//    }

//    private VideoClip SelectOccupationVideoClip(Occupation occupation, Work work = Work.NONE)
//    {
//            switch (occupation)
//            {
//                case Occupation.TRAIN: return SelectWorkVideoClip(StaticStrings.TRAIN_FOLDER, work);
//                case Occupation.TALK: return SelectRandomVideoClipFromFolder(StaticStrings.TALK_FOLDER);
//                case Occupation.WORK: return SelectWorkVideoClip(StaticStrings.WORK_FOLDER,work);
//                case Occupation.REST: return SelectRandomVideoClipFromFolder(StaticStrings.REST_FOLDER);
//                default: return null;
//            }
//    }
//    */
//    //Returns the occupation of the current girl for the current day phase
//    private Occupation CurrentOccupation()
//    {
//        switch (dayPhase)
//        {
//            case DayPhase.MORNING: return GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation;
//            case DayPhase.EVENING: return GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningOccupation;
//            case DayPhase.NIGHT: return GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightOccupation;
//            default: return Occupation.REST;
//        }
//    }
    
//    //Returns the work of the current girl for the current day phase
//    private Work CurrentWork()
//    {
//        switch (dayPhase)
//        {
//            case DayPhase.MORNING: return GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningWork;
//            case DayPhase.EVENING: return GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningWork;
//            case DayPhase.NIGHT: return GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightWork;
//            default: return Work.NONE;
//        }
//    }

//    private void LoadNewTexts()
//    {
//        string aux = "";
//        girlNameText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].name;

//        aux = (dayPhase.ToString().Substring(1, dayPhase.ToString().Length-1)).ToUpper();
//        //dayPhaseText.text = dayPhase.ToString().Substring(0,1) + aux;

//        string currentOccupation = CurrentOccupation().ToString();
//        aux = (currentOccupation.Substring(1, currentOccupation.Length - 1)).ToLower();
//        occupationText.text = currentOccupation.Substring(0, 1) + aux;

//        string currentWork = CurrentWork().ToString();
//        aux = (currentWork.Substring(1, currentWork.Length - 1)).ToLower();
//        workCategoryText.text = currentWork.Substring(0, 1) + aux;

//        workSubCategoryText.text = "TODO";
//    }

//    private void IncreaseGirlPopularity(GirlClass girl)
//    {
//        girl.popularity += girlPopularityIncreaseBaseValue + 
//            Random.Range(-girlPopularityIncreaseRandomRange, girlPopularityIncreaseRandomRange+1);
//    }

//    private int IncreaseGirlSkill()
//    {
//        return girlSkillIncreaseBaseValue + Random.Range(-girlSkillIncreaseRandomRange, girlSkillIncreaseRandomRange + 1);
//    }

//    private void IncreaseGirlSkills(GirlClass girl, Work work)
//    {
//        switch (work)
//        {
//            case Work.DANCE:girl.AddToDancing(IncreaseGirlSkill());break;
//            case Work.POSE: girl.posing += IncreaseGirlSkill(); break;
//            case Work.FOREPLAY: girl.foreplay += IncreaseGirlSkill(); break;
//            case Work.ORAL: girl.oral += IncreaseGirlSkill(); break;
//            case Work.SEX: girl.sex += IncreaseGirlSkill(); break;
//            case Work.GROUP: girl.group += IncreaseGirlSkill(); break;
//        }
//    }

//    //Increases the popularity and skills of the girls according to what they did this day
//    private void GirlProgress()
//    {
//        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
//        {
//            if (girl.morningOccupation == Occupation.TRAIN)
//            {
//                IncreaseGirlSkills(girl, girl.morningWork);
//            }
//            else if (girl.morningOccupation == Occupation.WORK)
//            {
//                //IncreaseGirlPopularity(girl);
//                //IncreaseGirlSkills(girl, girl.morningWork);
//            }
//            if (girl.eveningOccupation == Occupation.WORK)
//            {
//                IncreaseGirlPopularity(girl);
//                IncreaseGirlSkills(girl, girl.eveningWork);
//            }
//            else if (girl.eveningOccupation == Occupation.TRAIN)
//            {
//                IncreaseGirlSkills(girl, girl.eveningWork);
//            }

//            if (girl.nightOccupation == Occupation.WORK)
//            {
//                IncreaseGirlPopularity(girl);
//                IncreaseGirlSkills(girl, girl.nightWork);
//            }
//            else if (girl.nightOccupation == Occupation.TRAIN)
//            {
//                IncreaseGirlSkills(girl, girl.nightWork);
//            }
//        }
//    }

//    private int DayCost()
//    {
//        int dayCost = 0;
//        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
//        {
//            dayCost += girl.DaySalary();
//        }
//        return dayCost;
//    }

//    private int DayEarnings()
//    {
//        int dayEarnings = 0;
//        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
//        {
//            dayEarnings += girl.DayEarnings();
//        }
//        return dayEarnings;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        videoPlayer.clip = StaticFunctions.SelectOccupationVideoClip(
//            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation,
//            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].name,
//            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningWork);
//        LoadNewTexts();
//        videoPlayer.Play();
//        currentGirlIndex++;
//        //dayPhase = DayPhase.EVENING;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//    }

//    public void OnNextReportButtonPress()
//    {
//        if (currentGirlIndex < GMRecruitmentData.recruitedGirlsList.Count)
//        {

//            VideoClip clip = null;
//            /*
//            switch (dayPhase)
//            {
//                case DayPhase.MORNING:
//                    clip = SelectOccupationVideoClip(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation,
//                        GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningWork);
//                    LoadNewTexts();
//                    dayPhase = DayPhase.EVENING;
//                    break;
//                case DayPhase.EVENING:
//                    clip = SelectOccupationVideoClip(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningOccupation,
//                        GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningWork);
//                    LoadNewTexts();
//                    dayPhase = DayPhase.NIGHT;
//                    break;
//                case DayPhase.NIGHT:
//                    clip = SelectOccupationVideoClip(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightOccupation,
//                        GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightWork);
//                    LoadNewTexts();
//                    dayPhase = DayPhase.MORNING;
//                    currentGirlIndex++;
//                    break;
//            }
//            */
//            clip = StaticFunctions.SelectOccupationVideoClip(
//                GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation,
//                GMRecruitmentData.recruitedGirlsList[currentGirlIndex].name,
//            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningWork);
//            LoadNewTexts();
//            videoPlayer.clip = null;
//            videoPlayer.clip = clip;
//            currentGirlIndex++;
//        }
//    }

//    public void OnEndReportsButtonPress()
//    {
//        GirlProgress();

//        GMClubData.money -= DayCost();
//        //GMClubData.money += DayEarnings();
//        GMClubData.day++;
//        SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
//    }
//*/
//}
