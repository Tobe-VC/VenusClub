using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssistantDateBehaviors : MonoBehaviour
{
    //public GameObject contentList;

    //public GameObject itemsPrefab;

    //public GameObject popup;

    //public GameObject errorPopup;

    //private Image mainPopupImage;

    ////Creates one item in the list of improvements
    //private void CreateOneItemInList(DateData date, int dateNumber)
    //{

    //        GameObject instance = Instantiate(itemsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    //        instance.transform.SetParent(contentList.transform, false);
    //        instance.name = date.name;

    //        //Look at all the images in the prefab
    //        foreach (Image img in instance.GetComponentsInChildren<Image>(true))
    //        {
    //            //The untagged one is the main image
    //            if (img.CompareTag("Untagged"))
    //            {
    //                img.sprite = date.thumbnailImage;
    //            }
    //        }

    //        Button b = instance.GetComponentInChildren<Button>();
    //        b.onClick.AddListener(delegate () { OnButtonClick(date, dateNumber); });

    //        Text[] texts = instance.GetComponentsInChildren<Text>();
    //        foreach (Text t in texts)
    //        {
    //            if (t.CompareTag("ImprovementName"))
    //            {
    //                    t.text = date.name;
    //            }
    //        }
        
    //}

    ////Creates the list of improvements
    //private void CreateList()
    //{
    //    for (int i = 0; i < RegisterDates.dates.Count; i++)
    //    {
    //        DateData d = RegisterDates.dates[i];

    //        if (d.bought)
    //        {
    //            CreateOneItemInList(d, i);
    //        }
    //        i++;
    //    }

    //    /*
    //if (contentList.transform.childCount <= 0)
    //{
    //    contentList.gameObject.SetActive(false);
    //    errorPopup.SetActive(true);
    //    errorPopup.GetComponentInChildren<Text>().text = StaticStrings.ALL_ASSISTANT_STORE_ITEMS_BOUGHT;
    //    Button b = errorPopup.GetComponentInChildren<Button>();
    //    b.onClick.RemoveAllListeners();
    //    b.onClick.AddListener(delegate () { SceneManager.LoadScene(StaticStrings.OFFICE_SCENE); });
    //}
    //*/
    //}

    //private void Awake()
    //{
    //    CreateList();
    //}

    ////The OnClick action for each image in the items menu
    //private void OnButtonClick(DateData date, int dateNumber)
    //{
    //    RegisterDates.currentDate = RegisterDates.dates[dateNumber];

    //    if (date.id == GMGlobalNumericVariables.gnv.CAFE_DATE_ID)
    //    {
    //        LaunchCafeDate(date);
    //    }

    //    /*
    //    popup.SetActive(true);

    //    //Create the image in the popup
    //    foreach (Image img in popup.GetComponentsInChildren<Image>())
    //    {
    //        if (img.CompareTag("PopupMainImage"))
    //        {
    //            img.sprite = Resources.Load<Sprite>(StaticStrings.ASSISTANT_RESSOURCES_FOLDER + StaticStrings.ASSISTANT_STORE_FOLDER +
    //                item.name + "/" + item.imagesPaths[item.currentLevel]);
    //            mainPopupImage = img;
    //            break;
    //        }
    //    }
    //    popup.GetComponentInChildren<Text>().text = item.detailedDescription[item.currentLevel];
    //    if (item.priceAssistantPoints[item.currentLevel] > 1)
    //    {
    //        popup.GetComponentInChildren<Text>().text += "\nCosts " + item.priceAssistantPoints[item.currentLevel] + " points.";
    //    }
    //    else if (item.priceAssistantPoints[item.currentLevel] == 1)
    //    {
    //        popup.GetComponentInChildren<Text>().text += "\nCosts " + item.priceAssistantPoints[item.currentLevel] + " point.";
    //    }
    //    else
    //    {
    //        popup.GetComponentInChildren<Text>().text += "\nFree.";
    //    }

    //    //Create the confirm button in the popup
    //    foreach (Button b in popup.GetComponentsInChildren<Button>())
    //    {
    //        if (b.CompareTag("PopupConfirm"))
    //        {
    //            b.onClick.RemoveAllListeners();
    //            b.onClick.AddListener(delegate () { OnClick(improvementName, item); });
    //            break;
    //        }
    //    }
    //    */
    //}

    ////The OnClick action for the confirm button of the popup
    //private void OnClick(string improvementName, AssistantStoreItem item)
    //{
    //    //If the player doesn't have enough of one ressource, he can't buy this improvement
    //    //The prices of this improvement is registered in the JSON
    //    if (GMClubData.money >= item.priceAssistantPoints[item.currentLevel])
    //    {
    //        //If the level of the item is 0 before buying it, add it to the bought assistant items list
    //        if (item.currentLevel == 0)
    //        {
    //            StaticAssistantData.boughtStoreItems.Add(item);
    //        }

    //        StaticFunctions.BuyAssistantStoreItem(item);

    //        GMClubData.SpendAssistantPoints(item.priceAssistantPoints[item.currentLevel]);

    //        if (item.currentLevel < item.maxLevel)
    //        {
    //            item.currentLevel++;
    //        }

    //        popup.SetActive(false);
    //        mainPopupImage.gameObject.SetActive(true);
    //        SceneManager.LoadScene(StaticStrings.ASSISTANT_STORE_SCENE);
    //    }
    //    else
    //    {
    //        //If the player doesn't have enough of a ressource, the corresponding error message is displayed
    //        if (GMClubData.money < item.priceAssistantPoints[item.currentLevel])
    //        {
    //            errorPopup.SetActive(true);
    //            errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_ASSISTANT_POINTS_ERROR_MESSAGE;
    //        }
    //    }
    //}

    ////Cancels the popup
    //public void OnCancelClick()
    //{
    //    popup.SetActive(false);
    //    mainPopupImage.gameObject.SetActive(true);
    //}

    //private void LaunchCafeDate(DateData date)
    //{
    //    StaticDialogElements.dialogData.launchFirstCafeDateDialog = true;
    //    if(date.timesDone > 0)
    //        SceneManager.LoadScene(StaticStrings.DATE_SCENE);
    //}

}
