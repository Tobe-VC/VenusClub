using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//The behaviors for the improvements scene
//Remeber to add the directory in the list in the inspector when adding an improvement
public class ImprovementsBehaviors : MonoBehaviour
{
    public List<Improvement> improvements = new List<Improvement>();

    public GameObject contentList;

    public GameObject improvementPrefab;

    public GameObject popup;

    public GameObject errorPopup;

    public GameObject secondaryImages;

    private Image mainPopupImage;

    public GameObject toClubBlocker;

    //Contains the association of the instanciation in gameobject form to the improvement in data form (Key,Value)
    private Hashtable improvementToPrefabTable = new Hashtable();

    //Creates one item in the list of improvements
    private void CreateOneItemInList(Improvement imp)
    {
        if (imp.currentLevel < imp.maxLevel 
            && (imp.currentLevel < GMGlobalNumericVariables.gnv.CLUB_LEVEL || imp.name == StaticStrings.UPGRADE_CLUB_IMPROVEMENT_NAME)
            && (!imp.name.Equals(StaticStrings.PERSONAL_STUDY_IMPROVEMENT_NAME) || //If the improvement is the study, then the office is required
            GMImprovementsData.improvementsData.interactionRoom)
            && (!imp.name.Equals(StaticStrings.PERSONAL_COACH_IMPROVEMENT_NAME) || //If the improvement is the personal coach, show it only if the first date has been done at least once
            StaticFunctions.FindDialogWithID(GMGlobalNumericVariables.gnv.ASSISTANT_FIRST_CAFE_DATE_DIALOG_ID).seen)
         )   
        {
            //Show the upgrades only if they are available under the level of the club, or if it is the upgrading improvement itself
            
            if (imp.name != StaticStrings.MODULAR_BOOTHS_IMPROVEMENT_NAME 
                || (imp.name == StaticStrings.MODULAR_BOOTHS_IMPROVEMENT_NAME && GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS >= imp.currentLevel + 1))
            {
                GameObject instance = Instantiate(improvementPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                instance.transform.SetParent(contentList.transform, false);
                instance.name = imp.name;

                improvementToPrefabTable.Add(instance.transform, imp);

                //Look at all the images in the prefab
                foreach (Image img in instance.GetComponentsInChildren<Image>(true))
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

                Button b = instance.GetComponentInChildren<Button>();
                b.onClick.AddListener(delegate () { OnButtonClick(imp.name, imp); });

                Text[] texts = instance.GetComponentsInChildren<Text>();
                foreach (Text t in texts)
                {
                    if (t.CompareTag("ImprovementDescription"))
                    {
                        t.text = imp.description;

                    }
                    if (t.CompareTag("ImprovementName"))
                    {
                        if (imp.subNames.Count > imp.currentLevel)
                        {
                            t.text = imp.subNames[imp.currentLevel];
                        }
                        else
                            t.text = imp.name;
                    }
                    if (t.CompareTag("ImprovementPrice"))
                    {
                        if (imp.priceMoney[imp.currentLevel] > 0)
                        {
                            t.text = imp.priceMoney[imp.currentLevel] + StaticStrings.MONEY_SIGN;
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
                //If the improvement is the modular booth
            }
        }
    }

    //Creates the list of improvements
    private void CreateList()
    {
        if (GameMasterGlobalData.clubImprovementList == null)
        {
            foreach (string d in StaticStrings.IMPROVEMENTS_DIRECTORIES_NAMES)
            {
                Improvement imp = JsonUtility.FromJson<Improvement>(Resources.Load<TextAsset>(
                 StaticStrings.IMPROVEMENTS_FOLDER + d + "/" + StaticStrings.IMPROVEMENT_DATA_FILE).text);
                if (imp.showInList)
                {
                    improvements.Add(imp);

                    CreateOneItemInList(imp);
                }
            }
            GameMasterGlobalData.clubImprovementList = improvements;
        }
        else
        {
            foreach (Improvement imp in GameMasterGlobalData.clubImprovementList)
            {
                CreateOneItemInList(imp);
            }
        }
        if (contentList.transform.childCount <= 0)
        {
            contentList.gameObject.SetActive(false);
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>().text = StaticStrings.ALL_IMPROVEMENTS_BOUGHT_ERROR_POPUP_MESSAGE;
            Button b = errorPopup.GetComponentInChildren<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate () { SceneManager.LoadScene(StaticStrings.CLUB_SCENE); });
        }
    }

    private void Awake()
    {
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
                    improvementToPrefabTable.Remove(t);
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

    //The OnClick action for each image in the improvements menu
    private void OnButtonClick(string improvementName, Improvement imp)
    {
        popup.SetActive(true);

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.IMPROVEMENTS_FOLDER + imp.name + "/" + imp.imagesPaths[imp.currentLevel]);
                mainPopupImage = img;
                break;
            }
        }
        popup.GetComponentInChildren<Text>().text = imp.detailedDescription[imp.currentLevel];
        if (imp.priceMoney[imp.currentLevel] > 0)
        {
            popup.GetComponentInChildren<Text>().text += "\nCosts " + imp.priceMoney[imp.currentLevel] + StaticStrings.MONEY_SIGN + ".";
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
                b.onClick.AddListener(delegate () { OnClick(improvementName, imp); });
                break;
            }
        }

        //Special behavior for the waitresses costumes
        if (imp.name == StaticStrings.WAITRESSES_COSTUMES_IMPROVEMENT_NAME)
        {
            SecondaryMenu(imp, OnCostumeClick);
        }
        //Special behavior for the special training
        else if (imp.name == StaticStrings.SPECIAL_TRAINING_IMPROVEMENT_NAME)
        {
            SecondaryMenu(imp, OnSpecialTrainingClick);
        }
    }

