using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateData
{

    public int id;
    public string name;
    public string directoryName;
    public int maxBackgroundLevel;
    public int maxPortraitLevel;
    public int currentPortraitLevel;
    public int currentBackgroundLevel;
    public List<string> portraitsPaths;
    public List<string> backgroundsPaths;
    public int timesDone;
    public int topScore;
    public List<List<string>> dialogLines;
    public string characterDated;
    public string thumbnailImagePath;

    public bool bought = false;

    public NPCCode character;

    public int videoLevel;
    public int maxVideoLevel;

    /// <summary>
    /// Goes back to 0 each time a new video is unlocked
    /// </summary>
    public int currentVideoTokenCollected;

    [System.NonSerialized]
    public List<Sprite> portraits;
    [System.NonSerialized]
    public List<Sprite> backgrounds;
    [System.NonSerialized]
    public Sprite thumbnailImage;

    public static DateData CreateFromJSON(string json)
    {
        DateData date = JsonUtility.FromJson<DateData>(json);

        if (date.directoryName == null || date.directoryName == "")
            date.directoryName = date.name;

        if (date.name == null || date.name == "")
            date.name = date.directoryName;

        date.portraits = new List<Sprite>();
        date.backgrounds = new List<Sprite>();

        date.currentPortraitLevel = 0;
        date.currentBackgroundLevel = 0;
        date.timesDone = 0;
        date.topScore = 0;
        date.character = StaticDialogElements.NPCCodeFromString(date.characterDated);

        for (int i = 0; i < date.maxPortraitLevel; i++)
        {
            date.portraits.Add(Resources.Load<Sprite>(date.portraitsPaths[i]));
        }

        for (int i = 0; i < date.maxBackgroundLevel; i++)
        {
            date.backgrounds.Add(Resources.Load<Sprite>(date.backgroundsPaths[i]));
        }

        date.thumbnailImage = Resources.Load<Sprite>(date.thumbnailImagePath);

        return date;
    }

}
