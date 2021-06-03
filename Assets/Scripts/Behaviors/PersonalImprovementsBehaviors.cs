using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//The behaviors for the improvements scene
//Remeber to add the directory in the list in the inspector when adding an improvement
public class PersonalImprovementsBehaviors : MonoBehaviour
{
    public List<PersonalImprovement> items = new List<PersonalImprovement>();

    public GameObject contentList;

    public GameObject itemsPrefab;

    public GameObject popup;

    public GameObject errorPopup;

    private Image mainPopupImage;

    private bool improvementsAvailable = false;

    //Contains the association of the instanciation in gameobject form to the improvement in data form (Key,Value)
    private Hashtable itemToPrefabTable = new Hashtable();

    //Creates one item in the list of improvements
    private void CreateOneItemInList(PersonalImprovement item)
    {
        if (item.currentLevel < item.maxLevel)
        {
            if (item.currentLevel < GMGlobalNumericVariables.gnv.PERSONAL_IMPROVEMENT_STORE_LEVEL)
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
                        img.sprite = Resources.Load<Sprite>(StaticStrings.PERSONAL_IMPROVEMENTS_FOLDER +
                                item.name + "/" + item.imagesPaths[item.currentLevel]);
                    }
                    //The one tagged with "BuyableImage" is the green overlay that indicates that this item is buyable
                    else if (img.CompareTag("BuyableImage"))
                    {
                        if (StaticAssistantData.data.assistantPoints >= item.priceAssistantPoints[item.currentLevel]
                            && GMClubData.money >= item.priceMoney[item.currentLevel])
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

                        if (item.priceMoney[item.currentLevel] > 0)
                        {
                            t.text = item.priceMoney[item.currentLevel] + StaticStrings.MONEY_SIGN;

                            if (item.priceAssistantPoints[item.currentLevel] > 1)
                            {
                                t.text += " and " + item.priceAssistantPoints[item.currentLevel] + " points.";
                            }
                            else if (item.priceAssistantPoints[item.currentLevel] == 1)
                            {
                                t.text += " and " + item.priceAssistantPoints[item.currentLevel] + " point.";
                            }
                        }
                        else if (item.priceAssistantPoints[item.currentLevel] > 1)
                        {
                            t.text = item.priceAssistantPoints[item.currentLevel] + " points.";
                        }
                        else if (item.priceAssistantPoints[item.currentLevel] == 1)
                        {
                            t.text = item.priceAssistantPoints[item.currentLevel] + " point.";
                        }

                        else
                        {
                            t.text = "Free";
                        }
                    }
                }
            }
            else
            {
                improvementsAvailable = true;
            }
        }
    }

    //Creates the list of improvements
    private void CreateList()
    {
        if (RegisterPersonalImprovements.personalImprovements == null)
        {
            foreach (string d in StaticStrings.PERSONAL_IMPROVEMENTS_DIRECTORIES_NAMES)
            {
                PersonalImprovement imp = JsonUtility.FromJson<PersonalImprovement>(
                    Resources.Load<TextAsset>(StaticStrings.PERSONAL_IMPROVEMENTS_FOLDER + d + "/" +
                    StaticStrings.PERSONAL_IMPROVEMENT_DATA_FILE).text);
                if (imp.showInList)
                {
                    items.Add(imp);

                    CreateOneItemInList(imp);
                }
            }
            RegisterPersonalImprovements.personalImprovements = items;
        }
        else
        {
            foreach (PersonalImprovement imp in RegisterPersonalImprovements.personalImprovements)
            {
                CreateOneItemInList(imp);
            }
        }
        if (contentList.transform.childCount <= 0)
        {

            contentList.gameObject.SetActive(false);
            errorPopup.SetActive(true);
            if (improvementsAvailable)
            {
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.ALL_CURRENTLY_PERSONAL_IMPROVEMENTS_BOUGHT;
            }
            else
            {
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.ALL_PERSONAL_IMPROVEMENTS_BOUGHT;
            }
            Button b = errorPopup.GetComponentInChildren<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate () { SceneManager.LoadScene(StaticStrings.CLUB_SCENE); });
        }
    }

    private void Awake()
    {
        CreateList();
    }

    /*
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
    */

    //The OnClick action for each image in the items menu
    private void OnButtonClick(string improvementName, PersonalImprovement item)
    {
        popup.SetActive(true);

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.PERSONAL_IMPROVEMENTS_FOLDER +
                        item.name + "/" + item.imagesPaths[item.currentLevel]);

                mainPopupImage = img;
                break;
            }
        }
        popup.GetComponentInChildren<Text>().text = item.detailedDescription[item.currentLevel];

        if (item.priceMoney[item.currentLevel] > 0)
        {
            popup.GetComponentInChildren<Text>().text += "\nCosts " + item.priceMoney[item.currentLevel] + StaticStrings.MONEY_SIGN;

            if (item.priceAssistantPoints[item.currentLevel] > 1)
            {
                popup.GetComponentInChildren<Text>().text += " and " + item.priceAssistantPoints[item.currentLevel] + " points.";
            }
            else if (item.priceAssistantPoints[item.currentLevel] == 1)
            {
                popup.GetComponentInChildren<Text>().text += " and " + item.priceAssistantPoints[item.currentLevel] + " point.";
            }
        }
        else if (item.priceAssistantPoints[item.currentLevel] > 1)
        {
            popup.GetComponentInChildren<Text>().text += "\nCosts" + item.priceAssistantPoints[item.currentLevel] + " points.";
        }
        else if (item.priceAssistantPoints[item.currentLevel] == 1)
        {
            popup.GetComponentInChildren<Text>().text += "\nCosts" + item.priceAssistantPoints[item.currentLevel] + " point.";
        }

        else
        {
            popup.GetComponentInChildren<Text>().text = "Free";
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
    private void OnClick(string improvementName, PersonalImprovement item)
    {
        //If the player doesn't have enough of one ressource, he can't buy this improvement
        //The prices of this improvement is registered in the JSON
        if (StaticAssistantData.data.assistantPoints >= item.priceAssistantPoints[item.currentLevel] &&
            GMClubData.money >= item.priceMoney[item.currentLevel])
        {
            StaticFunctions.BuyPersonalImprovement(item);
            GMClubData.SpendAssistantPoints(item.priceAssistantPoints[item.currentLevel]);
            GMClubData.SpendMoney(item.priceMoney[item.currentLevel]);

            if (item.currentLevel < item.maxLevel)
            {
                item.currentLevel++;
            }

            popup.SetActive(false);
            mainPopupImage.gameObject.SetActive(true);
            SceneManager.LoadScene(StaticStrings.PERSONAL_IMPROVEMENTS_SCENE);
        }
        else
        {
            //If the player doesn't have enough of a ressource, the corresponding error message is displayed
            if (StaticAssistantData.data.assistantPoints < item.priceAssistantPoints[item.currentLevel])
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_ASSISTANT_POINTS_ERROR_MESSAGE;
            }
            //If the player doesn't have enough of a ressource, the corresponding error message is displayed
            if (GMClubData.money < item.priceMoney[item.currentLevel])
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_MONEY_PERSONAL_IMPROVEMENT_ERROR_MESSAGE;
            }
        }
    }

    //Cancels the popup
    public void OnCancelClick()
    {
        popup.SetActive(false);
        mainPopupImage.gameObject.SetActive(true);
    }

}
