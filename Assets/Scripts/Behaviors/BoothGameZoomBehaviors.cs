using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BoothGameZoomBehaviors : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public Slider volumeSlider;
    public Dropdown videoFitDropdown;

    public void ChangeSound()
    {
        PlayerPrefs.SetFloat(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER, volumeSlider.value);
        PlayerPrefs.Save();
        videoPlayer.SetDirectAudioVolume(0, volumeSlider.value);
    }

    public void OnVideoFitChange()
    {
        PlayerPrefs.SetInt(StaticStrings.PLAYER_PREFS_ZOOMED_BOOTH_VIDEO_ASPECT_RATIO, videoFitDropdown.value);
        PlayerPrefs.Save();
        switch (videoFitDropdown.value)
        {
            case 0: videoPlayer.aspectRatio = VideoAspectRatio.Stretch; break;
            case 1: videoPlayer.aspectRatio = VideoAspectRatio.FitOutside; break;
            case 2: videoPlayer.aspectRatio = VideoAspectRatio.NoScaling; break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER))
        {
            SetVolume(PlayerPrefs.GetFloat(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER));
        }
        else
        {
            SetVolume(1f);
        }

        if (videoFitDropdown != null)
        {
            if (PlayerPrefs.HasKey(StaticStrings.PLAYER_PREFS_ZOOMED_BOOTH_VIDEO_ASPECT_RATIO))
            {
                videoFitDropdown.value = PlayerPrefs.GetInt(StaticStrings.PLAYER_PREFS_ZOOMED_BOOTH_VIDEO_ASPECT_RATIO);
            }
            else
            {
                videoFitDropdown.value = 1;
            }
            videoFitDropdown.RefreshShownValue();
        }
    }

    private void SetVolume(float value)
    {
        videoPlayer.SetDirectAudioVolume(0, value);
        volumeSlider.value = value;
    }

}
