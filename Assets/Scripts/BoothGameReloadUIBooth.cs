using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BoothGameReloadUIBooth : MonoBehaviour
{

    public Image girlPortrait;
    public Image clientPortrait;
    public Image clientTypePortrait;
    public Text currentActText;
    public VideoPlayer actVideoPlayer;
    public GameObject endButtons;
    public Button insideFinishButton;
    public Button bodyCumshotButton;
    public Button facialButton;
    public Button insideAnalFinishButton;
    public Button titsCumshotButton;
    public Button swallowButton;
    public GameObject helpButtons;
    public Button peekButton;
    public Button zoomInButton;
    public Button muteButton;

    public Sprite newGirlPortrait;
    public Sprite newClientPortrait;

    public void ReloadUIBooth()
    {
        girlPortrait.sprite = newGirlPortrait;
        clientPortrait.sprite = newClientPortrait;
        clientTypePortrait.color = Color.clear;
        clientPortrait.GetComponentInChildren<Text>().text = "";
        currentActText.text = "";
        //actVideoPlayer.Stop();
        //actVideoPlayer.clip = null;
        actVideoPlayer.gameObject.GetComponent<RawImage>().CrossFadeAlpha(0, 0f, true);
        endButtons.SetActive(false);
        insideFinishButton.gameObject.SetActive(false);
        bodyCumshotButton.gameObject.SetActive(false);
        facialButton.gameObject.SetActive(false);
        insideAnalFinishButton.gameObject.SetActive(false);
        titsCumshotButton.gameObject.SetActive(false);
        swallowButton.gameObject.SetActive(false);
        helpButtons.SetActive(false);
        peekButton.gameObject.SetActive(false);
        muteButton.gameObject.SetActive(false);
    }

    public void TurnOffEndButtons()
    {
        endButtons.SetActive(false);
    }

    public void TurnOffPeekButton()
    {
        peekButton.gameObject.SetActive(false);
    }

    public void TurnOffZoomInButton()
    {
        zoomInButton.gameObject.SetActive(false);
    }
}
