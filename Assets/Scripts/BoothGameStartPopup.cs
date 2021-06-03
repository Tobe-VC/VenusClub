using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class BoothGameStartPopup : MonoBehaviour
{

    private VideoPlayer popupVideoPlayer;
    private Image popupBlocker;
    private RawImage popupVideoPlayerRawImage;

    public BoothGameBehavior boothGameBehavior;

    public GameObject popup;

    public VideoPlayer boothVideoPlayer;

    public Button popupStopButton;

    private Slider volumeSlider;

    public Dropdown dropdownAspectRatio;

    public Text volumeText;

    private void Awake()
    {
        //On awake, initialize the variables, activate the objects and prepare the video
        popup.SetActive(true);

        popupVideoPlayer = popup.GetComponentInChildren<VideoPlayer>();
        popupBlocker = popup.GetComponentInChildren<Image>();
        popupVideoPlayerRawImage = popupVideoPlayer.GetComponent<RawImage>();

        popupStopButton.gameObject.SetActive(false);
        
        popupBlocker.CrossFadeAlpha(0, 0f, true);
        popupVideoPlayerRawImage.CrossFadeAlpha(0, 0f, true);

        popupVideoPlayer.Prepare();

        volumeSlider = popup.GetComponentInChildren<Slider>(true);

        volumeSlider.gameObject.SetActive(false);

        dropdownAspectRatio.gameObject.SetActive(false);
        volumeText.gameObject.SetActive(false);

    }

    public void OnBoothVideoPlayerPress()
    {
        //If the video in the booth is playing
        if (boothVideoPlayer.isPlaying)
        {
            //Select the clip
            popupVideoPlayer.clip = boothVideoPlayer.clip;

            if(!boothVideoPlayer.url.Equals("") && !(boothVideoPlayer.url == null))
                popupVideoPlayer.url = boothVideoPlayer.url;

            //"Activate" the image blocker
            popupBlocker.CrossFadeAlpha(0.75f, 0f, true);
            popupBlocker.raycastTarget = true;

            //"Activate" the video player
            popupVideoPlayerRawImage.CrossFadeAlpha(1f, 0f, true);

            //"Activate" the stop button
            popupStopButton.gameObject.SetActive(true);

            volumeSlider.gameObject.SetActive(true);
            if (PlayerPrefs.HasKey(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER))
            {
                volumeSlider.value = PlayerPrefs.GetFloat(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER);
            }
            else
            {
                volumeSlider.value = 0.5f;
            }
            dropdownAspectRatio.gameObject.SetActive(true);
            volumeText.gameObject.SetActive(true);

            //Play the video
            popupVideoPlayer.Play();

            //Pause the booth game
            BoothGameData.boothGameIsPaused = true;

            boothGameBehavior.PauseVideos();

            if (boothVideoPlayer.GetAudioChannelCount(0) > 0)
            {
                //If the video has sound lower the music
                MusicManager.LowerMusicVolume();
            }
        }
    }

    private IEnumerator WaitForPreparePopupVideo()
    {
        //If the video in the booth is playing
        if (boothVideoPlayer.isPlaying)
        {
            yield return new WaitUntil(() => popupVideoPlayer.isPrepared);
            //Select the clip
            popupVideoPlayer.clip = boothVideoPlayer.clip;

            //"Activate" the image blocker
            popupBlocker.CrossFadeAlpha(0.75f, 0f, true);
            popupBlocker.raycastTarget = true;

            //"Activate" the video player
            popupVideoPlayerRawImage.CrossFadeAlpha(1f, 0f, true);

            //"Activate" the stop button
            popupStopButton.gameObject.SetActive(true);

            //Play the video
            popupVideoPlayer.Play();
        }
        
    }


    public void OnStopPopupPress()
    {
        //Unpause the booth game
        BoothGameData.boothGameIsPaused = false;

        popupStopButton.gameObject.SetActive(false);

        volumeSlider.gameObject.SetActive(false);
        dropdownAspectRatio.gameObject.SetActive(false);
        volumeText.gameObject.SetActive(false);

        popupBlocker.raycastTarget = false;
        popupBlocker.CrossFadeAlpha(0f, 0f, true);

        popupVideoPlayer.Stop();
        popupVideoPlayer.Prepare();
        popupVideoPlayer.clip = null;

        popupVideoPlayerRawImage.CrossFadeAlpha(0f,0f,false);

        boothGameBehavior.RestartVideos();

        if(BoothGameMuteButtonBehavior.AllVideosMuted())
            MusicManager.ReturnBaseMusicVolume();
    }
}
