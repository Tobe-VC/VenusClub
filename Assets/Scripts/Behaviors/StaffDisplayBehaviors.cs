using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaffDisplayBehaviors : MonoBehaviour
{
    public GameObject girlPortraitObject;

    public Text girlBio;
    public Text girlName;

    public Text dancingSkillText;
    public Text posingSkillText;
    public Text foreplaySkillText;
    public Text oralSkillText;
    public Text sexSkillText;
    public Text groupSkillText;

    private Image girlPortrait;

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

    public TextMeshProUGUI wontDoText;
    public TextMeshProUGUI wontDoTitle;

    public GameObject warningPopup;

    public Button fireButton;
    public Button resetButton;
    public Button repairButton;


    private void Awake()
    {
        StaffDisplayData.staffList = GMRecruitmentData.recruitedGirlsList;
    }

    private void LoadNewSprite()
    {
        girlPortrait = girlPortraitObject.GetComponent<Image>();

        if (!StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].external)
        {
            if (StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].name.Equals(StaticStrings.ASSISTANT_FULL_NAME))
            {
                girlPortrait.sprite = StaticAssistantData.data.currentCostume.GetCurrentCostume();
            }
            else
            {
                Sprite spr = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].folderName + "/" + StaticStrings.IMAGES_FOLDER
                + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);

                girlPortrait.sprite = spr;
            }
        }
        else
        {
                girlPortrait.sprite = StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetPortrait();
        }


    }

    private void LoadNewBio()
    {
        girlName.text = StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].folderName;
        if (!StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].external)
        {
            girlBio.text = Resources.Load<TextAsset>(StaticStrings.GIRLS_FOLDER + 
                StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].name + "/" +
                StaticStrings.TEXTS_FOLDER + StaticStrings.BIO_FILE_NO_EXTENSION).text;
        }
        else
        {
            try
            {
                using (StreamReader reader = new StreamReader(StaticStrings.GIRLPACKS_DIRECTORY +
                    StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].folderName + "/" +
                    StaticStrings.TEXTS_FOLDER + StaticStrings.BIO_FILE))
                {
                    girlBio.text = reader.ReadToEnd();
                }
            }
            catch
            {
                girlBio.text = "";
            }
        }


    }

    private void LoadNewSkills()
    {
        dancingSkillText.text = StaticStrings.DANCE + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetDancing();
        posingSkillText.text = StaticStrings.POSE + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetPosing();
        foreplaySkillText.text = StaticStrings.FOREPLAY + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetForeplay();
        oralSkillText.text = StaticStrings.ORAL + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetOral();
        sexSkillText.text = StaticStrings.SEX + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetSex();
        groupSkillText.text = StaticStrings.GROUP + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetGroup();
    }

    private void LoadNewCaracteristics()
    {
        bustTypeText.text = StaticStrings.BUST_TYPE_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].bustType);
        eyeColorText.text = StaticStrings.EYE_COLOR_TYPE_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].eyeColor);
        hairColorText.text = StaticStrings.HAIR_COLOR_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].hairColor);
        bodyTypeText.text = StaticStrings.BODY_TYPE_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].bodyType);
        skinComplexionText.text = StaticStrings.SKIN_COMPLEXION_TEXT + ": " + StaticFunctions.ToLowerCaseExceptFirst(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].skinComplexion);
        ageText.text = StaticStrings.AGE_TEXT + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].age;
        heightText.text = StaticStrings.HEIGHT_TEXT + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].height + "cm";
    }

    private void LoadNewStats()
    {
        popularityText.text = StaticStrings.POPULARITY_TEXT + ": " + StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetPopularity();
        energyText.text = StaticStrings.ENERGY_TEXT + ": " + Mathf.Floor(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetEnergy() * 100) / 100;
        opennessText.text = StaticStrings.OPENNESS_TEXT + ": " + Mathf.Floor(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].GetOpenness() * 100) / 100;
    }

    private void LoadNewWontDos()
    {
        wontDoText.text = "";
        string s = StaticFunctions.WontDoDisplay(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex]);
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
        //Debug.Log(GMRecruitmentData.recruitedGirlsList[0]);

        LoadNewWontDos();
        LoadNewSprite();
        LoadNewBio();
        LoadNewSkills();
        LoadNewCaracteristics();
        LoadNewStats();

        bool isAssistant = (StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex].name == StaticStrings.ASSISTANT_FULL_NAME);
        fireButton.gameObject.SetActive(!isAssistant);
        resetButton.gameObject.SetActive(!isAssistant); ;
        repairButton.gameObject.SetActive(!isAssistant); ;
}

    void Start()
    {
        LoadNewGirl();
    }

    private bool GirlListContainsName(List<GirlClass> girls, string name)
    {
        foreach (GirlClass girl in girls)
        {
            if (girl.name == name)
                return true;
        }

        return false;
    }

    public void NextGirlButtonPress()
    {
        if (StaffDisplayData.staffList.Count > 0)
        {
            StaffDisplayData.currentStaffGirlIndex++;
            if (StaffDisplayData.currentStaffGirlIndex >= StaffDisplayData.staffList.Count)
                StaffDisplayData.currentStaffGirlIndex = 0;

            LoadNewGirl();
        }
        else
        {
            SceneManager.LoadScene(StaticStrings.CLUB_SCENE);
        }
    }

    public void PreviousGirlButtonPress()
    {
        StaffDisplayData.currentStaffGirlIndex--;
        if (StaffDisplayData.currentStaffGirlIndex < 0)
            StaffDisplayData.currentStaffGirlIndex = StaffDisplayData.staffList.Count - 1;

        LoadNewGirl();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Right"))
            NextGirlButtonPress();
        else if (Input.GetButtonDown("Left"))
            PreviousGirlButtonPress();
    }

    public void OnRepairButtonClick()
    {
        Text text = warningPopup.GetComponentInChildren<Text>(true);
        text.text = "This will reset all the performance this girl won't do to the base values in her girlpack. You should only use this if you modified this girl's pack after creating the current savefile." +
            "\nIf you're not sure what this means, do not proceed." +
            "\n\nProceed? ";

        foreach (Button b in warningPopup.GetComponentsInChildren<Button>(true))
        {
            if (b.CompareTag("PopupConfirm"))
            {
                //If it is the confirm button
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(RepairGirl);
            }
            else
            {
                //If it is not the confirm button, then it is the cancel
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate () { warningPopup.SetActive(false); });
            }
        }

        warningPopup.SetActive(true);
    }

    public void OnRepairAllButtonClick()
    {
        Text text = warningPopup.GetComponentInChildren<Text>(true);
        text.text = "This will reset all the performance all the girls won't do to the base values in their girlpacks. You should only use this if you modified multiple girlpacks after creating the current savefile." +
            "\nIf you're not sure what this means, do not proceed." +
            "\n\nProceed? ";

        foreach(Button b in warningPopup.GetComponentsInChildren<Button>(true))
        {
            if (b.CompareTag("PopupConfirm"))
            {
                //If it is the confirm button
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(RepairAllGirls);
            }
            else
            {
                //If it is not the confirm button, then it is the cancel
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate () { warningPopup.SetActive(false); });
            }
        }

        warningPopup.SetActive(true);
    }

    public void RepairGirl()
    {
        RepairOneGirl(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex]);
        SceneManager.LoadScene(StaticStrings.STAFF_SCENE); 
    }

    public void RepairAllGirls()
    {
        foreach(GirlClass girl in StaffDisplayData.staffList)
        {
            if(girl.name != StaticStrings.ASSISTANT_FULL_NAME)
                RepairOneGirl(girl);
        }
        SceneManager.LoadScene(StaticStrings.STAFF_SCENE);
    }

    private void RepairOneGirl(GirlClass girl)
    {
        GirlClass g = null;
        if (girl.external)
        {
            StreamReader reader = new StreamReader(
                StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + "/" +
                StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE);
            g = GirlClass.CreateFromJSON(reader.ReadToEnd());
            reader.Close();
            girl.CopyDos(g);
        }
        else
        {
            if(girl.name != StaticStrings.ASSISTANT_FULL_NAME){
                g = GirlClass.CreateFromJSON((Resources.Load<TextAsset>(StaticStrings.GIRLS_FOLDER +
                    girl.folderName + "/" +
                    StaticStrings.JSON_DATA_FOLDER + StaticStrings.DESCRIPTION_FILE_NO_EXTENSION)).text);
                girl.CopyDos(g);
            }
        }

    }

    public void OnFireGirlButtonPress()
    {
        GirlClass girl = StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex];
        girl.moneyCost = girl.moneyCost + (int)(girl.money * 0.1f);
        girl.reputationCost = girl.reputationCost + Mathf.RoundToInt(girl.GetPopularity() * 0.1f);
        if(girl.reputationCost > 100)
        {
            girl.reputationCost = 100;
        }
        GirlClass g = GameMasterGlobalData.girlsList.Find(x => x.name == girl.name);
        g.CopyFiredGirl(girl);
        g.morningOccupation = Occupation.REST;

        GMRecruitmentData.firedGirlsList.Add(girl);
        GMRecruitmentData.recruitedGirlsList.RemoveAt(StaffDisplayData.currentStaffGirlIndex);
        NextGirlButtonPress();
        //SceneManager.LoadScene(StaticStrings.STAFF_SCENE);
    }

    private void ResetGirl()
    {
        StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex] = 
            StaticFunctions.ResetGirl(StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex]);

    }

    public void OnResetGirlButtonClick()
    {
        Text text = warningPopup.GetComponentInChildren<Text>(true);
        text.text = "This will completely reset this girl. Her skills, lesson, openness and all stats will be reset to their beginning values." +
            "\n\nProceed? ";
        foreach (Button b in warningPopup.GetComponentsInChildren<Button>(true))
        {
            if (b.CompareTag("PopupConfirm"))
            {
                //If it is the confirm button
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(ResetGirl);
            }
            else
            {
                //If it is not the confirm button, then it is the cancel
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate () { warningPopup.SetActive(false); });
            }
        }
        warningPopup.SetActive(true);
    }

    public void ToGalleryButtonPress()
    {
        GalleryBehavior.girlChecked = StaffDisplayData.staffList[StaffDisplayData.currentStaffGirlIndex];
        SceneManager.LoadScene(StaticStrings.GALLERY_SCENE);
    }

}
