using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//The behaviors for the improvements scene
//Remeber to add the directory in the list in the inspector when adding an improvement
public class AssistantWardrobeBehaviors : MonoBehaviour
{
    public List<AssistantCostume> items = new List<AssistantCostume>();

    public GameObject contentList;

    public GameObject itemsPrefab;

    public GameObject popup;

    public GameObject errorPopup;

    public GameObject secondaryImages;

    private Image mainPopupImage;

    private AssistantCostume clickedCostume;

    private int clickedCostumeLevel;

    //Creates one item in the list of improvements
    private void CreateOneItemInList(AssistantCostume item)
    {
        GameObject instance = Instantiate(itemsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(contentList.transform, false);
        instance.name = item.name;


        //Look at all the images in the prefab
        foreach (Image img in instance.GetComponentsInChildren<Image>(true))
        {
            //The untagged one is the main image
            if (img.CompareTag("Untagged"))
            {
                img.sprite = Resources.Load<Sprite>(StaticStrings.ASSISTANT_RESSOURCES_FOLDER +
                    StaticStrings.ASSISTANT_IMAGES_FOLDER + StaticStrings.ASSISTANT_OUTFITS_FOLDER +
                    item.directoryName +
                    item.imagesPaths[0]);
            }
        }

        Button b = instance.GetComponentInChildren<Button>();
        b.onClick.AddListener(delegate () { OnButtonClick(item.name, item); });

        Text[] texts = instance.GetComponentsInChildren<Text>();
        foreach (Text t in texts)
        {
            if (t.CompareTag("ImprovementDescription"))
            {
                t.text = item.description[0];

            }
            if (t.CompareTag("ImprovementName"))
            {
                if (item.subNames.Count > item.currentLevel)
                {
                    t.text = item.subNames[0];
                }
                else
                    t.text = item.name;
            }
        }
    }

    //Creates the list of improvements
    private void CreateList()
    {
        foreach (AssistantCostume imp in RegisterAssistantCostumes.assistantCostumes)
        {
            if (imp.bought)
            {
                items.Add(imp);
                if(imp.name == StaticStrings.ASSISTANT_CAFE_DRESS)
                {
                    imp.currentLevel = RegisterDates.dates.Find(x => x.name == StaticStrings.DATE_BAR_NAME).currentPortraitLevel;
                }
                else if(imp.name == StaticStrings.ASSISTANT_TENNIS_OUTFIT)
                {
                    imp.currentLevel = RegisterDates.dates.Find(x => x.name == StaticStrings.DATE_TENNIS_NAME).currentPortraitLevel;
                }

                CreateOneItemInList(imp);
            }
        }

        if (contentList.transform.childCount <= 0)
        {
            contentList.gameObject.SetActive(false);
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NO_ASSISTANT_COSTUME_AVAILABLE;
            Button b = errorPopup.GetComponentInChildren<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(delegate () { SceneManager.LoadScene(StaticStrings.OFFICE_SCENE); });
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
    private void OnButtonClick(string improvementName, AssistantCostume item)
    {
        popup.SetActive(true);

        clickedCostume = item;

        //Create the image in the popup
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                clickedCostumeLevel = item.name == StaticAssistantData.data.currentCostume.name ?
                                        StaticAssistantData.data.currentCostumeLevel :
                                        item.currentLevel;

                img.sprite = Resources.Load<Sprite>(
                    StaticStrings.ASSISTANT_RESSOURCES_FOLDER +
                        StaticStrings.ASSISTANT_IMAGES_FOLDER + 
                        StaticStrings.ASSISTANT_OUTFITS_FOLDER +
                        item.directoryName +
                    item.imagesPaths[clickedCostumeLevel]);
                mainPopupImage = img;
                break;
            }
        }

        popup.GetComponentInChildren<Text>().text = item.description[item.currentLevel];

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
    private void OnClick(string improvementName, AssistantCostume item)
    {
        StaticAssistantData.data.currentCostume = item;
        StaticAssistantData.data.currentCostumeLevel = clickedCostumeLevel;
        SceneManager.LoadScene(StaticStrings.OFFICE_SCENE);
    }

    //Cancels the popup
    public void OnCancelClick()
    {
        popup.SetActive(false);
        secondaryImages.SetActive(false);
        mainPopupImage.gameObject.SetActive(true);
    }

    public void OnNextOutfitClick()
    {
       if(clickedCostumeLevel < clickedCostume.currentLevel)
        {
            clickedCostumeLevel++;
            RefreshPopupImageDisplay();
        }
           
    }

    public void OnPreviousOutfitClick()
    {
        if (clickedCostumeLevel > 0)
        {
            clickedCostumeLevel--;
            RefreshPopupImageDisplay();
        }
    }

    private void RefreshPopupImageDisplay()
    {
        foreach (Image img in popup.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("PopupMainImage"))
            {
                img.sprite = Resources.Load<Sprite>(
                    StaticStrings.ASSISTANT_RESSOURCES_FOLDER +
                        StaticStrings.ASSISTANT_IMAGES_FOLDER +
                        StaticStrings.ASSISTANT_OUTFITS_FOLDER +
                        clickedCostume.directoryName +
                    clickedCostume.imagesPaths[clickedCostumeLevel]);
                mainPopupImage = img;
                break;
            }
        }
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
