using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class RecruitmentDisplay : MonoBehaviour
{

    public GameObject girlPortraitObject;

    public Text girlName;
    public Text girlBio;

    public Text dancingSkillText;
    public Text posingSkillText;
    public Text foreplaySkillText;
    public Text oralSkillText;
    public Text sexSkillText;
    public Text groupSkillText;

    public Text bustTypeText;
    public Text eyeColorText;
    public Text hairColorText;
    public Text bodyTypeText;
    public Text skinComplexionText;
    public Text ageText;
    public Text heightText;

    public Text popularityText;
    public Text energyText;
    public Text opennessText;

    public Text moneyCost;
    public Text reputationCost;
    public Text influenceCost;
    public Text connectionCost;

    public GameObject popup;
    public Text popupText;

    public GameObject confirmationPopup;

    private Image girlPortrait;

    public TextMeshProUGUI wontDoText;
    public TextMeshProUGUI wontDoTitle;

    //private Sprite tmpSpr = null;

    //The object used to block the to club button, used in the tutorial
    public GameObject toClubBlocker;

    private void LoadNewSprite()
    {
        girlPortrait = girlPortraitObject.GetComponent<Image>();

        if (!GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].external)
        {
            Sprite spr = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + GMRecruitmentData.currentGirlName + "/" + StaticStrings.IMAGES_FOLDER
            + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
            girlPortrait.sprite = spr;
        }
        else
        {
            girlPortrait.sprite = GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetPortrait();

        }
    }
    /*
    private IEnumerator LoadImage()
    {
        yield return StaticFunctions.LoadImageFromURL("file:///" +
    StaticStrings.EXTERNAL_ASSETS_DIRECTORY + GMRecruitmentData.currentGirlName + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_JPG,
    "file:///" +
    StaticStrings.EXTERNAL_ASSETS_DIRECTORY + GMRecruitmentData.currentGirlName + "/" +
    StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_PNG, girlPortrait);
        tmpSpr = girlPortrait.sprite;
    }
    */
    private void LoadNewBio()
    {
        girlName.text = GMRecruitmentData.currentGirlName;
        if (!GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].external)
        {
            girlBio.text = Resources.Load<TextAsset>(StaticStrings.GIRLS_FOLDER + GMRecruitmentData.currentGirlName + "/" +
                    StaticStrings.TEXTS_FOLDER + StaticStrings.BIO_FILE_NO_EXTENSION).text;
        }
        else
        {
            try
            {
                using (StreamReader reader = new StreamReader(StaticStrings.GIRLPACKS_DIRECTORY + 
                    GMRecruitmentData.currentGirlName + "/" + StaticStrings.TEXTS_FOLDER + StaticStrings.BIO_FILE))
                {
                    girlBio.text = reader.ReadToEnd();
                }
                //reader.Close();
            }
            catch
            {
                girlBio.text = "";
            }
        }
    }

    private void LoadNewSkills()
    {
        dancingSkillText.text = StaticStrings.DANCE + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetDancing();
        posingSkillText.text = StaticStrings.POSE + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetPosing();
        foreplaySkillText.text = StaticStrings.FOREPLAY + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetForeplay();
        oralSkillText.text = StaticStrings.ORAL + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetOral();
        sexSkillText.text = StaticStrings.SEX + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetSex();
        groupSkillText.text = StaticStrings.GROUP + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetGroup();
    }

    private void LoadNewCaracteristics()
    {
        bustTypeText.text = StaticStrings.BUST_TYPE_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].bustType);
        eyeColorText.text = StaticStrings.EYE_COLOR_TYPE_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].eyeColor);
        hairColorText.text = StaticStrings.HAIR_COLOR_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].hairColor);
        bodyTypeText.text = StaticStrings.BODY_TYPE_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].bodyType);
        skinComplexionText.text = StaticStrings.SKIN_COMPLEXION_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].skinComplexion);
        ageText.text = StaticStrings.AGE_TEXT + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].age;
        heightText.text = StaticStrings.HEIGHT_TEXT + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].height + "cm";
    }

    private void LoadNewStats()
    {
        popularityText.text = StaticStrings.POPULARITY_TEXT +": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetPopularity();
        energyText.text = StaticStrings.ENERGY_TEXT + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetEnergy();
        opennessText.text = StaticStrings.OPENNESS_TEXT + ": " + GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].GetOpenness();
    }

    //Changes the display for recruitment costs
    private void LoadNewCosts()
    {
        int money = GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].moneyCost;
        float repRequired = GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].reputationCost;
        float infRequired = GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].influenceCost;
        float conRequired = GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].connectionCost;

        if (money > 0)
            moneyCost.text = money + StaticStrings.MONEY_SIGN;
        else
            moneyCost.text = "";

        if (repRequired > 0)
            reputationCost.text = repRequired + " " + StaticStrings.REPUTATION_SIGN;
        else
            reputationCost.text = "";

        if (infRequired > 0)
            influenceCost.text = infRequired + " " + StaticStrings.INFLUENCE_SIGN;
        else
            influenceCost.text = "";

        if (conRequired > 0)
            connectionCost.text = conRequired + " " + StaticStrings.CONNECTION_SIGN;
        else
            connectionCost.text = "";
    }

    private void LoadNewWontDos()
    {
        wontDoText.text = "";
        string s = StaticFunctions.WontDoDisplay(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber]);
        if (s.Equals(""))
        {
            wontDoTitle.text = StaticStrings.NOTHING_SHE_WONT_DO_TEXT;
        }
        else
        {
            wontDoTitle.text = StaticStrings.WONT_DO_TEXT;
            wontDoText.text = s;
        }
    }

    private void LoadNewGirl()
    {
        LoadNewWontDos();
        LoadNewSprite();
        LoadNewBio();
        LoadNewSkills();
        LoadNewCaracteristics();
        LoadNewStats();
        LoadNewCosts();
    }

    void Start()
    {
        GMRecruitmentData.currentGirlName = GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].name;
        if (!GirlListContainsName(GMRecruitmentData.recruitedGirlsList, GMRecruitmentData.currentGirlName)
            && !GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].inLottery
            && !GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].doNotDisplay
            && (GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].influenceCost <= 0
                || StaticFunctions.IsCrimeOfficeAvailable()))
        {
                LoadNewGirl();
        }
        else
            NextGirlButtonPress();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Right"))
            NextGirlButtonPress();
        else if (Input.GetButtonDown("Left"))
            PreviousGirlButtonPress();
    }

    private bool GirlListContainsName(List<GirlClass> girls,string name)
    {
        foreach(GirlClass girl in girls)
        {
            if (girl.name == name)
                return true;
        }

        return false;
    }

    public void NextGirlButtonPress()
    {
        if (GMRecruitmentData.recruitedGirlsList.Count < GameMasterGlobalData.girlsList.Count)
        {
            if (!StaticFunctions.OnlyInfluenceOrLotteryGirlsInPool())
            {
                GMRecruitmentData.currentGirlNumber++;
                if (GMRecruitmentData.currentGirlNumber >= GameMasterGlobalData.girlsList.Count)
                    GMRecruitmentData.currentGirlNumber = 0;

                GMRecruitmentData.currentGirlName = GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].name;
                if (!GirlListContainsName(GMRecruitmentData.recruitedGirlsList, GMRecruitmentData.currentGirlName)
                    && !GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].inLottery
                    && !GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].doNotDisplay
                    && (GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].influenceCost <= 0
                        || StaticFunctions.IsCrimeOfficeAvailable()))
                {
                    LoadNewGirl();
                }
                else
                    NextGirlButtonPress();
            }
            else
            {
                SceneManager.LoadScene(StaticStrings.ALL_GIRL_CURRENTLY_AVAILABLE_RECRUITED_SCENE);
            }
        }
        else
        {
            SceneManager.LoadScene(StaticStrings.ALL_GIRL_RECRUITED_SCENE);
        }
    }

    public void PreviousGirlButtonPress()
    {
        if (GMRecruitmentData.recruitedGirlsList.Count < GameMasterGlobalData.girlsList.Count)
        {
            if (!StaticFunctions.OnlyInfluenceOrLotteryGirlsInPool())
            {
                GMRecruitmentData.currentGirlNumber--;
                if (GMRecruitmentData.currentGirlNumber < 0)
                    GMRecruitmentData.currentGirlNumber = GameMasterGlobalData.girlsList.Count - 1;

                GMRecruitmentData.currentGirlName = GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].name;
                if (!GirlListContainsName(GMRecruitmentData.recruitedGirlsList, GMRecruitmentData.currentGirlName)
                    && !GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].inLottery
                    && !GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].doNotDisplay
                    && (GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].influenceCost <= 0
                            || StaticFunctions.IsCrimeOfficeAvailable()))
                {
                    LoadNewGirl();
                }
                else
                    PreviousGirlButtonPress();
            }
            else
            {
                SceneManager.LoadScene(StaticStrings.ALL_GIRL_CURRENTLY_AVAILABLE_RECRUITED_SCENE);
            }
        }
        else
        {
            SceneManager.LoadScene(StaticStrings.ALL_GIRL_RECRUITED_SCENE);
        }
    }

    public void OnGirlPortraitPress()
    {
        //Test if any of the 3 ressources are missing to recruit the girl
        if (GMClubData.money >= GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].moneyCost)
        {
            //First, test if there is enough reputation
            if (GMClubData.GetReputation() >= GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].reputationCost)
            {

                //If there is enough reputation, test influence
                if (GMClubData.GetInfluence() >= GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].influenceCost)
                {

                    //If there is enough influence, test connection
                    if (GMClubData.GetConnection() >= GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].connectionCost)
                    {
                        confirmationPopup.SetActive(true);
                    }
                    else
                    {
                        //If there isn't enough connection, prompt the correct error message
                        popup.SetActive(true);
                        popupText.text = StaticStrings.NOT_ENOUGH_CONNECTION_ERROR_POPUP_MESSAGE;
                    }
                }
                else
                {
                    //If there isn't enough influence, prompt the correct error message
                    popup.SetActive(true);
                    popupText.text = StaticStrings.NOT_ENOUGH_INFLUENCE_TO_HIRE_ERROR_POPUP_MESSAGE;
                }

            }

            else
            {
                //If there isn't enough reputation, prompt the correct error message
                popup.SetActive(true);
                popupText.text = StaticStrings.NOT_ENOUGH_REPUTATION_ERROR_POPUP_MESSAGE;
            }
        }
        else
        {
            //If there isn't enough money, prompt the correct error message
            popup.SetActive(true);
            popupText.text = StaticStrings.NOT_ENOUGH_MONEY_RECRUITMENT_ERROR_POPUP_MESSAGE;
        }
    }

    public void OnConfirmationPopupConfirmButtonPress()
    {
        //If there is enough of each, buy the girl by removing the ressources needed and adding her to the list
        GMClubData.SpendMoney(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].moneyCost);
        GMClubData.SpendReputation(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].reputationCost);
        GMClubData.SpendInfluence(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].influenceCost);
        GMClubData.SpendConnection(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].connectionCost);

        GMRecruitmentData.recruitedGirlsList.Add(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber]);
        
        int indexToRemove = -1;
        for(int i = 0; i < GMRecruitmentData.firedGirlsList.Count; i++)
        {
            GirlClass girl = GMRecruitmentData.firedGirlsList[i];
            if (girl.name.Equals(GameMasterGlobalData.girlsList[GMRecruitmentData.currentGirlNumber].name))
            {
                indexToRemove = i;
                break;
            }
        }

        if(indexToRemove >= 0)
        {
            GMRecruitmentData.firedGirlsList.RemoveAt(indexToRemove);
        }

        NextGirlButtonPress();
        toClubBlocker.SetActive(false);
        confirmationPopup.SetActive(false);
    }

    public void OnConfirmationPopupCancelButtonPress()
    {
        confirmationPopup.SetActive(false);
    }

}
