using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//The behaviors for the improvements scene
//Remeber to add the directory in the list in the inspector when adding an improvement
public class AssistantStoreBehaviors : MonoBehaviour
{
    public List<AssistantStoreItem> items = new List<AssistantStoreItem>();

    public GameObject contentList;

    public GameObject itemsPrefab;

    public GameObject popup;

    public GameObject errorPopup;

    public GameObject secondaryImages;

    private Image mainPopupImage;

    public GameObject toClubBlocker;

    private GirlClass assistant;

    //Contains the association of the instanciation in gameobject form to the improvement in data form (Key,Value)
    private Hashtable itemToPrefabTable = new Hashtable();

    //Creates one item in the list of improvements
    private void CreateOneItemInList(AssistantStoreItem item)
    {
        if (item.currentLevel < item.maxLevel &&
            (
                (item.thirdLateNightMeetingRequired &&
            StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_FIRST_CAFE_DATE_DIALOG_ID).seen)
                || !item.thirdLateNightMeetingRequired
                )
            && ((item.name == StaticStrings.ASSISTANT_UNLOCK_POSING_ITEM &&
            StaticDialogElements.dialogData.launchAssistantUnlockToplessDanceDialog
                    && ((item.currentLevel == 0 && GMPoliciesData.IsPolicyOwned(StaticStrings.POSE_NAKED_POLICY_NAME) 
                        && assistant.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_POSE)
                        || (item.currentLevel == 1 && GMPoliciesData.IsPolicyOwned(StaticStrings.SOLO_FINGERING_POLICY_NAME)
                        && assistant.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_SOLO_FINGERING)
                        || (item.currentLevel == 2 && GMPoliciesData.IsPolicyOwned(StaticStrings.TOYS_MASTURBATING_POLICY_NAME)
                        && assistant.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_TOYS_MASTURBATION)
                       )
                )
                        //If the item is the unlock posing for the assistant, check that the topless dance has been unlocked 
                        //(by checking if the corresponding dialog has been seen)
                        //and check that the necessary policy has been unlocked 
                        //(e.g. the pose naked policy for the pose naked performance for Nicole)
                || item.name != StaticStrings.ASSISTANT_UNLOCK_POSING_ITEM
            )

                        && ((item.name == StaticStrings.ASSISTANT_UNLOCK_FOREPLAY_ITEM &&
            StaticDialogElements.dialogData.launchAssistantUnlockToysMasturbationDialog
                    && ((item.currentLevel == 0 && GMPoliciesData.IsPolicyOwned(StaticStrings.HANDJOB_POLICY_NAME)
                        && assistant.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_FOREPLAY)
                        || (item.currentLevel == 1 && GMPoliciesData.IsPolicyOwned(StaticStrings.FOOTJOB_POLICY_NAME)
                        && assistant.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_FOOTJOB)
                        || (item.currentLevel == 2 && GMPoliciesData.IsPolicyOwned(StaticStrings.TITSJOB_POLICY_NAME)
                        && assistant.GetOpenness() >= GMGlobalNumericVariables.gnv.MIN_OPENNESS_TITSJOB)
                       )
                )
                //If the item is the unlock foreplay for the assistant, check that the toys masturbation has been unlocked 
                //(by checking if the corresponding dialog has been seen)
                //and check that the necessary policy has been unlocked 
                //(e.g. the handjob policy for the handjob performance for Nicole)
                || item.name != StaticStrings.ASSISTANT_UNLOCK_FOREPLAY_ITEM
            )
            )
        {
                GameObject instance = Instantiate(itemsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                instance.transform.SetParent(contentList.transform, false);
                instance.name = item.name;

                itemToPrefabTable.Add(instance.transform, item);

                //Look at all the images in the prefab
                foreach (Image img in instance.GetComponentsInChildren<Image>(true))
                {
                    //The untagged one is the main image
                    if (img.CompareTag("Untagged"))
                    {
                        img.sprite = Resources.Load<Sprite>(StaticStrings.ASSISTANT_RESSOURCES_FOLDER + 
                            StaticStrings.ASSISTANT_IMAGES_FOLDER +
                            item.directoryName + "/" + item.imagesPaths[item.currentLevel]);
                    }
                    //The one tagged with "BuyableImage" is the green overlay that indicates that this item is buyable
                    else if (img.CompareTag("BuyableImage"))
                    {
                        if (StaticAssistantData.data.assistantPoints >= item.priceAssistantPoints[item.currentLevel])
                        {
                            img.gameObject.SetActive(true);
                        }
                        else
                        {
                            img.gameObject.SetActive(false);
                        }
                    }
                }

                Button b = instance.GetComponentInChildren<Button>();
                b.onClick.AddListener(delegate () { OnButtonClick(item.name, item); });

                Text[] texts = instance.GetComponentsInChildren<Text>();
                foreach (Text t in texts)
                {
                    if (t.CompareTag("ImprovementDescription"))
                    {
                        t.text = item.description;

                    }
                    if (t.CompareTag("ImprovementName"))
                    {
                        if (item.subNames.Count > item.currentLevel)
                        {
                            t.text = item.subNames[item.currentLevel];
                        }
                        else
                            t.text = item.name;
                    }
                    if (t.CompareTag("ImprovementPrice"))
                    {
                        if (item.priceAssistantPoints[item.currentLevel] > 1)
                        {
                            t.text = item.priceAssistantPoints[item.currentLevel] + " points";
                        }
                        else if (item.priceAssistantPoints[item.currentLevel] == 1)
                        {
                        t.text = item.priceAssistantPoints[item.currentLevel] + " point";
                        }
                        else
                        {
                            t.text = "Free";
                        }
                    }
                }
        }
    }

