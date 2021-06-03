using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlanningBehaviors : MonoBehaviour
{

    public GameObject noGirlWorkingPopup;

    public GameObject girlPortraitPrefab;

    public GameObject workListContent;
    public GameObject workList;

    public GameObject restListContent;
    public GameObject restList;

    public GameObject noEnergyPopup;
    public GameObject tutorialPopup;
    public GameObject boothsSpecialtiesPopup;

    public Button selectBoothsSpecialtiesButton;

    public Text girlNameText;
    public Image girlImage;
    public Text dancingSkillValue;
    public Text posingSkillValue;
    public Text foreplaySkillValue;
    public Text oralSkillValue;
    public Text sexSkillValue;
    public Text groupSkillValue;
    public Text energyValue;
    public Text opennessValue;
    public Text popularityValue;


    public GameObject startDayBlocker;

    public TMP_Dropdown dropdownCode;
    public TMP_Dropdown[] boothSpecialtySelectionDropDownList;

    public TextMeshProUGUI wontDoText;

    public Button favorited;

    public Button displayFavoritesButton;

    public static Work[] boothsSpecialties;

    public static GirlClass currentPlanningGirl;

    public bool displayOnlyFavorites = false;

    public void RefreshLists()
    {
        EmptyLists();
        FillLists();
    }

    public void EmptyLists()
    {
        Image[] images = workListContent.GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            Destroy(i);
        }

        images = restListContent.GetComponentsInChildren<Image>();
        foreach (Image i in images)
        {
            Destroy(i);
        }

        NewPlanningGirl[] npgs = workListContent.transform.GetComponentsInChildren<NewPlanningGirl>();
        foreach (NewPlanningGirl npg in npgs)
        {
            Destroy(npg.gameObject);
        }

        npgs = restListContent.transform.GetComponentsInChildren<NewPlanningGirl>();
        foreach (NewPlanningGirl npg in npgs)
        {
            Destroy(npg.gameObject);
        }

    }

    private void StoreBoothSpecialtySelectionInPlayerPrefs()
    {
        PlayerPrefs.SetInt("BoothSpecialty_1", (int)boothsSpecialties[0]);
        PlayerPrefs.SetInt("BoothSpecialty_2", (int)boothsSpecialties[1]);
        PlayerPrefs.SetInt("BoothSpecialty_3", (int)boothsSpecialties[2]);
        PlayerPrefs.SetInt("BoothSpecialty_4", (int)boothsSpecialties[3]);
        PlayerPrefs.SetInt("BoothSpecialty_5", (int)boothsSpecialties[4]);
        PlayerPrefs.SetInt("BoothSpecialty_6", (int)boothsSpecialties[5]);
    }

    /// <summary>
    /// Returns true if the performance type passed in parameter is available for this session
    /// </summary>
    /// <param name="performanceType"></param>
    /// <returns></returns>
    private bool HasPassedPolicy(Work performanceType)
    {
        switch (performanceType)
        {
            case Work.NONE: return true;
            case Work.DANCE: return true;
            case Work.POSE: return GMPoliciesData.policies.poseNaked;
            case Work.FOREPLAY: return GMPoliciesData.policies.handjob;
            case Work.ORAL: return GMPoliciesData.policies.blowjob;
            case Work.SEX: return GMPoliciesData.policies.facingVaginal;
            case Work.GROUP: return GMPoliciesData.policies.threesome;
            default: return false;
        }
    }

    private void LoadOneBoothSpecialty(int index, string key)
    {
        Work work = (Work)PlayerPrefs.GetInt(key);
        if (HasPassedPolicy(work))
        {
            boothsSpecialties[index] = work;
        }
        else
        {
            boothsSpecialties[index] = Work.NONE;
        }
    }

    private void LoadBoothSpecialtySelectionFromPlayerPrefs()
    {
        LoadOneBoothSpecialty(0, "BoothSpecialty_1");
        LoadOneBoothSpecialty(1, "BoothSpecialty_2");
        LoadOneBoothSpecialty(2, "BoothSpecialty_3");
        LoadOneBoothSpecialty(3, "BoothSpecialty_4");
        LoadOneBoothSpecialty(4, "BoothSpecialty_5");
        LoadOneBoothSpecialty(5, "BoothSpecialty_6");
    }

    public void FillLists()
    {
        GMRecruitmentData.recruitedGirlsList.Sort();

        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {

            if (!displayOnlyFavorites || (displayOnlyFavorites && girl.isFavorite))
            {
                GameObject instance = Instantiate(girlPortraitPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                //If the girl is resting today, OR if her energy is depleted
                //Put her occupation at resting and put her in the correct list
                if (girl.morningOccupation == Occupation.REST || girl.GetEnergy() <= GMGlobalNumericVariables.gnv.MIN_ENERGY)
                {
                    instance.transform.SetParent(restListContent.transform, false);
                    girl.morningOccupation = Occupation.REST;
                }
                else if (girl.morningOccupation == Occupation.WORK)
                {
                    instance.transform.SetParent(workListContent.transform, false);
                }

                instance.name = girl.name;

                NewPlanningDragHandler handler = instance.GetComponentInChildren<NewPlanningDragHandler>();

                handler.errorPopup = noEnergyPopup;
                handler.tutorialPopup = tutorialPopup;
                handler.workList = workList;
                handler.workListContent = workListContent;
                handler.restList = restList;
                handler.restListContent = restListContent;

                handler.dancingSkillValue = dancingSkillValue;
                handler.posingSkillValue = posingSkillValue;
                handler.foreplaySkillValue = foreplaySkillValue;
                handler.oralSkillValue = oralSkillValue;
                handler.sexSkillValue = sexSkillValue;
                handler.groupSkillValue = groupSkillValue;

                handler.energyValue = energyValue;
                handler.opennessValue = opennessValue;
                handler.popularityValue = popularityValue;

                handler.girlNameText = girlNameText;
                handler.girlImage = girlImage;

                handler.wontDoText = wontDoText;

                handler.favorited = favorited;

                handler.npb = this;

                NewPlanningGirl npg = instance.GetComponentInChildren<NewPlanningGirl>();
                npg.girl = girl;

                Image portrait = instance.GetComponentInChildren<Image>();
                if (!girl.external)
                {
                    portrait.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + girl.folderName + "/" +
                    StaticStrings.IMAGES_FOLDER + StaticStrings.CLOSEUP_PORTRAIT_FILE_NO_EXTENSION);
                }
                else
                {
                    portrait.sprite = girl.closeupPortrait;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //LoadBoothSpecialtySelectionFromPlayerPrefs();

        GirlClass g = GMRecruitmentData.recruitedGirlsList[0];

        currentPlanningGirl = g;

        girlNameText.text = g.name;
        dancingSkillValue.text = g.GetDancing().ToString();
        posingSkillValue.text = g.GetPosing().ToString();
        foreplaySkillValue.text = g.GetForeplay().ToString();
        oralSkillValue.text = g.GetOral().ToString();
        sexSkillValue.text = g.GetSex().ToString();
        groupSkillValue.text = g.GetGroup().ToString();

        energyValue.text = (Mathf.RoundToInt((g.GetEnergy() * 10)) / 10).ToString();
        opennessValue.text = (Mathf.RoundToInt((g.GetOpenness() * 10)) / 10).ToString();
        popularityValue.text = g.GetPopularity().ToString();

        DisplayFavorite(g);

        wontDoText.text = "Won't do: ";
        string tmpWontDoText = StaticFunctions.WontDoDisplay(g);

        if (tmpWontDoText.Equals(""))
        {
            wontDoText.text = StaticStrings.NOTHING_SHE_WONT_DO_TEXT;
        }
        else
        {
            wontDoText.text += tmpWontDoText;
        }


        if (!g.external)
        {
            if (g.name.Equals(StaticStrings.ASSISTANT_FULL_NAME))
            {
                girlImage.sprite = StaticAssistantData.data.currentCostume.GetCurrentCostume();
            }
            else
            {
                girlImage.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + g.folderName + "/" +
                                StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
            }
        }
        else
        {
            girlImage.sprite = g.GetPortrait();
        }

        FillLists();

        SpecialtyListsDisplay();

        //If the first specialty selection list is not active, then the button should not be
        //Because it means that there are no specialty selection list active
        if (!boothSpecialtySelectionDropDownList[0].gameObject.activeSelf)
        {
            selectBoothsSpecialtiesButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Creates the dropdowns to select the specialties of the booths
    /// </summary>
    private void SpecialtyListsDisplay()
    {
        string[] options = { "Random", StaticStrings.DANCE, StaticStrings.POSE, StaticStrings.FOREPLAY, StaticStrings.ORAL,
            StaticStrings.SEX, StaticStrings.GROUP };
        int numberOfItems = (int)HighestPerformanceUnlocked();

        for (int i = 0; i < boothSpecialtySelectionDropDownList.Length; i++)
        {
            TMP_Dropdown boothSpecialtyList = boothSpecialtySelectionDropDownList[i];
            if (i < GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS && GMImprovementsData.improvementsData.GetModularBooths()[i])
            {
                boothSpecialtyList.options = new List<TMP_Dropdown.OptionData>();
                if (numberOfItems > 1)
                {
                    boothSpecialtyList.gameObject.SetActive(true);
                    for (int j = 0; j <= numberOfItems; j++)
                    {
                        boothSpecialtyList.options.Add(new TMP_Dropdown.OptionData(options[j]));
                    }
                    boothSpecialtyList.value = (int)boothsSpecialties[i];
                    boothSpecialtyList.RefreshShownValue();
                }
                else
                {
                    boothSpecialtyList.gameObject.SetActive(false);
                }
            }
            else
            {
                boothSpecialtyList.gameObject.SetActive(false);
            }
        }
    }

    public void OnStartDayButtonPress()
    {
        BoothGameData.negotiatorUsage = 0;
        if (StaticBooleans.tutorialIsOn)
        {
            GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER = 8;
        }
        bool oneGirlWorking = false;
        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if (girl.morningOccupation == Occupation.WORK)
            {
                oneGirlWorking = true;
                break;
            }
        }
        if (oneGirlWorking)
        {
            foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
            {
                if (girl.morningOccupation == Occupation.WORK)
                    BoothGameData.girlsRoster.Add(girl);
                /*
                if (girl.morningOccupation == Occupation.REST)
                {
                    girl.AddToEnergy(GMGlobalNumericVariables.gnv.REST_ENERGY_GAIN);
                }
                */
            }
            GMGlobalNumericVariables.gnv.MONEY_BEFORE = GMClubData.money;
            GMGlobalNumericVariables.gnv.REPUTATION_BEFORE = GMClubData.GetReputation();

            SceneManager.LoadScene(StaticStrings.BOOTHS_MANAGEMENT_SCENE);
        }

        else
        {
            noGirlWorkingPopup.SetActive(true);
        }
    }

    public void OnWorkAllButtonPress()
    {
        int numberOfGirlsWorking = 0;
        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if (girl.morningOccupation == Occupation.WORK)
            {
                numberOfGirlsWorking++;
            }
        }
        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if (girl.morningOccupation == Occupation.REST
                && girl.GetEnergy() > GMGlobalNumericVariables.gnv.MIN_ENERGY
                && numberOfGirlsWorking < GMGlobalNumericVariables.gnv.MAX_WORKING_GIRLS)
            {
                girl.morningOccupation = Occupation.WORK;
                numberOfGirlsWorking++;
            }
        }
        RefreshLists();
    }

    public void OnRestAllButtonPress()
    {
        RestAllGirls();
        RefreshLists();
    }


    public void OnNoEnergyPopupClose()
    {
        noEnergyPopup.SetActive(false);
    }

    public void OnNoGirlWorkingCancel()
    {
        noGirlWorkingPopup.SetActive(false);
    }

    public void OnNoGirlWorkingConfirm()
    {
        GMGlobalNumericVariables.gnv.MONEY_BEFORE = GMClubData.money;
        GMGlobalNumericVariables.gnv.REPUTATION_BEFORE = GMClubData.GetReputation();
        StaticFunctions.PassADay(true, true);
        noGirlWorkingPopup.SetActive(false);
    }

    public void OnSortByName()
    {
        GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.NAME;
        CommonSortButtonBehavior();
    }

    public void OnSortByOpenness()
    {
        GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.OPENNESS;
        CommonSortButtonBehavior();
    }

    public void OnSortByEnergy()
    {
        GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.ENERGY;
        CommonSortButtonBehavior();
    }

    private void CommonSortButtonBehavior()
    {
        RefreshLists();
    }

    public void OnSortBySkill()
    {
        switch (dropdownCode.value)
        {
            case 1: GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.DANCING; break;
            case 2: GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.POSING; break;
            case 3: GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.FOREPLAY; break;
            case 4: GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.ORAL; break;
            case 5: GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.SEX; break;
            case 6: GMGlobalNumericVariables.gnv.SORT_TYPE = SortType.GROUP; break;
        }
        CommonSortButtonBehavior();
    }

    private void Update()
    {
        if (StaticBooleans.tutorialIsOn)
        {
            if (GMRecruitmentData.recruitedGirlsList[0].morningOccupation == Occupation.WORK)
            {
                startDayBlocker.SetActive(false);
            }
            else
            {
                startDayBlocker.SetActive(true);
            }
        }
    }

    public void ChooseSpecialty(int boothIndex)
    {
        boothsSpecialties[boothIndex] = (Work)boothSpecialtySelectionDropDownList[boothIndex].value;
        //StoreBoothSpecialtySelectionInPlayerPrefs();
    }

    private Work HighestPerformanceUnlocked()
    {
        if (GMPoliciesData.policies.threesome)
            return Work.GROUP;
        else if (GMPoliciesData.policies.facingVaginal)
            return Work.SEX;
        else if (GMPoliciesData.policies.blowjob)
            return Work.ORAL;
        else if (GMPoliciesData.policies.handjob)
            return Work.FOREPLAY;
        else if (GMPoliciesData.policies.poseNaked)
            return Work.POSE;

        return Work.DANCE;
    }

    public void OnSelectBoothSpecialtyButtonPress()
    {
        boothsSpecialtiesPopup.SetActive(true);
    }

    public void OnFavoritedButtonPress()
    {
        currentPlanningGirl.isFavorite = !currentPlanningGirl.isFavorite;

        DisplayFavorite(currentPlanningGirl);
    }

    private void DisplayFavorite(GirlClass girl)
    {
        if (girl.isFavorite)
        {
            favorited.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/star_full");
        }
        else
        {
            favorited.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/star_empty");
        }
    }

    public void OnDisplayOnlyFavoritesButtonPress()
    {
        displayOnlyFavorites = !displayOnlyFavorites;
        if (displayOnlyFavorites)
        {
            displayFavoritesButton.GetComponentInChildren<Text>().text = "Display all";
        }
        else
        {
            displayFavoritesButton.GetComponentInChildren<Text>().text = "Display favorites only";
        }
        RefreshLists();
    }

    private List<int> PickDiffrentRandomNumbers(int amountToPick, int maxValue)
    {
        if (amountToPick > maxValue)
        {
            Debug.LogError("The number of numbers to pick should be lower or equal to the max value");
        }

        else {
            List<int> numbers = new List<int>();
            for (int i = 0; i < maxValue; i++)
            {
                numbers.Add(i);
            }
        
        if (amountToPick == maxValue)
            {
                return numbers;
            }
        else
            {
                List<int> result = new List<int>();
                for(int i = 0; i < amountToPick; i++)
                {
                    int randomIndex = Random.Range(0, numbers.Count);
                    result.Add(numbers[randomIndex]);
                    numbers.RemoveAt(randomIndex);
                }
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// Puts all girls into the resting occupation
    /// </summary>
    private void RestAllGirls()
    {
        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            girl.morningOccupation = Occupation.REST;
        }
    }

    /// <summary>
    /// Puts random girls from a given list of girls to work
    /// </summary>
    /// <param name="listToPickFrom">The list of girls to pick from</param>
    private void PickRandomGirls(List<GirlClass> listToPickFrom)
    {
        //Here, we need to make it so all girls rest because some working girls might already be selected
        RestAllGirls();

        if (listToPickFrom.Count > GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS + 2)
        {
            int amountOfGirlsToPick = (GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS + 2 < GMGlobalNumericVariables.gnv.MAX_WORKING_GIRLS)
                ? GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS + 2 : GMGlobalNumericVariables.gnv.MAX_WORKING_GIRLS;

            List<int> girlIndexes =
                PickDiffrentRandomNumbers(amountOfGirlsToPick, listToPickFrom.Count);

            for (int i = 0; i < listToPickFrom.Count; i++)
            {
                if (girlIndexes.Contains(i))
                {
                    listToPickFrom[i].morningOccupation = Occupation.WORK;
                }
                else
                {
                    listToPickFrom[i].morningOccupation = Occupation.REST;
                }
            }
        }
        else
        {
            for (int i = 0; i < listToPickFrom.Count; i++)
            {
                listToPickFrom[i].morningOccupation = Occupation.WORK;
            }
        }
    }

    public void OnPickRandomGirlsButtonPress()
    {
        if (displayOnlyFavorites)
        {
            List<GirlClass> favorites = new List<GirlClass>();
            foreach (GirlClass g in GMRecruitmentData.recruitedGirlsList)
            {
                if (g.isFavorite)
                    favorites.Add(g);
            }
            PickRandomGirls(favorites);
        }
        else
        {
            PickRandomGirls(GMRecruitmentData.recruitedGirlsList);
        }
        RefreshLists();
    }

}
