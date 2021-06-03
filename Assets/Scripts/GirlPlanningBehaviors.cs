//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class GirlPlanningBehaviors : MonoBehaviour
//{

//    public Text girlName;

//    public Text occupationText;
//    public Text workText;

//    public Button danceButton;
//    public Button poseButton;
//    public Button foreplayButton;
//    public Button oralButton;
//    public Button sexButton;
//    public Button groupButton;

//    public Image girlPortrait;

//    private int currentGirlIndex = 0;

//    private List<Button> workButtons = new List<Button>();

//    public GameObject startDayBlocker;

//    public GameObject noGirlWorkingPopup;

//    public Text energyText;
//    public Text opennessText;
//    public Text popularityText;

//    public Text dancingSkillText;
//    public Text posingSkillText;
//    public Text foreplaySkillText;
//    public Text oralSkillText;
//    public Text sexSkillText;
//    public Text groupSkillText;

//    private string ChooseDisplayedOccupation(Occupation occupation)
//    {
//        switch (occupation)
//        {
//            case Occupation.TRAIN: return StaticStrings.TRAIN;
//            case Occupation.TALK: return StaticStrings.TALK;
//            case Occupation.WORK: return StaticStrings.WORK;
//            case Occupation.REST: return StaticStrings.REST;
//        }
//        return "";
//    }

//    private string ChooseDisplayedWork(Work work)
//    {
//        if (
//            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation == Occupation.REST ||
//            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation == Occupation.TALK ||
//            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation == Occupation.WORK
//            )
//            return "";
//        switch (work)
//        {
//            case Work.NONE: return "";
//            case Work.DANCE: return StaticStrings.DANCE;
//            case Work.POSE: return StaticStrings.POSE;
//            case Work.FOREPLAY: return StaticStrings.FOREPLAY;
//            case Work.ORAL: return StaticStrings.ORAL;
//            case Work.SEX: return StaticStrings.SEX;
//            case Work.GROUP: return StaticStrings.GROUP;
//        }
//        return "";
//    }

//    private void LoadNewStats()
//    {
//        energyText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].energy.ToString();
//        opennessText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].openness.ToString();
//        popularityText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].popularity.ToString();

//        dancingSkillText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].dancing.ToString();
//        posingSkillText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].posing.ToString();
//        foreplaySkillText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].foreplay.ToString();
//        oralSkillText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].oral.ToString();
//        sexSkillText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].sex.ToString();
//        groupSkillText.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].group.ToString();
//    }

//    private void LoadNewWorkText()
//    {
//        workText.text = ChooseDisplayedWork(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningWork);
//    }

//    private void LoadNewOccupationTexts()
//    {
//        occupationText.text = ChooseDisplayedOccupation(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation);
//        LoadNewWorkText();
//    }

//    private void LoadNewGirl()
//    {
//        girlName.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].name;
//        Sprite spr = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + GMRecruitmentData.recruitedGirlsList[currentGirlIndex].name + "/" +
//            StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
//        girlPortrait.sprite = spr;
//        LoadNewOccupationTexts();
//        LoadNewStats();
//    }

//    private void GenericOccupationButtonPress(Occupation occupation)
//    {
//        GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation = occupation;
//        LoadNewOccupationTexts();
//    }

//    private void GenericWorkButtonPress(Work work)
//    {
//        GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningWork = work;
//        LoadNewWorkText();
//    }

//    private void ActivateOrDeactivateButton(Button button, bool condition)
//    {
//        if (condition)
//            button.gameObject.SetActive(true);
//        else
//            button.gameObject.SetActive(false);
//    }

//    private void ActivateOrDeactivateButtons(List<Button> buttons)
//    {

//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        workButtons.Add(danceButton);
//        workButtons.Add(poseButton);
//        workButtons.Add(foreplayButton);
//        workButtons.Add(oralButton);
//        workButtons.Add(sexButton);
//        workButtons.Add(groupButton);

//        LoadNewGirl();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(StaticBooleans.tutorialIsOn)
//        {
//            if(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation == Occupation.WORK)
//            {
//                startDayBlocker.SetActive(false);
//            }
//            else
//            {
//                startDayBlocker.SetActive(true);
//            }
//        }

