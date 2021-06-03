using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class BoothGameBehavior : MonoBehaviour
{

    private float clientGeneratorTimer;
    private float boothGameTime = GMGlobalNumericVariables.gnv.BOOTH_GAME_TIME;

    public Image[] girlsPortraits;
    public GameObject[] booths;
    public Text timeLeftText;

    public Sprite clientPortrait;
    public Sprite mafiaIcon;
    public Sprite policeIcon;

    public static GirlClass activityDisplayedGirl;

    public Text dancingSkillValueText;
    public Text posingSkillValueText;
    public Text foreplaySkillValueText;
    public Text oralSkillValueText;
    public Text sexSkillValueText;
    public Text groupSkillValueText;

    public Text energyValueText;
    public Text opennessValueText;
    public Text popularityValueText;

    public Text wontDoTitle;
    public TextMeshProUGUI wontDoText;

    private void CreateBooths()
    {
        BoothGameData.UIBooths = new GameObject[booths.Length];
        for(int i = 0; i < booths.Length; i++)
        {
            BoothGameData.UIBooths[i] = booths[i];
        }


        BoothGameData.booths = new Booth[GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS];
        for(int i = 0; i < GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS; i++)
        {
            BoothGameData.booths[i] = new Booth(i, NewPlanningBehaviors.boothsSpecialties[i]);
        }
    }

    public void LoadGirlsList()
    {
        GirlClass[] girlsRoster = BoothGameData.girlsRoster.ToArray();

        for (int i = 0; i < girlsPortraits.Length; i++)
        {
            if (i < girlsRoster.Length)
            {
                if (!girlsRoster[i].external)
                {
                    girlsPortraits[i].sprite = Resources.Load<Sprite>(StaticStrings.GIRLS_FOLDER + girlsRoster[i].folderName + "/" +
                        StaticStrings.IMAGES_FOLDER
                    + StaticStrings.CLOSEUP_PORTRAIT_FILE_NO_EXTENSION);
                }
                else
                {
                    girlsPortraits[i].sprite = girlsRoster[i].closeupPortrait;
                }
                girlsPortraits[i].transform.parent.gameObject.SetActive(true);
                girlsPortraits[i].transform.parent.GetComponent<BoothGamePortraitData>().girl = girlsRoster[i];
                girlsPortraits[i].transform.parent.GetComponent<BoothGamePortraitData>().index = i;
            }
            else
            {
                girlsPortraits[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    //Return the first free booth number or -1
    private int FreeBooth()
    {
        foreach (Booth booth in BoothGameData.booths)
        {
            if (booth.client == null)
                return booth.number;
        }
        return -1;
    }

    private void PutClientInBooth(Client client, Booth booth)
    {
        booth.helpAsked = false;
        booth.timerHelp = GMGlobalNumericVariables.gnv.BOOTH_GAME_TIMER_HELP;
        booth.finished = false;
        booth.extended = false;
        booth.timeLeft = GMGlobalNumericVariables.gnv.BOOTH_GAME_BASIC_BOOTH_TIME;
        booth.client = client;
        booth.currentSexAct = client.favoriteSexAct;
}

    private void Awake()
    {
        activityDisplayedGirl = null;

        clientGeneratorTimer = 2f;


        for (int i = 0; i < booths.Length; i++)
        {
            booths[i].SetActive(i < GMGlobalNumericVariables.gnv.NUMBER_OF_BOOTHS);
        }
        
        foreach (Image img in girlsPortraits)
        {
            img.transform.parent.gameObject.SetActive(false);
        }
        
        LoadGirlsList();
        CreateBooths();
    }

    private void ProgressBooths()
    {
        foreach (Booth booth in BoothGameData.booths)
        {
            if(booth.timeLeft <= 0)
            {
                //If the booth time is over
                booth.finished = true;

            }
            else
            {
                booth.timeLeft -= Time.deltaTime;
            }
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activityDisplayedGirl != null)
        {
            //Update the values of the girl currently displayed if there is any
            dancingSkillValueText.text = activityDisplayedGirl.GetDancing().ToString();
            posingSkillValueText.text = activityDisplayedGirl.GetPosing().ToString();
            foreplaySkillValueText.text = activityDisplayedGirl.GetForeplay().ToString();
            oralSkillValueText.text = activityDisplayedGirl.GetOral().ToString();
            sexSkillValueText.text = activityDisplayedGirl.GetSex().ToString();
            groupSkillValueText.text = activityDisplayedGirl.GetGroup().ToString();

            energyValueText.text = activityDisplayedGirl.GetEnergy().ToString();
            opennessValueText.text = activityDisplayedGirl.GetOpenness().ToString();
            popularityValueText.text = activityDisplayedGirl.GetPopularity().ToString();
            string s = StaticFunctions.WontDoDisplay(activityDisplayedGirl);
            if (!s.Equals(""))
            {
                wontDoTitle.text = StaticStrings.WONT_DO_TEXT;
                wontDoText.text = s;
            }
            else
            {
                wontDoTitle.text = "";
                wontDoText.text = StaticStrings.NOTHING_SHE_WONT_DO_TEXT;
            }
        }

        if (!BoothGameData.boothGameIsPaused)
        {
            if (boothGameTime <= 0)
            {
                EndBoothGame();
            }

            //Adds 0s before the minutes and seconds if they are below 10
            int minutes = System.TimeSpan.FromSeconds(boothGameTime).Minutes;
            int seconds = System.TimeSpan.FromSeconds(boothGameTime).Seconds;
            string minutesText = minutes.ToString();
            string secondsText = seconds.ToString();

            if (minutes < 10)
                minutesText = "0" + minutesText;
            if (seconds < 10)
                secondsText = "0" + secondsText;

            timeLeftText.text = minutesText + ":" + secondsText;
            boothGameTime -= Time.deltaTime;

            int freeBoothNumber = FreeBooth();
            if (freeBoothNumber != -1 && clientGeneratorTimer <= 0)
            {
                //If there is a free booth and the timer is available
                Client client = new Client(freeBoothNumber);

                PutClientInBooth(client, BoothGameData.booths[freeBoothNumber]);
                
                //After putting the client in the booth (for the data side)
                //Change the currentSexAct text to display what sex act the client wants
                foreach (Text t in BoothGameData.UIBooths[freeBoothNumber].GetComponentsInChildren<Text>())
                {
                    if (t.gameObject.CompareTag("CurrentAct"))
                    {
                        t.text = StaticStrings.CLIENT_WANTS_TEXT + " " + 
                            StaticFunctions.ToLowerCaseExceptFirst(BoothGameData.booths[freeBoothNumber].client.favoriteSexAct.ToString());
                    }
                }

                clientGeneratorTimer = GMGlobalNumericVariables.gnv.CURRENT_CLIENT_GENERATOR_TIMER +
                    UnityEngine.Random.Range(-GMGlobalNumericVariables.gnv.BASIC_CLIENT_GENERATOR_RANDOM_RANGE, 
                    GMGlobalNumericVariables.gnv.BASIC_CLIENT_GENERATOR_RANDOM_RANGE);

                Image[] allBoothImages = booths[freeBoothNumber].GetComponentsInChildren<Image>();

                foreach (Image img in allBoothImages)
                {
                    if (img.gameObject.CompareTag("ClientPortrait"))
                    {
                        
                        img.sprite = clientPortrait;
                        img.GetComponentInChildren<Text>().text = BoothGameData.booths[freeBoothNumber].client.ClientTypeAsString();
                            //StaticFunctions.ToLowerCaseExceptFirst(BoothGameData.booths[freeBoothNumber].client.clientType.ToString());
                    }
                    if (img.gameObject.CompareTag("ClientTypeImage"))
                    {
                        img.color = Color.white;

                        if(BoothGameData.booths[freeBoothNumber].client.clientGroup == ClientGroup.CRIMINAL)
                            img.sprite = mafiaIcon;
                        else if (BoothGameData.booths[freeBoothNumber].client.clientGroup == ClientGroup.POLICE)
                            img.sprite = policeIcon;
                        else
                            img.color = Color.clear;
                    }
                }
            }
            else
            {
                //If there is a free booth, start waiting for a new client
                if (freeBoothNumber != -1)
                    clientGeneratorTimer -= Time.deltaTime;
            }
        }
    }

    //When the booth game ends
    private void EndBoothGame()
    {
        StaticFunctions.PassADay(true,true);
    }

    public void PauseVideos()
    {
        foreach (GameObject go in booths)
        {
            VideoPlayer videoPlayer = go.GetComponentInChildren<VideoPlayer>();
            if(go.activeSelf && videoPlayer.isPlaying && videoPlayer.GetComponent<RawImage>().color.a == 1)
                videoPlayer.Pause();

        }
    }

    public void RestartVideos()
    {
        foreach (GameObject go in booths)
        {
            VideoPlayer videoPlayer = go.GetComponentInChildren<VideoPlayer>();
            if (go.activeSelf && videoPlayer.isPaused && videoPlayer.GetComponent<RawImage>().color.a == 1)
                videoPlayer.Play(); ;

        }
    }


    public void LoadNewUIBooth()
    {
        for(int i = 0; i < booths.Length; i++)
        {
            GameObject booth = booths[i];
            Text[] texts = booth.GetComponentsInChildren<Text>();
            foreach(Text t in texts)
            {
                if (t.isActiveAndEnabled && t.CompareTag("CurrentAct"))
                {
                    if (BoothGameData.booths[i].currentSexAct == Work.NONE)
                        t.text = StaticStrings.CLIENT_WAITING;
                    else
                        t.text = StaticFunctions.ToLowerCaseExceptFirst(BoothGameData.booths[i].currentSexAct.ToString());
                }
                    
                    
            }

            VideoPlayer videoPlayer = booth.GetComponentInChildren<VideoPlayer>();
            if (booth.activeSelf && BoothGameData.booths[i].client != null && BoothGameData.booths[i].girl != null)
            {
                
                Button[] buttons = booth.GetComponentsInChildren<Button>(true);
                foreach (Button b in buttons){
                    if (b.CompareTag("SoundButton"))
                    {
                        b.gameObject.SetActive(true);
                        break;
                    }
                }
                
                
                if (!videoPlayer.isPlaying)
                {
                    Booth b = BoothGameData.booths[i];
                    Button button = videoPlayer.GetComponentInChildren<Button>(true);
                    button.gameObject.SetActive(true);
                    GirlClass g = BoothGameData.booths[i].girl;
                        StaticFunctions.SelectWorkVideoClip(StaticStrings.WORK_FOLDER, BoothGameData.booths[i].currentSexAct,
                        BoothGameData.booths[i].girl.name, BoothGameData.booths[i].girl, videoPlayer, BoothGameData.booths[i]);
                    videoPlayer.gameObject.GetComponent<RawImage>().CrossFadeAlpha(0, 0f, true);
                    videoPlayer.Prepare();
                    StartCoroutine(WaitForPrepare(videoPlayer));
                }
            }
        }

    }

    public void OnPeekButtonPress(VideoPlayer videoPlayer)
    {
        if (!videoPlayer.isPlaying)
        {
            //videoPlayer.Prepare();
            StartCoroutine(WaitForPrepare(videoPlayer));
            //videoPlayer.Play();
            //videoPlayer.gameObject.GetComponent<RawImage>().CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            videoPlayer.GetComponentInChildren<Button>().gameObject.SetActive(false);
            videoPlayer.gameObject.GetComponent<RawImage>().CrossFadeAlpha(0, 0f, true);
            videoPlayer.Pause();
            //StartCoroutine(WaitToStop(videoPlayer));
        }

    }


    private IEnumerator WaitForPrepare(VideoPlayer videoPlayer)
    {
        //yield return new WaitUntil(() => videoPlayer.isPrepared);
        videoPlayer.frame = 0;
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.01f);
        }
        videoPlayer.Play();
        yield return new WaitForSeconds(0.25f);
        videoPlayer.gameObject.GetComponent<RawImage>().CrossFadeAlpha(1, 0f, true);
        //videoPlayer.GetComponentInChildren<Button>(true).gameObject.SetActive(true);
    }

    private IEnumerator WaitToStop(VideoPlayer videoPlayer)
    {
        yield return new WaitForSeconds(0.5f);
        videoPlayer.Pause();
        //videoPlayer.Prepare();
    }

    public void OnEndDayButtonPress()
    {
        EndBoothGame();
    }

    public void OnErrorPopupClose(GameObject errorPopup)
    {
        BoothGameData.boothGameIsPaused = false;
        errorPopup.SetActive(false);
    }

    private void OnDestroy()
    {
        BoothGameMuteButtonBehavior.videosMuted = new bool[] { true, true, true, true, true, true };
        BoothGameMuteButtonBehavior.counter = 0;
    }

}
