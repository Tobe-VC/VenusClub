using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GirlLesson
{
    [System.NonSerialized]
    public Hashtable backgroundNameToSprite = new Hashtable();
    [System.NonSerialized]
    public Hashtable portraitNameToSprite = new Hashtable();

    public bool hasBeenLoaded = false;

    public string fileName;

    //The girl who has this dialog
    [System.NonSerialized]
    public GirlClass girl;

    //The dialog up to the question that will be asked (if any)
    public List<DialogLine> beforeQuestionDialog;


    public List<DialogLine> answerOneDialog;
    public List<DialogLine> answerTwoDialog;
    public List<DialogLine> answerThreeDialog;
    public List<DialogLine> answerFourDialog;

    //The dialogs when the answer is given
    //public List<DialogLine>[] answerDialogs = new List<DialogLine>[4];



    //The answers to the question
    //public string[] answers = new string[4];

    //The index of the correct answer (if any) values: 1,2,3,4. Anything else means there are no correct answer
    public int rightAnswer;

    //The gains (or losses if the value is negative) of openness
    public float[] opennessGains = new float[4];

    //public string portraitName;

    //public bool displayPortrait = true;

    //public string backgroundName;

    public bool done;

    public bool repeatable;

    //The minimum and maximum openness of the girl required to see this lesson
    public float minOpenness;
    public float maxOpenness;

    public List<string> needLessonsSeen = new List<string>();

    public bool disabled = false;

    private bool CheckLessonSeen()
    {
        foreach (GirlLesson lesson in girl.girlLessons)
        {
            foreach (string lessonNeeded in needLessonsSeen)
            {
                string lessonToCompare = lessonNeeded;
                if (lessonToCompare.Length > 5 && lessonToCompare.Substring(lessonToCompare.Length - 5).Equals(".json"))
                {
                    lessonToCompare = lessonToCompare.Substring(0, lessonToCompare.Length - 5);
                }
                if (lesson.fileName.ToLower().Equals(lessonToCompare.ToLower()))
                {
                    if (!lesson.done)
                        return false;
                }
            }
        }
        return true;
    }

    public bool CheckTrigger()
    {
        return minOpenness <= girl.GetOpenness() && maxOpenness >= girl.GetOpenness()
            && (!done || repeatable) //True if this event has not been seen or if it has been seen but is repeatable
            && CheckLessonSeen()
            && !disabled; //For testing purposes
    }

    public GirlLesson(GirlClass girl)
    {
        this.girl = girl;

        beforeQuestionDialog = new List<DialogLine>();

        answerOneDialog = new List<DialogLine>();
        answerTwoDialog = new List<DialogLine>();
        answerThreeDialog = new List<DialogLine>();
        answerFourDialog = new List<DialogLine>();

        //answers = new string[4];

        rightAnswer = 0;

        opennessGains = new float[4] { 0f, 0f, 0f, 0f };

        /*
        portraitName = "";

        displayPortrait = true;

        backgroundName = "";
        */
        done = false;

        repeatable = false;

        minOpenness = GMGlobalNumericVariables.gnv.MIN_OPENNESS_VALUE;
        maxOpenness = GMGlobalNumericVariables.gnv.MAX_OPENNESS_VALUE;


        needLessonsSeen = new List<string>();

        disabled = false;
    }

    public GirlLesson(GirlClass girl, string json) : this(girl)
    {
        string fileName = this.fileName;
        JsonUtility.FromJsonOverwrite(json, this);
        this.fileName = fileName;
        ChoosePortraitsPositions(beforeQuestionDialog);
        ChoosePortraitsPositions(answerOneDialog);
        ChoosePortraitsPositions(answerTwoDialog);
        ChoosePortraitsPositions(answerThreeDialog);
        ChoosePortraitsPositions(answerFourDialog);

        if (opennessGains.Length < 4)
        {
            float[] tmp = opennessGains;
            opennessGains = new float[] { 0, 0, 0, 0 };
            for (int i = 0; i < tmp.Length; i++)
            {
                opennessGains[i] = tmp[i];
            }
        }
    }

    private void ChoosePortraitsPositions(List<DialogLine> dialog)
    {
        foreach(DialogLine dL in dialog)
        {
            if (dL.portraitPosition != null)
            {
                if (dL.portraitPosition.ToLower() == "center")
                {
                    dL.portraitPositionEnum = PortraitPosition.CENTER;
                }
                else if (dL.portraitPosition.ToLower() == "right")
                {
                    dL.portraitPositionEnum = PortraitPosition.RIGHT;
                }
                else
                {
                    dL.portraitPositionEnum = PortraitPosition.LEFT;
                }
            }
            else
            {
                dL.portraitPositionEnum = PortraitPosition.LEFT;
            }
        }
    }

    private void CreateNamesList(List<DialogLine> dialog, List<string> backgroundNames, List<string> portraitNames)
    {
        foreach (DialogLine line in dialog)
        {
            if (!backgroundNames.Contains(line.background) && line.background != "" && line.background != null)
            {
                backgroundNames.Add(line.background);
            }

            if (!portraitNames.Contains(line.portrait) && line.portrait != "" && line.portrait != null)
            {
                portraitNames.Add(line.portrait);
            }
        }
    }

    public IEnumerator LoadBackgroundsAndPortraits(Image loadingImage)
    {
        if (!hasBeenLoaded)
        {
            List<string> backgroundNames = new List<string>();
            List<string> portraitNames = new List<string>();

            CreateNamesList(beforeQuestionDialog, backgroundNames, portraitNames);
            CreateNamesList(answerOneDialog, backgroundNames, portraitNames);
            CreateNamesList(answerTwoDialog, backgroundNames, portraitNames);
            CreateNamesList(answerThreeDialog, backgroundNames, portraitNames);
            CreateNamesList(answerFourDialog, backgroundNames, portraitNames);

            foreach (string name in backgroundNames)
            {
                if (girl.external)
                {
                    if (File.Exists(StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + "/" +
                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                        name))
                    {

                        yield return StaticFunctions.LoadImageFromURL("file:///" +
                            StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + "/" +
                            StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                            name, "", loadingImage, true, true);

                        backgroundNameToSprite.Add(name, loadingImage.sprite);
                    }
                }
                else
                {
                    backgroundNameToSprite.Add(name, Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                    girl.folderName + "/" +
                    StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_BACKGROUNDS_FOLDER +
                    name));
                }
            }

            foreach (string name in portraitNames)
            {
                if (girl.external)
                {
                    if (File.Exists(StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + "/" +
                        StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                        name))
                    {
                        yield return StaticFunctions.LoadImageFromURL("file:///" +
                            StaticStrings.GIRLPACKS_DIRECTORY + girl.folderName + "/" +
                            StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                            name, "", loadingImage, true, true);

                        portraitNameToSprite.Add(name, loadingImage.sprite);
                    }
                }
                else
                {
                    portraitNameToSprite.Add(name, Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER +
                    girl.folderName + "/" +
                    StaticStrings.IMAGES_FOLDER + StaticStrings.GIRLS_DIALOGS_PORTRAITS_FOLDER +
                    name));
                }
            }
            hasBeenLoaded = true;
        }
    }
}