//        if(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation != Occupation.TRAIN)
//        {
//            danceButton.gameObject.SetActive(false);
//            poseButton.gameObject.SetActive(false);
//            foreplayButton.gameObject.SetActive(false);
//            oralButton.gameObject.SetActive(false);
//            sexButton.gameObject.SetActive(false);
//            groupButton.gameObject.SetActive(false);
//        }
//        else
//        {
//            danceButton.gameObject.SetActive(true);
//            poseButton.gameObject.SetActive(true);
//            foreplayButton.gameObject.SetActive(true);
//            oralButton.gameObject.SetActive(true);
//            sexButton.gameObject.SetActive(true);
//            groupButton.gameObject.SetActive(true);
//        }
//    }

//    public void OnNextGirlButtonPress()
//    {
//        currentGirlIndex++;

//        if (currentGirlIndex >= GMRecruitmentData.recruitedGirlsList.Count)
//            currentGirlIndex = 0;

//        LoadNewGirl();
//    }

//    public void OnPreviousGirlButtonPress()
//    {
//        currentGirlIndex--;

//        if (currentGirlIndex < 0)
//            currentGirlIndex = GMRecruitmentData.recruitedGirlsList.Count-1;

//        LoadNewGirl();
//    }

//    public void OnTrainButtonPress()
//    {
//        GenericOccupationButtonPress(Occupation.TRAIN);
//    }

//    public void OnTalkButtonPress()
//    {
//        GenericOccupationButtonPress(Occupation.TALK);
//    }

//    public void OnWorkButtonPress()
//    {
//        if (StaticBooleans.tutorialIsOn)
//        {
//            startDayBlocker.SetActive(false);
//        }
//        GenericOccupationButtonPress(Occupation.WORK);
//    }

//    public void OnRestButtonPress()
//    {
//        GenericOccupationButtonPress(Occupation.REST);
//    }

//    public void OnDanceButtonPress()
//    {
//        GenericWorkButtonPress(Work.DANCE);
//    }

//    public void OnPoseButtonPress()
//    {
//        GenericWorkButtonPress(Work.POSE);
//    }

//    public void OnForeplayButtonPress()
//    {
//        GenericWorkButtonPress(Work.FOREPLAY);
//    }

//    public void OnOralButtonPress()
//    {
//        GenericWorkButtonPress(Work.ORAL);
//    }

//    public void OnSexButtonPress()
//    {
//        GenericWorkButtonPress(Work.SEX);
//    }

//    public void OnGroupButtonPress()
//    {
//        GenericWorkButtonPress(Work.GROUP);
//    }

//    public void OnStartDayButtonPress()
//    {
//        if (StaticBooleans.tutorialIsOn)
//        {
//            GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 8;
//        }
//        bool oneGirlWorking = false;
//        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
//        {
//            if(girl.morningOccupation == Occupation.WORK)
//            {
//                oneGirlWorking = true;
//                break;
//            }
//        }
//        if (oneGirlWorking)
//        {
//            foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
//            {
//                if (girl.morningOccupation == Occupation.WORK)
//                    BoothGameData.girlsRoster.Add(girl);
//                if (girl.morningOccupation == Occupation.REST)
//                {
//                    girl.energy += GMGlobalNumericVariables.gnv.REST_ENERGY_GAIN;
//                    if (girl.energy > GMGlobalNumericVariables.gnv.MAX_ENERGY)
//                        girl.energy = GMGlobalNumericVariables.gnv.MAX_ENERGY;
//                }

//            }
//            SceneManager.LoadScene(StaticStrings.BOOTHS_MANAGEMENT_SCENE);
//        }

//        else
//        {
//            noGirlWorkingPopup.SetActive(true);
//        }
//    }

//    public void OnConfirmWorkingDayButtonPress()
//    {
//        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
//        {
//            girl.energy += GMGlobalNumericVariables.gnv.REST_ENERGY_GAIN;
//            if (girl.energy > GMGlobalNumericVariables.gnv.MAX_ENERGY)
//                girl.energy = GMGlobalNumericVariables.gnv.MAX_ENERGY;
//        }
//        noGirlWorkingPopup.SetActive(false);
//        //Add a day
//        GMClubData.day++;

//        SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
//    }

//    public void OnCancelWorkingDayButtonPress()
//    {
//        noGirlWorkingPopup.SetActive(false);
//    }

//}
