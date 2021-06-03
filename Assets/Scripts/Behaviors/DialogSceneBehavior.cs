using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class DialogSceneBehavior : MonoBehaviour
{

    public static string pathToClip;
    public static bool startVideo;
    public static string startingSceneName;
    public static string pathToImageBackground;


    public Image background;
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        MusicManager.instance.StopMusic();

        background.sprite = Resources.Load<Sprite>(pathToImageBackground);

        videoPlayer.Stop();
        VideoClip clip = Resources.Load<VideoClip>(pathToClip);

        videoPlayer.SetDirectAudioVolume(0, PlayerPrefs.GetFloat(StaticStrings.PLAYER_PREFS_MUSIC_VOLUME_SLIDER));
        videoPlayer.clip = clip;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startVideo && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    public void EndView()
    {
        startVideo = false;
        pathToImageBackground = "";
        MusicManager.instance.StartMusic();
        SceneManager.LoadScene(startingSceneName);
    }
}