    //Creates the list of improvements
    private void CreateList()
    {
        if (RegisterAssistantStoreItems.assistantItemsList == null)
        {
            foreach (string d in StaticStrings.ASSISTANT_ITEMS_DIRECTORIES_NAMES)
            {
                AssistantStoreItem imp = JsonUtility.FromJson<AssistantStoreItem>(
                    Resources.Load<TextAsset>(StaticStrings.ASSISTANT_RESSOURCES_FOLDER +
                 StaticStrings.ASSISTANT_STORE_FOLDER + d + "/" + StaticStrings.ASSISTANT_STORE_ITEMS_DATA_FILE).text);
                if (imp.showInList)
                {
                    items.Add(imp);

                    CreateOneItemInList(imp);
                }
            }
            RegisterAssistantStoreItems.assistantItemsList = items;
        }
        else
        {
            foreach (AssistantStoreItem imp in RegisterAssistantStoreItems.assistantItemsList)
            {
                CreateOneItemInList(imp);
            }
        }
        if (contentList.transform.childCount <= 0)
        {
            contentList.gameObject.SetActive(false);
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>().text = StaticStrings.ALL_ASSISTANT_STORE_ITEMS_BOUGHT;
            Button b = errorPopup.GetComponentInChildren<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate () { SceneManager.LoadScene(StaticStrings.OFFICE_SCENE); });
        }
    }

    private void Awake()
    {
        assistant = GMRecruitmentData.recruitedGirlsList.Find(
        delegate (GirlClass girl) { return girl.name.Equals(StaticStrings.ASSISTANT_FULL_NAME); });
        CreateList();
    }

    private void RefreshOneItem(Improvement imp)
    {
        if (imp.currentLevel < imp.maxLevel)
        {
            foreach (Transform t in contentList.transform)
            {
                if (t.name == imp.name)
                {
                    //Look at all the images in the prefab
                    foreach (Image img in t.GetComponentsInChildren<Image>(true))
                    {
                        //The untagged one is the main image
                        if (img.CompareTag("Untagged"))
                        {
                            img.sprite = Resources.Load<Sprite>(StaticStrings.IMPROVEMENTS_FOLDER +
                                imp.name + "/" + imp.imagesPaths[imp.currentLevel]);
                        }
                        //The one tagged with "BuyableImage" is the green overlay that indicates that this improvement is buyable
                        else if (img.CompareTag("BuyableImage"))
                        {
                            if (GMClubData.money >= imp.priceMoney[imp.currentLevel]
                                && GMClubData.GetConnection() >= imp.priceConnection[imp.currentLevel]
                                && GMClubData.GetInfluence() >= imp.priceInfluence[imp.currentLevel])
                            {
                                img.gameObject.SetActive(true);
                            }
                            else
                            {
                                img.gameObject.SetActive(false);
                            }
                        }
                    }
                    Text[] texts = t.GetComponentsInChildren<Text>();
                    foreach (Text text in texts)
                    {
                        if (text.CompareTag("ImprovementName"))
                        {
                            if (imp.subNames.Count > imp.currentLevel)
                            {
                                text.text = imp.subNames[imp.currentLevel];
                            }
                            else
                                text.text = imp.name;
                        }
                    }
                    break;
                }
            }
        }
        else
        {
            foreach (Transform t in contentList.transform)
            {
                if (t.name == imp.name)
                {
                    itemToPrefabTable.Remove(t);
                    Destroy(t.gameObject);
                }
            }
        }
    }

    private void EmptyList()
    {
        foreach (Transform child in contentList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //The OnClick action for each image in the items menu
    private void OnButtonClick(string improvementName, AssistantStoreItem item)
    {
        popup.SetActive(true);

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.ASSISTANT_RESSOURCES_FOLDER + 
                    StaticStrings.ASSISTANT_IMAGES_FOLDER +
                    item.directoryName + "/" + item.imagesPaths[item.currentLevel]);
                mainPopupImage = img;
                break;
            }
        }
        popup.GetComponentInChildren<Text>().text = item.detailedDescription[item.currentLevel];
        if (item.priceAssistantPoints[item.currentLevel] > 1)
        {
            popup.GetComponentInChildren<Text>().text += "\nCosts " + item.priceAssistantPoints[item.currentLevel] + " points.";
        }
        else if (item.priceAssistantPoints[item.currentLevel] == 1)
        {
            popup.GetComponentInChildren<Text>().text += "\nCosts " + item.priceAssistantPoints[item.currentLevel] + " point.";
        }
        else
        {
            popup.GetComponentInChildren<Text>().text += "\nFree.";
        }

        //Create the confirm button in the popup
        foreach (Button b in popup.GetComponentsInChildren<Button>())
        {
            if (b.CompareTag("PopupConfirm"))
            {
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate () { OnClick(improvementName, item); });
                break;
            }
        }
    }

    //The OnClick action for the confirm button of the popup
    private void OnClick(string improvementName, AssistantStoreItem item)
    {
        //If the player doesn't have enough of one ressource, he can't buy this improvement
        //The prices of this improvement is registered in the JSON
        if (StaticAssistantData.data.assistantPoints >= item.priceAssistantPoints[item.currentLevel])
        {
                //If the level of the item is 0 before buying it, add it to the bought assistant items list
                if (item.currentLevel == 0)
                {
                    StaticAssistantData.boughtStoreItems.Add(item);
                }

                StaticFunctions.BuyAssistantStoreItem(item);
               
                GMClubData.SpendAssistantPoints(item.priceAssistantPoints[item.currentLevel]);

                if (item.currentLevel < item.maxLevel)
                {
                    item.currentLevel++;
                }

            secondaryImages.SetActive(false);
            popup.SetActive(false);
            mainPopupImage.gameObject.SetActive(true);
            SceneManager.LoadScene(StaticStrings.ASSISTANT_STORE_SCENE);
        }
        else
        {
            //If the player doesn't have enough of a ressource, the corresponding error message is displayed
            if (StaticAssistantData.data.assistantPoints < item.priceAssistantPoints[item.currentLevel])
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_ASSISTANT_POINTS_ERROR_MESSAGE;
            }
        }
    }

    //Cancels the popup
    public void OnCancelClick()
    {
        popup.SetActive(false);
        secondaryImages.SetActive(false);
        mainPopupImage.gameObject.SetActive(true);
    }

    //Refreshes the green image that indicates the ability to buy an item for each available item
    /*private void RefreshList()
    {
        foreach (Transform t in contentList.GetComponentInChildren<Transform>())
        {
            if (t.CompareTag("ImprovementPrefab"))
            {
                //Look at all the images in the prefab
                foreach (Image img in t.GetComponentsInChildren<Image>(true))
                {
                    if (img.CompareTag("BuyableImage") && improvementToPrefabTable.ContainsKey(t))
                    {
                        //Extract the improvement from the gameobject (stored as a transform)
                        Improvement imp = improvementToPrefabTable[t] as Improvement;

                        if (imp != null
                            && GMClubData.money >= imp.priceMoney[imp.currentLevel]
                            && GMClubData.GetConnection() >= imp.priceConnection[imp.currentLevel]
                            && GMClubData.GetInfluence() >= imp.priceInfluence[imp.currentLevel])
                        {
                            img.gameObject.SetActive(true);
                        }
                        else
                        {
                            img.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
    */
}
