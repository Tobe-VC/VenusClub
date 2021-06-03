using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogSystemBehavior : MonoBehaviour
{

    public List<Sprite> portraits;

    public Image portraitLeft;
    public Image portraitCenter;
    public Image portraitRight;

    public TextMeshProUGUI dialogBox;

    private Dialog currentDialog;

    public List<Button> responsesButtons;

    public Button nextDialogButton;

    public GameObject mainDialogObject;

    public TextMeshProUGUI talkerNameBox;

    public Image backgroundImage;

    public GameObject dialogBoxBackground;

    private bool UIIsHidden = false;
    private List<Button> disabledUIButton = new List<Button>();
    /// <summary>
    /// True if the current DialogSystem is in a dialog
    /// </summary>
    public static bool inDialog = false;

    private float basicTimeDelayBeforeSkip = 0.1f;
    private float timeDelayBeforeSkip = 0;

    private void Start()
    {
        CheckDialogs();
    }

    private void Awake()
    {
        StaticDialogElements.dialogBox = dialogBox;
        StaticDialogElements.dialogButtons = responsesButtons;
        StaticDialogElements.nextDialogButton = nextDialogButton;
        StaticDialogElements.portraitLeft = portraitLeft;
        StaticDialogElements.portraitCenter = portraitCenter;
        StaticDialogElements.portraitRight = portraitRight;
        StaticDialogElements.talkerNameBox = talkerNameBox;
        StaticDialogElements.backgroundImage = backgroundImage;
        StaticDialogElements.currentMainDialogObject = mainDialogObject;

        foreach (Dialog d in RegisterDialogs.dialogs)
        {
            if (d.Trigger() && !inDialog)
            {
                inDialog = true;
                StaticDialogElements.nextDialogButton.gameObject.SetActive(true);
                currentDialog = d;
                mainDialogObject.SetActive(true);
                //If the dialog is triggered
                PopResponseButtons();
                UpdateDisplays();
                if (currentDialog.currentMessage.GetType().Equals(typeof(MessagePermanent)))
                {
                    StaticDialogElements.nextDialogButton.gameObject.SetActive(false);
                }
                break;
            }
        }

        StartCoroutine(CheckDialogsCoRoutine());
    }

    public void ResponsePress(int responseIndex)
    {
        //currentDialog.ProgressMessage(responseIndex);
        
        if (currentDialog.currentMessage.GetType().Equals(typeof(MessageEndWithResponses)))
        {
            mainDialogObject.gameObject.SetActive(false);
            currentDialog.EndDialog();
            currentDialog.seen = true;
            currentDialog.ProgressMessage(responseIndex);
            UnpopResponseButtons();
            StaticDialogElements.hasSeenPortrait = false;
        }
        else
        {
            if (!SpecialDialog())
            {
                currentDialog.ProgressMessage(responseIndex);
               
            }
            UnpopResponseButtons();
            UpdateDisplays();
        }
    }

    public void NextDialogButtonPress()
    {
        if (!SpecialDialog())
        {
            currentDialog.ProgressMessage();
        }
        UpdateDisplays();
        PopResponseButtons();

    }

    private void UpdateDisplays()
    {
        
        StaticDialogElements.dialogBox.text = currentDialog.currentMessage.message;

        if (!currentDialog.done)
            StaticDialogElements.SetDialog(currentDialog.currentMessage.npcCode, PortraitPosition.CENTER, InteractionSceneBehavior.selectedGirl);

        StaticDialogElements.nextDialogButton.gameObject.SetActive(true);
    }

    private bool SpecialDialog()
    {
        if (currentDialog.currentMessage.GetType().Equals(typeof(MessageEnd)))
        {
            mainDialogObject.gameObject.SetActive(false);
            currentDialog.seen = true;
            currentDialog.EndDialog();
            

            StaticDialogElements.hasSeenPortrait = false;
            return true;
        }
        else if (currentDialog.currentMessage.GetType().Equals(typeof(MessagePermanent)))
        {
            StaticDialogElements.nextDialogButton.gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    private void PopResponseButtons()
    {
        if (currentDialog.currentMessage.GetType().Equals(typeof(MessageWithResponses)) || currentDialog.currentMessage.GetType().Equals(typeof(MessageEndWithResponses)))
        {
            StaticDialogElements.nextDialogButton.gameObject.SetActive(false);
            MessageWithResponses message = (currentDialog.currentMessage as MessageWithResponses);
            int responseIndex = 0;
            foreach (Button b in responsesButtons)
            {
                if (responseIndex < message.responses.Count)
                {
                    b.gameObject.SetActive(true);
                    b.GetComponentInChildren<TextMeshProUGUI>().text = message.responses[responseIndex];
                    responseIndex++;
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void UnpopResponseButtons()
    {
        foreach (Button b in responsesButtons)
        {
            b.gameObject.SetActive(false);
        }
    }

    private void CheckDialogs()
    {
        if (!mainDialogObject.activeSelf)
        {
            inDialog = false;
        }
        foreach (Dialog d in RegisterDialogs.dialogs)
        {
            if (!inDialog && d.Trigger())
            {
                inDialog = true;
                StaticDialogElements.nextDialogButton.gameObject.SetActive(true);
                currentDialog = d;
                mainDialogObject.SetActive(true);
                //If the dialog is triggered
                PopResponseButtons();
                UpdateDisplays();
                if (currentDialog.currentMessage.GetType().Equals(typeof(MessagePermanent)))
                {
                    StaticDialogElements.nextDialogButton.gameObject.SetActive(false);
                }
                break;
            }
        }
    }

    private IEnumerator CheckDialogsCoRoutine()
    {
        CheckDialogs();

        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(CheckDialogsCoRoutine());
    }

    private void Update()
    {
        if (timeDelayBeforeSkip <= 0) {
            if (Input.GetButton("SkipDialog") && nextDialogButton.gameObject.activeSelf
                && mainDialogObject.activeSelf)
            {
                NextDialogButtonPress();
                timeDelayBeforeSkip = basicTimeDelayBeforeSkip;
            }
            if (Input.GetButtonDown("HideUI") && mainDialogObject.activeSelf)
            {
                if (currentDialog.currentMessage.GetType().Equals(typeof(MessageWithResponses))
                    || currentDialog.currentMessage.GetType().Equals(typeof(MessageEndWithResponses)))
                {
                    if (!UIIsHidden)
                    {
                        foreach (Button b in responsesButtons)
                        {

                            if (b.gameObject.activeSelf)
                            {
                                b.gameObject.SetActive(!b.gameObject.activeSelf);
                                disabledUIButton.Add(b);
                            }
                        }
                    }

                    else
                    {
                        foreach (Button b in disabledUIButton)
                        {
                            b.gameObject.SetActive(!b.gameObject.activeSelf);
                        }
                        disabledUIButton = new List<Button>();
                    }
                }

                if (!(currentDialog.currentMessage.GetType().Equals(typeof(MessageWithResponses))
                    || currentDialog.currentMessage.GetType().Equals(typeof(MessageWithResponses))))
                {
                    nextDialogButton.gameObject.SetActive(!nextDialogButton.gameObject.activeSelf);
                }
                dialogBoxBackground.SetActive(!dialogBoxBackground.activeSelf);
                UIIsHidden = !UIIsHidden;
            }
        }
        else
        {
            timeDelayBeforeSkip -= Time.deltaTime;
        }
    }
}
