using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewPlanningDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public static GameObject itemDragged;
    Vector3 startPosition;

    public GameObject errorPopup;

    public GameObject tutorialPopup;

    public GameObject workList;
    public GameObject workListContent;

    public GameObject restList;
    public GameObject restListContent;

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

    public NewPlanningBehaviors npb;

    private GameObject copyDisplay;

    public TextMeshProUGUI wontDoText;

    public Button favorited;

    public float lastClickTime;

    public NewPlanningDragHandler(GameObject errorPopup, GameObject tutorialPopup, GameObject workList, GameObject workListContent,
     GameObject restList, GameObject restListContent, Text girlNameText, Image girlImage, Text dancingSkillValue, Text posingSkillValue,
     Text foreplaySkillValue, Text oralSkillValue, Text sexSkillValue, Text groupSkillValue, Text energyValue, Text opennessValue,
     Text popularityValue, NewPlanningBehaviors npb)
    {
        this.errorPopup = errorPopup;
        this.tutorialPopup = tutorialPopup;
        this.workList = workList;
        this.workListContent = workListContent;
        this.restList = restList;
        this.restListContent = restListContent;

        this.dancingSkillValue = dancingSkillValue;
        this.posingSkillValue = posingSkillValue;
        this.foreplaySkillValue = foreplaySkillValue;
        this.oralSkillValue = oralSkillValue;
        this.sexSkillValue = sexSkillValue;
        this.groupSkillValue = groupSkillValue;

        this.energyValue = energyValue;
        this.opennessValue = opennessValue;
        this.popularityValue = popularityValue;

        this.girlNameText = girlNameText;
        this.girlImage = girlImage;

        this.npb = npb;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragged = gameObject;
        startPosition = transform.position;

        //Create a copy to display on the canvas itself and not the content list
        copyDisplay = Instantiate(gameObject, Input.mousePosition, Quaternion.identity);
        copyDisplay.transform.SetParent(transform.GetComponentInParent<Canvas>().transform, false);
        RectTransform originalRect = (RectTransform)transform;
        ((RectTransform)copyDisplay.transform).sizeDelta = new Vector2(originalRect.rect.width, originalRect.rect.height);
        copyDisplay.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //When you drag the original, drag the copy as well
        transform.position = Input.mousePosition;
        copyDisplay.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //When dragging is over, destroy the copy
        Destroy(copyDisplay);

        if (workList.GetComponent<WorkListMouseHover>().mouseOver)
        {
            GirlClass girl = gameObject.GetComponent<NewPlanningGirl>().girl;

            //If the girl has some energy
            if (girl.GetEnergy() > 0)
            {
                if (!StaticFunctions.MaxGirlWorking())
                {
                    girl.morningOccupation = Occupation.WORK;
                    npb.RefreshLists();
                }
                else
                {
                    errorPopup.SetActive(true);
                    errorPopup.GetComponentInChildren<Text>().text = StaticStrings.TOO_MANY_GIRLS_WORKING_ERROR_MESSAGE;
                }
            }
            else
            {
                errorPopup.SetActive(true);
                errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_ENERGY_ERROR_MESSAGE;
            }
        }
        else if (restList.GetComponent<RestListMouseHover>().mouseOver)
        {
            
            GirlClass girl = gameObject.GetComponent<NewPlanningGirl>().girl;

            girl.morningOccupation = Occupation.REST;
            //gameObject.transform.SetParent(restListContent.transform, false);*/
            npb.RefreshLists();
        }


        transform.position = startPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GirlClass girl = gameObject.GetComponent<NewPlanningGirl>().girl;

        NewPlanningBehaviors.currentPlanningGirl = girl;

        girlNameText.text = girl.name;
        dancingSkillValue.text = girl.GetDancing().ToString();
        posingSkillValue.text = girl.GetPosing().ToString();
        foreplaySkillValue.text = girl.GetForeplay().ToString();
        oralSkillValue.text = girl.GetOral().ToString();
        sexSkillValue.text = girl.GetSex().ToString();
        groupSkillValue.text = girl.GetGroup().ToString();

        energyValue.text = (Mathf.RoundToInt((girl.GetEnergy() * 10)) / 10).ToString();
        opennessValue.text = (Mathf.RoundToInt((girl.GetOpenness() * 10)) / 10).ToString();
        popularityValue.text = girl.GetPopularity().ToString();

        if (girl.isFavorite)
        {
            favorited.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/star_full");
        }
        else
        {
            favorited.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/star_empty");
        }

        wontDoText.text = "Won't do: ";
        string tmpWontDoText = StaticFunctions.WontDoDisplay(girl);

        if (tmpWontDoText.Equals(""))
        {
            wontDoText.text = StaticStrings.NOTHING_SHE_WONT_DO_TEXT;
        }
        else
        {
            wontDoText.text += tmpWontDoText;
        }

        if (!girl.external)
        {
            if (girl.name.Equals(StaticStrings.ASSISTANT_FULL_NAME))
            {
                girlImage.sprite = StaticAssistantData.data.currentCostume.GetCurrentCostume();
            }
            else
            {
                girlImage.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + girl.folderName + "/" +
                                StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
            }
        }
        else
        {
            girlImage.sprite = girl.GetPortrait();
        }

        if (StaticFunctions.HasDoubleClicked(lastClickTime))
        {
            if (girl.morningOccupation == Occupation.REST)
            {
                if (girl.GetEnergy() > 0)
                {
                    if (!StaticFunctions.MaxGirlWorking())
                    {
                        girl.morningOccupation = Occupation.WORK;
                        npb.RefreshLists();
                    }
                    else
                    {
                        errorPopup.SetActive(true);
                        errorPopup.GetComponentInChildren<Text>().text = StaticStrings.TOO_MANY_GIRLS_WORKING_ERROR_MESSAGE;
                    }
                }
                else
                {
                    errorPopup.SetActive(true);
                    errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_ENERGY_ERROR_MESSAGE;
                }
            }
            else if (girl.morningOccupation == Occupation.WORK)
            {
                girl.morningOccupation = Occupation.REST;
            }
            npb.RefreshLists();
        }
        lastClickTime = Time.time;
    }


}
