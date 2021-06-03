using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GalleryBehavior : MonoBehaviour
{

    private VideoPlayer popupVideoPlayer;
    private Image popupBlocker;
    private RawImage popupVideoPlayerRawImage;

    public static GirlClass girlChecked;

    public GameObject choiceButtonPrefab;

    public Transform listContentOne;
    public Transform listContentTwo;
    public Transform listContentThree;

    public GameObject videoPopup;

    private List<GameObject> danceButtons;
    private List<GameObject> poseButtons;
    private List<GameObject> foreplayButtons;
    private List<GameObject> oralButtons;
    private List<GameObject> sexButtons;
    private List<GameObject> groupButtons;
    private List<GameObject> finishButtons;

    // Start is called before the first frame update
    void Start()
    {
        popupVideoPlayer = videoPopup.GetComponentInChildren<VideoPlayer>();
        popupBlocker = videoPopup.GetComponentInChildren<Image>();
        popupVideoPlayerRawImage = popupVideoPlayer.GetComponent<RawImage>();

        danceButtons = new List<GameObject>();
        poseButtons = new List<GameObject>();
        foreplayButtons = new List<GameObject>();
        oralButtons = new List<GameObject>();
        sexButtons = new List<GameObject>();
        groupButtons = new List<GameObject>();
        finishButtons = new List<GameObject>();

        if (girlChecked.external)
        {
            /*
            string girlVideosFolder = StaticStrings.GIRLPACKS_DIRECTORY + girlChecked.folderName + "/" +
                StaticStrings.VIDEOS_FOLDER + StaticStrings.PERFORMANCES_FOLDER + StaticStrings.WORK_FOLDER;

            FillViewPortList(danceVideosListContent, girlVideosFolder + StaticStrings.DANCE_FOLDER, 
                new string[]{ StaticStrings.DANCE_SUBFOLDER, StaticStrings.CLOSER_DANCE_SUBFOLDER,
                    StaticStrings.TOPLESS_DANCE_SUBFOLDER, });
            DirectoryInfo d = new DirectoryInfo(StaticStrings.GIRLPACKS_DIRECTORY + girlChecked.folderName + "/" +
                StaticStrings.VIDEOS_FOLDER + StaticStrings.PERFORMANCES_FOLDER + StaticStrings.WORK_FOLDER + 
                StaticStrings.DANCE_FOLDER + StaticStrings.DANCE_SUBFOLDER);

            int y = 0;
            foreach (FileInfo f in d.GetFiles())
            {
                GameObject button = Instantiate(choiceButtonPrefab, new Vector3(0, y, 0), Quaternion.identity);
                Button b = button.GetComponentInChildren<Button>();
                b.onClick.AddListener(delegate () { OnVideoChoiceButtonPress(f.FullName);});

                button.transform.SetParent(danceVideosListContent, false);
                TextMeshProUGUI textTMP = button.GetComponentInChildren<TextMeshProUGUI>();
                textTMP.text = f.Name.Split('.')[0];
                y += 100;
            }
            */


        }

        videoPopup.gameObject.SetActive(false);
    }

    public void OnVideoChoiceButtonPress(string videoName)
    {
        videoPopup.gameObject.SetActive(true);

        VideoClip clip = Resources.Load<VideoClip>(videoName);
        popupVideoPlayer.url = videoName;
        popupVideoPlayer.Play();

    }

    public void OnStopPopupButtonPress()
    {
        videoPopup.SetActive(false);
    }
    /// <summary>
    /// Fills one viewport list with buttons for each video
    /// </summary>
    /// <param name="listToFill">The viewport list to fill</param>
    /// <param name="folderToExtractVideosFrom">The name of the folder in which the videos are located</param>
    /// <param name="corespondingListOfButtons">The list of buttons, used to delete them later</param>
    /// <param name="fillCorrespondingList">True if the list should be filled, false if the button should only be reactivated</param>
    private void FillViewPortList(Transform listToFill, string folderToExtractVideosFrom, List<GameObject> corespondingListOfButtons, bool fillCorrespondingList)
    {
        DirectoryInfo d = new DirectoryInfo(folderToExtractVideosFrom);

        int y = 0;
        foreach (FileInfo f in d.GetFiles())
        {
            Debug.Log(f.Name);
            Debug.Log(girlChecked.videosSeen[0]);
            if (girlChecked.videosSeen.Exists(x => x.Equals(f.Name)))
            {
                GameObject button = Instantiate(choiceButtonPrefab, new Vector3(0, y, 0), Quaternion.identity);
                Button b = button.GetComponentInChildren<Button>();
                b.onClick.AddListener(delegate () { OnVideoChoiceButtonPress(f.FullName); });

                button.transform.SetParent(listToFill, false);
                TextMeshProUGUI textTMP = button.GetComponentInChildren<TextMeshProUGUI>();
                textTMP.text = f.Name.Split('.')[0];
                if (fillCorrespondingList)
                    corespondingListOfButtons.Add(button);
            }
        }
    }

    private void FillAllViewPortLists(string folderOne, string folderTwo, string folderThree,
        List<GameObject> buttonList)
    {
        bool fillLists = false;
        if (buttonList.Count <= 0)
            fillLists = true;

        if (fillLists)
        {
            FillViewPortList(listContentOne, folderOne, buttonList, fillLists);
            FillViewPortList(listContentTwo, folderTwo, buttonList, fillLists);
            FillViewPortList(listContentThree, folderThree, buttonList, fillLists);
        }
        else
        {
            buttonList.ForEach(x => x.SetActive(true));
        }
    }

    private void EmptyViewPortList(Transform list)
    {
        List<Transform> toDelete = new List<Transform>();
        foreach (Transform child in list)
        {
            toDelete.Add(child);
        }

        toDelete.ForEach(x => Destroy(x));
    }

    private void EmptyAllLists()
    {
        danceButtons.ForEach(x => x.SetActive(false));
        poseButtons.ForEach(x => x.SetActive(false));
        foreplayButtons.ForEach(x => x.SetActive(false));
        oralButtons.ForEach(x => x.SetActive(false));
        sexButtons.ForEach(x => x.SetActive(false));
        groupButtons.ForEach(x => x.SetActive(false));
        finishButtons.ForEach(x => x.SetActive(false));
    }

    private string CommonPerformanceButtonPress(string performanceMainFolder)
    {
        EmptyAllLists();
        return StaticStrings.GIRLPACKS_DIRECTORY + girlChecked.folderName + "/" +
            StaticStrings.VIDEOS_FOLDER + StaticStrings.PERFORMANCES_FOLDER + StaticStrings.WORK_FOLDER + performanceMainFolder;
    }

    public void OnDancePerformancesButtonPress()
    {
        string girlVideoFolder = CommonPerformanceButtonPress(StaticStrings.DANCE_FOLDER);

        FillAllViewPortLists(
            girlVideoFolder + StaticStrings.DANCE_SUBFOLDER,
            girlVideoFolder + StaticStrings.CLOSER_DANCE_SUBFOLDER,
            girlVideoFolder + StaticStrings.TOPLESS_DANCE_SUBFOLDER,
            danceButtons
            );
    }

    public void OnPosePerformancesButtonPress()
    {
        string girlVideoFolder = CommonPerformanceButtonPress(StaticStrings.POSE_FOLDER);

        FillAllViewPortLists(
            girlVideoFolder + StaticStrings.NAKED_SUBFOLDER,
            girlVideoFolder + StaticStrings.HAND_MASTURBATION_SUBFOLDER,
            girlVideoFolder + StaticStrings.TOY_MASTURBATION_SUBFOLDER,
            poseButtons
            );
    }

    public void OnForeplayPerformancesButtonPress()
    {
        string girlVideoFolder = CommonPerformanceButtonPress(StaticStrings.FOREPLAY_FOLDER);

        FillAllViewPortLists(
            girlVideoFolder + StaticStrings.HANDJOB_SUBFOLDER,
            girlVideoFolder + StaticStrings.FOOTJOB_SUBFOLDER,
            girlVideoFolder + StaticStrings.TITSJOB_SUBFOLDER,
            foreplayButtons
            );
    }

    public void OnOralPerformancesButtonPress()
    {
        string girlVideoFolder = CommonPerformanceButtonPress(StaticStrings.ORAL_FOLDER);

        FillAllViewPortLists(
            girlVideoFolder + StaticStrings.BLOWJOB_SUBFOLDER,
            girlVideoFolder + StaticStrings.DEEPTHROAT_SUBFOLDER,
            girlVideoFolder + StaticStrings.FACEFUCK_SUBFOLDER,
            oralButtons
            );
    }

    public void OnSexPerformancesButtonPress()
    {
        string girlVideoFolder = CommonPerformanceButtonPress(StaticStrings.SEX_FOLDER);

        FillAllViewPortLists(
            girlVideoFolder + StaticStrings.VAGINAL_FACING_SUBFOLDER,
            girlVideoFolder + StaticStrings.VAGINAL_BACK_SUBFOLDER,
            girlVideoFolder + StaticStrings.ANAL_SEX_SUBFOLDER,
            sexButtons
            );
    }

    public void OnGroupPerformancesButtonPress()
    {
        string girlVideoFolder = CommonPerformanceButtonPress(StaticStrings.GROUP_FOLDER);

        FillAllViewPortLists(
            girlVideoFolder + StaticStrings.THREESOME_SUBFOLDER,
            girlVideoFolder + StaticStrings.FOURSOME_SUBFOLDER,
            girlVideoFolder + StaticStrings.ORGY_SUBFOLDER,
            groupButtons
            );
    }

}