    //Delegate function to be used in the SecondaryMenu method
    private delegate void Del();

    //Creates the display for the waitresses costumes and special training zone
    private void SecondaryMenu(Improvement imp, Del del)
    {
        Image[] images = secondaryImages.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite =
                Resources.Load<Sprite>(StaticStrings.IMPROVEMENTS_FOLDER + imp.name + "/" + imp.imagesPaths[i]);
            Button b = images[i].GetComponentInChildren<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate () { del(); });
        }
        mainPopupImage.gameObject.SetActive(false);
        secondaryImages.SetActive(true);
    }

    private void OnCostumeClick()
    {

    }

    private void OnSpecialTrainingClick()
    {

    }

    //The OnClick action for the confirm button of the popup
    private void OnClick(string improvementName, Improvement imp)
    {
        //If the player doesn't have enough of one ressource, he can't buy this improvement
        //The prices of this improvement is registered in the JSON
        if (GMClubData.money >= imp.priceMoney[imp.currentLevel]
            && GMClubData.GetConnection() >= imp.priceConnection[imp.currentLevel]
            && GMClubData.GetInfluence() >= imp.priceInfluence[imp.currentLevel])
        {
            //There is a special case for the waitresses costumes
            if (improvementName == StaticStrings.WAITRESSES_COSTUMES_IMPROVEMENT_NAME)
            {


            }
            else
            {
                //If the level of the improvement is 0 before buying it, add it to the bought improvements list
                if (imp.currentLevel == 0)
                {
                    GMImprovementsData.boughtImprovements.Add(imp);
                }

                StaticFunctions.BuildImprovementsModifiers(improvementName, imp, toClubBlocker);
               
                GMClubData.SpendMoney(imp.priceMoney[imp.currentLevel]);
                GMClubData.SpendInfluence(imp.priceInfluence[imp.currentLevel]);
                GMClubData.SpendConnection(imp.priceConnection[imp.currentLevel]);

                if (imp.currentLevel < imp.maxLevel)
                {
                    imp.currentLevel++;
                }
                
                //RefreshOneItem(imp);
                //RefreshList();

            }

            secondaryImages.SetActive(false);
            popup.SetActive(false);
            mainPopupImage.gameObject.SetActive(true);
            if(GMGlobalNumericVariables.gnv.TUTORIAL_SCENE_STEP_NUMBER != 4)
                SceneManager.LoadScene(StaticStrings.IMPROVEMENTS_SCENE);
        }
        else
        {
            //If the player doesn't have enough of a ressource, the corresponding error message is displayed
            if (GMClubData.money < imp.priceMoney[imp.currentLevel])
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_MONEY_IMPROVEMENT_ERROR_MESSAGE;
            }
            else if (GMClubData.GetConnection() < imp.priceConnection[imp.currentLevel])
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_CONNECTION_IMPROVEMENT_ERROR_MESSAGE;
            }
            else if (GMClubData.GetInfluence() < imp.priceInfluence[imp.currentLevel])
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_INFLUENCE_IMPROVEMENT_ERROR_MESSAGE;
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

    //Refreshes the green image that indicates the ability to buy an improvement for each available improvement
    private void RefreshList()
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

}
