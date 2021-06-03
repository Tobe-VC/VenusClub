using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class StaticDialogElements
{
    [System.Serializable]
    public class DialogData
    {
        public int nicoletteLargePortraitIndex = 0;

        public bool nicoletteNameKnown = false;

        public bool assistantFirstLateNightMeetingSeen = false;

        public bool assistantSecondLateNightMeetingGoodAnswer = false;

        public bool stopTestingSecondLateNightMeeting = false;

        public bool launchFirstCafeDateDialog = false;

        public bool launchAssistantUnlockBasicDanceDialog = false;
        public bool launchAssistantUnlockCloserDanceDialog = false;
        public bool launchAssistantUnlockToplessDanceDialog = false;

        public bool launchAssistantUnlockPoseNakedDialog = false;
        public bool launchAssistantUnlockSoloFingeringDialog = false;
        public bool launchAssistantUnlockToysMasturbationDialog = false;

        public bool launchAssistantUnlockHandjobDialog = false;
        public bool launchAssistantUnlockFootjobDialog = false;
        public bool launchAssistantUnlockTitsjobDialog = false;

    }

    public static DialogData dialogData = new DialogData();

    /// <summary>
    /// The reference to the currently used text field as a dialog box
    /// </summary>
    public static TextMeshProUGUI dialogBox;

    /// <summary>
    /// The reference to the currently used text field as a name box
    /// </summary>
    public static TextMeshProUGUI talkerNameBox;

    /// <summary>
    /// The reference to the list of buttons used to answer the dialog
    /// </summary>
    public static List<Button> dialogButtons;

    /// <summary>
    /// The reference to the current button loading the next dialog
    /// </summary>
    public static Button nextDialogButton;

    /// <summary>
    /// The reference to the portrait of the NPC currently in the dialog
    /// </summary>
    public static Image portraitLeft;
    public static Image portraitCenter;
    public static Image portraitRight;

    public static PortraitPosition portraitPosition;

    /// <summary>
    /// The reference to the background currently used in the dialog
    /// </summary>
    public static Image backgroundImage;

    public static GameObject currentMainDialogObject;

    public static string specialBackground = "";

    public static Sprite specialPortraitSprite;

    public static bool displayPortrait = true;

    public static bool dialogSpecialBackgroundIsOn = false;
    public static bool dialogSpecialPortraitIsOn = false;

    public static bool hasSeenPortrait = false;



    public static Sprite ChooseAssistantPortrait()
    {
        return StaticAssistantData.data.currentCostume.GetCurrentCostume();
    }

    private static Image ChoosePortraitPlacement()
    {
        switch (portraitPosition)
        {
            case PortraitPosition.LEFT:
                portraitRight.color = Color.clear;
                portraitCenter.color = Color.clear;
                portraitLeft.color = Color.white;
                return portraitLeft;

            case PortraitPosition.CENTER:
                portraitRight.color = Color.clear;
                portraitLeft.color = Color.clear;
                portraitCenter.color = Color.white;
                return portraitCenter;

            case PortraitPosition.RIGHT:
                portraitLeft.color = Color.clear;
                portraitCenter.color = Color.clear;
                portraitRight.color = Color.white;
                return portraitRight;
        }
        return portraitLeft;
    }

    private static void ChoosePortrait(NPCCode code,  PortraitPosition portraitPosition, GirlClass girl = null)
    {

        Image portrait = ChoosePortraitPlacement();

        if(code != NPCCode.NARRATOR && code != NPCCode.MAIN_CHARACTER && code != NPCCode.UNKNOWN_CHARACTER)
        {
            hasSeenPortrait = true;
        }

        if (dialogSpecialPortraitIsOn)
        {
            portrait.sprite = specialPortraitSprite;
        }
        else
        {

            if (displayPortrait)
            {
                switch (code)
                {
                    case NPCCode.ASSISTANT: portrait.sprite = ChooseAssistantPortrait(); break;
                    case NPCCode.CRIMINAL_NICOLETTE_SHEA:
                        portrait.sprite = Resources.Load<Sprite>(StaticStrings.CRIMINALS_DIRECTORY +
                            StaticStrings.CRIMINALS_NICOLETTE_SHEA_DIRECTORY +
                            StaticStrings.CRIMINALS_NICOLETTE_SHEA_LARGE_PORTRAITS[dialogData.nicoletteLargePortraitIndex]);
                        break;
                    case NPCCode.RECRUITED_GIRLS:
                        if (girl != null)
                        {
                            if (!girl.external)
                            {
                                portrait.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + girl.folderName + "/" +
                                    StaticStrings.IMAGES_FOLDER +
                                    StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
                            }
                            else
                            {
                                portrait.sprite = girl.GetPortrait();
                            }
                        }

                        break;
                    case NPCCode.MAIN_CHARACTER:
                        if (portrait.sprite == null)
                        {
                            portrait.sprite = Resources.Load<Sprite>(StaticStrings.EMPTY_PORTRAIT);
                        }
                        break;
                    default:
                        if (portrait.sprite == null)
                        {
                            portrait.sprite = Resources.Load<Sprite>(StaticStrings.EMPTY_PORTRAIT);
                        }
                        break;
                }
            }
            else
            {
                portrait.sprite = Resources.Load<Sprite>(StaticStrings.EMPTY_PORTRAIT);
            }
        }
    }

    private static void ChooseBackground(NPCCode code, GirlClass girl = null)
    {
        if(dialogSpecialBackgroundIsOn && girl != null)
        {
            //backgroundImage.sprite = dialogSpecialBackground.sprite;
        }
        else if (specialBackground != "")
        {
            backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY + specialBackground);
        }
        else
        {
            switch (code)
            {
                case NPCCode.ASSISTANT:
                    backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY +
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_OFFICE);
                    break;
                case NPCCode.CRIMINAL_NICOLETTE_SHEA:

                    if (SceneManager.GetActiveScene().name.Equals(StaticStrings.CRIME_SERVICES_SCENE))
                    {
                        backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY +
                            StaticStrings.DIALOG_BACKGROUNDS_NAME_CRIME_OFFICE);
                    }
                    else
                    {
                        backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY +
                            StaticStrings.DIALOG_BACKGROUNDS_NAME_UNDERGROUND_CARPARK);
                    }
                    break;
                case NPCCode.RECRUITED_GIRLS:
                    backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY +
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_LOUNGE);
                    break;
                case NPCCode.MAIN_CHARACTER:
                    if (backgroundImage != null && backgroundImage.sprite == null)
                    backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY +
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_LOUNGE);
                    break;
                case NPCCode.NARRATOR:
                    if (backgroundImage != null && backgroundImage.sprite == null)
                        backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY +
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_LOUNGE);
                    break;
                default:
                    if (backgroundImage != null && backgroundImage.sprite == null)
                        backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY +
                        StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB);
                    break;
            }
        }
    }

    private static void ChooseNameColor(NPCCode code)
    {
        switch (code)
        {
            case NPCCode.ASSISTANT: talkerNameBox.color = Color.red; break;
            case NPCCode.CRIMINAL_NICOLETTE_SHEA: talkerNameBox.color = new Color(156/255f, 0, 204/255f); break;
            case NPCCode.RECRUITED_GIRLS: talkerNameBox.color = Color.white; break;
            case NPCCode.MAIN_CHARACTER: talkerNameBox.color = Color.blue; break;
            default: talkerNameBox.color = Color.gray; break;
        }
    }

    /// <summary>
    /// Returns the name of the person currently talking, based on their NPCCode
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private static void ChooseTalkerName(NPCCode code, GirlClass girl = null)
    {
        switch (code)
        {
            case NPCCode.ASSISTANT: talkerNameBox.text = StaticStrings.ASSISTANT_FIRST_NAME; break;
            case NPCCode.CRIMINAL_NICOLETTE_SHEA:
                if (dialogData.nicoletteNameKnown)
                {
                    talkerNameBox.text = StaticStrings.CRIMINALS_NICOLETTE_SHEA_FIRST_NAME;
                }
                else
                {
                    talkerNameBox.text = StaticStrings.UNKNOWN_CHARACTER_NAME;
                }
                break;
            case NPCCode.RECRUITED_GIRLS:                
                if (girl != null)
                {
                    talkerNameBox.text = girl.name;
                }
                break;
            case NPCCode.MAIN_CHARACTER:
                talkerNameBox.text = StaticStrings.MC_TALKER_NAME;
                break;
            case NPCCode.NARRATOR:
                talkerNameBox.text = StaticStrings.NARRATOR_TALKER_NAME;
                break;
            default: talkerNameBox.text = StaticStrings.UNKNOWN_CHARACTER_NAME; break;
        }
    }

    public static void SetDialog(NPCCode code, PortraitPosition portraitPosition, GirlClass girl = null)
    {
        ChooseBackground(code, girl);
        ChoosePortrait(code, portraitPosition, girl);
        ChooseTalkerName(code, girl);
        ChooseNameColor(code);
    }

    public static NPCCode NPCCodeFromString(string code)
    {
        foreach(string s in StaticStrings.STRING_NPCCODE_MC)
        {
            if (code.ToLower().Equals(s))
            {
                return NPCCode.MAIN_CHARACTER;
            }
        }

        foreach (string s in StaticStrings.STRING_NPCCODE_GIRL)
        {
            if (code.ToLower().Equals(s))
            {
                return NPCCode.RECRUITED_GIRLS;
            }
        }

        foreach (string s in StaticStrings.STRING_NPCCODE_NARRATOR)
        {
            if (code.ToLower().Equals(s))
            {
                return NPCCode.NARRATOR;
            }
        }

        foreach (string s in StaticStrings.STRING_NPCCODE_ASSISTANT)
        {
            if (code.ToLower().Equals(s))
            {
                return NPCCode.ASSISTANT;
            }
        }

        return NPCCode.UNKNOWN_CHARACTER;
    }

}
