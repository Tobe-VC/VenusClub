using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static AudioSource musicPlayer;

    private AudioClip[] musicClipsClub;
    private AudioClip[] musicClipsBoothGame;

    private bool isClubMusicPlaying;

    private static bool musicIsLowered = false;

    private static float originalMusicVolumeLevel = 0.5f;

    private string[] clubMusicsList;
    private string[] workMusicsList;

    private static bool doNotRestartMusic = false;
    private static float beforeFadeVolume;

    //public static float volume;

    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static MusicManager s_Instance = null;

    // A static property that finds or creates an instance of the manager object and returns it.
    public static MusicManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                // FindObjectOfType() returns the first GMClubData object in the scene.
                s_Instance = FindObjectOfType(typeof(MusicManager)) as MusicManager;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("MusicManager");
                s_Instance = obj.AddComponent<MusicManager>();
            }

            return s_Instance;
        }
    }

    private string[] SelectOggMusics(string[] list)
    {
        List<string> resultList = new List<string>();

        foreach(string s in list)
        {
            if (Path.GetExtension(s) == ".ogg")
                resultList.Add(s);
        }
        return resultList.ToArray();
    }

    private void Awake()
    {
        if (FindObjectsOfType<AudioSource>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            musicPlayer = GameObject.FindGameObjectWithTag("MusicManager").GetComponentInChildren<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;

            musicClipsClub = Resources.LoadAll<AudioClip>(StaticStrings.MUSICS_FOLDER + StaticStrings.MUSICS_CLUB_FOLDER);
            musicClipsBoothGame = Resources.LoadAll<AudioClip>(StaticStrings.MUSICS_FOLDER + StaticStrings.MUSICS_BOOTH_GAME_FOLDER);
        
            clubMusicsList = Directory.GetFiles(StaticStrings.MUSICS_DIRECTORY + StaticStrings.CLUB_MUSICS_DIRECTORY);
            clubMusicsList = SelectOggMusics(clubMusicsList);

            workMusicsList = Directory.GetFiles(StaticStrings.MUSICS_DIRECTORY + StaticStrings.WORK_MUSICS_DIRECTORY);
            workMusicsList = SelectOggMusics(workMusicsList);

            isClubMusicPlaying = true;
            DontDestroyOnLoad(transform.gameObject);
            if (PlayerPrefs.HasKey(StaticStrings.PLAYER_PREFS_MUSIC_VOLUME_SLIDER))
            {
                musicPlayer.volume = PlayerPrefs.GetFloat(StaticStrings.PLAYER_PREFS_MUSIC_VOLUME_SLIDER);
            }
            else
            {
                musicPlayer.volume = 0.5f;
                PlayerPrefs.SetFloat(StaticStrings.PLAYER_PREFS_MUSIC_VOLUME_SLIDER, 0.5f);
            }

            originalMusicVolumeLevel = musicPlayer.volume;

            if (!PlayerPrefs.HasKey(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER))
            {
                PlayerPrefs.SetFloat(StaticStrings.PLAYER_PREFS_VIDEO_VOLUME_SLIDER, 0.5f);
            }

        }
    }

    private void SelectClubMusic()
    {
        if(clubMusicsList != null && clubMusicsList.Length > 0)
        {
            StartCoroutine(LoadAudioClipFromURL(clubMusicsList[Random.Range(0, clubMusicsList.Length)]));
        }
        else
        {
            SelectRandomMusic(musicClipsClub);
        }
    }

    private void SelectWorkMusic()
    {
        if (workMusicsList != null && workMusicsList.Length > 0)
        {
            StartCoroutine(LoadAudioClipFromURL(workMusicsList[Random.Range(0, workMusicsList.Length)]));
        }
        else
        {
            SelectRandomMusic(musicClipsBoothGame);
        }
    }

    private void SelectRandomMusic(AudioClip[] clips)
    {
        musicPlayer.clip = clips[Random.Range(0,clips.Length)];
    }

    private void Update()
    {
        if (!musicPlayer.isPlaying && !doNotRestartMusic)
        {
            if (isClubMusicPlaying)
            {
                SelectClubMusic();
            }
            else
            {
                SelectWorkMusic();
            }
            musicPlayer.Play();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isClubMusicPlaying) {
            if (scene.name.Equals(StaticStrings.BOOTHS_MANAGEMENT_SCENE))
            {
                SelectWorkMusic();
                musicPlayer.Play();
                isClubMusicPlaying = false;
            }
        }
        else
        {
            if (!scene.name.Equals(StaticStrings.BOOTHS_MANAGEMENT_SCENE))
            {
                SelectClubMusic();
                musicPlayer.Play();
                isClubMusicPlaying = true;
            }
        }
    }

    private IEnumerator LoadAudioClipFromURL(string url)
    {
        AudioClip clip;
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            musicPlayer.Stop();
            clip = DownloadHandlerAudioClip.GetContent(request);
            musicPlayer.clip = clip;
            musicPlayer.Play();
        }
    }

    public static void LowerMusicVolume()
    {
        if (!musicIsLowered)
        {
            originalMusicVolumeLevel = musicPlayer.volume;
            musicPlayer.volume *= 0.1f;
            musicIsLowered = true;
        }
    }

    public static void ReturnBaseMusicVolume()
    {
        if (musicIsLowered)
        {
            musicPlayer.volume = originalMusicVolumeLevel;
            musicIsLowered = false;
        }
    }

    public static void SetNewVolume(float volume)
    {
        if (musicIsLowered)
        {
            originalMusicVolumeLevel = volume;
            musicPlayer.volume = volume * 0.1f;
        }
        else
        {
            musicPlayer.volume = volume;
        }
    }

    public void StopMusic()
    {
        doNotRestartMusic = true;
        beforeFadeVolume = musicPlayer.volume;
        StartCoroutine(StartFade(musicPlayer,0.1f,0));
        
    }

    public void StartMusic()
    {
        doNotRestartMusic = false;
        musicPlayer.volume = beforeFadeVolume;
    }

    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}

