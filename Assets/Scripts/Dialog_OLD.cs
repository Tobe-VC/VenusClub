using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialog_OLD
{
    //Represents one entire dialog, which is a list of messages
    public List<DialogMessage> messages;

    public int currentMessageIndex;

    public NPCCode npcCode;

    /// <summary>
    /// The function to execute when the last message is displayed and the "Next" message is supposed to be displayed
    /// </summary>
    public UnityAction EndEffect;

    public Dialog_OLD(NPCCode npcCode, UnityAction EndEffect)
    {
        Construct(npcCode);
        this.EndEffect = EndEffect;

    }

    public Dialog_OLD(NPCCode npcCode)
    {
        Construct(npcCode);
    }

    private void Construct(NPCCode npcCode)
    {
        currentMessageIndex = 0;
        messages = new List<DialogMessage>();
        this.EndEffect = null;
        this.npcCode = npcCode;

    }

    /// <summary>
    /// Adds the stored listener to the next button
    /// This function is required because, at creation, the next button mignt not exist or be the same as the one we want to add a listener to
    /// </summary>
    private void AddNextButtonListener()
    {
        StaticDialogElements.nextDialogButton.onClick.RemoveAllListeners();
        StaticDialogElements.nextDialogButton.onClick.AddListener(delegate () { NextEffect(); });
    }

    /// <summary>
    /// Adds the stored listener to the next button and chooses the portrait
    /// This function is required because, at creation, the different elements migt not exist or be the same as the ones we want
    /// </summary>
    public void Initialize()
    {
        StaticDialogElements.nextDialogButton.onClick.RemoveAllListeners();
        StaticDialogElements.nextDialogButton.onClick.AddListener(delegate () { NextEffect(); });
        //StaticDialogElements.SetDialog(npcCode);
    }

    /// <summary>
    /// Adds the stored listener to the next button and a possible added effect
    /// This function is required because, at creation, the next button mignt not exist or be the same as the one we want to add a listener to
    /// </summary>
    public void AddNextButtonListener(UnityAction action)
    {
        StaticDialogElements.nextDialogButton.onClick.RemoveAllListeners();
        StaticDialogElements.nextDialogButton.onClick.AddListener(delegate () { NextEffect(); });
        StaticDialogElements.nextDialogButton.onClick.AddListener(delegate () { action(); });

    }

    /// <summary>
    ///Displays the next message or an empty string if the last message has been reached
    ///It also calls the function used when the last message is displayed
    /// </summary>
    /// <returns></returns>
    private void NextEffect()
    {
            if (currentMessageIndex < messages.Count - 1)
            {
                //If the current index is less than the index of the last message

                if (messages[currentMessageIndex].GetType().Equals(typeof(DialogMessageBasic)) 
                || messages[currentMessageIndex].GetType().Equals(typeof(DialogMessageFirst)))
                {
                    //If the current message is a basic one, create the appropriate list of parameters
                    //And apply its next function
                    DialogMessage nextMessage = messages[currentMessageIndex + 1];
                    List<UnityEngine.Object> messageParameters = new List<UnityEngine.Object>();
                    messageParameters.Add(nextMessage);
                    messages[currentMessageIndex].NextEffect(messageParameters);
                }
                currentMessageIndex++;
                StaticDialogElements.dialogBox.text = messages[currentMessageIndex].message;
            }
            else
            {
                foreach (Button b in StaticDialogElements.dialogButtons)
                {
                    b.gameObject.SetActive(true);
                }
                EndEffect();
            }
    }

}
