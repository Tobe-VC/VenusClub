using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoothGameBoothPortraitClickHandler : MonoBehaviour, IPointerDownHandler
{
    private GirlClass girl;

    public int boothIndex;

    public GameObject textsSkills;
    public GameObject physicalTraitsTexts;

    public Text dancingSkillValueText;
    public Text posingSkillValueText;
    public Text foreplaySkillValueText;
    public Text oralSkillValueText;
    public Text sexSkillValueText;
    public Text groupSkillValueText;

    public Text energyValueText;
    public Text opennessValueText;
    public Text popularityValueText;

    public Text heightValueText;
    public Text bustSizeValueText;
    public Text eyeColorValueText;
    public Text hairColorValueText;
    public Text bodyTypeValueText;
    public Text skinComplexionValueText;
    public Text ageValueText;

    public GameObject clientTraitsTexts;

    public BoothGamePortraitData[] portraitData;

    public static int currentSelectedBoothIndex = -1;

    private void Awake()
    {
        currentSelectedBoothIndex = -1;
    }

    //Activates the girl's info
    private void LeftClickEvent()
    {
        BoothGameBehavior.activityDisplayedGirl = girl;

        clientTraitsTexts.SetActive(false);

        textsSkills.SetActive(true);
        /*
        physicalTraitsTexts.SetActive(true);

        dancingSkillValueText.text = girl.GetDancing().ToString();
        posingSkillValueText.text = girl.GetPosing().ToString();
        foreplaySkillValueText.text = girl.GetForeplay().ToString();
        oralSkillValueText.text = girl.GetOral().ToString();
        sexSkillValueText.text = girl.GetSex().ToString();
        groupSkillValueText.text = girl.GetGroup().ToString();

        energyValueText.text = girl.GetEnergy().ToString();
        opennessValueText.text = girl.GetOpenness().ToString();
        popularityValueText.text = girl.GetPopularity().ToString();

        heightValueText.text = girl.height.ToString() + StaticStrings.HEIGHT_UNIT;
        bustSizeValueText.text = StaticFunctions.ToLowerCaseExceptFirst(girl.bustType.ToString());
        eyeColorValueText.text = StaticFunctions.ToLowerCaseExceptFirst(girl.eyeColor.ToString());
        hairColorValueText.text = StaticFunctions.ToLowerCaseExceptFirst(girl.hairColor.ToString());
        bodyTypeValueText.text = StaticFunctions.ToLowerCaseExceptFirst(girl.bodyType.ToString());
        skinComplexionValueText.text = StaticFunctions.ToLowerCaseExceptFirst(girl.skinComplexion.ToString());
        ageValueText.text = girl.age.ToString();
        */

    }

    //Activates the girl's info on a left click
    public void OnPointerDown(PointerEventData eventData)
    {
        //If the click event is a left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (BoothGameData.booths[boothIndex].girl != null)
            {
                girl = BoothGameData.booths[boothIndex].girl;
                LeftClickEvent(); //Activate the girl's data
            }
            else if (BoothGameData.booths[boothIndex].client != null)
            {
                //If there is a client in the booth

                currentSelectedBoothIndex = boothIndex;

                Client client = BoothGameData.booths[boothIndex].client;

                if (client != null)
                {
                    foreach (BoothGamePortraitData bgpd in portraitData)
                    {

                        Image[] images = bgpd.GetComponentsInChildren<Image>();
                        foreach (Image img in images)
                        {
                            if (img.CompareTag("BuyableImage"))
                            {
                                if (bgpd.girl.GetEnergy() > 0 && bgpd.girl.AcceptsPerformance(client.favoriteSexAct))
                                {
                                    //If the image is the border of the portrait
                                    //Red and green are the value of the color of the border.
                                    //The higher the necessary skil, the greener and the less red
                                    //The idea is that skills below 50 (half of the max) will have a full red color and a green going from 0 to 1
                                    //On the other hand, skills above or equals 50 will have a full green color and a red going from 1 to 0
                                    float green = 0.5f;
                                    float red = 0.5f;
                                    if (bgpd.girl.StatsValue(client.favoriteSexAct) < GMGlobalNumericVariables.gnv.MAX_STATS_VALUE / 2)
                                    {
                                        red = 1f;
                                        green = (float)bgpd.girl.StatsValue(client.favoriteSexAct) / (GMGlobalNumericVariables.gnv.MAX_STATS_VALUE / 2);
                                    }
                                    else
                                    {
                                        green = 1f;
                                        red = 1f - ((float)(bgpd.girl.StatsValue(client.favoriteSexAct) - (GMGlobalNumericVariables.gnv.MAX_STATS_VALUE / 2)) / (GMGlobalNumericVariables.gnv.MAX_STATS_VALUE / 2));
                                    }
                                    img.color = new Color(red, green, 0, 1);
                                }
                                else
                                {
                                    img.color = Color.clear;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /*
    private void Update()
    {
        StartCoroutine(SkillImageUpdate());
    }

    private IEnumerator SkillImageUpdate()
    {
        if (currentSelectedBoothIndex >= 0
            && BoothGameData.booths[currentSelectedBoothIndex].client != null 
            && BoothGameData.booths[currentSelectedBoothIndex].girl == null)
        {
            //If there is a client in the current booth and no girl

            Client client = BoothGameData.booths[currentSelectedBoothIndex].client;

            foreach (BoothGamePortraitData bgpd in portraitData)
            {
                Image[] images = bgpd.GetComponentsInChildren<Image>();
                foreach (Image img in images)
                {
                    if (img.CompareTag("BuyableImage"))
                    {
                        if (bgpd.girl.GetEnergy() > 0 && bgpd.girl.AcceptsPrestation(client.favoriteSexAct))
                        {
                            //If the image is the border of the portrait
                            //Red and green are the value of the color of the border.
                            //The higher the necessary skil, the greener and the less red
                            //The idea is that skills below 50 (half of the max) will have a full red color and a green going from 0 to 1
                            //On the other hand, skills above or equals 50 will have a full green color and a red going from 1 to 0
                            float green = 0.5f;
                            float red = 0.5f;
                            if (bgpd.girl.StatsValue(client.favoriteSexAct) < GMGlobalNumericVariables.gnv.MAX_STATS_VALUE / 2)
                            {
                                red = 1f;
                                green = (float)bgpd.girl.StatsValue(client.favoriteSexAct) / (GMGlobalNumericVariables.gnv.MAX_STATS_VALUE / 2);
                            }
                            else
                            {
                                green = 1f;
                                red = 1f - ((float)(bgpd.girl.StatsValue(client.favoriteSexAct) - (GMGlobalNumericVariables.gnv.MAX_STATS_VALUE / 2)) / (GMGlobalNumericVariables.gnv.MAX_STATS_VALUE / 2));
                            }
                            img.color = new Color(red, green, 0, 1);
                        }
                        else
                        {
                            img.color = Color.clear;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(SkillImageUpdate());
    }
    */
}
