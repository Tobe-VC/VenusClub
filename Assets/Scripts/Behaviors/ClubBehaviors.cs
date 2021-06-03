using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ClubBehaviors : MonoBehaviour
{

    public GameObject popup;

    public GameObject recapPopup;

    public GameObject eventPopup;

    public Text eventPopupTitle;
    public Text eventPopupDescription;
    public VideoPlayer eventPopupVideoPlayer;
    public VideoClip clip;

    public Button buyCostumesButton;
    public Button equipCostumesButton;

    public Image recapPortrait;

    public Button crimeOfficeButton;

    public GameObject errorPopupPrefab;

    public GameObject dayRecapPrefab;

    public Button interactionRoomButton;
    public Button personalImprovementStoreButton;

    public void OnNextDayButtonPress()
    {
        GMClubData.day++;
        SceneManager.LoadScene(StaticStrings.DAY_RESULT_SCENE);
    }

    public void OnManageBoothsButtonPress()
    {
        //BoothGameData.girlsRoster = GMRecruitmentData.recruitedGirlsList;
        SceneManager.LoadScene(StaticStrings.BOOTHS_MANAGEMENT_SCENE);
    }


    public void SaveClubData()
    {
        StaticFunctions.Save(StaticStrings.SAVE_FILE);

    }

    private void Awake()
    {
        /*
        foreach(GirlLesson gl in GMRecruitmentData.recruitedGirlsList[0].girlLessons)
        {
            Debug.Log(GMRecruitmentData.recruitedGirlsList[0].name);
            Debug.Log(gl.done);
        }
        */

        interactionRoomButton.gameObject.SetActive(GMImprovementsData.improvementsData.interactionRoom);

        //Show the crimeOffice button only if the fifth dialog (which is the dialog signaling the opening of the crime office) is done
        crimeOfficeButton.gameObject.SetActive(StaticFunctions.IsCrimeOfficeAvailable());

        //Show the personal improvement store only if the personal coach is at level one or above
        personalImprovementStoreButton.gameObject.SetActive(GMGlobalNumericVariables.gnv.PERSONAL_IMPROVEMENT_STORE_LEVEL >=1);

        if (StaticBooleans.wardrobeAvailable)
        {
            buyCostumesButton.gameObject.SetActive(true);
            equipCostumesButton.gameObject.SetActive(true);
        }
        float eventChance = GMGlobalNumericVariables.gnv.EVENT_CHANCE;
        if(GMWardrobeData.currentlyUsedCostume.name == StaticStrings.EVENT_CHANCE_INCREASE_COSTUME_NAME)
        {
            eventChance *= GMGlobalNumericVariables.gnv.EVENT_CHANCE_INCREASE_COSTUME_EFFECT[GMWardrobeData.currentlyUsedCostume.currentLevel];
        }
        //If the player is coming from the work scene, there is a EVENT_CHANCE chance in EVENT_CHANCE_MAX of triggering an event
        if (StaticBooleans.comeFromWork && 
            StaticFunctions.Chance(Mathf.RoundToInt(eventChance), GMGlobalNumericVariables.gnv.EVENT_CHANCE_MAX))
        {
            //Then the event is chosen randomly from the possible events
            List<IEvent> possibleEvents = new List<IEvent>();
            foreach (IEvent e in GMEventData.eventsList)
            {
                if (e.Trigger())
                    possibleEvents.Add(e);
            }

            if (possibleEvents.Count > 0)
            {

                int indexToPlay = Random.Range(0, possibleEvents.Count);
                possibleEvents[indexToPlay].Effect(eventPopup, eventPopupTitle, eventPopupDescription, eventPopupVideoPlayer);
                if (eventPopupVideoPlayer.clip.GetAudioChannelCount(0) > 0)
                {
                    MusicManager.LowerMusicVolume();
                }
            }
            else
            {
                DisplayRecap();
            }
            StaticBooleans.comeFromWork = false;
        }
        else
        {
            DisplayRecap();
            StaticBooleans.comeFromWork = false;
        }
    }

    private void DisplayRecap()
    {
        if (StaticBooleans.displayDayRecap)
        {
            foreach (CrimeService cs in GMCrimeServiceData.unlockedCrimeServices)
            {
                if (cs.isSubscribed)
                    GMClubData.SpendMoney(cs.pricePerDayMoney);
            }

            recapPortrait.sprite = StaticDialogElements.ChooseAssistantPortrait();
            recapPortrait.gameObject.SetActive(true);
            string message = "Here is today's report.";

            Message m = new MessageEnd(message, NPCCode.ASSISTANT);

            //Dummy dialog to fool the compiler... Keep it here
            Dialog d = new Dialog(-100000, null,
                delegate () { return false; },
                delegate () { });

            GameObject instance = null;

            d = new Dialog(GMGlobalNumericVariables.gnv.DAY_RECAP_DIALOG_ID, m,
                delegate ()
                {
                    if (StaticBooleans.displayDayRecap)
                    {
                        StaticBooleans.displayDayRecap = false;
                        GameObject canvas = null;
                        GameObject[] gos = SceneManager.GetActiveScene().GetRootGameObjects();
                        foreach (GameObject go in gos)
                        {
                            if (go.GetComponent<Canvas>() != null)
                            {
                                canvas = go;
                                break;
                            }
                        }

                        if (canvas != null)
                        {
                            instance = Instantiate(dayRecapPrefab, new Vector3(0, 100, 0), Quaternion.identity);
                            instance.transform.SetParent(canvas.transform, false);
                            Text[] texts = instance.GetComponentsInChildren<Text>(true);
                            foreach (Text t in texts)
                            {
                                if (t.CompareTag("RecapIncomeDisplay"))
                                {
                                    if (GMClubData.GetMoneyEarned() == 0)
                                        t.color = Color.black;
                                    t.text = StaticStrings.MONEY_SIGN + " " + GMClubData.GetMoneyEarned();
                                }

                                else if (t.CompareTag("RecapReputationDisplay"))
                                {
                                    float total = (Mathf.Floor(GMClubData.GetReputationEarned() * 10) / 10);
                                    t.text = total.ToString();
                                    if (total == 0)
                                    {
                                        t.color = Color.black;
                                    }
                                }
                                else if (t.CompareTag("RecapInfluenceDisplay"))
                                {
                                    float total = (Mathf.Floor(GMClubData.GetInfluenceEarned() * 10) / 10);
                                    t.text = total.ToString();
                                    if (total == 0)
                                    {
                                        t.color = Color.black;
                                    }
                                }

                                else if (t.CompareTag("RecapExpensesDisplay"))
                                {
                                    t.text = GMClubData.GetMoneySpent().ToString();
                                    if (GMClubData.GetMoneySpent() == 0)
                                    {
                                        t.color = Color.black;
                                    }
                                }
                                else if (t.CompareTag("RecapReputationLostDisplay"))
                                {
                                    float total = (Mathf.Floor(GMClubData.GetReputationSpent() * 10) / 10);
                                    t.text = total.ToString();
                                    if (total == 0)
                                    {
                                        t.color = Color.black;
                                    }
                                }
                                else if (t.CompareTag("RecapInfluenceLostDisplay"))
                                {
                                    float total = (Mathf.Floor(GMClubData.GetInfluenceSpent() * 10) / 10);
                                    t.text = total.ToString();
                                    if (total == 0)
                                    {
                                        t.color = Color.black;
                                    }
                                }

                                else if (t.CompareTag("RecapMoneyTotalDisplay"))
                                {
                                    int total = GMClubData.GetMoneyEarned() - GMClubData.GetMoneySpent();
                                    if (total > 0)
                                    {
                                        t.color = Color.green;
                                    }
                                    else
                                    {
                                        t.color = Color.red;
                                    }
                                    t.text = total.ToString();
                                }
                                else if (t.CompareTag("RecapReputationTotalDisplay"))
                                {
                                    float total = Mathf.Floor((GMClubData.GetReputationEarned() - GMClubData.GetReputationSpent()) * 10) / 10;
                                    if (total > 0)
                                    {
                                        t.color = Color.green;
                                    }
                                    else
                                    {
                                        t.color = Color.red;
                                    }

                                    t.text = total.ToString();
                                }
                                else if (t.CompareTag("RecapInfluenceTotalDisplay"))
                                {
                                    float total = Mathf.Floor((GMClubData.GetInfluenceEarned() - GMClubData.GetInfluenceSpent()) * 10) / 10;
                                    if (total > 0)
                                    {
                                        t.color = Color.green;
                                    }
                                    else
                                    {
                                        t.color = Color.red;
                                    }
                                    t.text = total.ToString();
                                }

                            }
                        }
                        return true;
                    }
                    return false;

                },
                delegate () {
                    RegisterDialogs.dialogs.Remove(d);
                    StaticBooleans.dayRecapOver = true;
                    Destroy(instance);
                    recapPortrait.gameObject.SetActive(false);

                    GMClubData.SetSpentMoneyToZero();
                    GMClubData.SetSpentReputationToZero();
                    GMClubData.SetSpentInfluenceToZero();
                    GMClubData.SetSpentConnectionToZero();

                    GMClubData.SetEarnedMoneyToZero();
                    GMClubData.SetEarnedReputationToZero();
                    GMClubData.SetEarnedInfluenceToZero();
                    GMClubData.SetEarnedConnectionToZero();
                });

            RegisterDialogs.dialogs.Add(d);
        }
    }

    public void OnToEndRecapPopupButtonPress()
    {
        recapPopup.SetActive(false);
    }

    public void OnLoadTextButtonPress()
    {
        GameObject canvas = null;
        GameObject[] gos = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in gos)
        {
            if (go.GetComponent<Canvas>() != null)
            {
                canvas = go;
                break;
            }
        }

        if (!StaticBooleans.tutorialIsOn)
        {
            SceneManager.LoadScene(StaticStrings.LOAD_SCENE);
        }
        else
        {
            if (canvas != null)
            {
                GameObject errorPopupInstance = Instantiate(errorPopupPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                errorPopupInstance.transform.SetParent(canvas.transform, false);
                errorPopupInstance.GetComponentInChildren<Text>().text = "Cannot load during the tutorial.";
            }
        }
    }

    public void OnSaveTextButtonPress()
    {

        GameObject canvas = null;
        GameObject[] gos = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in gos)
        {
            if (go.GetComponent<Canvas>() != null)
            {
                canvas = go;
                break;
            }
        }

        if (!StaticBooleans.tutorialIsOn)
        {
            SceneManager.LoadScene(StaticStrings.SAVE_SCENE);
        }
        else
        {
            GameObject errorPopupInstance = Instantiate(errorPopupPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            errorPopupInstance.transform.SetParent(canvas.transform, false);
            errorPopupInstance.GetComponentInChildren<Text>().text = "Cannot save during the tutorial.";
        }
    }

    public void OnEndEventPress()
    {
        MusicManager.ReturnBaseMusicVolume();
        DisplayRecap();
    }

}

