using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoothGameDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public static GameObject itemDragged;
    Vector3 startPosition;

    public BoothGameBehavior boothGameBehavior;

    public GameObject errorPopup;

    public GameObject tutorialPopup;

    public GameObject portraitsList;

    private float lastTimeClicked;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragged = gameObject;
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int i = 0; i < GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS; i++)
        {
            GameObject UIBooth = BoothGameData.UIBooths[i];
            if (UIBooth.GetComponent<BoothMouseHover>().mouseOver && BoothGameData.booths[i].client != null 
                && BoothGameData.booths[i].girl == null)
            {
                AssignGirlToBoothAndCheckForError(i);
            }

        }
        transform.position = startPosition;
    }

    private void AssignGirlToBoothAndCheckForError(int index)
    {
        GirlClass girl = gameObject.GetComponent<BoothGamePortraitData>().girl;

        //If the girl has some energy
        if (girl.GetEnergy() > 0)
        {
            if (girl.AcceptsPerformance(BoothGameData.booths[index].client.favoriteSexAct))
            {
                AssignGirlToBooth(index,girl);
            }
            else
            {
                errorPopup.SetActive(true);
                if (girl.CanDoPerformance(BoothGameData.booths[index].client.favoriteSexAct))
                {
                    errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_OPENNESS_ERROR_MESSAGE;
                }
                else
                {
                    errorPopup.GetComponentInChildren<Text>().text = StaticStrings.WONT_DO_THAT_ERROR_MESSAGE;
                }
                BoothGameData.boothGameIsPaused = true;
                Button[] buttons = errorPopup.GetComponentsInChildren<Button>(true);
                foreach (Button b in buttons)
                {
                    if (b.CompareTag("SpecialButton"))
                    {
                        CrimeService cs = GMCrimeServiceData.unlockedCrimeServices.Find(
                                            x => x.id == GMGlobalNumericVariables.gnv.CRIME_SERVICE_NEGOTIATOR_ID);
                        if (cs != null && cs.isSubscribed) 
                        {
                            int negotiatorInfluenceCost = GMGlobalNumericVariables.gnv.NEGOTIATOR_PER_USAGE_MULTIPLIER * BoothGameData.negotiatorUsage;
                            b.gameObject.SetActive(true);
                            b.GetComponentInChildren<Text>().text = StaticStrings.USE_NEGOTIATOR_BUTTON_TEXT;
                            if(BoothGameData.negotiatorUsage > 0)
                            {
                                b.GetComponentInChildren<Text>().text += "\nCosts " + negotiatorInfluenceCost + " out of your " + GMClubData.GetInfluence() + " influence.";
                            }
                            b.onClick.RemoveAllListeners();
                            if (girl.name == StaticStrings.ASSISTANT_FULL_NAME)
                            {
                                // If the girl is the assistant, then the negotiator cant be used
                                b.onClick.AddListener(delegate ()
                                {
                                    errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NO_NEGOTIATOR_WITH_ASSISTANT_ERROR_MESSAGE;
                                    b.gameObject.SetActive(false);
                                });
                                
                            }
                            else if(GMClubData.GetInfluence() < negotiatorInfluenceCost)
                            {
                                b.onClick.AddListener(delegate ()
                                {
                                    errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_INFLUENCE_FOR_NEGOTIATOR_USAGE_ERROR_MESSAGE;
                                    b.gameObject.SetActive(false);
                                });
                            }

                            else
                            {
                                b.onClick.AddListener(delegate ()
                                {
                                    GMClubData.SpendInfluence(negotiatorInfluenceCost);
                                    BoothGameData.negotiatorUsage++;
                                    AssignGirlToBooth(index, girl);
                                    BoothGameData.boothGameIsPaused = false;
                                    errorPopup.gameObject.SetActive(false);
                                });
                            }
                        }
                        else
                        {
                            b.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
        else
        {
            Button[] buttons = errorPopup.GetComponentsInChildren<Button>(true);
            foreach (Button b in buttons)
            {
                if (b.CompareTag("SpecialButton"))
                {
                    b.gameObject.SetActive(false);
                }
            }
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>().text = StaticStrings.NOT_ENOUGH_ENERGY_ERROR_MESSAGE;
            BoothGameData.boothGameIsPaused = true;
        }
    }

    private void AssignGirlToBooth(int boothIndex, GirlClass girl)
    {
        BoothGameData.booths[boothIndex].girl = girl;
        foreach (Image img in BoothGameData.UIBooths[boothIndex].GetComponentsInChildren<Image>())
        {
            if (img.gameObject.CompareTag("GirlPortrait"))
            {
                Image[] images = gameObject.GetComponentsInChildren<Image>();
                foreach (Image image in images)
                {
                    if (image.CompareTag("PortraitImage"))
                        img.sprite = image.sprite;
                }

            }
        }

        foreach (Text text in BoothGameData.UIBooths[boothIndex].GetComponentsInChildren<Text>())
        {
            if (text.gameObject.CompareTag("CurrentAct"))
            {
                text.gameObject.SetActive(false);
            }
        }

        foreach (Image img in portraitsList.GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("BuyableImage"))
                img.color = Color.clear;
        }

        BoothGameData.girlsRoster.RemoveAt(gameObject.GetComponent<BoothGamePortraitData>().index);
        boothGameBehavior.LoadGirlsList();
        boothGameBehavior.LoadNewUIBooth();

        if (tutorialPopup != null && StaticBooleans.tutorialIsOn && StaticBooleans.tutorialIsOnBoothGame)
        {
            tutorialPopup.SetActive(true);
            BoothGameData.boothGameIsPaused = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (StaticFunctions.HasDoubleClicked(lastTimeClicked))
        {
            if (BoothGameBoothPortraitClickHandler.currentSelectedBoothIndex >= 0
                && BoothGameData.booths[BoothGameBoothPortraitClickHandler.currentSelectedBoothIndex].client != null
                && BoothGameData.booths[BoothGameBoothPortraitClickHandler.currentSelectedBoothIndex].girl == null)
            {
                AssignGirlToBoothAndCheckForError(BoothGameBoothPortraitClickHandler.currentSelectedBoothIndex);
            }
        }
        lastTimeClicked = Time.time;
    }
}
