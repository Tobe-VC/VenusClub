using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class InteractionSceneBehavior : MonoBehaviour
{
    public GameObject girlsPortraitsContentList;

    public GameObject portraitPrefab;

    public Image mainPortrait;

    public Text girlNameText;

    public TMPro.TMP_Text numberOfAvailableLessonsText;

    public static GirlClass selectedGirl;

    public static int availableTalks = GMGlobalNumericVariables.gnv.MAX_TALKS_PER_DAY;

    public VideoPlayer videoPlayer;
    public VideoPlayer bigVideoPlayer;

    public GameObject loadingPopup;

    public Image loadingImage;

    public Image backgroundFillerImage;

    public GameObject dialogSystemBehaviorBackgroundBlocker;

    public static GameObject allDialogsDropdownList;

    private bool defaultLessonUsed = false;

    public GameObject lessonSummaryPopup;

    public GameObject errorPopup;

    public static Button testLessonButton;

    public static GameObject conditionsDescription;

    public TMPro.TextMeshProUGUI totalAvailableLessonsText;

    private List<GameObject> createdPortraits = new List<GameObject>();

    private void DisplayAvailableLessons()
    {
        totalAvailableLessonsText.text = "You can give " + availableTalks + ((availableTalks > 1) ? " lessons today." : " lesson today.");
        if (availableTalks <= 0)
        {
            totalAvailableLessonsText.text = "You cannot give anymore lessons today.";
        }
    }

    private string ConditionDescription(int index)
    {
        if (selectedGirl.girlLessons != null && selectedGirl.girlLessons.Count > 0)
        {
            GirlLesson currentLesson = selectedGirl.girlLessons[index];
            string description = "Conditions for this lesson: \n\n";
            description += "Min openness: " + currentLesson.minOpenness + "\n\n";
            description += "Max openness: " + currentLesson.maxOpenness + "\n\n";

            if (currentLesson != null && currentLesson.needLessonsSeen.Count > 0)
            {
                description += "Needs to see lessons:\n";

                foreach (string need in currentLesson.needLessonsSeen)
                {
                    description += need + "\n";
                }
            }
            else
            {
                description += "No previous lessons needed.\n";
            }

            description += "\n";
            if (currentLesson.CheckTrigger())
            {
                description += "This lesson is currently reachable.";
            }
            else
            {
                description += "This lesson is currently unreachable.";
            }

            return description;
        }
        return "";
    }

    private void CreateGirlsList()
    {
        TMPro.TMP_Dropdown allDialogs = allDialogsDropdownList.GetComponent<TMPro.TMP_Dropdown>();
        allDialogs.ClearOptions();
        List<TMPro.TMP_Dropdown.OptionData> options = new List<TMPro.TMP_Dropdown.OptionData>();

        TMPro.TextMeshProUGUI description = conditionsDescription.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        foreach (GirlClass girl in GMRecruitmentData.recruitedGirlsList)
        {
            if (girl.name != StaticStrings.ASSISTANT_FULL_NAME)
            {
                GameObject instance = Instantiate(portraitPrefab);

                createdPortraits.Add(instance);

                instance.transform.SetParent(girlsPortraitsContentList.transform, false);

                Image buttonPortrait = null;
                Image greenBorder = null;

                Text opennessText = null;
                foreach (Text t in instance.GetComponentsInChildren<Text>(true))
                {
                    if (t.CompareTag("AllPurposeText"))
                    {
                        opennessText = t;
                        break;
                    }
                }
                opennessText.text = "Openness: " + (Mathf.Round(girl.GetOpenness() * 100) / 100);

                foreach (Image img in instance.GetComponentsInChildren<Image>(true))
                {
                    if (img.CompareTag("PortraitImage"))
                    {
                        buttonPortrait = img;
                    }
                    else if (img.CompareTag("BuyableImage"))
                    {
                        greenBorder = img;
                    }
                }

                if (!girl.external)
                {
                    buttonPortrait.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + girl.folderName + "/" +
                                        StaticStrings.IMAGES_FOLDER + StaticStrings.CLOSEUP_PORTRAIT_FILE_NO_EXTENSION);
                }
                else
                {
                    buttonPortrait.sprite = girl.closeupPortrait;
                }

                if (girl.PossibleLessons().Count > 0)
                {
                    greenBorder.gameObject.SetActive(true);
                }
                else
                {
                    greenBorder.gameObject.SetActive(false);
                }

                Button b = instance.GetComponent<Button>();

                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate ()
                {
                    selectedGirl = girl;
                    numberOfAvailableLessonsText.text = CreateAvailableLessonsText();
                    girlNameText.text = girl.name;
                    if (!girl.external)
                    {
                        mainPortrait.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + girl.folderName + "/" +
                                            StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
                    }
                    else
                    {
                        mainPortrait.sprite = girl.GetPortrait();
                    }
                    allDialogs = allDialogsDropdownList.GetComponent<TMPro.TMP_Dropdown>();
                    allDialogs.ClearOptions();
                    options = new List<TMPro.TMP_Dropdown.OptionData>();
                    foreach (GirlLesson lesson in selectedGirl.girlLessons)
                    {
                        TMPro.TMP_Dropdown.OptionData option = new TMPro.TMP_Dropdown.OptionData(lesson.fileName);

                        options.Add(option);
                    }
                    allDialogs.AddOptions(options);
                    allDialogs.value = 0;
                    allDialogs.RefreshShownValue();

                    description.text = ConditionDescription(allDialogs.value);

                });
            }
        }
    }

    private void EmptyGirlsList()
    {
        foreach (GameObject go in createdPortraits)
        {
            Destroy(go);
        }
        createdPortraits = new List<GameObject>();
    }

    private void Start()
    {
        DisplayAvailableLessons();

        selectedGirl = GMRecruitmentData.recruitedGirlsList[0];

        girlNameText.text = selectedGirl.name;

        numberOfAvailableLessonsText.text = CreateAvailableLessonsText();

        TMPro.TextMeshProUGUI description = conditionsDescription.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if (!GMRecruitmentData.recruitedGirlsList[0].external)
        {
            mainPortrait.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                GMRecruitmentData.recruitedGirlsList[0].folderName + "/" +
                StaticStrings.IMAGES_FOLDER + StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
        }
        else
        {
            mainPortrait.sprite = GMRecruitmentData.recruitedGirlsList[0].GetPortrait();
        }

        TMPro.TMP_Dropdown allDialogs = allDialogsDropdownList.GetComponent<TMPro.TMP_Dropdown>();
        allDialogs.ClearOptions();
        List<TMPro.TMP_Dropdown.OptionData> options = new List<TMPro.TMP_Dropdown.OptionData>();
        foreach (GirlLesson lesson in selectedGirl.girlLessons)
        {
            TMPro.TMP_Dropdown.OptionData option = new TMPro.TMP_Dropdown.OptionData(lesson.fileName);
            options.Add(option);
        }
        allDialogs.onValueChanged.RemoveAllListeners();
        allDialogs.AddOptions(options);
        allDialogs.value = 0;
        allDialogs.RefreshShownValue();

        description.text = ConditionDescription(allDialogs.value);

        allDialogs.onValueChanged.AddListener(delegate
        {
            description.text = ConditionDescription(allDialogs.value);
        });

        testLessonButton.onClick.RemoveAllListeners();
        testLessonButton.onClick.AddListener(delegate
        {
            StartCoroutine(LessonButtonPress(selectedGirl.girlLessons[allDialogs.value]));
        });

        CreateGirlsList();
    }

    private string CreateAvailableLessonsText()
    {
        int possibleLessonsCount = PossibleLessons().Count;
        string result = possibleLessonsCount + ((possibleLessonsCount > 1) ? " possible lessons" : " possible lesson");
        if (possibleLessonsCount == 0)
        {
            return "No available lesson. You can still give her a basic one";
        }
        return result;
    }

    private Message CreateDialogFromDialogLines(GirlLesson lesson, List<DialogLine> dialog, Message previousMessage, int startingIndex)
    {
        Message prev = previousMessage;
        for (int i = startingIndex; i < dialog.Count - 1; i++)
        {
            DialogLine line = dialog[i];
            int index = i;
            Message message = new MessageBasicWithAction(line.line, StaticDialogElements.NPCCodeFromString(line.talker),
                delegate () {
                    SetBackgroundAndPortrait(lesson, dialog[index + 1].background,
                        dialog[index + 1].portrait, dialog[index + 1].portraitPositionEnum);
                });

            prev.AddSuccessor(message);

            prev = message;
        }
        return prev;
    }

    private List<int> CreateIndexesToGoList(GirlLesson lesson)
    {
        if (lesson.answerThreeDialog.Count > 0)
        {
            if (lesson.answerFourDialog.Count > 0)
            {
                return new List<int> { 0, 1, 2, 3 };
            }
            return new List<int> { 0, 1, 2 };
        }

        return new List<int> { 0, 1 };
    }

    private List<string> CreateAnswers(GirlLesson lesson)
    {
        if (lesson.answerThreeDialog.Count > 0)
        {
            if (lesson.answerFourDialog.Count > 0)
            {
                return new List<string> { lesson.answerOneDialog[0].line,
                    lesson.answerTwoDialog[0].line,
                    lesson.answerThreeDialog[0].line,
                    lesson.answerFourDialog[0].line
                };
            }
            return new List<string> { lesson.answerOneDialog[0].line,
                    lesson.answerTwoDialog[0].line,
                    lesson.answerThreeDialog[0].line
                };
        }

        return new List<string> { lesson.answerOneDialog[0].line,
                    lesson.answerTwoDialog[0].line
                };
    }

    private Message CreateAnswersDialogs(GirlLesson lesson, List<DialogLine> answerDialog)
    {
        if (answerDialog.Count > 2)
        {
            Message answerFirstMessage;
            answerFirstMessage = new MessageBasicWithAction(answerDialog[1].line,
            StaticDialogElements.NPCCodeFromString(answerDialog[1].talker),
            delegate () { SetBackgroundAndPortrait(lesson, answerDialog[2].background, answerDialog[2].portrait, answerDialog[2].portraitPositionEnum ); });

            Message answer = answerFirstMessage;

            answer = CreateDialogFromDialogLines(lesson, answerDialog, answer, 2);

            DialogLine lastLine = answerDialog[answerDialog.Count - 1];

            answer.AddSuccessor(new MessageEnd(lastLine.line, StaticDialogElements.NPCCodeFromString(lastLine.talker)));
            return answerFirstMessage;
        }
        else
        {
            return new MessageEnd(answerDialog[1].line, StaticDialogElements.NPCCodeFromString(answerDialog[1].talker));
        }

    }

    private List<GirlLesson> PossibleLessons()
    {
        List<GirlLesson> possibleLessons = new List<GirlLesson>();
        foreach (GirlLesson lesson in selectedGirl.girlLessons)
        {
            if (lesson.CheckTrigger())
            {
                possibleLessons.Add(lesson);
            }
        }
        return possibleLessons;
    }

    private GirlLesson SelectLesson()
    {
        List<GirlLesson> possibleLessons = PossibleLessons();

        if (possibleLessons.Count > 0)
        {
            defaultLessonUsed = false;
            return possibleLessons[Random.Range(0, possibleLessons.Count)];
        }
        else
        {
            defaultLessonUsed = true;
            return null;
        }

    }

    public void OnTalkButtonPress()
    {
        if (availableTalks > 0)
        {
            StartLesson();
            availableTalks--;
            DisplayAvailableLessons();
        }
        else
        {
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<Text>().text =
                "You can't have more lessons today!\n You can upgrade the lounge or go to another day if you want to see more.";
        }
    }
    /*
    private IEnumerator SetPortraitAndBackground(GirlLesson selectedLesson)
    {
        loadingPopup.SetActive(true);

        StaticDialogElements.displayPortrait = selectedLesson.displayPortrait;

        if (selectedLesson.portraitName != "")
        {
            StaticDialogElements.dialogSpecialPortraitIsOn = true;
            if (selectedGirl.external)
            {
                if (File.Exists(StaticStrings.GIRLPACKS_DIRECTORY + selectedGirl.folderName + "/" +
                    StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                    selectedLesson.portraitName))
                {

                    yield return StaticFunctions.LoadImageFromURL("file:///" +
                        StaticStrings.GIRLPACKS_DIRECTORY + selectedGirl.folderName + "/" +
                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                        selectedLesson.portraitName, "", loadingImage, true);

                    StaticDialogElements.specialPortraitSprite = loadingImage.sprite;
                }
                else
                {
                    StaticDialogElements.specialPortraitSprite = selectedGirl.portrait;
                }
            }
            else
            {
                StaticDialogElements.specialPortraitSprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                                        selectedGirl.folderName + "/" +
                                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                                        selectedLesson.portraitName);
            }
        }

        if (selectedLesson.backgroundName != "")
        {
            StaticDialogElements.dialogSpecialBackgroundIsOn = true;
            if (selectedGirl.external)
            {
                if (File.Exists(StaticStrings.GIRLPACKS_DIRECTORY + selectedGirl.folderName + "/" +
                     StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                   selectedLesson.backgroundName))
                {

                    yield return StaticFunctions.LoadImageFromURL("file:///" +
                        StaticStrings.GIRLPACKS_DIRECTORY + selectedGirl.folderName + "/" +
                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                        selectedLesson.backgroundName, "", loadingImage, true);

                    StaticDialogElements.backgroundImage.sprite = loadingImage.sprite;
                }
                else
                {
                    StaticDialogElements.dialogSpecialBackgroundIsOn = false;
                }
            }
            else
            {
                StaticDialogElements.backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                                        selectedGirl.folderName + "/" +
                                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                                        selectedLesson.backgroundName);

                if (StaticDialogElements.backgroundImage.sprite == null)
                {
                    StaticDialogElements.dialogSpecialBackgroundIsOn = false;
                }
            }
        }

        loadingPopup.SetActive(false);
    }
    */
    public void StartLesson()
    {
        GirlLesson selectedLesson = SelectLesson();
        StartCoroutine(LessonButtonPress(selectedLesson));
    }

    private void SetBackgroundAndPortrait(GirlLesson lesson, string backgroundName, string portraitName, PortraitPosition portraitPosition)
    {
        StaticDialogElements.dialogSpecialBackgroundIsOn = true;
        if (backgroundName != null && lesson.backgroundNameToSprite.ContainsKey(backgroundName)
            && backgroundName != "default")
        {
            StaticDialogElements.backgroundImage.sprite = (Sprite)lesson.backgroundNameToSprite[backgroundName];
        }
        else
        {
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
        }


        StaticDialogElements.displayPortrait = true;
        StaticDialogElements.dialogSpecialPortraitIsOn = true;
        StaticDialogElements.portraitPosition = portraitPosition;
        if (portraitName != null && lesson.portraitNameToSprite.ContainsKey(portraitName) && portraitName != "default")
        {

            if (portraitName.ToLower() != "none")
            {
                StaticDialogElements.specialPortraitSprite = (Sprite)lesson.portraitNameToSprite[portraitName];
            }
            else
            {
                StaticDialogElements.specialPortraitSprite = Resources.Load<Sprite>(StaticStrings.EMPTY_PORTRAIT);
            }
        }
        else
        {
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
            if (portraitName == null || portraitName.ToLower() != "default")
                StaticDialogElements.displayPortrait = false;
        }
    }

    private List<DialogLine> FindAnswerDialogFromIndex(GirlLesson lesson, int index)
    {
        switch (index)
        {
            case 0: return lesson.answerOneDialog;
            case 1: return lesson.answerTwoDialog;
            case 2: return lesson.answerThreeDialog;
            case 3: return lesson.answerFourDialog;
            default: return null;
        }
    }

    public IEnumerator LessonButtonPress(GirlLesson selectedLesson)
    {
        //StartCoroutine(TalkButtonPress());

        if (defaultLessonUsed)
        {
            StaticDialogElements.dialogSpecialPortraitIsOn = true;
            if (!selectedGirl.external)
            {
                StaticDialogElements.specialPortraitSprite =
                    Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + selectedGirl.folderName + "/" +
                    StaticStrings.IMAGES_FOLDER +
                    StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
            }
            else
            {
                StaticDialogElements.specialPortraitSprite = selectedGirl.GetPortrait();
            }
            //Dummy dialog to fool the compiler... Keep it here
            Dialog d = new Dialog(-100000, null,
                delegate () { return false; },
                delegate () { });

            d = new Dialog(100000, new MessageEnd("You had a quick lesson with her, which helped her open up a little.", NPCCode.NARRATOR),
                delegate () { return true; },
                delegate ()
                {
                    StaticDialogElements.dialogSpecialPortraitIsOn = false;
                    StaticDialogElements.specialPortraitSprite = null;
                    RegisterDialogs.dialogs.Remove(d);
                    selectedGirl.AddToOpenness(1);
                    lessonSummaryPopup.SetActive(true);
                    Text text = lessonSummaryPopup.GetComponentInChildren<Text>();
                    text.text = "She gained 1 openness after the lesson.";

                    EmptyGirlsList();
                    CreateGirlsList();
                }
                );

            RegisterDialogs.dialogs.Add(d);
        }
        else
        {
            float opennessGained = 0;
            bool rightAnswerChosen = false;
            //yield return SetPortraitAndBackground(selectedLesson);

            loadingPopup.SetActive(true);
            yield return selectedLesson.LoadBackgroundsAndPortraits(loadingImage);
            loadingPopup.SetActive(false);

            SetBackgroundAndPortrait(selectedLesson, selectedLesson.beforeQuestionDialog[0].background,
                selectedLesson.beforeQuestionDialog[0].portrait, selectedLesson.beforeQuestionDialog[0].portraitPositionEnum);

            Message firstMessage;
            if (selectedLesson.beforeQuestionDialog.Count > 1)
            {
                firstMessage = new MessageBasicWithAction(selectedLesson.beforeQuestionDialog[0].line,
                    StaticDialogElements.NPCCodeFromString(selectedLesson.beforeQuestionDialog[0].talker),
                    delegate ()
                    {
                        SetBackgroundAndPortrait(selectedLesson,
                            selectedLesson.beforeQuestionDialog[1].background,
                            selectedLesson.beforeQuestionDialog[1].portrait,
                            selectedLesson.beforeQuestionDialog[1].portraitPositionEnum);
                    }
                    );
            }
            else
            {
                firstMessage = new MessageBasic(selectedLesson.beforeQuestionDialog[0].line,
                    StaticDialogElements.NPCCodeFromString(selectedLesson.beforeQuestionDialog[0].talker));
            }

            Message previousMessage = firstMessage;

            previousMessage = CreateDialogFromDialogLines(selectedLesson, selectedLesson.beforeQuestionDialog, previousMessage, 1);

            DialogLine lastLine = selectedLesson.beforeQuestionDialog[selectedLesson.beforeQuestionDialog.Count - 1];

            Message questionMessage;

            if (selectedLesson.answerTwoDialog.Count <= 0)
            {
                questionMessage = new MessageEnd(lastLine.line, StaticDialogElements.NPCCodeFromString(lastLine.talker));
                selectedLesson.done = true;
                if (selectedLesson.beforeQuestionDialog.Count == 1)
                {
                    firstMessage = new MessageEnd(selectedLesson.beforeQuestionDialog[0].line,
                StaticDialogElements.NPCCodeFromString(selectedLesson.beforeQuestionDialog[0].talker));
                }
                else
                {
                    previousMessage.AddSuccessor(questionMessage);
                }
            }
            else
            {
                questionMessage = new MessageWithResponses(lastLine.line, StaticDialogElements.NPCCodeFromString(lastLine.talker),
                    CreateIndexesToGoList(selectedLesson),
                    CreateAnswers(selectedLesson),
                    delegate (int responseIndex)
                    {
                        opennessGained = selectedLesson.opennessGains[responseIndex];
                        selectedGirl.AddToOpenness(selectedLesson.opennessGains[responseIndex]);
                        if (responseIndex + 1 == selectedLesson.rightAnswer)
                        {
                            //+1 because the response index starts at 0 while the right answer is between 1 and 4 (both included)
                            //If the response index is equal to the right answer, then the dialog is considered done
                            selectedLesson.done = true;
                            rightAnswerChosen = true;
                        }
                        List<DialogLine> answerDialog = FindAnswerDialogFromIndex(selectedLesson, responseIndex);

                        SetBackgroundAndPortrait(selectedLesson,
                            answerDialog[1].background, answerDialog[1].portrait, answerDialog[1].portraitPositionEnum);
                    }
                    );


                previousMessage.AddSuccessor(questionMessage);

                Message answerOneFirstMessage = CreateAnswersDialogs(selectedLesson, selectedLesson.answerOneDialog);
                Message answerTwoFirstMessage = CreateAnswersDialogs(selectedLesson, selectedLesson.answerTwoDialog);

                questionMessage.AddSuccessor(answerOneFirstMessage);
                questionMessage.AddSuccessor(answerTwoFirstMessage);
                if (selectedLesson.answerThreeDialog.Count > 0)
                {
                    Message answerThreeFirstMessage = CreateAnswersDialogs(selectedLesson, selectedLesson.answerThreeDialog);
                    questionMessage.AddSuccessor(answerThreeFirstMessage);
                    if (selectedLesson.answerFourDialog.Count > 0)
                    {
                        Message answerFourFirstMessage = CreateAnswersDialogs(selectedLesson, selectedLesson.answerFourDialog);
                        questionMessage.AddSuccessor(answerFourFirstMessage);
                    }
                }



            }
            //Dummy dialog to fool the compiler... Keep it here
            Dialog d = new Dialog(-100000, null,
                delegate () { return false; },
                delegate () { });

            d = new Dialog(100000, firstMessage, delegate () { return true; },
                delegate ()
                {
                    RegisterDialogs.dialogs.Remove(d);
                    StaticDialogElements.dialogSpecialPortraitIsOn = false;
                    StaticDialogElements.dialogSpecialBackgroundIsOn = false;
                    StaticDialogElements.displayPortrait = true;
                    StaticDialogElements.backgroundImage.sprite = null;
                    StaticDialogElements.portraitLeft.sprite = null;

                    numberOfAvailableLessonsText.text = CreateAvailableLessonsText();

                    lessonSummaryPopup.SetActive(true);
                    Text text = lessonSummaryPopup.GetComponentInChildren<Text>();
                    if (typeof(MessageEnd).Equals(questionMessage.GetType()))
                    {
                        selectedGirl.AddToOpenness(1);
                        text.text = "This lesson earned her 1 openness.";
                    }
                    else
                    {
                        if (rightAnswerChosen)
                        {
                            text.text = "Good job!\n The lesson went well and she gained " + opennessGained + " openness.";
                        }
                        else
                        {
                            text.text = "Wrong answer.\n";
                            if (opennessGained < 0)
                            {
                                text.text += "The lesson went poorly and she lost " + -opennessGained + " openness.";
                            }
                            else if (opennessGained == 0)
                            {
                                text.text += "The lesson went poorly but didn't affect her openness.";
                            }
                            else
                            {
                                text.text += "The lesson went poorly but she still gained " + opennessGained + " openness.";
                            }
                        }
                    }
                    EmptyGirlsList();
                    CreateGirlsList();
                    numberOfAvailableLessonsText.text = CreateAvailableLessonsText();

                }
                );

            RegisterDialogs.dialogs.Add(d);
        }

    }

    /*
    private Sprite FindBackgroundFromLineNumber(List<BackgroundLineSprite> backgroundSprites, int lineNumber)
    {
        if (backgroundSprites != null)
        {
            foreach (BackgroundLineSprite bgls in backgroundSprites)
            {
                foreach (int i in bgls.lines)
                {
                    if (i == lineNumber)
                    {
                        return bgls.backgroundImage;
                    }
                }
            }
        }
        //If the line isn't found in the backgroundSprites, return the one currently used
        return StaticDialogElements.backgroundImage.sprite;
    }

    private Sprite FindPortraitFromLineNumber(List<PortraitLineSprite> portraitsSprites, int lineNumber)
    {
        if (portraitsSprites != null)
        {
            foreach (PortraitLineSprite pls in portraitsSprites)
            {
                foreach (int i in pls.lines)
                {
                    if (i == lineNumber)
                    {
                        return pls.portraitSprite;
                    }
                }
            }
        }
        //If the line isn't found in the portraitSprite, return null
        return null;
    }

    private bool BackgroundsListContainsLineNumber(List<BackgroundLineSprite> backgroundSprites, int lineNumber)
    {
        if (backgroundSprites != null)
        {
            foreach (BackgroundLineSprite bgls in backgroundSprites)
            {
                foreach (int i in bgls.lines)
                {
                    if (i == lineNumber)
                    {
                        return true;
                    }
                }
            }
        }
        //If the line isn't found in the backgroundSprites, return the one currently used
        return false;
    }

    private bool PortraitListContainsLineNumber(List<PortraitLineSprite> portraitSprites, int lineNumber)
    {
        if (portraitSprites != null)
        {
            foreach (PortraitLineSprite pls in portraitSprites)
            {
                foreach (int i in pls.lines)
                {
                    if (i == lineNumber)
                    {
                        return true;
                    }
                }
            }
        }
        //If the line isn't found in the backgroundSprites, return the one currently used
        return false;
    }

    public IEnumerator StartDialog(GirlDialog selectedDialog)
    {
        //If there are no possible dialogs, do nothing
        //But there should ALWAYS be at least one dialog possible
        //This is insured by the default dialog's existence

        selectedDialog.girl.interactionSeen++;
        selectedDialog.seen = true;

        loadingPopup.SetActive(true);

        if (selectedDialog.backgrounds != null && selectedDialog.backgrounds.Count > 0 &&
            (selectedDialog.backgroundsSprite == null || selectedDialog.backgroundsSprite.Count <= 0))
        {
            yield return GirlDialog.LoadBackgrounds(selectedDialog, loadingImage, loadingPopup);
        }

        if (selectedDialog.portraits != null && selectedDialog.portraits.Count > 0 &&
            (selectedDialog.portraitsSprites == null || selectedDialog.portraitsSprites.Count <= 0))
        {
            yield return GirlDialog.LoadPortraits(selectedDialog, loadingImage, loadingPopup);
        }

        loadingPopup.SetActive(false);

        backgroundFillerImage.gameObject.SetActive(true);

        StaticDialogElements.dialogSpecialBackgroundIsOn = true;
        StaticDialogElements.dialogSpecialPortraitIsOn = true;

        if (selectedDialog.backgroundsSprite != null && selectedDialog.backgroundsSprite.Count > 0)
        {
            if (BackgroundsListContainsLineNumber(selectedDialog.backgroundsSprite, 1))
            {
                StaticDialogElements.backgroundImage.sprite = FindBackgroundFromLineNumber(selectedDialog.backgroundsSprite, 1);
            }
            else
            {
                StaticDialogElements.backgroundImage.sprite = Resources.Load<Sprite>(StaticStrings.MAIN_DIALOG_BACKGROUNDS_DIRECTORY +
                StaticStrings.DIALOG_BACKGROUNDS_NAME_MAIN_CLUB);
            }
        }
        else
        {
            StaticDialogElements.dialogSpecialBackgroundIsOn = false;
        }


        if (selectedDialog.portraitsSprites != null && selectedDialog.portraitsSprites.Count > 0)
        {
            if (PortraitListContainsLineNumber(selectedDialog.portraitsSprites, 1))
            {
                if (StaticDialogElements.NPCCodeFromString(selectedDialog.dialog[0].talker) == NPCCode.RECRUITED_GIRLS)
                {
                    StaticDialogElements.portrait.sprite = FindPortraitFromLineNumber(selectedDialog.portraitsSprites, 1);
                }
                else
                {
                    StaticDialogElements.portrait.sprite = Resources.Load<Sprite>(StaticStrings.MC_PORTRAIT_NAME);
                }
            }
            else
            {
                if (selectedGirl.external)
                {
                    StaticDialogElements.portrait.sprite = selectedGirl.portrait;
                }
                else
                {
                    StaticDialogElements.portrait.sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                        selectedGirl.folderName + "/" + StaticStrings.IMAGES_FOLDER +
                        StaticStrings.PORTRAIT_FILE_NO_EXTENSION);
                }
            }
        }
        else
        {
            StaticDialogElements.dialogSpecialPortraitIsOn = false;
        }


        List<Message> messages = new List<Message>();

        for (int i = 0; i < selectedDialog.dialog.Count - 1; i++)
        {
            int index = i + 2;
            if (i == selectedDialog.lastLineWithVideo - 1)
            {
                messages.Add(new MessageBasicWithAction(selectedDialog.dialog[i].line,
                    StaticDialogElements.NPCCodeFromString(selectedDialog.dialog[i].talker),
                    delegate ()
                    {
                        if (!selectedDialog.isVideoFullScreen)
                        {
                            videoPlayer.Stop();
                            videoPlayer.gameObject.SetActive(false);

                        }
                        else
                        {
                            bigVideoPlayer.Stop();
                            bigVideoPlayer.gameObject.SetActive(false);
                        }

                        DisplayPortraitAndBackground(selectedDialog, index);
                    }
                ));
            }
            else if (i == selectedDialog.firstLineWithVideo - 2)
            {
                messages.Add(new MessageBasicWithAction(selectedDialog.dialog[i].line,
                    StaticDialogElements.NPCCodeFromString(selectedDialog.dialog[i].talker),
                    delegate ()
                    {
                        StartVideo(selectedDialog);
                        DisplayPortraitAndBackground(selectedDialog, index);
                    }
                ));
            }
            else
            {
                messages.Add(new MessageBasicWithAction(selectedDialog.dialog[i].line,
                    StaticDialogElements.NPCCodeFromString(selectedDialog.dialog[i].talker),
                    delegate ()
                    {
                        DisplayPortraitAndBackground(selectedDialog, index);
                    }));
            }
        }

        messages.Add(new MessageEnd(selectedDialog.dialog[selectedDialog.dialog.Count - 1].line,
            StaticDialogElements.NPCCodeFromString(selectedDialog.dialog[selectedDialog.dialog.Count - 1].talker)));

        for (int i = 0; i < messages.Count - 1; i++)
        {
            messages[i].AddSuccessor(messages[i + 1]);
        }

        //Dummy dialog to fool the compiler... Keep it here
        Dialog d = new Dialog(-100000, null,
            delegate () { return false; },
            delegate () { });

        d = new Dialog(100000, messages[0], delegate () { return true; },
            delegate ()
            {
                RegisterDialogs.dialogs.Remove(d);
                selectedGirl.AddToAffection(GMGlobalNumericVariables.gnv.INTERACTION_AFFECTION_GAIN);
                selectedGirl.AddToOpenness(GMGlobalNumericVariables.gnv.INTERACTION_OPENNESS_GAIN);
                availableTalks--;
                numberOfAvailableLessonsText.text = CreateAvailableLessonsText();

                videoPlayer.Stop();
                videoPlayer.clip = null;
                videoPlayer.gameObject.SetActive(false);
                bigVideoPlayer.Stop();
                bigVideoPlayer.clip = null;
                bigVideoPlayer.gameObject.SetActive(false);
                StaticDialogElements.dialogSpecialBackgroundIsOn = false;
                StaticDialogElements.dialogSpecialPortraitIsOn = false;
                StaticDialogElements.backgroundImage.sprite = null;
                StaticDialogElements.portrait.sprite = null;
                StaticDialogElements.displayPortrait = true;
                StaticDialogElements.specialBackground = "";
                backgroundFillerImage.gameObject.SetActive(false);
                selectedDialog.Effects();
                    //Debug.Log(selectedGirl.doDance);
                }
            );

        if (selectedDialog.firstLineWithVideo <= 1)
        {
            StartVideo(selectedDialog);
        }



        RegisterDialogs.dialogs.Add(d);
    }
      
    
    public IEnumerator TalkButtonPress()
    {
        List<GirlDialog> girlDialogs = selectedGirl.PossibleDialogs();

        if (girlDialogs.Count > 0 && availableTalks > 0)
        {
            yield return StartDialog(girlDialogs[Random.Range(0, girlDialogs.Count)]);
        }
    }

    private void DisplayPortraitAndBackground(GirlDialog selectedDialog, int index)
   {
        StaticDialogElements.backgroundImage.sprite = FindBackgroundFromLineNumber(selectedDialog.backgroundsSprite, index);
        Sprite spr  = FindPortraitFromLineNumber(selectedDialog.portraitsSprites, index);
        if(spr != null && StaticDialogElements.NPCCodeFromString(selectedDialog.dialog[index - 1].talker) ==  NPCCode.RECRUITED_GIRLS)
        {
            StaticDialogElements.portrait.sprite = spr;
        }
        else
        {
            StaticDialogElements.portrait.sprite = Resources.Load<Sprite>(StaticStrings.MC_PORTRAIT_NAME);
        }

    }

    private void StartVideo(GirlDialog selectedDialog)
    {
        if (selectedDialog.videoName != "")
        {
            VideoClip clip = null;
            if (!selectedGirl.external)
            {
                clip = Resources.Load<VideoClip>(StaticStrings.GIRLS_FOLDER + selectedGirl.folderName + "/" +
                    StaticStrings.VIDEOS_FOLDER + StaticStrings.TALK_FOLDER + selectedDialog.videoName);

                if (clip != null)
                {
                    if (!selectedDialog.isVideoFullScreen)
                    {
                        videoPlayer.Prepare();
                        videoPlayer.gameObject.SetActive(true);
                        videoPlayer.clip = clip;
                        videoPlayer.Play();

                    }
                    else
                    {
                        bigVideoPlayer.Prepare();
                        dialogSystemBehaviorBackgroundBlocker.SetActive(true);
                        bigVideoPlayer.gameObject.SetActive(true);
                        bigVideoPlayer.clip = clip;
                        bigVideoPlayer.Play();
                        
                    }
                    StaticDialogElements.displayPortrait = selectedDialog.displayPortrait;
                }
            }
            else
            {
                string videoPath = StaticStrings.GIRLPACKS_DIRECTORY +
                    selectedGirl.folderName + "/" + StaticStrings.VIDEOS_FOLDER + StaticStrings.TALK_FOLDER + selectedDialog.videoName;

                if (File.Exists(videoPath) && StaticFunctions.IsCompatibleVideoFile(new FileInfo(videoPath)))
                {
                    if (!selectedDialog.isVideoFullScreen)
                    {
                        videoPlayer.gameObject.SetActive(true);
                        videoPlayer.url = videoPath;
                        videoPlayer.Play();
                    }
                    else
                    {
                        dialogSystemBehaviorBackgroundBlocker.SetActive(true);
                        bigVideoPlayer.gameObject.SetActive(true);
                        bigVideoPlayer.url = videoPath;
                        bigVideoPlayer.Play();
                    }
                    StaticDialogElements.displayPortrait = selectedDialog.displayPortrait;
                }
            }
        }
    }

    */
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.D) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            || (Input.GetKey(KeyCode.D) && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
            )
        {
            allDialogsDropdownList.gameObject.SetActive(!allDialogsDropdownList.gameObject.activeSelf);
            testLessonButton.gameObject.SetActive(!testLessonButton.gameObject.activeSelf);
            conditionsDescription.SetActive(!conditionsDescription.activeSelf);
        }
    }
}
