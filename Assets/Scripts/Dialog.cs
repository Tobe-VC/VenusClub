using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Dialog
{
    public readonly int id;

    public Message firstMessage;

    public Message currentMessage;

    public Func<bool> trigger;

    /// <summary>
    /// True if this dialog has been seen, in wich case it is not shown
    /// </summary>
    public bool seen;

    /// <summary>
    /// True if this dialog is finished
    /// </summary>
    public bool done;

    public UnityAction endDialog;

    public Dialog(int id, Message firstMessage, Func<bool> trigger)
    {
        this.id = id;
        this.firstMessage = firstMessage;
        this.currentMessage = firstMessage;
        this.trigger = trigger;
        this.seen = false;
        this.done = false;
        this.endDialog = delegate () { };
    }

    public Dialog(int id, Message firstMessage, Func<bool> trigger, UnityAction endDialog)
    {
        this.id = id;
        this.firstMessage = firstMessage;
        this.currentMessage = firstMessage;
        this.trigger = trigger;
        this.seen = false;
        this.done = false;
        this.endDialog = endDialog;
    }

    public Dialog(Dialog dialog)
    {
        this.id = dialog.id;
        this.firstMessage = dialog.firstMessage;
        this.currentMessage = dialog.firstMessage;
        this.trigger = dialog.trigger;
        this.seen = dialog.seen;
        this.done = dialog.done;
        this.endDialog = dialog.endDialog;
    }

    public bool Trigger()
    {
        return !seen && trigger();
    }

    public void ProgressMessage()
    {
        seen = true;
        currentMessage = currentMessage.SelectNextMessage();
    }

    public void ProgressMessage(int responseCode)
    {
        seen = true;
        currentMessage = ((MessageWithResponses)currentMessage).ResponseResult(responseCode);
    }

    public void EndDialog()
    {
        done = true;
        DialogSystemBehavior.inDialog = false;
        endDialog();
    }

}
