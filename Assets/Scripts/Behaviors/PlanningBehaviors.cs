using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanningBehaviors : MonoBehaviour
{

    public Text girlName;

    public Text morningOccupationText;
    public Text eveningOccupationText;
    public Text nightOccupationText;
    public Text morningWorkText;
    public Text eveningWorkText;
    public Text nightWorkText;

    public Button morningDanceButton;
    public Button morningPoseButton;
    public Button morningForeplayButton;
    public Button morningOralButton;
    public Button morningSexButton;
    public Button morningGroupButton;

    public Button eveningDanceButton;
    public Button eveningPoseButton;
    public Button eveningForeplayButton;
    public Button eveningOralButton;
    public Button eveningSexButton;
    public Button eveningGroupButton;

    public Button nightDanceButton;
    public Button nightPoseButton;
    public Button nightForeplayButton;
    public Button nightOralButton;
    public Button nightSexButton;
    public Button nightGroupButton;

    public Image girlPortrait;

    private int currentGirlIndex = 0;

    private List<Button> morningWorkButtons = new List<Button>();
    private List<Button> eveningWorkButtons = new List<Button>();
    private List<Button> nightWorkButtons = new List<Button>();

    private string ChooseDisplayedOccupation(Occupation occupation)
    {
        switch (occupation)
        {
            case Occupation.TRAIN: return StaticStrings.TRAIN;
            case Occupation.TALK: return StaticStrings.TALK;
            case Occupation.WORK: return StaticStrings.WORK;
            case Occupation.REST: return StaticStrings.REST;
        }
        return "";
    }

    private string ChooseDisplayedWork(Work work, DayPhase phase)
    {
        if (
            phase == DayPhase.MORNING && GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation == Occupation.REST ||
            phase == DayPhase.MORNING && GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation == Occupation.TALK ||
            phase == DayPhase.EVENING && GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningOccupation == Occupation.REST ||
            phase == DayPhase.EVENING && GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningOccupation == Occupation.TALK ||
            phase == DayPhase.NIGHT && GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightOccupation == Occupation.REST ||
            phase == DayPhase.NIGHT && GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightOccupation == Occupation.TALK
            )
            return "";
        switch (work)
        {
            case Work.NONE: return "";
            case Work.DANCE: return StaticStrings.DANCE;
            case Work.POSE: return StaticStrings.POSE;
            case Work.FOREPLAY: return StaticStrings.FOREPLAY;
            case Work.ORAL: return StaticStrings.ORAL;
            case Work.SEX: return StaticStrings.SEX;
            case Work.GROUP: return StaticStrings.GROUP;
        }
        return "";
    }

    private void LoadNewWorkText()
    {
        morningWorkText.text = ChooseDisplayedWork(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningWork, DayPhase.MORNING);
        eveningWorkText.text = ChooseDisplayedWork(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningWork, DayPhase.EVENING);
        nightWorkText.text = ChooseDisplayedWork(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightWork, DayPhase.NIGHT);
    }

    private void LoadNewOccupationTexts()
    {
        morningOccupationText.text = ChooseDisplayedOccupation(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation);
        eveningOccupationText.text = ChooseDisplayedOccupation(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningOccupation);
        nightOccupationText.text = ChooseDisplayedOccupation(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightOccupation);
        LoadNewWorkText();
    }

    private void LoadNewGirl()
    {
        girlName.text = GMRecruitmentData.recruitedGirlsList[currentGirlIndex].name;
        Sprite spr = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + GMRecruitmentData.recruitedGirlsList[currentGirlIndex].folderName + "/" +
            StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
        girlPortrait.sprite = spr;
        LoadNewOccupationTexts();
    }

    //We use a string because it is impossible to give an enum parameter through a button
    private void GenericOccupationButtonPress(string dayPhase, Occupation occupation)
    {
        if (dayPhase == "Morning")
        {
            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation = occupation;
        }
        else if (dayPhase == "Evening")
        {
            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningOccupation = occupation;
        }
        else if (dayPhase == "Night")
        {
            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightOccupation = occupation;
        }
        LoadNewOccupationTexts();
    }

    //We use a string because it is impossible to give an enum parameter through a button
    private void GenericWorkButtonPress(string dayPhase, Work work)
    {
        if (dayPhase == "Morning")
        {
            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningWork = work;
        }

        else if (dayPhase == "Evening")
        {
            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningWork = work;
        }
        else if (dayPhase == "Night")
        {
            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightWork = work;
        }
        LoadNewWorkText();
    }

    private void ActivateOrDeactivateButton(Button button, bool condition)
    {
        if (condition)
            button.gameObject.SetActive(true);
        else
            button.gameObject.SetActive(false);
    }

    private void ActivateOrDeactivateButtons(List<Button> buttons)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        morningWorkButtons.Add(morningDanceButton);
        morningWorkButtons.Add(morningPoseButton);
        morningWorkButtons.Add(morningForeplayButton);
        morningWorkButtons.Add(morningOralButton);
        morningWorkButtons.Add(morningSexButton);
        morningWorkButtons.Add(morningGroupButton);

        eveningWorkButtons.Add(eveningDanceButton);
        eveningWorkButtons.Add(eveningPoseButton);
        eveningWorkButtons.Add(eveningForeplayButton);
        eveningWorkButtons.Add(eveningOralButton);
        eveningWorkButtons.Add(eveningSexButton);
        eveningWorkButtons.Add(eveningGroupButton);

        nightWorkButtons.Add(nightDanceButton);
        nightWorkButtons.Add(nightPoseButton);
        nightWorkButtons.Add(nightForeplayButton);
        nightWorkButtons.Add(nightOralButton);
        nightWorkButtons.Add(nightSexButton);
        nightWorkButtons.Add(nightGroupButton);

        LoadNewGirl();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("c")){
            SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
        }

        if(GMRecruitmentData.recruitedGirlsList[currentGirlIndex].morningOccupation != Occupation.TRAIN)
        {
            morningDanceButton.gameObject.SetActive(false);
            morningPoseButton.gameObject.SetActive(false);
            morningForeplayButton.gameObject.SetActive(false);
            morningOralButton.gameObject.SetActive(false);
            morningSexButton.gameObject.SetActive(false);
            morningGroupButton.gameObject.SetActive(false);
        }
        else
        {
            ActivateOrDeactivateButton(morningDanceButton,GMPoliciesData.policies.danceCloser);
            ActivateOrDeactivateButton(morningPoseButton, GMPoliciesData.policies.danceTopless);
            ActivateOrDeactivateButton(morningForeplayButton, GMPoliciesData.policies.soloHand);
            ActivateOrDeactivateButton(morningOralButton, GMPoliciesData.policies.mastToy);
            ActivateOrDeactivateButton(morningSexButton, GMPoliciesData.policies.footjob);
            ActivateOrDeactivateButton(morningGroupButton, GMPoliciesData.policies.titsjob);
        }
        if (GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningOccupation != Occupation.TRAIN &&
            GMRecruitmentData.recruitedGirlsList[currentGirlIndex].eveningOccupation != Occupation.WORK)
        {
            eveningDanceButton.gameObject.SetActive(false);
            eveningPoseButton.gameObject.SetActive(false);
            eveningForeplayButton.gameObject.SetActive(false);
            eveningOralButton.gameObject.SetActive(false);
            eveningSexButton.gameObject.SetActive(false);
            eveningGroupButton.gameObject.SetActive(false);
        }
        else
        {
            ActivateOrDeactivateButton(eveningDanceButton, GMPoliciesData.policies.danceCloser);
            ActivateOrDeactivateButton(eveningPoseButton, GMPoliciesData.policies.danceTopless);
            ActivateOrDeactivateButton(eveningForeplayButton, GMPoliciesData.policies.soloHand);
            ActivateOrDeactivateButton(eveningOralButton, GMPoliciesData.policies.mastToy);
            ActivateOrDeactivateButton(eveningSexButton, GMPoliciesData.policies.footjob);
            ActivateOrDeactivateButton(eveningGroupButton, GMPoliciesData.policies.titsjob);
        }

        if (GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightOccupation != Occupation.TRAIN &&
    GMRecruitmentData.recruitedGirlsList[currentGirlIndex].nightOccupation != Occupation.WORK)
        {
            nightDanceButton.gameObject.SetActive(false);
            nightPoseButton.gameObject.SetActive(false);
            nightForeplayButton.gameObject.SetActive(false);
            nightOralButton.gameObject.SetActive(false);
            nightSexButton.gameObject.SetActive(false);
            nightGroupButton.gameObject.SetActive(false);
        }
        else
        {
            ActivateOrDeactivateButton(nightDanceButton, GMPoliciesData.policies.danceCloser);
            ActivateOrDeactivateButton(nightPoseButton, GMPoliciesData.policies.danceTopless);
            ActivateOrDeactivateButton(nightForeplayButton, GMPoliciesData.policies.soloHand);
            ActivateOrDeactivateButton(nightOralButton, GMPoliciesData.policies.mastToy);
            ActivateOrDeactivateButton(nightSexButton, GMPoliciesData.policies.footjob);
            ActivateOrDeactivateButton(nightGroupButton, GMPoliciesData.policies.titsjob);
        }
    }

    public void OnNextGirlButtonPress()
    {
        currentGirlIndex++;

        if (currentGirlIndex >= GMRecruitmentData.recruitedGirlsList.Count)
            currentGirlIndex = 0;

        LoadNewGirl();
    }

    public void OnPreviousGirlButtonPress()
    {
        currentGirlIndex--;

        if (currentGirlIndex < 0)
            currentGirlIndex = GMRecruitmentData.recruitedGirlsList.Count-1;

        LoadNewGirl();
    }

    public void OnTrainButtonPress(string dayPhase)
    {
        GenericOccupationButtonPress(dayPhase, Occupation.TRAIN);
    }

    public void OnTalkButtonPress(string dayPhase)
    {
        GenericOccupationButtonPress(dayPhase,Occupation.TALK);
    }

    public void OnWorkButtonPress(string dayPhase)
    {
        GenericOccupationButtonPress(dayPhase, Occupation.WORK);
    }

    public void OnRestButtonPress(string dayPhase)
    {
        GenericOccupationButtonPress(dayPhase, Occupation.REST);
    }

    public void OnDanceButtonPress(string dayPhase)
    {
        GenericWorkButtonPress(dayPhase,Work.DANCE);
    }

    public void OnPoseButtonPress(string dayPhase)
    {
        GenericWorkButtonPress(dayPhase, Work.POSE);
    }

    public void OnForeplayButtonPress(string dayPhase)
    {
        GenericWorkButtonPress(dayPhase, Work.FOREPLAY);
    }

    public void OnOralButtonPress(string dayPhase)
    {
        GenericWorkButtonPress(dayPhase, Work.ORAL);
    }

    public void OnSexButtonPress(string dayPhase)
    {
        GenericWorkButtonPress(dayPhase, Work.SEX);
    }

    public void OnGroupButtonPress(string dayPhase)
    {
        GenericWorkButtonPress(dayPhase, Work.GROUP);
    }

    public void OnStartDayButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.DAY_RESULT_SCENE);
    }

}
