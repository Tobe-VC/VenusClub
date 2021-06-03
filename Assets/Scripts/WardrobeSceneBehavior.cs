using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CostumesData
{
    public bool interaction_duration = false;
    public bool earnings_increase = false;
    public bool helps_increase = false;
    public bool all_increase = false;
    public bool energy_lost_decrease = false;
    public bool reputation_increase = false;
    public bool event_chance_increase = false;
}

public static class GMWardrobeData
{
    public static List<Costume> ownedCostumes = new List<Costume>();

    public static CostumesData costumesData = new CostumesData();

    public static Costume currentlyUsedCostume = new Costume();

    public static bool IsCostumeOwned(string s)
    {
        foreach(Costume c in ownedCostumes)
        {
            for (int i = 0; i < c.currentLevel + 1; i++)
            {
                if (c.subNames[i].ToLower().Equals(s.ToLower()))
                    return true;
            }
        }
        return false;
    }
}



public class WardrobeSceneBehavior : MonoBehaviour
{
    //The list of directories where the different costumes are
    //public List<string> directories;

    public GameObject contentList;

    public GameObject costumePrefab;

    public GameObject popup;

    public GameObject errorPopup;

    private Image mainPopupImage;

    public GameObject toClubBlocker;

    // Start is called before the first frame update
    void Start()
    {
        CreateList();
    }

    private void CreateList()
    {
        if (GameMasterGlobalData.costumeList.Count == 0)
        {
            foreach (string d in StaticStrings.COSTUMES_DIRECTORIES_NAMES)
            {
                Costume costume = JsonUtility.FromJson<Costume>(Resources.Load<TextAsset>(
                StaticStrings.COSTUMES_FOLDER + d + "/" + StaticStrings.COSTUME_DATA_FILE).text);
                GameMasterGlobalData.costumeList.Add(costume);
            }
        }
        foreach (Costume c in GameMasterGlobalData.costumeList)
        {
            CreateOneItemInList(c);
        }
    }

    private void CreateOneItemInList(Costume costume)
    {
        if (costume.currentLevel < costume.maxLevel - 1 && costume.currentLevel < GMGlobalNumericVariables.gnv.COSTUME_LEVEL)
        {
            GameObject instance = Instantiate(costumePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(contentList.transform, false);
            instance.name = costume.name;

            //Look at all the images in the prefab
            foreach (Image img in instance.GetComponentsInChildren<Image>(true))
            {
                //The untagged one is the main image
                if (img.CompareTag("Untagged"))
                {
                    img.sprite = Resources.Load<Sprite>(StaticStrings.COSTUMES_FOLDER +
                        costume.name + "/" + costume.imagesPaths[costume.currentLevel + 1]);
                }
                //The one tagged with "BuyableImage" is the green overlay that indicates that this policy is buyable
                else if (img.CompareTag("BuyableImage"))
                {
                    if (costume.currentLevel < costume.maxLevel - 1)
                    {
                            if (GMClubData.money >= costume.pricesMoney[costume.currentLevel + 1]
                                && GMClubData.GetConnection() >= costume.pricesConnection[costume.currentLevel + 1]
                                && GMClubData.GetInfluence() >= costume.pricesInfluence[costume.currentLevel + 1])
                            {
                                img.gameObject.SetActive(true);
                            }
                            else
                            {
                                img.gameObject.SetActive(false);
                            }
                    }
                    else
                    {
                        img.gameObject.SetActive(false);
                    }
                }

                Button b = instance.GetComponentInChildren<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate () { OnImageClick(costume.name, costume); });

                Text[] texts = instance.GetComponentsInChildren<Text>();
                foreach (Text t in texts)
                {
                    if (t.CompareTag("ImprovementDescription"))
                    {
                        t.text = costume.description;
                    }
                    if (t.CompareTag("ImprovementName"))
                    {
                        t.text = costume.subNames[costume.currentLevel + 1];
                    }
                    if (t.CompareTag("ImprovementPrice"))
                    {
                        t.text = costume.pricesMoney[costume.currentLevel + 1] + StaticStrings.MONEY_SIGN;
                    }
                }
            }
        }
    }

