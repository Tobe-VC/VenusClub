using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GMCrimeServiceData
{
    public static List<CrimeService> crimeServices = new List<CrimeService>();

    public static List<CrimeService> unlockedCrimeServices = new List<CrimeService>();

    public static bool IsServiceUnlocked(string s)
    {
        foreach(CrimeService cs in unlockedCrimeServices)
        {
            if (cs.name.ToLower().Equals(s.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsServiceSubscribed(string s)
    {
        foreach (CrimeService cs in unlockedCrimeServices)
        {
            if (cs.name.ToLower().Equals(s.ToLower()))
            {
                return cs.isSubscribed;
            }
        }
        return false;
    }
}

public class CrimeServiceBehavior : MonoBehaviour
{
    public GameObject contentList;

    public GameObject crimeServicePrefab;

    public GameObject popup;

    public GameObject errorPopup;

    public GameObject girlRecruitedPopup;

    private Image mainPopupImage;

    public GameObject toClubBlocker;

    public Text switchButtonText;

    public static GirlClass girlJustRecruited = null;



    /// <summary>
    /// True if the list to be displayed is the locked one
    /// </summary>
    public static bool displayLockedList = true;

    // Start is called before the first frame update
    void Start()
    {
        switchButtonText.text = ChooseSwitchButtonText();
        if (displayLockedList)
        {
            CreateLockedList();
        }
        else
        {
            CreateUnlockedList();
        }
    }

    private void CreateLockedList()
    {
        foreach (CrimeService cs in GMCrimeServiceData.crimeServices)
        {
            if(!cs.isUnlocked)
                CreateOneItemInList(cs);
        }
    }

    private void CreateUnlockedList()
    {
        foreach (CrimeService cs in GMCrimeServiceData.crimeServices)
        {
            if (cs.isUnlocked)
                CreateOneItemInList(cs);
        }
    }

    private void CreateOneItemInList(CrimeService cs)
    {
        GameObject instance = Instantiate(crimeServicePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(contentList.transform, false);
        instance.name = cs.name;

        //Look at all the images in the prefab
        foreach (Image img in instance.GetComponentsInChildren<Image>(true))
        {
            //The untagged one is the main image
            if (img.CompareTag("Untagged"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.CRIME_SERVICES_FOLDER +
                    cs.folder + cs.imagePath);
            }
            //The one tagged with "BuyableImage" is the green overlay that indicates that this service is unlockable
            else if (img.CompareTag("BuyableImage"))
            {
                if (displayLockedList)
                {
                    if (GMClubData.GetInfluence() >= cs.priceToAccess)
                    {
                        img.color = Color.green;
                    }
                    else
                    {
                        img.color = Color.clear;
                    }
                }
                else
                {
                    if(cs.isSubscribed)
                    {
                        img.color = Color.green;
                    }
                    else
                    {
                        img.color = Color.clear;
                    }
                }
            }

            Button b = instance.GetComponentInChildren<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate () { OnImageClick(cs); });

            Text[] texts = instance.GetComponentsInChildren<Text>();
            foreach (Text t in texts)
            {
                if (t.CompareTag("ImprovementDescription"))
                {
                    t.text = cs.description;
                }
                if (t.CompareTag("ImprovementName"))
                {
                    t.text = cs.name;
                }
                if (t.CompareTag("ImprovementPrice"))
                {
                    if (displayLockedList)
                    {
                        t.text = cs.priceToAccess + " influence";
                    }
                    else
                    {
                        t.text = cs.pricePerDayMoney + StaticStrings.MONEY_SIGN +  " per day";
                    }
                }
            }
        }

    }

    private void OnImageClick(CrimeService cs)
    {
        popup.SetActive(true);

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.CRIME_SERVICES_FOLDER + cs.folder + cs.imagePath);
                mainPopupImage = img;
                break;
            }
        }
        Text[] texts = popup.GetComponentsInChildren<Text>();
        foreach (Text t in texts)
        {
            if (t.CompareTag("PopupTitle"))
            {
                t.text = cs.name;
            }
            else if (t.CompareTag("PopupDescription"))
            {
                t.text = cs.detailedDescription;
                if (displayLockedList)
                {
                    t.text += "\n\nCosts " + cs.priceToAccess + " influence to unlock.";
                }
                else
                {
                    t.text += "\n\nCosts " + cs.pricePerDayMoney + StaticStrings.MONEY_SIGN + " per day of subscription.";
                }
            }
        }
        Button[] buttons = popup.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            if (b.CompareTag("PopupConfirm"))
            {
                b.onClick.RemoveAllListeners();
                if (!cs.isUnlocked)
                {
                    if (cs.id == GMGlobalNumericVariables.gnv.CRIME_SERVICE_LOTTERY_ID)
                    {
                        b.GetComponentInChildren<Text>().text = "Play the lottery!";
                        b.onClick.AddListener(delegate () { OnPlayLotteryClick(cs); });
                    }
                    else
                    {
                        b.GetComponentInChildren<Text>().text = "Unlock";
                        b.onClick.AddListener(delegate () { OnUnlockClick(cs); });
                    }
                }
                else 
                {
                    if (!cs.isSubscribed)
                    {
                        b.GetComponentInChildren<Text>().text = "Subscribe";
                        b.onClick.AddListener(delegate () { OnSubscribeClick(cs); });
                    }
                    else
                    {
                        b.GetComponentInChildren<Text>().text = "Unsubscribe";
                        b.onClick.AddListener(delegate () { OnUnsubscribeClick(cs); });
                    }
                }
            }
        }
    }

    //Cancels the popup
    public void OnCancelClick()
    {
        popup.SetActive(false);
        mainPopupImage.gameObject.SetActive(true);
    }

    public void OnUnlockClick(CrimeService cs)
    {
        if (GMClubData.GetInfluence() >= cs.priceToAccess)
        {
            cs.Unlock();
            GMCrimeServiceData.unlockedCrimeServices.Add(cs);
            GMClubData.SpendInfluence(cs.priceToAccess);
            SceneManager.LoadScene(StaticStrings.CRIME_SERVICES_SCENE);
        }
        else
        {
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_INFLUENCE_TO_UNLOCK_SERVICE_ERROR_POPUP_MESSAGE;
        }
        
    }

    public void OnSubscribeClick(CrimeService cs)
    {
        cs.Subscribe();
        SceneManager.LoadScene(StaticStrings.CRIME_SERVICES_SCENE);
    }

    public void OnUnsubscribeClick(CrimeService cs)
    {
        cs.Unsubscribe();
        SceneManager.LoadScene(StaticStrings.CRIME_SERVICES_SCENE);
    }

    public void OnPlayLotteryClick(CrimeService cs)
    {
        if (GMClubData.GetInfluence() >= cs.priceToAccess)
        {
            if (StaticFunctions.GirlsStillInLotteryPool())
            {
                cs.Unlock();
                GMClubData.SpendInfluence(cs.priceToAccess);
                girlRecruitedPopup.SetActive(true);
                Text[] texts = girlRecruitedPopup.GetComponentsInChildren<Text>();
                foreach(Text t in texts)
                {
                     if (t.CompareTag("PopupDescription"))
                    {
                        t.text = "You just recruited " + girlJustRecruited.name + "! \nYou can now give her work.";
                    }
                }
                Image[] images = girlRecruitedPopup.GetComponentsInChildren<Image>();
                foreach(Image img in images)
                {
                    if (img.CompareTag("PopupMainImage"))
                    {
                        if (girlJustRecruited.external)
                        {
                            img.sprite = girlJustRecruited.GetPortrait();
                        }
                        else
                        {
                            img.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + girlJustRecruited.folderName + "/" + 
                                StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
                        }
                    }
                }

            }
            else
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NO_MORE_GIRLS_IN_LOTTERY_POOL_ERROR_POPUP_MESSAGE;
            }
        }
        else
        {
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_INFLUENCE_TO_PLAY_LOTTERY_ERROR_POPUP_MESSAGE;
        }
    }

    public void SwitchListButtonPress()
    {
        displayLockedList = !displayLockedList;
        //switchButtonText.text = ChooseSwitchButtonText();
        SceneManager.LoadScene(StaticStrings.CRIME_SERVICES_SCENE);
    }

    private string ChooseSwitchButtonText()
    {
        if (displayLockedList)
            return StaticStrings.CRIME_SERVICE_SCENE_SWITCH_TO_UNLOCKED_LIST;
        else
            return StaticStrings.CRIME_SERVICE_SCENE_SWITCH_TO_LOCKED_LIST;
    }

}
