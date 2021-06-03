using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BoothGameMuteButtonBehavior : MonoBehaviour
{
    public Sprite muted;
    public Sprite sound;
    public Button button;
    public VideoPlayer videoPlayer;

    public bool isMuted = true;

    public static bool[] videosMuted = { true,true, true, true, true, true };

    public static int counter = 0;

    private int index;

    public void OnButtonPress()
    {
        if (isMuted)
        {
            button.image.sprite = sound;
            videoPlayer.SetDirectAudioVolume(0, PlayerPrefs.GetFloat(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER));
            videoPlayer.SetDirectAudioMute(0, false);
            if (AllVideosMuted())
            {
                MusicManager.LowerMusicVolume();
            }
            videosMuted[index] = false;

        }
        else
        {
            button.image.sprite = muted;
            videoPlayer.SetDirectAudioVolume(0, 0);
            videoPlayer.SetDirectAudioMute(0, true);
            videosMuted[index] = true;
            if (AllVideosMuted())
            {
                MusicManager.ReturnBaseMusicVolume();
            }
        }
        isMuted = !isMuted;
    }

    private void Start()
    {
        index = counter;
        counter++;

        isMuted = true;
        button.image.sprite = muted;
        videoPlayer.SetDirectAudioVolume(0, 0);
        videoPlayer.SetDirectAudioMute(0,true);
    }

    public static bool AllVideosMuted()
    {
        foreach(bool b in videosMuted)
        {
            if (!b)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        videoPlayer.SetDirectAudioVolume(0, PlayerPrefs.GetFloat(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER));

        Button b = GetComponent<Button>();
        if (videoPlayer.GetAudioChannelCount(0) > 0)
        {
            b.image.color = Color.white;
            b.interactable = true;
        }
        else
        {
            b.image.color = Color.clear;
            b.interactable = false;
        }
    }

}
