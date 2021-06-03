using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehaviors : MonoBehaviour
{

    public GameObject tutorialPopup;

    public Button startTutorialButton;
    public Button stopTutorialButton;

    public Text tutorialTextZone;

    public Button nextTutorialButton;
    public Button previousTutorialButton;

    public Button recruitGirlsButton;
    public Button improvementsButton;
    public Button policiesButton;
    public Button planningButton;

    private Button newRecruitGirlsButton;
    private Button newImprovementsButton;
    private Button newPoliciesButton;
    private Button newPlanningButton;

    public Canvas canvas;

    public GameObject backgroundBlocker;

    public GameObject backToClubBlocker;
    public GameObject startDayBlocker;
    public GameObject restBlocker;

    //The index of the text that is currently displayed on the tutorial text space
    private int textStepNumber = 0;

    private void Awake()
    {
        if (StaticBooleans.tutorialIsOn)
        {
            switch (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER)
            {
                case 0: tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    startTutorialButton.gameObject.SetActive(false);
                    stopTutorialButton.gameObject.SetActive(false);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_ZERO[0];
                    nextTutorialButton.gameObject.SetActive(true);
                    break;
                case 1:
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_RECRUITMENT_STRINGS_PHASE_ZERO[0];
                    textStepNumber = 0;
                    backToClubBlocker.SetActive(true);
                    break;
                case 2:
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_ONE[0];
                    textStepNumber = 0;
                    startTutorialButton.gameObject.SetActive(false);
                    stopTutorialButton.gameObject.SetActive(false);
                    newImprovementsButton = Instantiate(improvementsButton, improvementsButton.transform.position, improvementsButton.transform.rotation, canvas.transform);
                    newImprovementsButton.onClick.AddListener(delegate () { GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = -3; });
                    break;
                case -3:
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_IMPROVEMENTS_STRINGS_PHASE_ZERO[0];
                    textStepNumber = 0;
                    backToClubBlocker.SetActive(true);
                    break;
                case 3:
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_IMPROVEMENTS_STRINGS_PHASE_ONE[0];
                    textStepNumber = 0;
                    backToClubBlocker.SetActive(true);
                    break;
                case 4:
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_TWO[0];
                    textStepNumber = 0;
                    startTutorialButton.gameObject.SetActive(false);
                    stopTutorialButton.gameObject.SetActive(false);
                    newPoliciesButton = Instantiate(policiesButton, policiesButton.transform.position, policiesButton.transform.rotation, canvas.transform);
                    newPoliciesButton.onClick.AddListener(delegate () { GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 5; });
                    break;
                case 5:
                    if (StaticBooleans.tutorialIsOnPolicies)
                    {
                        tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                        tutorialTextZone.text = StaticStrings.TUTORIAL_POLICIES_STRINGS_PHASE_ZERO[0];
                        textStepNumber = 0;
                        backToClubBlocker.SetActive(true);
                    }
                    break;
                case 6:
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_THREE[0];
                    textStepNumber = 0;
                    startTutorialButton.gameObject.SetActive(false);
                    stopTutorialButton.gameObject.SetActive(false);
                    newPlanningButton = Instantiate(planningButton, planningButton.transform.position, planningButton.transform.rotation, canvas.transform);
                    newPlanningButton.onClick.AddListener(delegate () { GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 7; });
                    break;
                case 7:
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    startDayBlocker.SetActive(true);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_PLANNING_STRINGS_PHASE_ZERO[0];
                    textStepNumber = 0;
                    backToClubBlocker.SetActive(true);
                    break;
                case 8:
                    //Since step 8 is the booth game tutorial, start by pausing it to display the tutorial
                    BoothGameData.boothGameIsPaused = true;
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_BOOTH_GAME_STRINGS_PHASE_ZERO[0];
                    textStepNumber = 0;
                    backToClubBlocker.SetActive(true);
                    break;
                case 9:
                    startTutorialButton.gameObject.SetActive(false);
                    stopTutorialButton.gameObject.SetActive(false);
                    textStepNumber = 0;
                    tutorialPopup.SetActive(StaticBooleans.tutorialIsOn);
                    tutorialTextZone.text = StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_FOUR[0];
                    textStepNumber = 0;
                    nextTutorialButton.gameObject.SetActive(true);
                    break;

            }
        }
    }

    public void OnStopTutorialButtonPress()
    {
        tutorialPopup.SetActive(false);
        StaticBooleans.tutorialIsOn = false;
    }

    public void OnStartTutorialButtonPress()
    {


    }

    public void OnNextTutorialButtonPress()
    {
        string[] stringsToDisplay = StringsToDisplay();

        //Activates of deactivates the next and previous buttons, depending on where we are on the chain of strings
        if (textStepNumber == 0)
            previousTutorialButton.gameObject.SetActive(true);
        else if (textStepNumber == stringsToDisplay.Length - 2)
            nextTutorialButton.gameObject.SetActive(false);



        //If the scene is the first of the tutorial and will reach the last text step
        if (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 0 && textStepNumber == stringsToDisplay.Length - 2)
        {
            //Instantiate a copy of the recruit girls button on the coordinates of said button
            //This is used to bypass the image blocking the background
            newRecruitGirlsButton = Instantiate(recruitGirlsButton, recruitGirlsButton.transform.position, recruitGirlsButton.transform.rotation, canvas.transform);
            newRecruitGirlsButton.onClick.AddListener(delegate () { GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 1; });
        }
        else if (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 1
            || GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 3
            || GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 5
            || GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == -3

            )
        {
            //If the tutorial scene is on the first recruitment phase
            if (textStepNumber == stringsToDisplay.Length - 2)
                nextTutorialButton.gameObject.SetActive(true);
            else if (textStepNumber == stringsToDisplay.Length - 1)
            {
                backgroundBlocker.SetActive(false);
                tutorialPopup.SetActive(false);
                textStepNumber--;//Used to avoid a index out of range exception
            }
        }
        else if (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 7)
        {
            //If the tutorial scene is on the first planning phase
            if (textStepNumber == stringsToDisplay.Length - 2)
                nextTutorialButton.gameObject.SetActive(true);
            else if (textStepNumber == stringsToDisplay.Length - 1)
            {
                tutorialPopup.SetActive(false);
                textStepNumber--;//Used to avoid a index out of range exception
            }
        }
        else if (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 8)
        {
            if (textStepNumber == 3)
            {
                nextTutorialButton.gameObject.SetActive(true);
                tutorialPopup.SetActive(false);
                BoothGameData.boothGameIsPaused = false;
            }

            if (textStepNumber == 4)
            {
                nextTutorialButton.gameObject.SetActive(true);
                tutorialPopup.SetActive(false);
                BoothGameData.boothGameIsPaused = false;
            }

            if (textStepNumber == stringsToDisplay.Length - 2)
                nextTutorialButton.gameObject.SetActive(true);
            else if (textStepNumber == stringsToDisplay.Length - 1)
            {
                tutorialPopup.SetActive(false);
                textStepNumber--;//Used to avoid a index out of range exception
                BoothGameData.boothGameIsPaused = false;
                backToClubBlocker.SetActive(false);
            }
        }
        else if (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 9)
        {
            //If the tutorial scene is on the first recruitment phase
            if (textStepNumber == stringsToDisplay.Length - 2)
                nextTutorialButton.gameObject.SetActive(true);
            else if (textStepNumber == stringsToDisplay.Length - 1)
            {
                tutorialPopup.SetActive(false);
                textStepNumber--;//Used to avoid a index out of range exception
                StaticBooleans.tutorialIsOn = false;
            }
        }


        textStepNumber++;
        tutorialTextZone.text = stringsToDisplay[textStepNumber];
    }

    public void OnPreviousTutorialButtonPress()
    {
        string[] stringsToDisplay = StringsToDisplay();

        //Activates of deactivates the next and previous buttons, depending on where we are on the chain of strings
        if (textStepNumber == 1)
            previousTutorialButton.gameObject.SetActive(false);
        else if (textStepNumber == stringsToDisplay.Length - 1)
            nextTutorialButton.gameObject.SetActive(true);

        textStepNumber--;
        tutorialTextZone.text = stringsToDisplay[textStepNumber];
    }

    private string[] StringsToDisplay()
    {
        switch (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER)
        {
            case 0: return StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_ZERO;
            case 1: return StaticStrings.TUTORIAL_RECRUITMENT_STRINGS_PHASE_ZERO;
            case 2: return StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_ONE;
            case 3: return StaticStrings.TUTORIAL_IMPROVEMENTS_STRINGS_PHASE_ONE;
            case -3: return StaticStrings.TUTORIAL_IMPROVEMENTS_STRINGS_PHASE_ZERO;
            case 4: return StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_TWO;
            case 5: return StaticStrings.TUTORIAL_POLICIES_STRINGS_PHASE_ZERO;
            case 6: return StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_THREE;
            case 7: return StaticStrings.TUTORIAL_PLANNING_STRINGS_PHASE_ZERO;
            case 8: return StaticStrings.TUTORIAL_BOOTH_GAME_STRINGS_PHASE_ZERO;
            case 9: return StaticStrings.TUTORIAL_CLUB_STRINGS_PHASE_FOUR;
        }
        return null;
    }

}