    private void OnImageClick(string costumeName, Costume costume)
    {
        popup.SetActive(true);

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.COSTUMES_FOLDER + costume.name + "/" + costume.imagesPaths[costume.currentLevel + 1]);
                mainPopupImage = img;
                break;
            }
        }
        Text[] texts = popup.GetComponentsInChildren<Text>();
        foreach(Text t in texts)
        {
            if (t.CompareTag("WardrobeCurrentLevelText"))
            {
                if(costume.currentLevel > -1)
                {
                    t.text = "Current level effects:\n" + costume.subDetailedDescriptions[costume.currentLevel];
                }
                else
                {
                    t.text = "";
                }
            }
            else if(t.CompareTag("WardrobeNextLevelText"))
            {
                //In this case, this the text for the effect at next level
                if(costume.currentLevel < costume.maxLevel - 1)
                    t.text = "Next level effects:\n" + costume.subDetailedDescriptions[costume.currentLevel + 1] 
                        + "\nCosts " + costume.pricesMoney[costume.currentLevel + 1] + StaticStrings.MONEY_SIGN + ".";
                else
                {
                    t.text = "";
                }

            }
        }

        if (CostumeOwned(costume))
        {
            //If the player owns the costume
            //Create the confirm button in the popup
            foreach (Button b in popup.GetComponentsInChildren<Button>(true))
            {
                if (b.CompareTag("PopupConfirm"))
                {
                    if (costume.currentLevel < costume.maxLevel - 1 )
                    {
                        b.gameObject.SetActive(true);
                        b.GetComponentInChildren<Text>().text = StaticStrings.COSTUME_UPGRADE_CONFIRM_BUTTON_TEXT;
                        b.onClick.RemoveAllListeners();
                        b.onClick.AddListener(delegate () { OnUpdateClick(costumeName, costume); });
                    }
                    else
                    {
                        b.gameObject.SetActive(false);
                    }
                }
            }
        }

        else
        {
            //Create the confirm button in the popup
            foreach (Button b in popup.GetComponentsInChildren<Button>(true))
            {
                if (b.CompareTag("PopupConfirm"))
                {
                    b.gameObject.SetActive(true);
                    b.GetComponentInChildren<Text>().text = StaticStrings.COSTUME_BUY_CONFIRM_BUTTON_TEXT;
                    b.onClick.RemoveAllListeners();
                    b.onClick.AddListener(delegate () { OnClick(costumeName, costume); });
                }
                if (b.CompareTag("UpgradeCostumeButton"))
                {
                    b.gameObject.SetActive(false);
                }
            }

        }
    }

    private bool CostumeOwned(Costume costume)
    {
        foreach (Costume c in GMWardrobeData.ownedCostumes)
        {
            if (costume.name == c.name)
            {
                return true;
            }
        }
        return false;
    }

    private void SubButtonsCommonBehavior()
    {
        popup.SetActive(false);
        mainPopupImage.gameObject.SetActive(true);
        SceneManager.LoadScene(StaticStrings.WARDROBE_SCENE);
    }

    /*
    //The behavior when clicking on the confirmation of an already owned costume
    private void OnAlreadyOwnedClick(string costumeName, Costume costume)
    {
        GMWardrobeData.currentlyUsedCostume = costume;
        SubButtonsCommonBehavior();
    }
    */

    //The behavior when clicking on the confirmation of an already owned costume
    private void OnUpdateClick(string costumeName, Costume costume)
    {
        if (costume.pricesMoney[costume.currentLevel + 1] <= GMClubData.money &&
            costume.pricesInfluence[costume.currentLevel + 1] <= GMClubData.GetInfluence() &&
            costume.pricesConnection[costume.currentLevel + 1] <= GMClubData.GetConnection())
        {
            GMClubData.SpendMoney(costume.pricesMoney[costume.currentLevel + 1]);
            GMClubData.SpendInfluence(costume.pricesInfluence[costume.currentLevel + 1]);
            GMClubData.SpendConnection(costume.pricesConnection[costume.currentLevel + 1]);
            costume.currentLevel++;
            SubButtonsCommonBehavior();
        }
    }

    private void OnClick(string costumeName, Costume costume)
    {
        // If the player doesn't have enough of one ressource, he can't buy this costume
        //The prices of this costume is registered in the JSON
        if (GMClubData.money >= costume.pricesMoney[0]
            && GMClubData.GetConnection() >= costume.pricesConnection[0]
            && GMClubData.GetInfluence() >= costume.pricesInfluence[0])
        {

            GMWardrobeData.ownedCostumes.Add(costume);
            GMClubData.SpendMoney(costume.pricesMoney[0]);
            GMClubData.SpendConnection(costume.pricesConnection[0]);
            GMClubData.SpendInfluence(costume.pricesInfluence[0]);

            costume.currentLevel++;

            if (costumeName == StaticStrings.INTERACTION_DURATION_INCREASE_COSTUME_NAME)
            {
                GMWardrobeData.costumesData.interaction_duration = true;
            }
            else if (costumeName == StaticStrings.EARNINGS_INCREASE_COSTUME_NAME)
            {
                GMWardrobeData.costumesData.earnings_increase = true;
            }
            else if (costumeName == StaticStrings.HELPS_INCREASE_COSTUME_NAME)
            {
                GMWardrobeData.costumesData.helps_increase = true;
            }
            else if (costumeName == StaticStrings.ALL_INCREASE_COSTUME_NAME)
            {
                GMWardrobeData.costumesData.all_increase = true;
            }
            else if (costumeName == StaticStrings.ENERGY_LOST_DECREASE_COSTUME_NAME)
            {
                GMWardrobeData.costumesData.energy_lost_decrease = true;
            }
            else if (costumeName == StaticStrings.REPUTATION_INCREASE_COSTUME_NAME)
            {
                GMWardrobeData.costumesData.reputation_increase = true;
            }

            SubButtonsCommonBehavior();
        }
    }

    //Cancels the popup
    public void OnCancelClick()
    {
        SubButtonsCommonBehavior();
    }

}
