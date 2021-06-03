using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehavior : MonoBehaviour
{

    public void OnMusicCreditsButtonPress()
    {
        SceneManager.LoadScene(StaticStrings.MUSIC_CREDITS_SCENE);
    }

}
