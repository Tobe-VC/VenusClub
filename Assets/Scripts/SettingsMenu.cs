using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown resolutionDropdown;

    public Dropdown fullscreenModeDropdown;

    public Slider musicVolumeSlider;

    public Toggle footjobToggle;

    private void Start()
    {
        footjobToggle.SetIsOnWithoutNotify(PlayerPrefs.GetInt(StaticStrings.PLAYER_PREFS_ACTIVATE_FOOTJOBS) != 0);

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width
             && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        fullscreenModeDropdown.value = (int)Screen.fullScreenMode;
        fullscreenModeDropdown.RefreshShownValue();

        musicVolumeSlider.value = PlayerPrefs.GetFloat(StaticStrings.PLAYER_PREFS_MUSIC_VOLUME_SLIDER);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetFullScreenMode(int mode)
    {
        Screen.fullScreenMode = (FullScreenMode)mode;
    }

    public void OnBackButtonPress()
    {
        Destroy(gameObject);
    }

    public void OnFootjobTogglePress()
    {
        if(PlayerPrefs.GetInt(StaticStrings.PLAYER_PREFS_ACTIVATE_FOOTJOBS) != 0)
        {
            //If the player pref is not 0, then set it to 0, meaning that footjobs are now deactivated
            PlayerPrefs.SetInt(StaticStrings.PLAYER_PREFS_ACTIVATE_FOOTJOBS, 0);
        }
        else
        {
            //If the player pref is 0, then set it to 1, meaning that footjobs are now activated
            PlayerPrefs.SetInt(StaticStrings.PLAYER_PREFS_ACTIVATE_FOOTJOBS, 1);
        }
    }

    public void ChangeMusicSound()
    {
        PlayerPrefs.SetFloat(StaticStrings.PLAYER_PREFS_MUSIC_VOLUME_SLIDER, musicVolumeSlider.value);
        MusicManager.SetNewVolume(musicVolumeSlider.value);
    }

}
