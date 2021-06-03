using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuNavigation : MonoBehaviour
{

    //Ued when the New Game button is pressed
    public void OnToNewGameButtonPress()
    {
        StaticBooleans.tutorialIsOn = true;
        if (StaticBooleans.gameIsLoaded)
        {
            //If the game has been loaded the different variables must be reinitialized
            GMClubData.money = GMGlobalNumericVariables.gnv.STARTING_MONEY;
            GMClubData.SetReputation(GMGlobalNumericVariables.gnv.STARTING_REPUTATION);
            GMClubData.SetInfluence(GMGlobalNumericVariables.gnv.STARTING_INFLUENCE);
            GMClubData.SetConnection(GMGlobalNumericVariables.gnv.STARTING_CONNECTION);
            GMClubData.day = GMGlobalNumericVariables.gnv.STARTING_DAY;

            GMRecruitmentData.recruitedGirlsList = new List<GirlClass>();
            GMRecruitmentData.firedGirlsList = new List<GirlClass>();
            GMRecruitmentData.currentGirlNumber = 0;

            GMPoliciesData.policies = new PoliciesData();
            GMPoliciesData.ownedPolicies = new List<Policy>();

            GMImprovementsData.improvementsData = new ImprovementsData();
            GMImprovementsData.boughtImprovements = new List<Improvement>();

            GameMasterGlobalData.clubImprovementList = null;

            GMWardrobeData.costumesData = new CostumesData();
            GMWardrobeData.ownedCostumes = new List<Costume>();
            GMWardrobeData.currentlyUsedCostume = new Costume();

            GMGlobalNumericVariables.gnv = new GlobalNumericVariables();
            StaticBooleans.InitializeBooleans();
            StaticFunctions.ReinitializeVariables();
        }

        

        StaticBooleans.gameIsLoaded = true;
        //In any case, game loaded or not, launch the office scene
        SceneManager.LoadScene(StaticStrings.OFFICE_SCENE);
    }

    public void OnToLoadGameButton()
    {
        SceneManager.LoadScene(StaticStrings.LOAD_SCENE_FROM_MAIN_MENU);
    }

    private void Awake()
    {
        StaticFunctions.ReinitializeVariables();
        if (!PlayerPrefs.HasKey(StaticStrings.PLAYER_PREFS_ACTIVATE_FOOTJOBS))
        {
            PlayerPrefs.SetInt(StaticStrings.PLAYER_PREFS_ACTIVATE_FOOTJOBS, 1);
        }
    }

    public void OnToCreditsButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.CREDITS_SCENE);
    }

}
