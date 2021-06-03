using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfficeBehavior : MonoBehaviour
{
    public GameObject backToClubBlocker;

    public GameObject errorPopup;

    public Image assistantPortrait;

    public GameObject missionDisplayPrefab;

    public GameObject missionDisplayListContent;

    public GameObject toStoreButton;

    public Text assistantPointsText;

    public Button toAssistantWardrobeButton;
    public Button dateButton;

    public Button startMoneyToPointPopup;
    public GameObject moneyToPointsPopup;
    public TMP_InputField moneyToExchange;

    public GameObject assistantPointsObject;

    void Start()
    {
        foreach (Mission m in RegisterMissions.activeMissions)
        {
            m.isDisplayed = false;
        }
            StartCoroutine(DisplayMission());
        
        toStoreButton.SetActive(StaticAssistantData.data.assistantStoreUnlocked);

        toAssistantWardrobeButton.gameObject.SetActive(StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_FIRST_CAFE_DATE_DIALOG_ID).seen);

        dateButton.gameObject.SetActive(StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_THIRD_LATE_NIGHT_MEETING_DIALOG_ID).seen);

        startMoneyToPointPopup.gameObject.SetActive(StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_FIRST_CAFE_DATE_DIALOG_ID).seen);
    }

    IEnumerator DisplayMission()
    {
        assistantPortrait.sprite = StaticDialogElements.ChooseAssistantPortrait();

        /*
        while (missionDisplayListContent.transform.childCount > 0)
        {
            Destroy(missionDisplayListContent.transform.GetChild(0).gameObject);
        }
        */

        //foreach (Transform child in missionDisplayListContent.transform)
            //Destroy(child.gameObject);

        foreach (Mission m in RegisterMissions.activeMissions)
        {
            if (!m.isDisplayed)
            {
                m.isDisplayed = true;
                GameObject missionInstance = Instantiate(missionDisplayPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                missionInstance.transform.SetParent(missionDisplayListContent.transform, false);
                missionInstance.name = m.title;
                TextMeshProUGUI text = missionInstance.GetComponentInChildren<TextMeshProUGUI>();
                text.text = m.description;
                if (m.CheckDoable())
                {
                    Button b = missionInstance.GetComponentInChildren<Button>();
                    b.GetComponentInChildren<TextMeshProUGUI>().text = "Finish";
                    b.onClick.RemoveAllListeners();
                    b.onClick.AddListener(delegate () {
                        if (m.CheckDoable())
                        {
                            m.Effect();
                            RegisterMissions.activeMissions.Remove(m);
                            text.text = "";
                            b.gameObject.SetActive(false);
                            Destroy(missionInstance);
                        }
                        else
                        {
                            errorPopup.GetComponentInChildren<Text>(true).text = "You don't fulfill the criterias to finish this mission.";
                            errorPopup.gameObject.SetActive(true);
                        }
                    });
                }
                else
                {
                    Button b = missionInstance.GetComponentInChildren<Button>(true);
                    b.gameObject.SetActive(false);
                }
            }
        }

        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(DisplayMission());
    }

    // Update is called once per frame
    void Update()
    {
        backToClubBlocker.SetActive(StaticBooleans.tutorialIsOn);
    }

    public void OnAcceptMissionPress()
    {
        RegisterMissions.availableMissions[0].Accept();
        RegisterMissions.activeMissions.Add(RegisterMissions.availableMissions[0]);
        RegisterMissions.availableMissions.RemoveAt(0);
    }

    public void OnDateButtonPress()
    {
        if (StaticAssistantData.data.datesAvailableToday > 0)
        {
            SceneManager.LoadScene(StaticStrings.DATE_CHOOSING_SCENE);
        }
        else
        {
            errorPopup.GetComponentInChildren<Text>(true).text = "You can't date her anymore today!";
            errorPopup.gameObject.SetActive(true);
        }
    }

    public void OnWardrobeButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.ASSISTANT_WARDROBE_SCENE);
    }

    public void OnStartMoneyToPointsButtonPress()
    {
        moneyToPointsPopup.SetActive(true);
        moneyToPointsPopup.GetComponentInChildren<Text>(true).text = "How many points would you like to buy?\n(" 
            + "1 point costs " + GMGlobalNumericVariables.gnv.MONEY_TO_NICOLE_POINTS_CONVERSION_VALUE + "$)";
    }

    public void CancelMoneyToPointsPopupButtonPress()
    {
        moneyToExchange.text = "";
        moneyToPointsPopup.SetActive(false);
    }

    public void ConfirmMoneyToPointsButtonPress()
    {
        int pointsToGet = int.Parse(moneyToExchange.text);
        if (pointsToGet >= 0)
        {
            if (pointsToGet * GMGlobalNumericVariables.gnv.MONEY_TO_NICOLE_POINTS_CONVERSION_VALUE <= GMClubData.money)
            {
                GMClubData.money -= pointsToGet * GMGlobalNumericVariables.gnv.MONEY_TO_NICOLE_POINTS_CONVERSION_VALUE;
                moneyToExchange.text = "";
                moneyToPointsPopup.SetActive(false);
                StaticAssistantData.data.assistantPoints += pointsToGet;
                assistantPointsText.text = StaticAssistantData.data.assistantPoints.ToString();
            }
            else
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>(true).text = "Not enough money to buy that amount of points.\n" +
                    "You need at least " + pointsToGet * GMGlobalNumericVariables.gnv.MONEY_TO_NICOLE_POINTS_CONVERSION_VALUE + "$ to buy that much.";
            }
        }
        else
        {
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>(true).text = "You can't exchange a negative amount of points!";
        }
    }
}
