using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoothGamePortraitClickHandler : MonoBehaviour, IPointerDownHandler
{

    private GirlClass girl;

    public BoothGamePortraitData boothGamePortraitData;

    public GameObject textsSkills;
    public GameObject physicalTraitsTexts;

    /*
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
    */

    public GameObject clientTraitsTexts;

    //Activates the girl's info
    private void LeftClickEvent()
    {
        BoothGameBehavior.activityDisplayedGirl = girl;

        clientTraitsTexts.SetActive(false);

        textsSkills.SetActive(true);
        //physicalTraitsTexts.SetActive(true);

        /*
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

    // Start is called before the first frame update
    void Start()
    {
        girl = boothGamePortraitData.girl;
    }

    private void Update()
    {
        girl = boothGamePortraitData.girl;
    }

    //Activates the girl's info on a left click
    public void OnPointerDown(PointerEventData eventData)
    {
        //If the click event is a left click
        if (eventData.button == PointerEventData.InputButton.Left)
            LeftClickEvent(); //Activate the girl's data
    }
}
