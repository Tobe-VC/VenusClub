using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommonNavigationButtons : MonoBehaviour
{
    public GameObject popup;

    private void LaunchPopup(string text)
    {
        if (popup != null)
        {
            popup.SetActive(true);
            popup.GetComponentInChildren<Text>().text = text;
        }
    }

    private void KillPopup()
    {
        popup.SetActive(false);
    }

    public void OnToClubButtonPress()
    {

        if(GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 1)
        {
            //For the tutorial
            //If the scene step number is 1, then we are about to go from the recruitment scene back to the club scene
            //And so the scene step number must be changed
            GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 2;
        }
        else if (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 3)
        {
            //For the tutorial
            //If the scene step number is 3, then we are about to go from the improvement scene back to the club scene
            //And so the scene step number must be changed
            GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 4;
        }
        else if (GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER == 5)
        {
            //For the tutorial
            //If the scene step number is 3, then we are about to go from the improvement scene back to the club scene
            //And so the scene step number must be changed
            GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 6;
        }

        SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
    }

    public void OnToPoliciesButtonPress()
    {
            SceneManager.LoadScene(StaticStrings.POLICIES_SCENE);

    }

    public void OnToRecruitmentButtonPress()
    {
        //It is only possible to go to the recruitment scene if there are girls to recruit
        if (GMRecruitmentData.recruitedGirlsList == null || GMRecruitmentData.recruitedGirlsList.Count < GameMasterGlobalData.girlsList.Count)
        {
            if (!StaticFunctions.OnlyInfluenceOrLotteryGirlsInPool())
            {
                SceneManager.LoadScene(StaticStrings.RECRUITMENT_SCENE);
            }
            else
            {
                LaunchPopup(StaticStrings.RECRUITMENT_UNAVAILABLE_GIRLS_ERROR_POPUP_MESSAGE);
            }
        }
        //If there are no more girls to recruit, give a message
        else
        {
            LaunchPopup(StaticStrings.RECRUITMENT_ERROR_POPUP_MESSAGE);
        }
    }

    public void OnToPlanningButtonPress()
    {
        if (GMRecruitmentData.recruitedGirlsList != null && GMRecruitmentData.recruitedGirlsList.Count > 0)
            SceneManager.LoadScene(StaticStrings.PLANNING_SCENE);
        else
        {
            LaunchPopup(StaticStrings.STAFF_DISPLAY_ERROR_POPUP_MESSAGE);
        }
    }

    public void OnToNewPlanningButtonPress()
    {

        if (GMRecruitmentData.recruitedGirlsList != null && GMRecruitmentData.recruitedGirlsList.Count > 0)
            if(GMPoliciesData.policies.dance)
                SceneManager.LoadScene(StaticStrings.NEW_GIRL_PLANNING_SCENE);
            else
            {
                LaunchPopup(StaticStrings.NO_DANCE_ERROR_POPUP_MESSAGE);
            }
        else
        {
            LaunchPopup(StaticStrings.STAFF_DISPLAY_ERROR_POPUP_MESSAGE);
        }
    }

    public void OnToGirlPlanningButtonPress()
    {
        if (GMRecruitmentData.recruitedGirlsList != null && GMRecruitmentData.recruitedGirlsList.Count > 0)
            SceneManager.LoadScene(StaticStrings.GIRL_PLANNING_SCENE);
        else
        {
            LaunchPopup(StaticStrings.STAFF_DISPLAY_ERROR_POPUP_MESSAGE);
        }
    }

    public void OnToBuilderButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.IMPROVEMENTS_SCENE);
    }

    public void OnToStaffDisplayButtonPress()
    {
        //It is only possible to go to the staff display scene if there are girls recruited
        if (GMRecruitmentData.recruitedGirlsList != null && GMRecruitmentData.recruitedGirlsList.Count > 0)
            SceneManager.LoadScene(StaticStrings.STAFF_SCENE);
        //If there are no girls recruited, give a message
        else
        {
            LaunchPopup(StaticStrings.STAFF_DISPLAY_ERROR_POPUP_MESSAGE);
        }
    }

    public void OnToEndPopuButtonPress()
    {
        KillPopup();
    }

    public void OnToRoomsBuiltButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.ROOMS_BUILT_SCENE);
    }

    public void OnToPoliciesBoughtPress()
    {
        SceneManager.LoadScene(StaticStrings.POLICIES_BOUGHT_SCENE);
    }

    public void OnToWardrobeButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.WARDROBE_SCENE);
    }

    public void OnToWardrobeEquipButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.WARDROBE_EQUIP_SCENE);
    }

    public void OnToOfficeButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.OFFICE_SCENE);
    }

    public void OnToCrimeOfficeButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.CRIME_SERVICES_SCENE);
    }

    public void OnToInteractionRoomButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.INTERACTION_SCENE);
    }

    public void OnToMainMenuButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.MAIN_MENU_SCENE);
    }

    public void OnToAssistantStoreButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.ASSISTANT_STORE_SCENE);
    }

    public void OnToPersonalImprovementsButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.PERSONAL_IMPROVEMENTS_SCENE);
    }

    public void OnExitButtonPress()
    {
        Application.Quit();
    }

}
